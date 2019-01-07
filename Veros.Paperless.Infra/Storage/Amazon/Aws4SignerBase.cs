namespace Veros.Paperless.Infra.Storage.Amazon
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    public abstract class Aws4SignerBase
    {
        //// SHA256 hash of an empty request body
        public const string EmptyBodySha256 = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";

        public const string Scheme = "AWS4";
        public const string Algorithm = "HMAC-SHA256";
        public const string Terminator = "aws4_request";

        //// format strings for the date/time and date stamps required during signing
        public const string Iso8601BasicFormat = "yyyyMMddTHHmmssZ";
        public const string DateStringFormat = "yyyyMMdd";

        //// some common x-amz-* parameters
        public const string XAmzAlgorithm = "X-Amz-Algorithm";
        public const string XAmzCredential = "X-Amz-Credential";
        public const string XAmzSignedHeaders = "X-Amz-SignedHeaders";
        public const string XAmzDate = "X-Amz-Date";
        public const string XAmzSignature = "X-Amz-Signature";
        public const string XAmzExpires = "X-Amz-Expires";
        public const string XAmzContentSha256 = "X-Amz-Content-SHA256";
        public const string XAmzDecodedContentLength = "X-Amz-Decoded-Content-Length";
        public const string XAmzMetaUuid = "X-Amz-Meta-UUID";

        //// the name of the keyed hash algorithm used in signing
        public const string Hmacsha256 = "HMACSHA256";

        public static HashAlgorithm CanonicalRequestHashAlgorithm = HashAlgorithm.Create("SHA-256");

        //// request canonicalization requires multiple whitespace compression
        protected static readonly Regex CompressWhitespaceRegex = new Regex("\\s+");

        /// <summary>
        /// The service endpoint, including the path to any resource.
        /// </summary>
        public Uri EndpointUri { get; set; }

        /// <summary>
        /// The HTTP verb for the request, e.g. GET.
        /// </summary>
        public string HttpMethod { get; set; }

        /// <summary>
        /// The signing name of the service, e.g. 's3'.
        /// </summary>
        public string Service { get; set; }

        /// <summary>
        /// The system name of the AWS region associated with the endpoint, e.g. us-east-1.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Helper to format a byte array into string
        /// </summary>
        /// <param name="data">The data blob to process</param>
        /// <param name="lowercase">If true, returns hex digits in lower case form</param>
        /// <returns>String version of the data</returns>
        public static string ToHexString(byte[] data, bool lowercase)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString(lowercase ? "x2" : "X2"));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Returns the canonical collection of header names that will be included in
        /// the signature. For AWS4, all header names must be included in the process 
        /// in sorted canonicalized order.
        /// </summary>
        /// <param name="headers">
        /// The set of header names and values that will be sent with the request
        /// </param>
        /// <returns>
        /// The set of header names canonicalized to a flattened, ;-delimited string
        /// </returns>
        protected string CanonicalizeHeaderNames(IDictionary<string, string> headers)
        {
            var headersToSign = new List<string>(headers.Keys);
            headersToSign.Sort(StringComparer.OrdinalIgnoreCase);

            var sb = new StringBuilder();
            foreach (var header in headersToSign)
            {
                if (sb.Length > 0)
                {
                    sb.Append(";");
                }

                sb.Append(header.ToLower());
            }

            return sb.ToString();
        }

        /// <summary>
        /// Computes the canonical headers with values for the request. 
        /// For AWS4, all headers must be included in the signing process.
        /// </summary>
        /// <param name="headers">The set of headers to be encoded</param>
        /// <returns>Canonicalized string of headers with values</returns>
        protected virtual string CanonicalizeHeaders(IDictionary<string, string> headers)
        {
            if (headers == null || headers.Count == 0)
            {
                return string.Empty;
            }

            var sortedHeaderMap = new SortedDictionary<string, string>();
            
            foreach (var header in headers.Keys)
            {
                sortedHeaderMap.Add(header.ToLower(), headers[header]);
            }

            //// step2: form the canonical header:value entries in sorted order. 
            //// Multiple white spaces in the values should be compressed to a single 
            //// space.
            var sb = new StringBuilder();

            foreach (var header in sortedHeaderMap.Keys)
            {
                var headerValue = CompressWhitespaceRegex.Replace(sortedHeaderMap[header], " ");
                sb.AppendFormat("{0}:{1}\n", header, headerValue.Trim());
            }

            return sb.ToString();
        }

        /// <summary>
        /// Returns the canonical request string to go into the signer process; this 
        /// consists of several canonical sub-parts.
        /// </summary>
        /// <param name="endpointUri"></param>
        /// <param name="httpMethod"></param>
        /// <param name="queryParameters"></param>
        /// <param name="canonicalizedHeaderNames">
        /// The set of header names to be included in the signature, formatted as a flattened, ;-delimited string
        /// </param>
        /// <param name="canonicalizedHeaders">
        /// </param>
        /// <param name="bodyHash">
        /// Precomputed SHA256 hash of the request body content. For chunked encoding this
        /// should be the fixed string ''.
        /// </param>
        /// <returns>String representing the canonicalized request for signing</returns>
        protected string CanonicalizeRequest(Uri endpointUri,
                                             string httpMethod,
                                             string queryParameters,
                                             string canonicalizedHeaderNames,
                                             string canonicalizedHeaders,
                                             string bodyHash)
        {
            var canonicalRequest = new StringBuilder();

            canonicalRequest.AppendFormat("{0}\n", httpMethod);
            canonicalRequest.AppendFormat("{0}\n", this.CanonicalResourcePath(endpointUri));
            canonicalRequest.AppendFormat("{0}\n", queryParameters);

            canonicalRequest.AppendFormat("{0}\n", canonicalizedHeaders);
            canonicalRequest.AppendFormat("{0}\n", canonicalizedHeaderNames);

            canonicalRequest.Append(bodyHash);

            return canonicalRequest.ToString();
        }

        /// <summary>
        /// Returns the canonicalized resource path for the service endpoint
        /// </summary>
        /// <param name="endpointUri">Endpoint to the service/resource</param>
        /// <returns>Canonicalized resource path for the endpoint</returns>
        protected string CanonicalResourcePath(Uri endpointUri)
        {
            if (string.IsNullOrEmpty(endpointUri.AbsolutePath))
            {
                return "/";
            }

            // encode the path per RFC3986
            return AmazonStorageHelpers.UrlEncode(endpointUri.AbsolutePath, true);
        }

        /// <summary>
        /// Compute and return the multi-stage signing key for the request.
        /// </summary>
        /// <param name="algorithm">Hashing algorithm to use</param>
        /// <param name="awsSecretAccessKey">The clear-text AWS secret key</param>
        /// <param name="region">The region in which the service request will be processed</param>
        /// <param name="date">Date of the request, in yyyyMMdd format</param>
        /// <param name="service">The name of the service being called by the request</param>
        /// <returns>Computed signing key</returns>
        protected byte[] DeriveSigningKey(string algorithm, string awsSecretAccessKey, string region, string date, string service)
        {
            const string KsecretPrefix = Scheme;
            char[] ksecret = null;

            ksecret = (KsecretPrefix + awsSecretAccessKey).ToCharArray();

            byte[] hashDate = this.ComputeKeyedHash(algorithm, Encoding.UTF8.GetBytes(ksecret), Encoding.UTF8.GetBytes(date));
            byte[] hashRegion = this.ComputeKeyedHash(algorithm, hashDate, Encoding.UTF8.GetBytes(region));
            byte[] hashService = this.ComputeKeyedHash(algorithm, hashRegion, Encoding.UTF8.GetBytes(service));
            return this.ComputeKeyedHash(algorithm, hashService, Encoding.UTF8.GetBytes(Terminator));
        }

        /// <summary>
        /// Compute and return the hash of a data blob using the specified algorithm
        /// and key
        /// </summary>
        /// <param name="algorithm">Algorithm to use for hashing</param>
        /// <param name="key">Hash key</param>
        /// <param name="data">Data blob</param>
        /// <returns>Hash of the data</returns>
        protected byte[] ComputeKeyedHash(string algorithm, byte[] key, byte[] data)
        {
            var kha = KeyedHashAlgorithm.Create(algorithm);
            kha.Key = key;
            return kha.ComputeHash(data);
        }
    }
}

namespace Veros.Paperless.Infra.Storage.Amazon
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using Framework;

    /// <summary>
    /// Sample AWS4 signer demonstrating how to sign requests to Amazon S3
    /// using query string parameters.
    /// </summary>
    public class Aws4SignerForQueryParameterAuth : Aws4SignerBase
    {
        /// <summary>
        /// Computes an AWS4 authorization for a request, suitable for embedding 
        /// in query parameters.
        /// </summary>
        /// <param name="headers">
        /// The request headers; 'Host' and 'X-Amz-Date' will be added to this set.
        /// </param>
        /// <param name="queryParameters">
        /// Any query parameters that will be added to the endpoint. The parameters 
        /// should be specified in canonical format.
        /// </param>
        /// <param name="bodyHash">
        /// Precomputed SHA256 hash of the request body content; this value should also
        /// be set as the header 'X-Amz-Content-SHA256' for non-streaming uploads.
        /// </param>
        /// <param name="awsAccessKey">
        /// The user's AWS Access Key.
        /// </param>
        /// <param name="awsSecretKey">
        /// The user's AWS Secret Key.
        /// </param>
        /// <returns>
        /// The string expressing the Signature V4 components to add to query parameters.
        /// </returns>
        public string ComputeSignature(IDictionary<string, string> headers,
                                       string queryParameters,
                                       string bodyHash,
                                       string awsAccessKey,
                                       string awsSecretKey)
        {
            var requestDateTime = DateTime.UtcNow;
            var dateTimeStamp = requestDateTime.ToString(Iso8601BasicFormat, CultureInfo.InvariantCulture);

            if (!headers.ContainsKey("Host"))
            {
                var hostHeader = this.EndpointUri.Host;
                if (!this.EndpointUri.IsDefaultPort)
                {
                    hostHeader += ":" + this.EndpointUri.Port;
                }

                headers.Add("Host", hostHeader);
            }

            var dateStamp = requestDateTime.ToString(DateStringFormat, CultureInfo.InvariantCulture);
            
            var scope = string.Format("{0}/{1}/{2}/{3}",
                                      dateStamp,
                                      this.Region,
                                      this.Service,
                                      Terminator);

            var canonicalizedHeaderNames = this.CanonicalizeHeaderNames(headers);
            var canonicalizedHeaders = this.CanonicalizeHeaders(headers);

            var paramDictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            
            if (!string.IsNullOrEmpty(queryParameters))
            {
                paramDictionary = queryParameters.Split('&').Select(p => p.Split('='))
                                                     .ToDictionary(nameval => nameval[0],
                                                                   nameval => nameval.Length > 1
                                                                        ? nameval[1] : string.Empty);
            }

            paramDictionary.Add(XAmzAlgorithm, AmazonStorageHelpers.UrlEncode(string.Format("{0}-{1}", Scheme, Algorithm)));
            paramDictionary.Add(XAmzCredential, AmazonStorageHelpers.UrlEncode(string.Format("{0}/{1}", awsAccessKey, scope)));
            paramDictionary.Add(XAmzSignedHeaders, AmazonStorageHelpers.UrlEncode(canonicalizedHeaderNames));

            paramDictionary.Add(XAmzDate, AmazonStorageHelpers.UrlEncode(dateTimeStamp));

            var sb = new StringBuilder();
            var paramKeys = new List<string>(paramDictionary.Keys);
            paramKeys.Sort(StringComparer.Ordinal);

            foreach (var p in paramKeys)
            {
                if (sb.Length > 0)
                {
                    sb.Append("&");
                }

                sb.AppendFormat("{0}={1}", p, paramDictionary[p]);
            }

            var canonicalizedQueryParameters = sb.ToString();

            var canonicalRequest = this.CanonicalizeRequest(this.EndpointUri,
                                                       this.HttpMethod,
                                                       canonicalizedQueryParameters,
                                                       canonicalizedHeaderNames,
                                                       canonicalizedHeaders,
                                                       bodyHash);
            Log.Application.InfoFormat("\nCanonicalRequest:\n{0}", canonicalRequest);

            var canonicalRequestHashBytes 
                = CanonicalRequestHashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(canonicalRequest));

            var stringToSign = new StringBuilder();

            stringToSign.AppendFormat("{0}-{1}\n{2}\n{3}\n", Scheme, Algorithm, dateTimeStamp, scope);
            stringToSign.Append(ToHexString(canonicalRequestHashBytes, true));

            Log.Application.InfoFormat("\nStringToSign:\n{0}", stringToSign);

            var kha = KeyedHashAlgorithm.Create(Hmacsha256);
            kha.Key = this.DeriveSigningKey(Hmacsha256, awsSecretKey, this.Region, dateStamp, this.Service);

            var signature = kha.ComputeHash(Encoding.UTF8.GetBytes(stringToSign.ToString()));
            var signatureString = ToHexString(signature, true);

            Log.Application.InfoFormat("\nSignature:\n{0}", signatureString);

            var authString = new StringBuilder();

            var authParams = new[]
            {
                XAmzAlgorithm, XAmzCredential, XAmzDate, XAmzSignedHeaders
            };

            foreach (var p in authParams)
            {
                if (authString.Length > 0)
                {
                    authString.Append("&");
                }

                authString.AppendFormat("{0}={1}", p, paramDictionary[p]);
            }

            authString.AppendFormat("&{0}={1}", XAmzSignature, signatureString);

            var authorization = authString.ToString();
            Log.Application.InfoFormat("\nAuthorization:\n{0}", authorization);

            return authorization;
        }
    }
}

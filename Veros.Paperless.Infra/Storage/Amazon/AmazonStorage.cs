namespace Veros.Paperless.Infra.Storage.Amazon
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using Veros.Paperless.Model.FrameworkLocal;
    using Org.BouncyCastle.Security;
    
    public class AmazonStorage
    {
        public dynamic Postar(string pathfileName, string remoteDirectory, string remoteFileName)
        {
            var regionUrlPart = string.Empty;

            if (this.ConfiguracoesAmazonCorretas() == false)
            {
                throw new InvalidParameterException("Não foi possivel enviar arquivo. Faltando configurações da amazon");
            }

            var region = ContextoInfra.AmazonRegion;
            var bucketName = ContextoInfra.AmazonBucketName;
            
            var objectKey = Path.GetFileName(remoteFileName);

            if (string.IsNullOrEmpty(region) == false)
            {
                if (region.Equals("us-east-1", StringComparison.OrdinalIgnoreCase) == false)
                {
                    regionUrlPart = string.Format("-{0}", region);
                }
            }

            var objectContent = System.IO.File.ReadAllBytes(pathfileName);

            ////"https://{0}.s3{1}.amazonaws.com/{2}/{3}" --> sem aceleracao
            ////"https://{0}.s3{1}-accelerate.amazonaws.com/{2}/{3}" --> com recurso de aceleração
            var endpointUri = string.Format(ContextoInfra.AmazonEntryPointUrl,
                                                bucketName,
                                                regionUrlPart,
                                                remoteDirectory,
                                                objectKey);

            var uri = new Uri(endpointUri);

            var contentHash = Aws4SignerBase.CanonicalRequestHashAlgorithm.ComputeHash(objectContent);

            var contentHashString = Aws4SignerBase.ToHexString(contentHash, true);

            var mime = MimeTypes.GetMimeType(Path.GetExtension(pathfileName));

            var headers = new Dictionary<string, string>
            {
                { Aws4SignerBase.XAmzContentSha256, contentHashString },
                { "content-length", objectContent.Length.ToString() },
                { "content-type", mime }
            };

            var signer = new Aws4SignerForAuthorizationHeader
            {
                EndpointUri = uri,
                HttpMethod = "PUT",
                Service = "s3",
                Region = region
            };

            var authorization = signer.ComputeSignature(headers,
                                                        string.Empty,   
                                                        contentHashString,
                                                        ContextoInfra.AmazonAccessKey,
                                                        ContextoInfra.AmazonSecretKey);

            headers.Add("Authorization", authorization);

            AmazonStorageHelpers.InvokeHttpRequest(uri, "PUT", headers, objectContent);
            return null;
        }

        public string ObterUrl(string remoteDirectory, string remoteFileName)
        {
            var regionUrlPart = string.Empty;

            if (this.ConfiguracoesAmazonCorretas() == false)
            {
                throw new InvalidParameterException("Não foi possivel enviar arquivo. Faltando configurações da amazon");
            }

            var region = ContextoInfra.AmazonRegion;
            var bucketName = ContextoInfra.AmazonBucketName;

            var objectKey = Path.GetFileName(remoteFileName);

            if (string.IsNullOrEmpty(region) == false)
            {
                if (region.Equals("us-east-1", StringComparison.OrdinalIgnoreCase) == false)
                {
                    regionUrlPart = string.Format("-{0}", region);
                }
            }

            var endpointUri = string.Format("https://{0}.s3{1}.amazonaws.com/{2}/{3}",
                                               bucketName,
                                               regionUrlPart,
                                               remoteDirectory,
                                               objectKey);

            var queryParams = new StringBuilder();

            var expiresOn = DateTime.UtcNow.AddDays(1);
            var period = Convert.ToInt64((expiresOn.ToUniversalTime() - DateTime.UtcNow).TotalSeconds);
            queryParams.AppendFormat("{0}={1}", Aws4SignerBase.XAmzExpires, AmazonStorageHelpers.UrlEncode(period.ToString()));

            var headers = new Dictionary<string, string>();

            var signer = new Aws4SignerForQueryParameterAuth
            {
                EndpointUri = new Uri(endpointUri),
                HttpMethod = "GET",
                Service = "s3",
                Region = region
            };

            var authorization = signer.ComputeSignature(headers,
                                                        queryParams.ToString(),
                                                        "UNSIGNED-PAYLOAD",
                                                        ContextoInfra.AmazonAccessKey,
                                                        ContextoInfra.AmazonSecretKey);

            var urlBuilder = new StringBuilder(endpointUri.ToString());

            urlBuilder.AppendFormat("?{0}", queryParams.ToString());

            urlBuilder.AppendFormat("&{0}", authorization);

            return urlBuilder.ToString();
        }

        public void DownloadUsingAwsSdk(string remoteDirectory, string remoteFileName, string pathDestinoTemp)
        {
            var url = this.ObterUrl(remoteDirectory, remoteFileName);
            this.DownloadImage(url, pathDestinoTemp);
        }

        public void Download(string remoteDirectory, string remoteFileName, string pathDestinoTemp)
        {
            var regionUrlPart = string.Empty;

            if (this.ConfiguracoesAmazonCorretas() == false)
            {
                throw new InvalidParameterException("Não foi possivel enviar arquivo. Faltando configurações da amazon");
            }
            
            var region = ContextoInfra.AmazonRegion;
            var bucketName = ContextoInfra.AmazonBucketName;

            if (string.IsNullOrEmpty(region) == false)
            {
                if (region.Equals("us-east-1", StringComparison.OrdinalIgnoreCase) == false)
                {
                    regionUrlPart = string.Format("-{0}", region);
                }
            }

            /////https://{0}.s3{1}-accelerate.amazonaws.com/{2}/{3}
            //// https://{0}.s3{1}.amazonaws.com/{2}/{3}
            var endpointUri = string.Format("https://{0}.s3{1}-accelerate.amazonaws.com/{2}/{3}",
                                               bucketName,
                                               regionUrlPart,
                                               remoteDirectory,
                                               remoteFileName);

            var mime = MimeTypes.GetMimeType(Path.GetExtension(remoteFileName));

            var uri = new Uri(endpointUri);

            var headers = new Dictionary<string, string>
            {
                { Aws4SignerBase.XAmzContentSha256, Aws4SignerBase.EmptyBodySha256 },
                { "content-type", mime }
            };

            var signer = new Aws4SignerForAuthorizationHeader
            {
                EndpointUri = uri,
                HttpMethod = "GET",
                Service = "s3",
                Region = region
            };

            var authorization = signer.ComputeSignature(
                headers,
                string.Empty,
                Aws4SignerBase.EmptyBodySha256,
                ContextoInfra.AmazonAccessKey,
                ContextoInfra.AmazonSecretKey);

            headers.Add("Authorization", authorization);

            AmazonStorageHelpers.InvokeHttpRequest(uri, "GET", headers, null, pathDestinoTemp);
        }

        private void DownloadImage(string url, string pathDestinoTemp)
        {
            var request = WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            using (var output = File.Open(pathDestinoTemp, FileMode.CreateNew))
            using (var input = response.GetResponseStream())
            {
                input.CopyTo(output);
            }

            response.Close();
        }

        private bool ConfiguracoesAmazonCorretas()
        {
            if (string.IsNullOrEmpty(ContextoInfra.AmazonAccessKey))
            {
                return false;
            }
            
            if (string.IsNullOrEmpty(ContextoInfra.AmazonSecretKey))
            {
                return false;
            }

            if (string.IsNullOrEmpty(ContextoInfra.AmazonRegion))
            {
                return false;
            }
            
            if (string.IsNullOrEmpty(ContextoInfra.AmazonBucketName))
            {
                return false;
            }
            
            return true;
        }
    }
}
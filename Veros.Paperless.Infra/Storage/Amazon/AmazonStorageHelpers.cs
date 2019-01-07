namespace Veros.Paperless.Infra.Storage.Amazon
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using Framework.IO;
    using Veros.Framework;

    public abstract class AmazonStorageHelpers
    {
        public static void InvokeHttpRequest(
            Uri endpointUri,
            string httpMethod,
            IDictionary<string, string> headers,
            byte[] requestBody, 
            string caminhoDoDownload = "")
        {
            try
            {
                var request = ConstructWebRequest(endpointUri, httpMethod, headers);

                if (requestBody != null)
                {
                    var buffer = new byte[8192];
                    var requestStream = request.GetRequestStream();

                    using (var inputStream = new MemoryStream(requestBody))
                    {
                        var bytesRead = 0;

                        while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            requestStream.Write(buffer, 0, bytesRead);
                        }
                    }
                }

                CheckResponse(request, caminhoDoDownload);
            }
            catch (WebException ex)
            {
                using (var response = ex.Response as HttpWebResponse)
                {
                    if (response != null)
                    {
                        var errorMsg = ReadResponseBody(response, string.Empty);

                        Log.Application.ErrorFormat(
                            "\n-- HTTP call failed with exception [{0}], status code [{1}]", 
                            errorMsg, 
                            response.StatusCode);
                    }

                    throw;
                }
            }
        }

        public static HttpWebRequest ConstructWebRequest(
            Uri endpointUri,
            string httpMethod,
            IDictionary<string, string> headers)
        {
            var request = (HttpWebRequest)WebRequest.Create(endpointUri);
            request.Method = httpMethod;

            foreach (var header in headers.Keys)
            {
                if (header.Equals("host", StringComparison.OrdinalIgnoreCase))
                {
                    request.Host = headers[header];
                }
                else if (header.Equals("content-length", StringComparison.OrdinalIgnoreCase))
                {
                    request.ContentLength = long.Parse(headers[header]);
                }
                else if (header.Equals("content-type", StringComparison.OrdinalIgnoreCase))
                {
                    request.ContentType = headers[header];
                }
                else
                {
                    request.Headers.Add(header, headers[header]);
                }
            }

            return request;
        }

        public static void CheckResponse(HttpWebRequest request, string caminhoDoDownload)
        {
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Log.Application.Debug("\n-- HTTP call succeeded");

                        var responseBody = ReadResponseBody(response, caminhoDoDownload);
                        if (!string.IsNullOrEmpty(responseBody))
                        {
                            Log.Application.Debug("\n-- Response body:");
                            Log.Application.Info(responseBody);
                        }
                    }
                    else
                    {
                        Log.Application.DebugFormat("\n-- HTTP call failed, status code: {0}", response.StatusCode);
                        throw new Exception("-- HTTP call failed, status code: " + response.StatusCode);
                    }
                }
            }
            catch (Exception exception)
            {
                Log.Application.Error("\n-- HTTP call failed, status code: ", exception);
                throw;
            }
        }

        /// <summary>
        /// Reads the response data from the service call, if any
        /// </summary>
        /// <param name="response">
        /// The response instance obtained from the previous request
        /// </param>
        /// <returns>The body content of the response</returns>
        public static string ReadResponseBody(HttpWebResponse response, string caminhoDoDownload)
        {
            if (response == null)
            {
                throw new ArgumentNullException("response", "Value cannot be null");
            }

            var responseBody = string.Empty;
            using (var responseStream = response.GetResponseStream())
            {
                if (responseStream != null)
                {
                    if (string.IsNullOrEmpty(caminhoDoDownload))
                    {
                        using (var reader = new StreamReader(responseStream))
                        {
                            responseBody = reader.ReadToEnd();
                        }
                    }
                    else
                    {
                        var diretorio = Path.GetDirectoryName(caminhoDoDownload);
                        Directories.CreateIfNotExist(diretorio);

                        using (var reader = responseStream)
                        {
                            var bytes = new byte[responseStream.Length];
                            reader.Read(bytes, 0, bytes.Length);

                            using (var fileStream = new FileStream(caminhoDoDownload, FileMode.Create))
                            {
                                fileStream.Write(bytes, 0, bytes.Length);
                                fileStream.Flush();
                            }
                        }
                        
                        ////using (var output = File.OpenWrite(caminhoDoDownload))
                        ////{
                        ////    responseStream.CopyTo(output);
                        ////}
                    }
                }
            }

            return responseBody;
        }

        /// <summary>
        /// Helper routine to url encode canonicalized header names and values for safe
        /// inclusion in the presigned url.
        /// </summary>
        /// <param name="data">The string to encode</param>
        /// <param name="pahtIs">Whether the string is a URL path or not</param>
        /// <returns>The encoded string</returns>
        public static string UrlEncode(string data, bool pahtIs = false)
        {
            //// The Set of accepted and valid Url characters per RFC3986. Characters outside of this set will be encoded.
            const string ValidUrlCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

            var encoded = new StringBuilder(data.Length * 2);
            var unreservedChars = string.Concat(ValidUrlCharacters, pahtIs ? "/:" : string.Empty);

            foreach (char symbol in System.Text.Encoding.UTF8.GetBytes(data))
            {
                if (unreservedChars.IndexOf(symbol) != -1)
                {
                    encoded.Append(symbol);
                }
                else
                {
                    encoded.Append("%").Append(string.Format("{0:X2}", (int) symbol));
                }
            }

            return encoded.ToString();
        }
    }
}
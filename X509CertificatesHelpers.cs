using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Hyphen.Libraries.CertificatesHelper
{
    public class X509CertificatesHelpers
    {
        /// <summary>
        /// Gets the certificate from a certain url
        /// </summary>
        /// <param name="urlPath">Url path</param>
        /// <returns>X509Certificate2 object</returns>
        static async Task<X509Certificate2> GetX509CertificateAsync(string urlPath)
        {
            var certificate = new X509Certificate2();

            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (request, cert, chain, policyErrors) =>
                {
                    certificate = new X509Certificate2(cert);
                    return true;
                }
            };

            using HttpClient httpClient = new HttpClient(httpClientHandler);
            await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, urlPath));

            return certificate;
        }
    }
}

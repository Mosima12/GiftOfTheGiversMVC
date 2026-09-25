using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GiftOfTheGivers.Functions
{
    public class TaxCertificateGenerator
    {
        private readonly ILogger _logger;

        public TaxCertificateGenerator(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TaxCertificateGenerator>();
        }

        [Function("GenerateTaxCertificate")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "certificate/generate")]
            HttpRequestData req)
        {
            _logger.LogInformation("Tax certificate function triggered.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic? data = JsonConvert.DeserializeObject(requestBody);

            if (data?.donorName == null || data?.amount == null)
            {
                var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await badResponse.WriteStringAsync("donorName and amount are required");
                return badResponse;
            }

            string certificateNumber = $"GOTG-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";

            var certificate = new
            {
                CertificateNumber = certificateNumber,
                DonorName = (string)data.donorName,
                Amount = (decimal)(data.amount ?? 0),
                Currency = (string)(data.currency ?? "ZAR"),
                IssuedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                TaxYear = DateTime.Now.Year.ToString(),
                Message = "Placeholder tax certificate for demonstration"
            };

            _logger.LogInformation($"Certificate generated: {certificateNumber}");

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(certificate);
            return response;
        }
    }
}
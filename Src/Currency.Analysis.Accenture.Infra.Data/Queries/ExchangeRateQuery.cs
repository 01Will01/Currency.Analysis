using Currency.Analysis.Accenture.Domain.DTOs;
using Currency.Analysis.Accenture.Domain.Interfaces;
using RestSharp;
using System.Text.Json;

namespace Currency.Analysis.Accenture.Infra.Data.Queries
{
    public class ExchangeRateQuery : IExchangeRateQuery
    {
        public ExchangeRateDTO Get(string url, string token, string cunrrencies)
        {
            var URLToken = new RestClient(url + "?access_key=" + token + "&symbols" + cunrrencies);
            URLToken.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            var request = new RestRequest(Method.GET);

            string ResponseBody = URLToken.Execute(request).Content;

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            ExchangeRateDTO data = JsonSerializer.Deserialize<ExchangeRateDTO>(ResponseBody, options);

            return data;
        }
    }
}

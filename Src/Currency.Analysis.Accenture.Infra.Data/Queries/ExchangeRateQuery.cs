using Currency.Analysis.Accenture.Domain.DTOs;
using Currency.Analysis.Accenture.Domain.Interfaces;
using RestSharp;
using System;
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

            /*
             * Estes cambios precisaram ser alterados manualmente pois,
             * está aplicação que é responsavel por me fornecer os cambios atualizados, 
             * não presta suporte para o meu tipo de usuário.
             * 
             * Local base: Eurora
            */

            data.Rates.BCH = Decimal.Parse("4.018");
            data.Rates.BTH = Decimal.Parse("0.0966062");
            data.Rates.XMR = Decimal.Parse("0.00001966062");
            data.Rates.ZEC = Decimal.Parse("0.00564454");

            return data;
        }

        public MessariExchangeRateDTO GetOfficial(string url, string type)
        {
            var URLToken = new RestClient(url + type);
            URLToken.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            var request = new RestRequest(Method.GET);

            string ResponseBody = URLToken.Execute(request).Content;

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            MessariExchangeRateDTO data = JsonSerializer.Deserialize<MessariExchangeRateDTO>(ResponseBody, options);

            return data;
        }
    }
}

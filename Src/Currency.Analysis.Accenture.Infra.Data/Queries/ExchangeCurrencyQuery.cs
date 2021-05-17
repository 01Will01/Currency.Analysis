
using Currency.Analysis.Accenture.Domain.DTOs;
using Currency.Analysis.Accenture.Domain.DTOs.Relationships;
using Currency.Analysis.Accenture.Domain.Entities;
using Currency.Analysis.Accenture.Domain.Interfaces.Queries;
using Currency.Analysis.Accenture.Infra.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Currency.Analysis.Accenture.Infra.Data.Queries
{
    public class ExchangeCurrencyQuery : IExchangeCurrencyQuery
    {
        private readonly CAASystemDataContext _context;

        public ExchangeCurrencyQuery(CAASystemDataContext context)
        {
            _context = context;
        }

        public async Task<List<CurrencyDTO>> GetAll()
        {
            List<CurrencyDTO> listData = new List<CurrencyDTO>();
            List<Currencies> response = await _context.Currency.ToListAsync();

            response.ForEach(element =>
            {
                CurrencyDTO data = new CurrencyDTO()
                {
                    Id = element.Id
                ,
                    Value = (decimal)element.Value
                ,
                    Applied = new CurrencyNameDTO() { Name = element.AppliedName, Symbol = element.AppliedSymbol }
                ,
                    Replacement = new CurrencyNameDTO() { Name = element.ReplacementName, Symbol = element.ReplacementSymbol }
                ,
                    OutputValue = element.OutputValue
                ,
                    Date = element.Date
                };

                listData.Add(data);
            });

            return listData;
        }

        public async Task<CurrencyDTO> GetById(Guid id)
        {
            Currencies response = await _context.Currency.FirstOrDefaultAsync(m => m.Id == id);

            CurrencyDTO data = new CurrencyDTO()
            {
                Id = response.Id
            ,
                Value = (decimal)response.Value
            ,
                Applied = new CurrencyNameDTO() { Name = response.AppliedName, Symbol = response.AppliedSymbol }
                ,
                Replacement = new CurrencyNameDTO() { Name = response.ReplacementName, Symbol = response.ReplacementSymbol }
            ,
                OutputValue = response.OutputValue
            ,
                Date = response.Date
            };

            return data;
        }
    }
}

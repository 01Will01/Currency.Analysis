
using Currency.Analysis.Accenture.Domain.DTOs;
using Currency.Analysis.Accenture.Domain.Entities;
using Currency.Analysis.Accenture.Domain.Interfaces.Queries;
using Currency.Analysis.Accenture.Infra.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Currency.Analysis.Accenture.Infra.Data.Queries
{
    public class CurrencyExchangeQuery: ICurrencyExchangeQuery
    {
        private readonly CAASystemDataContext _context;

        public CurrencyExchangeQuery(CAASystemDataContext context)
        {
            _context = context;
        }

        public async Task<List<CurrencyDTO>> GetAll()
        {
            List<CurrencyDTO> listData = new List<CurrencyDTO>();
            List<Currencies> response = await _context.Currency.ToListAsync();

            response.ForEach(element => {
                CurrencyDTO data = new CurrencyDTO();

                data.Id = element.Id;
                data.Value = (decimal)element.Value;
                data.Applied = (int)element.Applied;
                data.AppliedName = element.AppliedName;
                data.Replacement = (int)element.Replacement;
                data.ReplacementName = element.ReplacementName;
                data.OutputValue = element.OutputValue;
                data.Date = element.Date;

                listData.Add(data);
            });

            return listData;
        }

        public async Task<CurrencyDTO> GetById(Guid id)
        {
            Currencies response =  await _context.Currency.FirstOrDefaultAsync(m => m.Id == id);

            CurrencyDTO data = new CurrencyDTO();

            data.Id = response.Id;
            data.Value = (decimal)response.Value;
            data.Applied = (int)response.Applied;
            data.AppliedName = data.AppliedName;
            data.Replacement = (int)response.Replacement;
            data.ReplacementName = response.ReplacementName;
            data.OutputValue = response.OutputValue;
            data.Date = response.Date;

            return data;
        }
    }
}

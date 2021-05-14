
using Currency.Analysis.Accenture.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Currency.Analysis.Accenture.Domain.Interfaces.Queries
{
   public  interface ICurrencyExchangeQuery
    {
        Task<List<CurrencyDTO>> GetAll();

        Task<CurrencyDTO> GetById(Guid id);
    }
}

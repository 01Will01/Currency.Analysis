using Currency.Analysis.Accenture.Domain.DTOs.Relationships;
using Currency.Analysis.Accenture.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Currency.Analysis.Accenture.Domain.DTOs
{
    public class CurrencyDTO: Entity
    {

        [Required]
        [Column(TypeName = "string")]
        [Display(Name = "Valor de entrada")]
        public decimal Value { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        [Display(Name = "Moéda de entrada")]
        public CurrencyNameDTO Applied { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        [Display(Name = "Moéda de troca")]
        public CurrencyNameDTO Replacement { get; set; }

        [Column(TypeName = "string")]
        [Display(Name = "Data")]
        public string Date { get; set; }

        [Column(TypeName = "decimal")]
        [Display(Name = "Valor a receber")]
        public decimal? OutputValue { get; set; }

    }
}


using Currency.Analysis.Accenture.Shared.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Currency.Analysis.Accenture.Domain.DTOs
{
    public class CurrencyDTO: Entity
    {

        [Required]
        [Column(TypeName = "string")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required]
        [Range(1, 999999999)]
        [Column(TypeName = "decimal")]
        [Display(Name = "Valor aplicado")]
        public decimal RateValue { get; set; }

        [Required]
        [Column(TypeName = "string")]
        [Display(Name = "Nome")]
        public string Base { get; set; }

        [Required]
        [Range(1, 999999999)]
        [Column(TypeName = "decimal")]
        [Display(Name = "Valor Final")]
        public decimal Value { get; set; }
    }
}


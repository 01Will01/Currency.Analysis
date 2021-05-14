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
        [Display(Name = "Valor de entrada")]
        public decimal Value { get; set; }

        [Required]
        [Range(1, 9)]
        [Column(TypeName = "decimal")]
        [Display(Name = "Moéda a ser aplicada")]
        public int Applied { get; set; }

        [Required]
        [Range(1, 9)]
        [Column(TypeName = "decimal")]
        [Display(Name = "Moéda de troca")]
        public int Replacement { get; set; }

        [Column(TypeName = "string")]
        [Display(Name = "Data")]
        public string Date { get; set; }

        [Column(TypeName = "decimal")]
        [Display(Name = "Valor a receber")]
        public decimal? OutputValue { get; set; }

        [Column(TypeName = "string")]
        [Display(Name = "Moéda aplicada")]
        public string AppliedName { get; set; }

        [Column(TypeName = "string")]
        [Display(Name = "Moéda de troca")]
        public string ReplacementName { get; set; }


    }
}


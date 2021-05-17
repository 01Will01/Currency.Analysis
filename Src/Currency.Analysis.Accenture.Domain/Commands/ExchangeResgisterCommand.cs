using Currency.Analysis.Accenture.Domain.DTOs.Relationships;
using Currency.Analysis.Accenture.Shared.Commands.Interfaces;
using Flunt.Notifications;
using Flunt.Validations;

namespace Currency.Analysis.Accenture.Domain.Commands
{
    public class ExchangeResgisterCommand: Notifiable, ICommand
    {
        public ExchangeResgisterCommand(decimal? value, CurrencyNameDTO applied, CurrencyNameDTO replacement)
        {
            Value = value;
            Applied = applied;
            Replacement = replacement;
        }

        public decimal? Value { get; set; }
        public CurrencyNameDTO Applied { get; set; }
        public CurrencyNameDTO Replacement { get; set; }
        public void Validate()
        {
            AddNotifications(new Contract().Requires().IsNotNull(Value, "Valor", "Preencha algum valor!")
                                                      .IsNotNull(Applied, "Valor", "Preencha algum valor!")
                                                      .IsNotNull(Replacement, "Valor", "Preencha algum valor!")
                                                      );


        }
    }
}

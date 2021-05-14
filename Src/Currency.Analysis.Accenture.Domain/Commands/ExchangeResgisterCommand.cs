using Currency.Analysis.Accenture.Shared.Commands.Interfaces;
using Flunt.Notifications;
using Flunt.Validations;

namespace Currency.Analysis.Accenture.Domain.Commands
{
    public class ExchangeResgisterCommand: Notifiable, ICommand
    {
        public ExchangeResgisterCommand(decimal? value, int? applied, int? replacement)
        {
            Value = value;
            Applied = applied;
            Replacement = replacement;
        }

        public decimal? Value { get; set; }
        public int? Applied { get; set; }
        public int? Replacement { get; set; }
        public void Validate()
        {
            AddNotifications(new Contract().Requires().IsNotNull(Value, "Valor", "Preencha algum valor!")
                                                      .IsNotNull(Applied, "Valor", "Preencha algum valor!")
                                                      .IsNotNull(Replacement, "Valor", "Preencha algum valor!")
                                                      );


        }
    }
}

using Currency.Analysis.Accenture.Shared.Commands;
using Currency.Analysis.Accenture.Shared.Commands.Interfaces;
using System.Threading.Tasks;

namespace Currency.Analysis.Accenture.Shared.Handlers.Interfaces
{
    public interface IHandler<T> where T : ICommand
    {
        Task<GenericCommandResult> Handle(T command);
    }
}

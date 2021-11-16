using Poof.Snaps;
using System.Threading.Tasks;
using Yaapii.Atoms;

namespace Poof.Talk
{
    public interface ICommand
    {
        Task<IOutcome<IInput>> Content();

        Task Touch();
    }
}

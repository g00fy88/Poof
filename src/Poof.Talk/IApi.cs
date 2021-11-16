using Poof.Snaps;

namespace Poof.Talk
{
    public interface IApi
    {
        ICommand Public(IDemand demand, string contentType);

        ICommand Private(IDemand demand, string contentType);
    }
}

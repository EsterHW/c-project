

namespace DalApi;

internal interface ITests
{
    public interface ITests: ICrud<ITests>
    {
        ITests Tests   { get; }
    }
}

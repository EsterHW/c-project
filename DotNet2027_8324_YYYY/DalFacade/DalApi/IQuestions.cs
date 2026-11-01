
using DalApi;

namespace DalApi;

internal interface IQuestions
{
    public interface IQuestions : ICrud<IQuestions>
    {
        IQuestions Questions { get; }
    }
}

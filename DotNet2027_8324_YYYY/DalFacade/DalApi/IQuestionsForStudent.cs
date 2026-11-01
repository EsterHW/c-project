

namespace DalApi;

internal interface IQuestionsForStudent 
{
    public interface IQuestionsForStudent : ICrud<IQuestionsForStudent>
    {
        IQuestionsForStudent QuestionsForStudent { get; }
    }
}

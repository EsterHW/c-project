

using static DalApi.IStudent.cs;





namespace DalApi;

internal interface IAnswers
{
    public interface IAnswers : ICrud<IAnswers>
    {
        IAnswers Answers { get; }
    }
}
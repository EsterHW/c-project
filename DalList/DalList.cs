using DalApi;

namespace Dal;


public class DalList : IDal
{
    /// <summary>גישה לפעולות על התלמידים.</summary>
    public IStudent Student => new StudentImplementation();

    /// <summary>גישה לפעולות על המבחנים.</summary>
    public ITest Test => new TestImplementation();

    /// <summary>גישה לפעולות על השאלות.</summary>
    public IQuestion Question => new QuestionImplementation();

    /// <summary>גישה לפעולות על התשובות.</summary>
    public IAnswer Answer => new AnswerImplementation();

    /// <summary>גישה לפעולות על השאלות לנבחן.</summary>
    public IQuestionForStudent QuestionForStudent => new QuestionForStudentImplementation();
}

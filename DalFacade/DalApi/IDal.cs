namespace DalApi;

public interface IDal
{
    /// <summary>נקודת גישה לפעולות על התלמידים.</summary>
    IStudent Student { get; }

    /// <summary>נקודת גישה לפעולות על המבחנים.</summary>
    ITest Test { get; }

    /// <summary>נקודת גישה לפעולות על השאלות.</summary>
    IQuestion Question { get; }

    /// <summary>נקודת גישה לפעולות על התשובות.</summary>
    IAnswer Answer { get; }

    /// <summary>נקודת גישה לפעולות על השאלות לנבחן.</summary>
    IQuestionForStudent QuestionForStudent { get; }
}

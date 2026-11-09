using System.Reflection;
using Tools;
using DalApi;
using DO;

namespace Dal;


internal class QuestionForStudentImplementation : IQuestionForStudent
{

    public int Create(QuestionForStudent item)
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"start: {item}");

        if (Contains(item.StudentId, item.TestId, item.QuestionId))
        {
            LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name,
                $"error: student={item.StudentId} test={item.TestId} question={item.QuestionId} already exists");
            throw new DalAlreadyExistsException(
                $"Question number {item.QuestionId} already exists for student {item.StudentId} in test {item.TestId}");
        }

        DataSource.QuestionsForStudent.Add(item);

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"end: studentId={item.StudentId}");
        return item.StudentId;
    }

    public QuestionForStudent? Read(int id)
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"start: studentId={id}");

        QuestionForStudent? result =
            DataSource.QuestionsForStudent.FirstOrDefault(q => q.StudentId == id);

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name,
            $"end: {(result is null ? "not found" : "found")}");
        return result;
    }


    public List<QuestionForStudent> ReadAll()
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, "start");

        List<QuestionForStudent> result = new(DataSource.QuestionsForStudent);

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"end: count={result.Count}");
        return result;
    }

    public void Update(QuestionForStudent item)
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"start: {item}");

        if (!Contains(item.StudentId, item.TestId, item.QuestionId))
        {
            LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name,
                $"error: student={item.StudentId} test={item.TestId} question={item.QuestionId} does not exist");
            throw new DalDoesNotExistException(
                $"Question number {item.QuestionId} of student {item.StudentId} in test {item.TestId} does not exist");
        }

        DataSource.QuestionsForStudent.RemoveAll(q =>
            q.StudentId == item.StudentId && q.TestId == item.TestId && q.QuestionId == item.QuestionId);
        DataSource.QuestionsForStudent.Add(item);

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, "end");
    }

    /// <summary>
    /// מוחק את כל הרשומות של השאלות לנבחן השייכות לתלמיד עם המזהה שהתקבל.
    /// </summary>
    /// <param name="id">המזהה של התלמיד שרשומותיו נמחקות.</param>
    /// <exception cref="DalDoesNotExistException">לא קיימת רשומה של אותו תלמיד.</exception>
    public void Delete(int id)
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"start: studentId={id}");

        if (Read(id) is null)
        {
            LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name,
                $"error: student={id} has no questions for students");
            throw new DalDoesNotExistException($"Student with ID={id} has no questions for students");
        }

        int removed = DataSource.QuestionsForStudent.RemoveAll(q => q.StudentId == id);
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"removed {removed} rows");

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, "end");
    }

    /// <summary>
    /// בודק אם כבר קיימת רשומה עם אותו תלמיד, מבחן ושאלה.
    /// </summary>
    /// <param name="studentId">המזהה של התלמיד.</param>
    /// <param name="testId">המזהה של המבחן.</param>
    /// <param name="questionId">המזהה של השאלה.</param>
    private static bool Contains(int studentId, int testId, int questionId) =>
        DataSource.QuestionsForStudent.Any(q =>
            q.StudentId == studentId && q.TestId == testId && q.QuestionId == questionId);
}

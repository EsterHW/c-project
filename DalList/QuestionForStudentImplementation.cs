using DalApi;
using DO;

namespace Dal;


internal class QuestionForStudentImplementation : IQuestionForStudent
{
  
    public int Create(QuestionForStudent item)
    {
        if (Contains(item.StudentId, item.TestId, item.QuestionId))
            throw new DalAlreadyExistsException(
                $"Question number {item.QuestionId} already exists for student {item.StudentId} in test {item.TestId}");

        DataSource.QuestionsForStudent.Add(item);
        return item.StudentId;
    }

    public QuestionForStudent? Read(int id) =>
        DataSource.QuestionsForStudent.FirstOrDefault(q => q.StudentId == id);

    
    public List<QuestionForStudent> ReadAll() => new(DataSource.QuestionsForStudent);

    public void Update(QuestionForStudent item)
    {
        if (!Contains(item.StudentId, item.TestId, item.QuestionId))
            throw new DalDoesNotExistException(
                $"Question number {item.QuestionId} of student {item.StudentId} in test {item.TestId} does not exist");

        DataSource.QuestionsForStudent.RemoveAll(q =>
            q.StudentId == item.StudentId && q.TestId == item.TestId && q.QuestionId == item.QuestionId);
        DataSource.QuestionsForStudent.Add(item);
    }

    /// <summary>
    /// מוחק את כל הרשומות של השאלות לנבחן השייכות לתלמיד עם המזהה שהתקבל.
    /// </summary>
    /// <param name="id">המזהה של התלמיד שרשומותיו נמחקות.</param>
    /// <exception cref="DalDoesNotExistException">לא קיימת רשומה של אותו תלמיד.</exception>
    public void Delete(int id)
    {
        if (Read(id) is null)
            throw new DalDoesNotExistException($"Student with ID={id} has no questions for students");

        DataSource.QuestionsForStudent.RemoveAll(q => q.StudentId == id);
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

using DalApi;
using DO;

namespace DalTest;

public static class Initialization
{
    private static IDal s_dal = null!;

    
    public static void Initialize(IDal dal)
    {
        s_dal = dal;
        CreateTests();
        CreateQuestions();
        CreateAnswers();
        CreateStudents();
        CreateQuestionsForStudent();
    }

    private static void CreateTests()
    {
        for (int i = 0; i < 5; i++)
        {
            s_dal.Test.Create(new Test(0, (Subject)(i % 4), new DateTime(2026, 1, 1).AddDays(7 * i), 4));
        }
    }

    private static void CreateQuestions()
    {
        for (int i = 0; i < 20; i++)
        {
            s_dal.Question.Create(new Question(0, (Subject)(i % 4), $"Question {i + 1}", 0));
        }
    }

    
    private static void CreateAnswers()
    {
        foreach (Question question in s_dal.Question.ReadAll())
        {
            int firstAnswerId = s_dal.Answer.Create(new Answer(0, question.Id, $"Answer 1 for question {question.Id}"));
            s_dal.Answer.Create(new Answer(0, question.Id, $"Answer 2 for question {question.Id}"));
            s_dal.Question.Update(question with { CorrectAnswerId = firstAnswerId });
        }
    }

    
    private static void CreateStudents()
    {
        List<Test> tests = s_dal.Test.ReadAll();
        for (int i = 0; i < 5; i++)
        {
            s_dal.Student.Create(new Student(1000 + i, $"Student {i + 1}", tests[i].Id, 0));
        }
    }

    
    private static void CreateQuestionsForStudent()
    {
        List<Student> students = s_dal.Student.ReadAll();
        foreach (Student student in students)
        {
            List<Question> questions = s_dal.Question.ReadAll();
            for (int i = 0; i < 4 && i < questions.Count; i++)
            {
                s_dal.QuestionForStudent.Create(
                    new QuestionForStudent(student.Id, student.TestId, questions[i].Id, 0));
            }
        }
    }
}

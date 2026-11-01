
namespace Dal;

public class IDalImplementation
{
    public IStudent Student => new StudentImplementation();
    public IQuestions Question => new QuestionImplementation();
    public ITests Tests => new TestImplementation();
    public IAnswers Answers => new AnswerImplementation();
    public IQuestionsForStudent QuestionsForStudent => new QuestionForStudentImplementation();

}

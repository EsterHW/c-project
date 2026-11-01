

namespace DalApi;

public interface IDal
{
   IStudent Student { get; }
   ITests Tests { get; }
   IQuestions  Questions { get; }
   IQuestionsForStudent QuestionsForStudent { get; }
   IAnswers Answers { get; }
     


}

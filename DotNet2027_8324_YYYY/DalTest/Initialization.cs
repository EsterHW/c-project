
using Dal;
using DalApi;
using DO;

   
namespace DalTest;

public static class Initialization
{
   private static IDal s_dal  ;

   private static Student createStudents()
    {
        Student newStudent = new Student();
        return newStudent;
    }

    private static Question createQuestion()
    {
        Question newQuestion = new Question();
        
        return newQuestion;
    }

    private static Answer createAnswer()
    {
        Answer newAnswer = new Answer();

        return newAnswer;
    }

    private static QuestionForStudent createQuestionForStudent()
    {
        QuestionForStudent newQuestionForStudent = new QuestionForStudent();

        return newQuestionForStudent;
    }

    private static Tests createTests()
    {
        Tests newTests = new Tests();

        return newTests;
    }

    public IDal Initialize()
    {

    }



}

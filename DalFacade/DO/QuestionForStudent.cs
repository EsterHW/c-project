namespace DO;

public record QuestionForStudent(int StudentId, int TestId, int QuestionId, int AnswerSelectedId)
{

    public QuestionForStudent() : this(0, 0, 0, 0) { }
}

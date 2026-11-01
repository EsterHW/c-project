namespace DO;


public record Answer(int Id, int QuestionId, string Content)
{
    public Answer() : this(0, 0, string.Empty) { }
}

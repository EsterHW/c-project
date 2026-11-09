namespace DO;

public record Question(int Id, Subject Subject, string Content, int CorrectAnswerId)
{
    public Question() : this(0, Subject.Grammar, string.Empty, 0) { }
}

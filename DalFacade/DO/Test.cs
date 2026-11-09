namespace DO;


public record Test(int Id, Subject Subject, DateTime Date, int CountQuestions)
{

    public Test() : this(0, Subject.Grammar, DateTime.MinValue, 0) { }
}

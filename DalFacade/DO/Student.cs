namespace DO;

public record Student(int Id, string Name, int TestId, int Mark)
{

    public Student() : this(0, string.Empty, 0, 0) { }
}

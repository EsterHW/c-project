using DalApi;
using DO;

namespace Dal;

internal class QuestionImplementation : IQuestion
{
    public int Create(Question item)
    {
        int id = DataSource.Config.NextQuestionId;
        DataSource.Questions.Add(item with { Id = id });
        return id;
    }

    
    public Question? Read(int id) => DataSource.Questions.FirstOrDefault(q => q.Id == id);

    
    public List<Question> ReadAll() => new(DataSource.Questions);

    
    public void Update(Question item)
    {
        if (Read(item.Id) is null)
            throw new DalDoesNotExistException($"Question with ID={item.Id} does not exist");

        DataSource.Questions.RemoveAll(q => q.Id == item.Id);
        DataSource.Questions.Add(item);
    }

    public void Delete(int id)
    {
        if (Read(id) is null)
            throw new DalDoesNotExistException($"Question with ID={id} does not exist");

        DataSource.Questions.RemoveAll(q => q.Id == id);
    }
}

using DalApi;
using DO;

namespace Dal;


internal class AnswerImplementation : IAnswer
{
    public int Create(Answer item)
    {
        int id = DataSource.Config.NextAnswerId;
       // Answer answer= new Answer( id, item.QuestionId,item.Content);
        DataSource.Answers.Add(item with { Id = id });
        return id;
    }

    
    public Answer? Read(int id) => DataSource.Answers.FirstOrDefault(a => a.Id == id);

        public List<Answer> ReadAll() => new(DataSource.Answers);

        public void Update(Answer item)
    {
        if (Read(item.Id) is null)
            throw new DalDoesNotExistException($"Answer with ID={item.Id} does not exist");

        DataSource.Answers.RemoveAll(a => a.Id == item.Id);
        DataSource.Answers.Add(item);
    }

    
    public void Delete(int id)
    {
        if (Read(id) is null)
            throw new DalDoesNotExistException($"Answer with ID={id} does not exist");

        DataSource.Answers.RemoveAll(a => a.Id == id);
    }
}

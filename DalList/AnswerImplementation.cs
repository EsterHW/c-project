using System.Reflection;
using Tools;
using DalApi;
using DO;

namespace Dal;


internal class AnswerImplementation : IAnswer
{
    public int Create(Answer item)
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"start: {item}");

        int id = DataSource.Config.NextAnswerId;
        // Answer answer= new Answer( id, item.QuestionId,item.Content);
        DataSource.Answers.Add(item with { Id = id });

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"end: new ID={id}");
        return id;
    }


    public Answer? Read(int id)
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"start: id={id}");

        Answer? result = DataSource.Answers.FirstOrDefault(a => a.Id == id);

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name,
            $"end: {(result is null ? "not found" : "found")}");
        return result;
    }

    public List<Answer> ReadAll()
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, "start");

        List<Answer> result = new(DataSource.Answers);

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"end: count={result.Count}");
        return result;
    }

    public void Update(Answer item)
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"start: {item}");

        if (Read(item.Id) is null)
        {
            LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name,
                $"error: Answer ID={item.Id} does not exist");
            throw new DalDoesNotExistException($"Answer with ID={item.Id} does not exist");
        }

        DataSource.Answers.RemoveAll(a => a.Id == item.Id);
        DataSource.Answers.Add(item);

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, "end");
    }


    public void Delete(int id)
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"start: id={id}");

        if (Read(id) is null)
        {
            LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name,
                $"error: Answer ID={id} does not exist");
            throw new DalDoesNotExistException($"Answer with ID={id} does not exist");
        }

        DataSource.Answers.RemoveAll(a => a.Id == id);

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, "end");
    }
}

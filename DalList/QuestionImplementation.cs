using System.Reflection;
using Tools;
using DalApi;
using DO;

namespace Dal;

internal class QuestionImplementation : IQuestion
{
    public int Create(Question item)
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"start: {item}");

        int id = DataSource.Config.NextQuestionId;
        DataSource.Questions.Add(item with { Id = id });

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"end: new ID={id}");
        return id;
    }


    public Question? Read(int id)
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"start: id={id}");

        Question? result = DataSource.Questions.FirstOrDefault(q => q.Id == id);

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name,
            $"end: {(result is null ? "not found" : "found")}");
        return result;
    }


    public List<Question> ReadAll()
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, "start");

        List<Question> result = new(DataSource.Questions);

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"end: count={result.Count}");
        return result;
    }


    public void Update(Question item)
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"start: {item}");

        if (Read(item.Id) is null)
        {
            LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name,
                $"error: Question ID={item.Id} does not exist");
            throw new DalDoesNotExistException($"Question with ID={item.Id} does not exist");
        }

        DataSource.Questions.RemoveAll(q => q.Id == item.Id);
        DataSource.Questions.Add(item);

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, "end");
    }

    public void Delete(int id)
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"start: id={id}");

        if (Read(id) is null)
        {
            LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name,
                $"error: Question ID={id} does not exist");
            throw new DalDoesNotExistException($"Question with ID={id} does not exist");
        }

        DataSource.Questions.RemoveAll(q => q.Id == id);

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, "end");
    }
}

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

        // המזהה של תשובה חייב להיות חיובי.
        if (item.Id <= 0)
        {
            LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name,
                $"error: Answer ID={item.Id} is not positive");
            throw new DalDoesNotExistException($"Answer with invalid ID={item.Id}: ID must be positive");
        }

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

    /// <summary>
    /// מחזיר את האובייקט הראשון שעליו הפונקציה מחזירה true, או null אם אין כזה.
    /// </summary>
    public Answer? Read(Func<Answer, bool> filter)
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, "start: filter");

        Answer? result = DataSource.Answers.FirstOrDefault(filter);

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name,
            $"end: {(result is null ? "not found" : "found")}");
        return result;
    }

    public List<Answer?> ReadAll(Func<Answer, bool>? filter = null)
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, "start");

        // ללא סינון - כל הרשימה; עם סינון - רק האיברים שעליהם הפונקציה מחזירה true.
        List<Answer?> result = filter is null
            ? DataSource.Answers.ToList<Answer?>()
            : DataSource.Answers.Where(filter).ToList<Answer?>();

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

        Answer? existing = DataSource.Answers.First(a => a.Id == item.Id);
        DataSource.Answers.Remove(existing);
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

        DataSource.Answers.Remove(DataSource.Answers.First(a => a.Id == id));

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, "end");
    }
}

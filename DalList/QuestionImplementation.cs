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

        // המזהה של שאלה חייב להיות חיובי.
        if (item.Id <= 0)
        {
            LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name,
                $"error: Question ID={item.Id} is not positive");
            throw new DalDoesNotExistException($"Question with invalid ID={item.Id}: ID must be positive");
        }

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

    /// <summary>
    /// מחזיר את האובייקט הראשון שעליו הפונקציה מחזירה true, או null אם אין כזה.
    /// </summary>
    public Question? Read(Func<Question, bool> filter)
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, "start: filter");

        Question? result = DataSource.Questions.FirstOrDefault(filter);

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name,
            $"end: {(result is null ? "not found" : "found")}");
        return result;
    }


    public List<Question?> ReadAll(Func<Question, bool>? filter = null)
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, "start");

        // ללא סינון - כל הרשימה; עם סינון - רק האיברים שעליהם הפונקציה מחזירה true.
        List<Question?> result = filter is null
            ? DataSource.Questions.ToList<Question?>()
            : DataSource.Questions.Where(filter).ToList<Question?>();

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

        Question? existing = DataSource.Questions.First(q => q.Id == item.Id);
        DataSource.Questions.Remove(existing);
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

        DataSource.Questions.Remove(DataSource.Questions.First(q => q.Id == id));

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, "end");
    }
}

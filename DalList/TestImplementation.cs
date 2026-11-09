using System.Reflection;
using Tools;
using DalApi;
using DO;

namespace Dal;
internal class TestImplementation : ITest
{

    public int Create(Test item)
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"start: {item}");

        // המזהה של מבחן חייב להיות חיובי.
        if (item.Id <= 0)
        {
            LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name,
                $"error: Test ID={item.Id} is not positive");
            throw new DalDoesNotExistException($"Test with invalid ID={item.Id}: ID must be positive");
        }

        int id = DataSource.Config.NextTestId;
        DataSource.Tests.Add(item with { Id = id });

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"end: new ID={id}");
        return id;
    }


    public Test? Read(int id)
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"start: id={id}");

        Test? result = DataSource.Tests.FirstOrDefault(t => t.Id == id);

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name,
            $"end: {(result is null ? "not found" : "found")}");
        return result;
    }

    /// <summary>
    /// מחזיר את האובייקט הראשון שעליו הפונקציה מחזירה true, או null אם אין כזה.
    /// </summary>
    public Test? Read(Func<Test, bool> filter)
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, "start: filter");

        Test? result = DataSource.Tests.FirstOrDefault(filter);

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name,
            $"end: {(result is null ? "not found" : "found")}");
        return result;
    }

    public List<Test?> ReadAll(Func<Test, bool>? filter = null)
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, "start");

        // ללא סינון - כל הרשימה; עם סינון - רק האיברים שעליהם הפונקציה מחזירה true.
        List<Test?> result = filter is null
            ? DataSource.Tests.ToList<Test?>()
            : DataSource.Tests.Where(filter).ToList<Test?>();

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"end: count={result.Count}");
        return result;
    }


    public void Update(Test item)
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"start: {item}");

        if (Read(item.Id) is null)
        {
            LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name,
                $"error: Test ID={item.Id} does not exist");
            throw new DalDoesNotExistException($"Test with ID={item.Id} does not exist");
        }

        Test? existing = DataSource.Tests.First(t => t.Id == item.Id);
        DataSource.Tests.Remove(existing);
        DataSource.Tests.Add(item);

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, "end");
    }

    public void Delete(int id)
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"start: id={id}");

        if (Read(id) is null)
        {
            LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name,
                $"error: Test ID={id} does not exist");
            throw new DalDoesNotExistException($"Test with ID={id} does not exist");
        }

        DataSource.Tests.Remove(DataSource.Tests.First(t => t.Id == id));

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, "end");
    }
}

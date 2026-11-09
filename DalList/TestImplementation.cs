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

    public List<Test> ReadAll()
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, "start");

        List<Test> result = new(DataSource.Tests);

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

        DataSource.Tests.RemoveAll(t => t.Id == item.Id);
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

        DataSource.Tests.RemoveAll(t => t.Id == id);

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, "end");
    }
}

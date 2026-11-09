using System.Reflection;
using Tools;
using DalApi;
using DO;

namespace Dal;


internal class StudentImplementation : IStudent
{

    public int Create(Student item)
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"start: {item}");

        if (Read(item.Id) is not null)
        {
            LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name,
                $"error: Student ID={item.Id} already exists");
            throw new DalAlreadyExistsException($"Student with ID={item.Id} already exists");
        }

        DataSource.Students.Add(item);

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"end: id={item.Id}");
        return item.Id;
    }


    public Student? Read(int id)
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"start: id={id}");

        Student? result = DataSource.Students.FirstOrDefault(s => s.Id == id);

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name,
            $"end: {(result is null ? "not found" : "found")}");
        return result;
    }


    public List<Student> ReadAll()
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, "start");

        List<Student> result = new(DataSource.Students);

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"end: count={result.Count}");
        return result;
    }


    public void Update(Student item)
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"start: {item}");

        if (Read(item.Id) is null)
        {
            LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name,
                $"error: Student ID={item.Id} does not exist");
            throw new DalDoesNotExistException($"Student with ID={item.Id} does not exist");
        }

        DataSource.Students.RemoveAll(s => s.Id == item.Id);
        DataSource.Students.Add(item);

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, "end");
    }

    public void Delete(int id)
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, $"start: id={id}");

        if (Read(id) is null)
        {
            LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name,
                $"error: Student ID={id} does not exist");
            throw new DalDoesNotExistException($"Student with ID={id} does not exist");
        }

        DataSource.Students.RemoveAll(s => s.Id == id);

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, "end");
    }
}

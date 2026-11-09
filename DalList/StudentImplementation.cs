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

        // המזהה של תלמיד חייב להיות חיובי.
        if (item.Id <= 0)
        {
            LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name,
                $"error: Student ID={item.Id} is not positive");
            throw new DalDoesNotExistException($"Student with invalid ID={item.Id}: ID must be positive");
        }

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

    /// <summary>
    /// מחזיר את האובייקט הראשון שעליו הפונקציה מחזירה true, או null אם אין כזה.
    /// </summary>
    public Student? Read(Func<Student, bool> filter)
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, "start: filter");

        Student? result = DataSource.Students.FirstOrDefault(filter);

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name,
            $"end: {(result is null ? "not found" : "found")}");
        return result;
    }


    public List<Student?> ReadAll(Func<Student, bool>? filter = null)
    {
        MethodBase m = MethodBase.GetCurrentMethod()!;
        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, "start");

        // ללא סינון - כל הרשימה; עם סינון - רק האיברים שעליהם הפונקציה מחזירה true.
        List<Student?> result = filter is null
            ? DataSource.Students.ToList<Student?>()
            : DataSource.Students.Where(filter).ToList<Student?>();

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

        Student? existing = DataSource.Students.First(s => s.Id == item.Id);
        DataSource.Students.Remove(existing);
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

        DataSource.Students.Remove(DataSource.Students.First(s => s.Id == id));

        LogManager.WriteToLog(m.DeclaringType!.FullName!, m.Name, "end");
    }
}

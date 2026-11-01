using DalApi;
using DO;

namespace Dal;


internal class StudentImplementation : IStudent
{
   
    public int Create(Student item)
    {
        if (Read(item.Id) is not null)
            throw new DalAlreadyExistsException($"Student with ID={item.Id} already exists");

        DataSource.Students.Add(item);
        return item.Id;
    }

   
    public Student? Read(int id) => DataSource.Students.FirstOrDefault(s => s.Id == id);

    
    public List<Student> ReadAll() => new(DataSource.Students);

    
    public void Update(Student item)
    {
        if (Read(item.Id) is null)
            throw new DalDoesNotExistException($"Student with ID={item.Id} does not exist");

        DataSource.Students.RemoveAll(s => s.Id == item.Id);
        DataSource.Students.Add(item);
    }

       public void Delete(int id)
    {
        if (Read(id) is null)
            throw new DalDoesNotExistException($"Student with ID={id} does not exist");

        DataSource.Students.RemoveAll(s => s.Id == id);
    }
}

using DalApi;
using DO;

namespace Dal;
internal class TestImplementation : ITest
{
    
    public int Create(Test item)
    {
        int id = DataSource.Config.NextTestId;
        DataSource.Tests.Add(item with { Id = id });
        return id;
    }

   
    public Test? Read(int id) => DataSource.Tests.FirstOrDefault(t => t.Id == id);

        public List<Test> ReadAll() => new(DataSource.Tests);

    
    public void Update(Test item)
    {
        if (Read(item.Id) is null)
            throw new DalDoesNotExistException($"Test with ID={item.Id} does not exist");

        DataSource.Tests.RemoveAll(t => t.Id == item.Id);
        DataSource.Tests.Add(item);
    }

    public void Delete(int id)
    {
        if (Read(id) is null)
            throw new DalDoesNotExistException($"Test with ID={id} does not exist");

        DataSource.Tests.RemoveAll(t => t.Id == id);
    }
}

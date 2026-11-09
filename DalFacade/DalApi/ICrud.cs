namespace DalApi;

public interface ICrud<T>
{

    int Create(T item);
    T? Read(int id);
    T? Read(Func<T, bool> filter); // stage 2
    List<T?> ReadAll(Func<T, bool>? filter = null); // stage 2
    void Update(T item);
    void Delete(int id);
}

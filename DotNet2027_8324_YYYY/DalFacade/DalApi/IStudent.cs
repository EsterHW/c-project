
namespace DalApi;


internal interface IStudent
{
    public interface IStudent : ICrud<IStudent>
    {
        IStudent Student { get; }
    }
}

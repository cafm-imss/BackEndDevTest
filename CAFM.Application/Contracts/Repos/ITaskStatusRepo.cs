
namespace CAFM.Application.Contracts.Repos
{
    public interface ITaskStatusRepo
    {
        Task<bool> IsTaskStatusExist(int taskStatusId);
    }
}

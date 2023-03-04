using KGD.Domain.Entity;

namespace KGD.Application.Contracts
{
    public interface IUserRepository
    {
        Task<List<User>> GetUserList(CancellationToken cancellationToken);
        Task AddUser(User user);
    }
}

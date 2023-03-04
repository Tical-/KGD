using KGD.Application.DTO;

namespace KGD.Application.Contracts
{
    public interface IUserService
    {
        Task<List<UserDTO>> RetrieveUserListAsync(CancellationToken cancellationToken);
        Task AddUser(UserDTO userDto);
    }
}

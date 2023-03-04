using KGD.Application.Contracts;
using KGD.Application.DTO;
using KGD.Domain.Entity;
using Microsoft.Extensions.Logging;

namespace KGD.Application.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<List<UserDTO>> RetrieveUserListAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("RetrieveUserListAsync");

            // Perform business logic steps in service
            //Call Mapper to map from domain to dto
            var userList = await _userRepository.GetUserList(cancellationToken);
            var res = new List<UserDTO>();
            foreach (var item in userList)
            {
                res.Add(new UserDTO()
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }

            return res;
        }

        public async Task AddUser(UserDTO userDto)
        {
            _logger.LogInformation("AddUser");

            //mappers from userdto to user

            await _userRepository.AddUser(new User()
            {
                Name = userDto.Name
            });
        }
    }
}

using KGD.Application.Contracts;
using KGD.Application.DTO;
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

            return new List<UserDTO>();
        }
    }
}

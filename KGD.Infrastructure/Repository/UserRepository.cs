using KGD.Application.Contracts;
using KGD.Domain.Entity;
using KGD.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KGD.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUserList(CancellationToken cancellationToken) => await _context.Users.ToListAsync(cancellationToken);

        public async Task AddUser(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}

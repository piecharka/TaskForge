using Domain;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public sealed class UserRepository : GenericRepository<User>, IUserRepository
    {

        public UserRepository(TaskForgeDbContext forgeDbContext) 
            : base(forgeDbContext){}

        public async Task<User> GetByNameAsync(string username)
        {
            return await _forgeDbContext.Set<User>().FirstOrDefaultAsync(x => x.Username == username);
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _forgeDbContext.Set<User>().FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}

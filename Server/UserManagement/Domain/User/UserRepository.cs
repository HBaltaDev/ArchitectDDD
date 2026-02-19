using Dapper;
using Infrastructure.DbContext;
using Server.UserManagement.Domain.Abstract;

namespace Server.UserManagement.Domain.User;

public class UserRepository(IDbContext dbContext) : IUserRepository
{
    public async Task<UserModel?> GetById(long id)
    {
        const string query = @"SELECT * FROM user_management.user WHERE id = @Id AND is_deleted = @IsDeleted";

        return await dbContext.GetConnection().QueryFirstOrDefaultAsync<UserModel>(query, new { Id = id, IsDeleted = false });
    }
}
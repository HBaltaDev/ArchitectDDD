using Dapper;
using Infrastructure.DbContext;
using Server.UserManagement.Domain.Abstract;

namespace Server.UserManagement.Domain.User;

public class UserRepository(IDbContext dbContext) : IUserRepository
{
    public async Task<ulong> InsertAsync(UserModel user)
    {
        const string query = @"INSERT INTO user_management.user (username, name, surname, email_address, phone_number, password, is_one_time_password, is_tfa_active, role_id_list, password_expire_duration, is_deleted, record_info)
                    VALUES (@Username, @Name, @Surname, @EmailAddress, @PhoneNumber, @Password, @IsOneTimePassword, @IsTFAActive, @RoleIdList, @PasswordExpiration, @IsDeleted, @RecordInfo::jsonb)
                    RETURNING id";

        return await dbContext.GetConnection().ExecuteScalarAsync<ulong>(query, new
        {
            user.Username,
            user.Name,
            user.Surname,
            user.EmailAddress,
            user.PhoneNumber,
            user.Password,
            user.IsOneTimePassword,
            user.IsTFAActive,
            user.RoleIdList,
            user.PasswordExpiration,
            user.IsDeleted,
            user.RecordInfo
        });
    }
    
    public async Task<UserModel?> GetByIdAsync(long id)
    {
        const string query = @"SELECT * FROM user_management.user WHERE id = @Id AND is_deleted = @IsDeleted";

        return await dbContext.GetConnection().QueryFirstOrDefaultAsync<UserModel>(query, new { Id = id, IsDeleted = false });
    }
}
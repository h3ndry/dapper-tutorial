using DataAccess.DBAccess;
using DataAccess.Models;

namespace DataAccess.Data;

public class UserData : IUserData
{
    private readonly ISqlDataAccess _db;

    public UserData(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<UserModel>> GetUsers() =>
    _db.LoadData<UserModel, dynamic>("get_all_users", new { });

    public async Task<UserModel?> GetUser(int id)
    {
        var results = await _db.LoadData<UserModel, dynamic>("get_user", new { id });
        return results.FirstOrDefault();
    }

    public Task InsertUser(UserModel user) =>
        _db.SaveData("insert_user", new { user.first_name, user.last_name });

    public Task UpdateUser(UserModel user) =>
        _db.SaveData("update_user", user);

    public Task DeleteUser(int id) =>
        _db.SaveData("delete_user", new { id });
}

using DataAccess.Data;

namespace api;

public static class Routes
{
    public static void ConfigureRoutes(this WebApplication app)
    {
        // ALL API metho
        app.MapGet("/users", GetUsers);
        app.MapGet("/users/{id}", GetUser);
    }

    public static async Task<IResult> GetUsers(IUserData data)
    {
        try
        {
            return Results.Ok(await data.GetUsers());
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }


    public static async Task<IResult> GetUser(int id, IUserData data)
    {
        try
        {
            return Results.Ok(await data.GetUser(id));
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

}

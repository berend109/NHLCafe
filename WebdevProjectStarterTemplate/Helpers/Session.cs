using Newtonsoft.Json;
using WebdevProjectStarterTemplate.Models;

namespace WebdevProjectStarterTemplate.Helpers;

public class Session
{ 
    public static bool CheckIfLoggedIn(string? username)
    {
        return username != null;
    }
    
    public int GetUserId(string? username)
    {
        if (CheckIfLoggedIn(username))
        {
            if (username != null)
            {
                var user = JsonConvert.DeserializeObject<User>(username);
                if (user != null) return user.OberId;
            }
        }
        return 0;
    }
}
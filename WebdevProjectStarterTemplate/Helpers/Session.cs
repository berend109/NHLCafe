using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebdevProjectStarterTemplate.Models;

namespace WebdevProjectStarterTemplate.Helpers;

public class Session
{ 
    public bool CheckIfLoggedIn(string username)
    {
        return username != null;
    }
    
    public int GetUserId(string username)
    {
        if (CheckIfLoggedIn(username))
        {
            User user = JsonConvert.DeserializeObject<User>(username);
            return user.OberId;
        }
        return 0;
    }
}
using Practice7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practice7.Repository
{
    public class UserRepo : IDisposable
    {
        AppDbContex db=new AppDbContex();
        public void Dispose()
        {
            db.Dispose();
        }
        public User ValidateUser(string username,string password)
        {
            return db.Users.FirstOrDefault(x=>x.UserName.
            Equals(username, StringComparison.OrdinalIgnoreCase) 
            && x.Password==password);
        }
    }
}
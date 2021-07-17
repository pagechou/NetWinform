using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.IService
{
    public interface IUserService : IBaseService
    {
        string GetUserName(string userId);
        string GetUserName2(string id);
    }
}

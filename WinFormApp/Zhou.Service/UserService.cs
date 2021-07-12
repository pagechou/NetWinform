using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Zhou.IService;

namespace Zhou.Service
{
    public class UserService : BaseService,IUserService
    {
        public string GetUserName(string name)
        {
            return name;
        }
    }
}
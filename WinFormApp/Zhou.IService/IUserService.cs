using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhou.IService
{
    public interface IUserService:IBaseService
    {
        string GetUserName(string name);
    }
}

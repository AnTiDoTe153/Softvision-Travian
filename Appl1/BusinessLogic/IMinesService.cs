using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace BusinessLogic
{
    public interface IMinesService
    {
        City UpdateResources(string userId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evidencija.Controllers.RequestBinders
{
    public class AuthRequest
    {
       public string UserName { get; set; }
       public int Key { get; set; }
    }
}

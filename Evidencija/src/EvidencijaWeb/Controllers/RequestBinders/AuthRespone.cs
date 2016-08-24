using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evidencija.Controllers.RequestBinders
{
    public class AuthRespone
    {
        public string Token { get; set; }

        public DateTime ExpirationDate { get; set; }

        public bool IsAdmin { get; set; }
    }
}

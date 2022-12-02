using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarSync.Console
{
    internal class TokenFile
    {
        public string ClientId { get; set; }
        public string RefreshToken { get; set; }
    }
}

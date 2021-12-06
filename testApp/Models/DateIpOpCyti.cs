using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testApp.Models
{
    public class DateIpOpCyti
    {
        public string Ip { get; set; }        
        public string Cyti { get; set; }  
      
        public DateIpOpCyti(string ip, string cyti)
        {
            Ip = ip;
            Cyti = cyti;
        }
    }
}

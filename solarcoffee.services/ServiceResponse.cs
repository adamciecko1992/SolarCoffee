using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarcoffee.services
{
    public class ServiceResponse<T>
        //uzycie generycznego typu pozwala zwrocic serverowi cos innego niz tylko wiadomosc, np caly obiekt
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public T Data { get; set; }

        
    }
}

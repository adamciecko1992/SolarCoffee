using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarcoffee.data.models
{
    //za pomoca tej klasy dane beda modelowane na tabele w sql
    public class CustomerAdress
    {
        //Id potrzebne dla dzialania z entity które samo będzie inkrementować Id
        public int Id {get;set;}
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        [MaxLength(35)] //dekoratory - validatory
        public string AdressLine1 { get; set; }
        [MaxLength(35)]
        public string AdressLine2 { get; set; }
        [MaxLength(25)]
        public string State { get; set; }
        [MaxLength(10)]
        public string PostalCode { get; set; }
       
    }
}

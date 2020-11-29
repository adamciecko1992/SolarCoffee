using System;

namespace solarcoffee.data.models
{
    //za pomoca tej klasy dane beda modelowane na tabele w sql
    public class Customer
    {
        //Id potrzebne dla dzialania z entity które samo będzie inkrementować Id
        public int Id {get;set;}
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //entity framwork zauwazy customerAdress i nawiąże relacje pomiędzy tymi dwiema klasami
        public CustomerAdress PrimaryAdress { get; set; }
    }
}

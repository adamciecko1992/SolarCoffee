using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace solarcoffee.web.ViewModels
{/// <summary>
/// Product entity DTO  - obiekt ktory dostarcza dany model do roznych miejsc w zaleznoscci od contextu
/// Kopiuje 1:1 dane z Product.cs ale dobra praktyka jest to zeby te dwie classy żyły oddzielnie w roznych miejscach i mogly sie zmieniac
/// niezaleznie od tego drugiego.
/// </summary>
    public class ProductModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        [MaxLength(36)]
        public string Name { get; set; }
        [MaxLength(64)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsArchived { get; set; }
        public bool IsTaxable { get; set; }
    }
}

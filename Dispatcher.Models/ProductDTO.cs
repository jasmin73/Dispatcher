using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Morate uneti ime proizvoda")]
        [Display(Name ="Naziv")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Obavezan je opis proizvoda")]
        [Display(Name = "Opis")]

        public string Description { get; set; }
        [Required(ErrorMessage ="Cena je obavezna")]
        [Display(Name = "Cena")]

        public double Price { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
        [Range(1,int.MaxValue,ErrorMessage ="Kategorija je obavezna")]
        public int CategoryId { get; set; }
        public CategoryDTO Category { get; set; }
    }
}

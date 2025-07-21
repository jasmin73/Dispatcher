using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher.Models
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Morate uneti naziv kategorije")]
        [MaxLength(50,ErrorMessage ="Maksimalna duzina za naziv kategorije je 50 karaktera")]
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime Added_Date { get; set; } = DateTime.Now;
    }
}

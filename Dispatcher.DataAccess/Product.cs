using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher.DataAccess
{
    public class Product
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}

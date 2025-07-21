using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher.DataAccess
{
    public class Category
    {
        public int Id { get; set; }
       
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime Added_Date { get; set; } = DateTime.Now;
    }
}

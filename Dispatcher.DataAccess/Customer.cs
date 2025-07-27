using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher.DataAccess
{
    public class Customer
    {
        public int Id { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<CustomerAddress> Addresses { get; set; } = new List<CustomerAddress>();

        public int TenantId { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
    }

}

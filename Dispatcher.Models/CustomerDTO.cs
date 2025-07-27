using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher.Models
{
    public class CustomerDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        public ICollection<CustomerAddressDTO> Addresses { get; set; } = new List<CustomerAddressDTO>();

        public int TenantId { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
    }
}

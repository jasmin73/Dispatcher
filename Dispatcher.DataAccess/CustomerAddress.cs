using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher.DataAccess
{
    public class CustomerAddress
    {
        public int Id { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public string Floor { get; set; } // npr: „III“
        public string Apartment { get; set; } // npr: „14“
        public string Intercom { get; set; } // npr: „#314“

        public string Label { get; set; } // „Kuca“, „Firma“, „Posao kod supruge“ (opcionalno)

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.Now;
    }

}

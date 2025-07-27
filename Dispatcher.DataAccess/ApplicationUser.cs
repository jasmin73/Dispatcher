using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher.DataAccess
{
    public class ApplicationUser : IdentityUser
    {
        public int TenantId { get; set; }

        public string FullName { get; set; }
        public Tenant Tenant { get; set; }


        // Možeš dodati i druge podatke po potrebi (npr. uloga, status itd.)
    }
}

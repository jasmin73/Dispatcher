using Dispatcher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher.Bussiness.Repository.IRepository
{
    public interface ICustomerRepository
    {
        Task<CustomerDTO> Get(int id,int tenantId);
        Task<IEnumerable<CustomerDTO>> SearchByPhone(string phone,int tenantId);
        Task<CustomerDTO> Create(CustomerDTO customer);
        Task Update(CustomerDTO customer);
        Task<int> Delete(int id,int tenantId);
        Task<CustomerDTO> GetWithAddresses(int id,int tenantId);
        
        Task AddAddress(int customerId, CustomerAddressDTO address);
        Task UpdateAddress(CustomerAddressDTO address,int expectedTenantId);
        Task DeleteAddress(int addressId);
        Task<IEnumerable<CustomerAddressDTO>> GetAddresses(int customerId,int tenantId);
        
    }
}

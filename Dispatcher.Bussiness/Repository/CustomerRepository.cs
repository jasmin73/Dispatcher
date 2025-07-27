using AutoMapper;
using Dispatcher.Bussiness.Repository.IRepository;
using Dispatcher.DataAccess;
using Dispatcher.DataAccess.Data;
using Dispatcher.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher.Bussiness.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public CustomerRepository(ApplicationDbContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task AddAddress(int customerId, CustomerAddressDTO address)
        {
            // Proveri da li kupac postoji
            var customer = await _db.Customers
                .Include(c => c.Addresses)
                .FirstOrDefaultAsync(c => c.Id == customerId);

            if (customer == null)
                throw new Exception($"Kupac sa ID {customerId} ne postoji.");

            // Mapiraj DTO u entitet
            var newAddress = _mapper.Map<CustomerAddress>(address);
            newAddress.CustomerId = customerId;

            // Dodatno: ako unosiš i u kolekciju zbog EF trackinga
            customer.Addresses.Add(newAddress);

            await _db.SaveChangesAsync();
        }


        public async Task<CustomerDTO> Create(CustomerDTO customer)
        {
            var obj = _mapper.Map<CustomerDTO, Customer>(customer);
            obj.AddedDate = DateTime.Now;
            var addedObj =_db.Customers.Add(obj);
            await _db.SaveChangesAsync();
            return _mapper.Map<Customer, CustomerDTO>(obj);
        }

        public async Task<int> Delete(int id,int tenantId)
        {
            var obj = await _db.Customers.FirstOrDefaultAsync(x => x.Id == id && x.TenantId==tenantId);
            if (obj != null)
            {
                _db.Customers.Remove(obj);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task DeleteAddress(int addressId)
        {
            var address = await _db.CustomerAddresses
                .FirstOrDefaultAsync(a => a.Id == addressId);

            if (address == null)
                throw new Exception($"Adresa sa ID {addressId} ne postoji.");

            _db.CustomerAddresses.Remove(address);
            await _db.SaveChangesAsync();
        }

        public async Task<CustomerDTO> Get(int id,int tenantId)
        {
            var objFromDb = await _db.Customers.FirstOrDefaultAsync(x => x.Id == id && x.TenantId==tenantId);
            if (objFromDb!= null)
            {
                return _mapper.Map<Customer, CustomerDTO>(objFromDb);
            }
            return new CustomerDTO();
        }

        public async Task<IEnumerable<CustomerAddressDTO>> GetAddresses(int customerId, int tenantId)
        {
            var addresses = await _db.CustomerAddresses
                .Include(a => a.Customer)
                .Where(a => a.CustomerId == customerId && a.Customer.TenantId == tenantId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<CustomerAddressDTO>>(addresses);
        }



        public async Task<CustomerDTO> GetWithAddresses(int id,int tenantId)
        {
            var customer = await _db.Customers
                .Include(c => c.Addresses)
                .FirstOrDefaultAsync(c => c.Id == id&&c.TenantId==tenantId);

            if (customer == null)
                return null;

            return _mapper.Map<CustomerDTO>(customer);
        }


        public async Task<IEnumerable<CustomerDTO>> SearchByPhone(string phone,int tenantId)
        {
            var customers = await _db.Customers
                .Include(c => c.Addresses)
                .Where(c => c.PhoneNumber.Contains(phone)&& c.TenantId==tenantId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<CustomerDTO>>(customers);
        }


        public async Task Update(CustomerDTO customer)
        {
            var objFromDb = await _db.Customers
                .Include(c => c.Addresses)
                .FirstOrDefaultAsync(x => x.Id == customer.Id &&x.TenantId==customer.TenantId);

            if (objFromDb != null)
            {
                objFromDb.PhoneNumber = customer.PhoneNumber;
                objFromDb.TenantId = customer.TenantId;

                // DTO adrese
                var updatedAddresses = customer.Addresses ?? new List<CustomerAddressDTO>();

                // Postojeće adrese iz baze
                var existingAddresses = objFromDb.Addresses.ToList();

                // 1. Ažuriraj postojeće
                foreach (var existing in existingAddresses)
                {
                    var updated = updatedAddresses.FirstOrDefault(a => a.Id == existing.Id);
                    if (updated != null)
                    {
                        _mapper.Map(updated, existing); // mapira u isti objekat
                    }
                }

                // 2. Dodaj nove adrese
                var newAddresses = updatedAddresses
                    .Where(a => a.Id == 0) // nove nemaju ID
                    .Select(a => _mapper.Map<CustomerAddress>(a))
                    .ToList();

                foreach (var addr in newAddresses)
                {
                    objFromDb.Addresses.Add(addr);
                }

                // 3. Obriši uklonjene adrese
                var deleted = existingAddresses
                    .Where(e => !updatedAddresses.Any(a => a.Id == e.Id))
                    .ToList();

                foreach (var addr in deleted)
                {
                    _db.CustomerAddresses.Remove(addr);
                }

                await _db.SaveChangesAsync();
            }
        }


        public async Task UpdateAddress(CustomerAddressDTO addressDto,int expectedTenantId)
        {
            var addressFromDb = await _db.CustomerAddresses.Include(a=>a.Customer)
                .FirstOrDefaultAsync(a => a.Id == addressDto.Id);
            if (addressFromDb.Customer.TenantId != expectedTenantId)
                throw new UnauthorizedAccessException("Adresa ne pripada vašem nalogu.");


            if (addressFromDb == null)
                throw new Exception($"Adresa sa ID {addressDto.Id} ne postoji.");

            // Mapiraj nove podatke u postojeći entitet
            _mapper.Map(addressDto, addressFromDb);

            await _db.SaveChangesAsync();
        }

    }
}

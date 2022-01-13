using APIClientes.Data;
using APIClientes.Dto;
using APIClientes.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIClientes.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<CustomerDto>> GetCustomers();
        Task<CustomerDto> GetCustomerById(int id);
        Task<CustomerDto> CreateUpdate(CustomerDto customerDto);
        Task<bool> DeleteCustomer(int id);
    }

    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CustomerRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomerDto> CreateUpdate(CustomerDto customerDto)
        {
            Customer customer = _mapper.Map<CustomerDto, Customer>(customerDto);
            if (customer.Id > 0)
            {
                _context.Customers.Update(customer);
            }
            else
            {
                await _context.Customers.AddAsync(customer);
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<Customer, CustomerDto>(customer);
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            try
            {
                Customer customer = await _context.Customers.FindAsync(id);
                if (customer == null)
                {
                    return false;
                }

                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }


            return true;
        }

        public async Task<CustomerDto> GetCustomerById(int id)
        {
            Customer customer = await _context.Customers.FindAsync(id);

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<List<CustomerDto>> GetCustomers()
        {
            List<Customer> list = await _context.Customers.ToListAsync();

            return _mapper.Map<List<CustomerDto>>(list);
        }
    }
}

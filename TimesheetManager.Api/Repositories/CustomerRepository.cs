using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using TimesheetManager.Api.Models;
using TimesheetManager.Api.Interfaces;
using TimesheetManager.Api.Database;
using TimesheetManager.Api.Exceptions;

namespace TimesheetManager.Api.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly AppDbContext _database_context;

        public CustomerRepository(AppDbContext context)
        {
            _database_context = context;
        }


        
        public async Task<List<Customer>> List(){

            return await _database_context.Customers.OrderBy(x => x.Id).ToListAsync();
        }






        public async Task<Customer> GetById(int id)
        {
            return await _database_context.Customers.FirstOrDefaultAsync(x => x.Id == id);
        }





        public async Task Insert(Customer customer)
        {
            _database_context.Add(customer);
            await _database_context.SaveChangesAsync();
        }




        public async Task Delete(int id)
        {
            try
            {
                var customer = await _database_context.Customers.FindAsync(id);
                _database_context.Customers.Remove(customer);
                await _database_context.SaveChangesAsync();
            }catch(DbUpdateException e)
            {
                throw new DbUpdateException("Não é possível excluir este usuário. ");
            }
        }




        public async Task Update(int id, Customer customer)
        {
            bool hasAny = await _database_context.Customers.AnyAsync(x => x.Id == id);

            if(!hasAny)
                throw new NotFoundException("Usuário não encontrado");

            try
            {
                _database_context.Update(customer);
                await _database_context.SaveChangesAsync();
            }
            catch(DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
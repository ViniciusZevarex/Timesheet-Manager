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
    public class UserRepository : IRepository<User>
    {

        private readonly AppDbContext _database_context;


        public UserRepository(AppDbContext context)
        {
            _database_context = context;
        }


        
        public async Task<List<User>> List(){

            return await _database_context.Users.OrderBy(x => x.Id).ToListAsync();
        }






        public async Task<User> GetById(int id)
        {
            return await _database_context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }





        public async Task Insert(User user)
        {
            _database_context.Add(user);
            await _database_context.SaveChangesAsync();
        }




        public async Task Delete(int id)
        {
            try
            {
                var user = await _database_context.Users.FindAsync(id);
                _database_context.Users.Remove(user);
                await _database_context.SaveChangesAsync();
            }catch(DbUpdateException e)
            {
                throw new DbUpdateException("Não é possível excluir este usuário. ");
            }
        }





        public async Task Update(int id, User user)
        {
            bool hasAny = await _database_context.Users.AnyAsync(x => x.Id == id);

            if(!hasAny)
                throw new NotFoundException("Usuário não encontrado");

            try
            {
                _database_context.Update(user);
                await _database_context.SaveChangesAsync();
            }
            catch(DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

    }
}
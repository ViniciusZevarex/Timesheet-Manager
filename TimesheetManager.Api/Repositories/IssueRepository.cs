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
    public class IssueRepository : IRepository<Issue>
    {
        private readonly AppDbContext _database_context;


        public IssueRepository(AppDbContext context)
        {
            _database_context = context;
        }




        public async Task<List<Issue>> List()
        {
            return await _database_context.Issues.OrderBy(x => x.Id).ToListAsync();
        }







        public async Task<Issue> GetById(int id)
        {
            return await _database_context.Issues.FirstOrDefaultAsync(x => x.Id == id);
        }







        public async Task<int> Insert(Issue issue)
        {
            _database_context.Add(issue);
            await _database_context.SaveChangesAsync();

            return issue.Id;
        }








        public async Task Delete(int id)
        {
            try
            {
                var issue = await _database_context.Issues.FindAsync(id);
                _database_context.Issues.Remove(issue);
                await _database_context.SaveChangesAsync();
            }catch(DbUpdateException e)
            {
                throw new DbUpdateException("Não é possível excluir este usuário. ");
            }
        }







        public async Task Update(int id, Issue issue)
        {
            bool hasAny = await _database_context.Issues.AnyAsync(x => x.Id == id);

            if(!hasAny)
                throw new NotFoundException("Projeto não encontrado");

            try
            {
                _database_context.Update(issue);
                await _database_context.SaveChangesAsync();
            }
            catch(DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

    }
}
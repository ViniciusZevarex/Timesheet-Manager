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
    public class ProjectRepository : IRepository<Project>
    {


        private readonly AppDbContext _database_context;


        public ProjectRepository(AppDbContext context)
        {
            _database_context = context;
        }




        public async Task<List<Project>> List(){

            return await _database_context.Projects.OrderBy(x => x.Id).ToListAsync();
        }







        public async Task<Project> GetById(int id)
        {
            return await _database_context.Projects.FirstOrDefaultAsync(x => x.Id == id);
        }







        public async Task<int> Insert(Project project)
        {
            _database_context.Add(project);
            await _database_context.SaveChangesAsync();

            return project.Id;
        }








        public async Task Delete(int id)
        {
            try
            {
                var project = await _database_context.Projects.FindAsync(id);
                _database_context.Projects.Remove(project);
                await _database_context.SaveChangesAsync();
            }catch(DbUpdateException e)
            {
                throw new DbUpdateException("Não é possível excluir este usuário. ");
            }
        }







        public async Task Update(int id, Project project)
        {
            bool hasAny = await _database_context.Projects.AnyAsync(x => x.Id == id);

            if(!hasAny)
                throw new NotFoundException("Projeto não encontrado");

            try
            {
                _database_context.Update(project);
                await _database_context.SaveChangesAsync();
            }
            catch(DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        
    }
}
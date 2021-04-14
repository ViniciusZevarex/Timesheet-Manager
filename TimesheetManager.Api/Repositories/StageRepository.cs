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
    public class StageRepository : IRepository<Stage>
    {
        private readonly AppDbContext _database_context;


        public StageRepository(AppDbContext context)
        {
            _database_context = context;
        }




        public async Task<List<Stage>> List(){

            return await _database_context.Stages.OrderBy(x => x.Id).ToListAsync();
        }







        public async Task<Stage> GetById(int id)
        {
            return await _database_context.Stages.FirstOrDefaultAsync(x => x.Id == id);
        }







        public async Task<int> Insert(Stage stage)
        {
            _database_context.Add(stage);
            await _database_context.SaveChangesAsync();

            return stage.Id;
        }








        public async Task Delete(int id)
        {
            try
            {
                var stage = await _database_context.Stages.FindAsync(id);
                _database_context.Stages.Remove(stage);
                await _database_context.SaveChangesAsync();
            }catch(DbUpdateException e)
            {
                throw new DbUpdateException("Não é possível excluir esta etapa. ");
            }
        }







        public async Task Update(int id, Stage stage)
        {
            bool hasAny = await _database_context.Stages.AnyAsync(x => x.Id == id);

            if(!hasAny)
                throw new NotFoundException("Etapa não encontrado");

            try
            {
                _database_context.Update(stage);
                await _database_context.SaveChangesAsync();
            }
            catch(DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

    }
}
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using TimesheetManager.Api.Models;
using TimesheetManager.Api.Repositories;
using TimesheetManager.Api.Exceptions;

namespace TimesheetManager.Api.Controllers
{
    public class StageController : Controller
    {
        
        private readonly StageRepository _stageRepository;


        public StageController(StageRepository stageRepository)
        {
            _stageRepository = stageRepository;
        }


        [HttpGet]
        [Route("index")]
        public async Task<ActionResult<Response>> Index()
        {
            var list = await _stageRepository.List();

            return Ok(new Response{
                Status = 200,
                Message = "",
                Data = list
            });   
        }





        [HttpGet]
        [Route("details")]
        public async Task<ActionResult<Response>> Details(int id)
        {

            var stage = await _stageRepository.GetById(id);

            if(stage == null)
                return NotFound(new Response {
                    Status = 404,
                    Message = "Etapa não encontrado",
                    Data = null
                });

            
            return Ok(new Response{
                Status = 200,
                Message = "",
                Data = stage
            });

        }









        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<Response>> Create([FromBody] Stage model)
        {
            int id = await _stageRepository.Insert(stage: model);

            Stage stage = await _stageRepository.GetById(model.Id);

            return Ok(new Response{ 
                Status = 200,
                Message = "Etapa cadastrada com sucesso.",
                Data = stage
            });
        }







        [HttpPut]
        [Route("update")]
        public async Task<ActionResult<Response>> Update([FromBody] Stage model, [FromHeader] int id)
        {
            if(model.Id != id)
                return BadRequest(new Response{
                    Status = 409,
                    Message = "Id enviado e o objeto que deve-se fazer a alteração são distintos.",
                    Data = null
                });
            




            
            try
            {
                await _stageRepository.Update(id, model);


                return Ok(new Response{
                    Status = 200,
                    Message = "Dados da etapa alterados com sucesso.",
                    Data = model
                });

            }
            catch(NotFoundException e)
            {
                return NotFound(new Response{
                    Status = 404,
                    Message = "Etapa não encontrada",
                    Data = null
                });
            }


        }
    }
}
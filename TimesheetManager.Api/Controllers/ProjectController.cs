using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

using TimesheetManager.Api.Models;
using TimesheetManager.Api.Repositories;
using System;
using TimesheetManager.Api.Exceptions;
namespace TimesheetManager.Api.Controllers
{

    [Route("v1/project")]
    public class ProjectController : Controller
    {

        private readonly ProjectRepository _projectRepository;

        
        public ProjectController(ProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }


        [HttpGet]
        [Route("index")]
        [Authorize]
        public async Task<ActionResult<Response>> Index()
        {
            var list = await _projectRepository.List();

            return Ok(new Response{
                Status = 200,
                Message = "",
                Data = list
            });   
        }





        [HttpGet]
        [Route("details")]
        [Authorize]
        public async Task<ActionResult<Response>> Details(int id)
        {

            var project = await _projectRepository.GetById(id);

            if(project == null)
                return NotFound(new Response {
                    Status = 404,
                    Message = "Projeto não encontrado",
                    Data = null
                });

            
            return Ok(new Response{
                Status = 200,
                Message = "",
                Data = project
            });

        }









        [HttpPost]
        [Route("create")]
        [Authorize]
        public async Task<ActionResult<Response>> Create([FromBody] Project model)
        {
            int id = await _projectRepository.Insert(project: model);

            Project project = await _projectRepository.GetById(model.Id);

            return Ok(new Response{ 
                Status = 200,
                Message = "Projeto cadastrado com sucesso.",
                Data = project
            });
        }







        [HttpPut]
        [Route("update")]
        [Authorize]
        public async Task<ActionResult<Response>> Update([FromBody] Project model, [FromHeader] int id)
        {
            if(model.Id != id)
                return BadRequest(new Response{
                    Status = 409,
                    Message = "Id enviado e o objeto que deve-se fazer a alteração são distintos.",
                    Data = null
                });
            




            
            try
            {
                await _projectRepository.Update(id, model);


                return Ok(new Response{
                    Status = 200,
                    Message = "Dados do projeto alterados com sucesso.",
                    Data = model
                });

            }
            catch(NotFoundException e)
            {
                return NotFound(new Response{
                    Status = 404,
                    Message = "Projeto não encontrado",
                    Data = null
                });
            }


        }
        

    }
}
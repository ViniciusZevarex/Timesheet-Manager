using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using TimesheetManager.Api.Models;
using TimesheetManager.Api.Repositories;
using TimesheetManager.Api.Exceptions;


namespace TimesheetManager.Api.Controllers
{
    public class IssueController : Controller
    {
        private readonly IssueRepository _issueRepository;


        public IssueController(IssueRepository issueRepository)
        {
            _issueRepository = issueRepository;
        }





        [HttpGet]
        [Route("index")]
        public async Task<ActionResult<Response>> Index()
        {
            var issues = await _issueRepository.List();
            return Ok(new Response{
                Status = 200,
                Message = "",
                Data = issues
            });
        }





        [HttpGet]
        [Route("details")]
        public async Task<ActionResult<Response>> Details(int id)
        {

            var issue = await _issueRepository.GetById(id);

            if(issue == null)
                return NotFound(new Response {
                    Status = 404,
                    Message = "Pendência não encontrada", // não encontrei uma boa tradução para issue
                    Data = null
                });

            
            return Ok(new Response{
                Status = 200,
                Message = "",
                Data = issue
            });

        }





        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<User>> Create([FromBody] Issue model)
        {
            await _issueRepository.Insert(issue: model);
            return Ok("Pendência Inserida com Sucesso!");
        }





        [HttpPut]
        [Route("update")]
        public async Task<ActionResult<Response>> Update([FromBody] Issue model, [FromHeader] int id)
        {
            if(model.Id != id)
                return BadRequest(new Response{
                    Status = 409,
                    Message = "Id enviado e o objeto que deve-se fazer a alteração são distintos.",
                    Data = null
                });
            

            try
            {
                await _issueRepository.Update(id, model);

                return Ok(new Response{
                    Status = 200,
                    Message = "Dados da pendência alterados com sucesso.",
                    Data = model
                });

            }
            catch(NotFoundException e)
            {
                return NotFound(new Response{
                    Status = 404,
                    Message = "Pendência não encontrada",
                    Data = null
                });
            }
        }

    }
}
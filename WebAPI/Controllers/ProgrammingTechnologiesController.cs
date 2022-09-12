using Application.Features.ProgrammingLanguages.Models;
using Application.Features.ProgrammingTechnologies.Commands.CreateProgrammingTechnology;
using Application.Features.ProgrammingTechnologies.Commands.DeleteProgrammingTechnology;
using Application.Features.ProgrammingTechnologies.Commands.UpdateProgrammingTechnology;
using Application.Features.ProgrammingTechnologies.DTOs;
using Application.Features.ProgrammingTechnologies.Models;
using Application.Features.ProgrammingTechnologies.Queries.GetByIdProgrammingTechnology;
using Application.Features.ProgrammingTechnologies.Queries.GetListByDynamicProgrammingTechnology;
using Application.Features.ProgrammingTechnologies.Queries.GetListProgrammingTechnology;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgrammingTechnologiesController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] CreateProgrammingTechnologyCommand createProgrammingTechnologyCommand)
        {
            CreatedProgrammingTechnologyDto result = await Mediator.Send(createProgrammingTechnologyCommand);
            return Created("", result);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] DeleteProgrammingTechnologyCommand deleteProgrammingTechnologyByIdCommand)
        {
            DeletedProgrammingTechnologyByIdDto result = await Mediator.Send(deleteProgrammingTechnologyByIdCommand);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateProgrammingTechnologyCommand updateProgrammingTechnologyCommand)
        {
            UpdatedProgrammingTechnologyDto updatedProgrammingLanguage = await Mediator.Send(updateProgrammingTechnologyCommand);
            return Accepted(updatedProgrammingLanguage);
        }

        [HttpGet]
        public async Task<ActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListProgrammingTechnologyQuery getListProgrammingTechnologyQuery = new() { PageRequest = pageRequest };
            ProgrammingTechnologyListModel result = await Mediator.Send(getListProgrammingTechnologyQuery);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> GetById([FromRoute] GetByIdProgrammingTechnologyQuery getByIdProgrammingTechnologyQuery)
        {
            ProgrammingTechnologyGetByIdDto programmingTechnologyGetByIdDto =
                await Mediator.Send(getByIdProgrammingTechnologyQuery);
            return Ok(programmingTechnologyGetByIdDto);
        }

        [HttpPost("getbydynamic")]
        public async Task<ActionResult> GetByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
        {
            GetListByDynamicProgrammingTechnologyQuery getListByDynamicProgrammingTechnologyQuery = new GetListByDynamicProgrammingTechnologyQuery { Dynamic = dynamic, PageRequest = pageRequest };
            var result = await Mediator.Send(getListByDynamicProgrammingTechnologyQuery);
            return Ok(result);
        }
    }
}

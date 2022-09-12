using Application.Features.SocialMedias.Commands.CreateSocialMedias;
using Application.Features.SocialMedias.Commands.DeleteSocialMedias;
using Application.Features.SocialMedias.Commands.UpdateSocialMedias;
using Application.Features.SocialMedias.DTOs;
using Application.Features.SocialMedias.Models;
using Application.Features.SocialMedias.Queries.GetListSocialMedia;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediasController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateSocialMediaCommand createSocialMediaCommand)
        {
            CreatedSocialMediaDto createdSocialMediaDto = await Mediator.Send(createSocialMediaCommand);
            return Created("", createdSocialMediaDto);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteSocialMediaCommand deleteSocialMediaCommand)
        {
            DeletedSocialMediaDto deletedSocialMediaDto = await Mediator.Send(deleteSocialMediaCommand);
            return Ok(deletedSocialMediaDto);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateSocialMediaCommand updateSocialMediaCommand)
        {
            UpdatedSocialMediaDto updatedSocialMediaDto = await Mediator.Send(updateSocialMediaCommand);
            return Ok(updatedSocialMediaDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListSocialMediaQuery getListSocialMediaQuery = new() { PageRequest = pageRequest };
            GetListSocialMediaModel getListSocialMediaModel = await Mediator.Send(getListSocialMediaQuery);
            return Ok(getListSocialMediaModel);
        }
    }
}

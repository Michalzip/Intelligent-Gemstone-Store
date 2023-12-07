using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using IntelligentStore.Application.DTOs;
using IntelligentStore.Application;
using Shared;

namespace IntelligentStore.Api.Controllers.AdminControllers;

[Tags("Admin/AuthorizationController")]
[ApiController]
[Route("")]
public class AuthorizationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthorizationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [SwaggerOperation(Summary = "Admin sign in here to get to him privilege")]
    [HttpPost("admin/sign-in")]
    public async Task<ResponseMessage> AdminAuthorization([FromBody] AdminDto adminDto)
    {
        return await _mediator.Send(new AdminSignInCommand(adminDto.Email, adminDto.Password));
    }
}

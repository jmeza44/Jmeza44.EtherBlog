using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jmeza44.EtherBlog.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
        private readonly ISender _mediator;
        protected ISender Mediator => _mediator ?? HttpContext.RequestServices.GetService<IMediator>();
    }
}

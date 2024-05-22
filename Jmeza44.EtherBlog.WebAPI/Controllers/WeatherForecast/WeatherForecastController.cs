using Jmeza44.EtherBlog.Application.Common.DTOs;
using Jmeza44.EtherBlog.Application.Main.WeatherForecastRequest.Commands.CreateForecast;
using Jmeza44.EtherBlog.Application.Main.WeatherForecastRequest.Commands.DeleteForecast;
using Jmeza44.EtherBlog.Application.Main.WeatherForecastRequest.Commands.UpdateForecast;
using Jmeza44.EtherBlog.Application.Main.WeatherForecastRequest.Queries.GetForecast;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jmeza44.EtherBlog.WebApi.Controllers.WeatherForecast
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WeatherForecastController : ApiController
    {
        [HttpGet("GetForecast")]
        public async Task<ActionResult<List<WeatherForecastDto>>> GetForecast(
            [FromQuery] GetForecastQuery request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("CreateForecast")]
        public async Task<ActionResult<bool>> CreateForecast(
            [FromBody] CreateForecastCommand request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpPut("UpdateForecast")]
        public async Task<ActionResult<bool>> UpdateForecast(
            [FromQuery] UpdateForecastCommand request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("DeleteForecast")]
        public async Task<ActionResult<bool>> DeleteForecast(
            [FromQuery] DeleteForecastCommand request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }
    }
}
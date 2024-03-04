using Cine.Application.Dtos.Request.Movie;
using Cine.Application.Interfaces;
using Cine.Infrastructure.Commons.Bases.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieApplication _movieApplication;

        public MovieController(IMovieApplication movieApplication)
        {
            _movieApplication = movieApplication;
        }

        [HttpPost("ObtenerPorFiltros")]
        public async Task<IActionResult> ListMovies([FromBody] BaseFiltersRequest _filters)
        {
            var response = await _movieApplication.ListMovies(_filters);
            return Ok(response);
        }

        [HttpGet("{movieId:int}")]
        public async Task<IActionResult> MovieById(int movieId)
        {
            var respose = await _movieApplication.GetMovieById(movieId);
            return Ok(respose);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterMovie([FromBody] MovieRequestDto requestDto)
        {
            var response = await _movieApplication.RegisterMovie(requestDto);
            return Ok(response);
        }

        [HttpPut("Edit/{movieId:int}")]
        public async Task<IActionResult> EditMovie(int movieId, [FromBody] MovieUpdateRequestDto requestDto)
        {
            var response = await _movieApplication.UpdateMovie(movieId,requestDto);
            return Ok(response);
        }

        [HttpDelete("Remove/{movieId:int}")]
        public async Task<IActionResult> RemoveMovie(int movieId)
        {
            var response = await _movieApplication.RemoveMovie(movieId);
            return Ok(response);
        }
    }
}

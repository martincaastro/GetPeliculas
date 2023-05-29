using GetPeliculas.Models;
using GetPeliculas.Services;
using Microsoft.AspNetCore.Mvc;

namespace GetPeliculas.Controllers
{
    [ApiController]
    [Route("api/pelicula")]


    public class PeliculasController : ControllerBase
    {
        private readonly IPeliculasService peliculasService;

        public PeliculasController(IPeliculasService peliculasService)
        {
            this.peliculasService = peliculasService;
        }

        [HttpGet("{title}")]
        public IActionResult GetPelicula(string title)
        {
            Pelicula movie = peliculasService.GetPeliculaByTitulo(title);
            if (movie == null)
                return NotFound();

            return Ok(movie);
        }
    }
}

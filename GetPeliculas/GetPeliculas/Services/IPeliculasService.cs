using GetPeliculas.Models;
using GetPeliculas.Models;

namespace GetPeliculas.Services
{
    public interface IPeliculasService
    {
        Pelicula GetPeliculaByTitulo(string title);
    }
}

using GetPeliculas.Models;
using GetPeliculas.Services;
using Newtonsoft.Json;
using RestSharp;

public class PeliculasService : IPeliculasService
{
    private readonly string apiKey = "67735e21bf969c50b104b81f4e5c0de2";

    public Pelicula GetPeliculaByTitulo(string title)
    {
        Pelicula movie = GetDetallePeliculaByTitulo(title);
        return movie;
    }

    private Pelicula GetDetallePeliculaByTitulo(string title)
    {
        RestClient client = new RestClient("https://api.themoviedb.org/3");
        RestRequest request = new RestRequest($"search/movie");
        request.AddParameter("api_key", apiKey);
        request.AddParameter("query", title);

        RestResponse response = client.Get(request);

        Peliculas result = JsonConvert.DeserializeObject<Peliculas>(response.Content);

        if (result.Results.Count == 0)
            return null;

        Pelicula primeraPelicula = result.Results.FirstOrDefault();

        Pelicula movie = new Pelicula
        {
            Titulo = primeraPelicula.Titulo,
            TituloOriginal = primeraPelicula.TituloOriginal,
            PuntuacionMedia = primeraPelicula.PuntuacionMedia,
            FechaEstreno = primeraPelicula.FechaEstreno,
            Descripcion = primeraPelicula.Descripcion,
            ListaPeliculasSimilares = GetListaPeliculasSimilares(result.Results, primeraPelicula.Id)
        };

        return movie;
    }

    //private string GetPeliculasSimilares(List<Pelicula> PeliculasSimilares, int idPrimeraPelicula)
    //{
    //    List<string> peliculasSimilares = new List<string>();

    //    for (int i = 0; i < PeliculasSimilares.Count && peliculasSimilares.Count < 5; i++)
    //    {
    //        Pelicula similar = PeliculasSimilares[i];

    //        if (similar.Id == idPrimeraPelicula)
    //        {
    //            continue;
    //        }

    //        string title = similar.Titulo;
    //        string fechaEstreno = string.IsNullOrEmpty(similar.FechaEstreno) ? "" : $" ({Convert.ToDateTime(similar.FechaEstreno).Year})";

    //        peliculasSimilares.Add($"{title}{fechaEstreno}");
    //    }

    //    return string.Join(", ", peliculasSimilares);
    //}

    private List<string> GetListaPeliculasSimilares(List<Pelicula> PeliculasSimilares, int idPrimeraPelicula)
    {
        List<string> peliculasSimilares = new List<string>();

        for (int i = 0; i < PeliculasSimilares.Count && peliculasSimilares.Count < 5; i++)
        {
            Pelicula similar = PeliculasSimilares[i];

            if (similar.Id == idPrimeraPelicula)
            {
                continue;
            }

            string title = similar.Titulo;
            string fechaEstreno = string.IsNullOrEmpty(similar.FechaEstreno) ? "" : $" ({Convert.ToDateTime(similar.FechaEstreno).Year})";

            peliculasSimilares.Add($"{title}{fechaEstreno}");
        }

        return peliculasSimilares;
    }
}

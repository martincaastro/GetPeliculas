using GetPeliculas.Models;
using GetPeliculas.Services;
using Newtonsoft.Json;
using RestSharp;
using System.Reflection.Metadata.Ecma335;

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
            ListaPeliculasSimilares = GetListaPeliculasSimilares(primeraPelicula.Id)
        };

        return movie;
    }

    private List<string> GetListaPeliculasSimilares(int peliculaId)
    {
        RestClient client = new RestClient("https://api.themoviedb.org/3");
        RestRequest request = new RestRequest($"movie/{peliculaId}/similar");
        request.AddParameter("api_key", apiKey);

        RestResponse response = client.Get(request);

        Peliculas result = JsonConvert.DeserializeObject<Peliculas>(response.Content);

        List<string> peliculasSimilares = new List<string>();

        foreach (Pelicula pelicula in result.Results)
        {
            if (pelicula.Id == peliculaId) { continue; }

            if (peliculasSimilares.Count == 5) { break; }

            string title = pelicula.Titulo;
            string fechaEstreno = string.IsNullOrEmpty(pelicula.FechaEstreno) ? "" : $" ({Convert.ToDateTime(pelicula.FechaEstreno).Year})";

            peliculasSimilares.Add($"{title}{fechaEstreno}");

        }

        return peliculasSimilares;
    }
}

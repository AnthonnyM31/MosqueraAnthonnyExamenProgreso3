namespace MosqueraAnthonnyExamenProgreso3;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

public partial class Buscar : ContentPage
{
	public Buscar()
	{
		InitializeComponent();
	}




    private async void BuscarButton_Clicked(object sender, EventArgs e)
	{
        String nombrePais = NombrePaisEntry.Text;

		if (string.IsNullOrWhiteSpace(nombrePais))
		{
			await DisplayAlert("Error", "pais invalido", "OK");
            return;
        }

		string url = $"https://restcountries.com/v3.1/name/{nombrePais}?fields=name,region,maps";

		try
		{
			using HttpClient client = new();
			var resultado = await client.GetFromJsonAsync<List<Pais>>(url);
			if (resultado != null && resultado.Count > 0)
			{
				var pais = resultado[0];
				ResultadosLabel.Text = $"Nombre: {pais.name.Common}\nRegión: {pais.Region}\nMapa: {pais.maps.GoogleMaps}";
			}
			else
			{
				ResultadosLabel.Text = "No se encontró ningun pais con ese nombre.";
            }
		}
		catch (Exception ex) {

			ResultadoLabel.Text = "ocurrió un error al buscar el pais, verifique el nombre o la conexión";
		}

	}

    public class Pais
    {
        public Nombre name { get; set; }
        public string Region { get; set; }
        public Mapas maps { get; set; }
    }


    public class Nombre
    {
        public string Common { get; set; }
    }

    public class Mapas
    {
        public string GoogleMaps { get; set; }
    }
}
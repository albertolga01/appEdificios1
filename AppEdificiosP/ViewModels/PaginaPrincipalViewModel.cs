using AppEdificiosP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppEdificiosP.ViewModels
{
    public class PaginaPrincipalViewModel : BaseViewModel
    {

        string _Nombre;
        string _CalleNumero;
        //public string CalleNumero { get; set; }
        public string Nombre
        {
            get { return _Nombre; }
            set { SetValue(ref _Nombre, value); }
        }
        public string CalleNumero
        {
            get { return _CalleNumero; }
            set { SetValue(ref _CalleNumero, value); }
        }

        private static readonly HttpClient client = new HttpClient();
        public PaginaPrincipalViewModel()
        {
            Title = "Pagina Principal";

        }

        public async Task datosSesion()
        {
            string id = Preferences.Get("id", "");


            var values = new Dictionary<string, string>
            {
                { "identificador", id},
                { "id","datosSesion" }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("https://app.petromargas.com/api/indexapp.php", content);
            var responseString = await response.Content.ReadAsStringAsync();

            var res = JsonConvert.DeserializeObject<MPrincipal>(responseString);
            Nombre = res.nombre;
            string telefono = res.telefono;
            string rfc = res.rfc;
            CalleNumero = res.calleNumero;
        }

        public async Task lecturas(ListView listV)
        {
            listV.ItemTapped += abrirRecibo;
            string id = Preferences.Get("id", "");
            var values = new Dictionary<string, string>
            {
                { "identificador", id},
                { "id","obtenerRecibos" }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("https://app.petromargas.com/api/indexapp.php", content);
            var responseString = await response.Content.ReadAsStringAsync();

            var lecturas = JsonConvert.DeserializeObject<List<Recibo>>(responseString);

            listV.ItemsSource = lecturas;

        }

        private void abrirRecibo(object sender, ItemTappedEventArgs e)
        {

            if (e.Item != null)
            {
                var selectedItem = (Recibo)e.Item;
                DisplayAlert("Campos Vacios", selectedItem.Total + "  " + selectedItem.Date, "OK");
                // Do something with the selected item
            }
            // Disable visual selection state
            if (sender is ListView listView)
                listView.SelectedItem = null;
        }
        public ICommand DatosSesioncommand => new Command(async () => await datosSesion());


        public class Recibo
        {
            public string Total { get; set; }
            public string Date { get; set; }
            public string Icon { get; set; }
        }
    }
}
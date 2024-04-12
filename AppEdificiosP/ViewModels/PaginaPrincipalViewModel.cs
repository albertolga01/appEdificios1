using AppEdificiosP.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
        double _Saldo;
        string _FechaCorte;
        string _IDlectura;
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

        public string IDlectura
        {
            get { return _IDlectura; }
            set { SetValue(ref _IDlectura, value); }
        }
        public double Saldo
        {
            get { return _Saldo; }
            set { SetValue(ref _Saldo, value); }
        }
        public string FechaCorte
        {
            get { return _FechaCorte; }
            set { SetValue(ref _FechaCorte, value); }
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

            double saldo = 0;
            foreach (var property in lecturas)
            {
                if (property.pagado == 0)
                {
                    saldo += property.total;
                }
            }
            Saldo = saldo;

            FechaCorte = lecturas[0].Date.ToString();
            IDlectura = lecturas[0].idLectura.ToString();            

        }

        private async void abrirRecibo(object sender, ItemTappedEventArgs e)
        {

            if (e.Item != null)
            {
                
                    var selectedItem = (Recibo)e.Item;
            
                    // URL del PDF que quieres descargar
                    string urlPDF = "https://app.petromargas.com/Recibo/" + selectedItem.idLectura;

                    HttpClient httpClient = new HttpClient();
                    var stream = await httpClient.GetStreamAsync(urlPDF);

                    // Guardar el PDF localmente
                    string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "archivo.pdf");
                    using (var fileStream = File.Create(filePath))
                    {
                        await stream.CopyToAsync(fileStream);
                    }

                    // Verificar si el archivo se descargó correctamente
                    if (File.Exists(filePath))
                    {
                        // Abrir el archivo PDF con la aplicación predeterminada del dispositivo
                        await Launcher.OpenAsync(new OpenFileRequest
                        {
                            File = new ReadOnlyFile(filePath)
                        });
                    }
                    else
                    {
                        await DisplayAlert("Error", "El archivo PDF no se pudo descargar", "OK");
                    }
            }

                    //DisplayAlert("Campos Vacios", selectedItem.idLectura + "  " + selectedItem.Date, "OK");
         
            
            
            // Disable visual selection state
            if (sender is ListView listView)
                listView.SelectedItem = null;
        }

        public async Task PaginaPagar(double saldo)
        {
            string ruta = $"//openPay?parametro={saldo}";
            await Shell.Current.GoToAsync(ruta);


        }
        public async Task PDFRecibo()
        {
            // URL del PDF que quieres descargar
            string urlPDF = "https://app.petromargas.com/Recibo/" + IDlectura;

            HttpClient httpClient = new HttpClient();
            var stream = await httpClient.GetStreamAsync(urlPDF);

            // Guardar el PDF localmente
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "archivo.pdf");
            using (var fileStream = File.Create(filePath))
            {
                await stream.CopyToAsync(fileStream);
            }

            // Verificar si el archivo se descargó correctamente
            if (File.Exists(filePath))
            {
                // Abrir el archivo PDF con la aplicación predeterminada del dispositivo
                await Launcher.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(filePath)
                });
            }
            else
            {
                await DisplayAlert("Error", "El archivo PDF no se pudo descargar", "OK");
            }
        }




        public ICommand DatosSesioncommand => new Command(async () => await datosSesion());
        //public ICommand PaginaPagarcommand => new Command(async () => await PaginaPagar());
        public ICommand PaginaPagarcommand => new Command<double>(async (parametro) => await PaginaPagar(parametro));
        public ICommand PDFRecibocommand => new Command(async () => await PDFRecibo());

        public class Recibo
        {
            public string id { get; set; }
            public string idLectura { get; set; }
            public double total { get; set; }
            public string Date { get; set; }
            public string Icon { get; set; }
            public double pagado { get; set; }
        }
    }
}
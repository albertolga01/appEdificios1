using AppEdificiosP.Models;
using AppEdificiosP.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppEdificiosP.ViewModels
{
    public class MetodoPagosViewModel : BaseViewModel
    {
        double _Saldo;
        string _CodigoBarra;
        
        public double Saldo
        {
            get { return _Saldo; }
            set { SetValue(ref _Saldo, value); }
        }
        public string CodigoBarra
        {
            get { return _CodigoBarra; }
            set { SetValue(ref _CodigoBarra, value); }
        }
  
        public ImageSource ImagenUrl { get; set; }
        private static readonly HttpClient client = new HttpClient();
        public MetodoPagosViewModel()
        {
           /* string ssaldo = Preferences.Get("Saldo", "");
            if (double.TryParse(ssaldo, out _Saldo))
            {
          
            }*/

            Title = "Metodos de Pagos";
       

        }

        public async Task SaldoCliente()
        {
            string id = Preferences.Get("id", "");
            var values = new Dictionary<string, string>
            {
                { "identificador", id},
                { "id","obtenerSaldoPendiente" }
            };
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("https://app.petromargas.com/api/indexapp.php", content);
            var responseString = await response.Content.ReadAsStringAsync();

            var lecturas = JsonConvert.DeserializeObject<List<Recibo>>(responseString);

            double saldo = 0;
            foreach (var property in lecturas)
            {
                if (property.pagado == 0)
                {
                    saldo += property.total;
                }
            }
            Saldo = saldo;

            CodigoBarra = lecturas[0].barcode.ToString();
            
        }


        public async Task vistaOpenPay()
        {
            if(Saldo == 0)
            {
                await DisplayAlert("Alerta", "Saldo en $0", "OK");
            }
            else
            {
                await Shell.Current.GoToAsync($"//{nameof(openPay)}");
            }
           
        }

        public async Task atras()
        {
            await Shell.Current.GoToAsync($"//{nameof(PaginaPrincipal)}");
            //await Shell.Current.GoToAsync($"MetodoPago");
        }


        public ICommand vistaOpenpaycommand => new Command(async () => await vistaOpenPay());
        public ICommand BackCommand => new Command(async () => await atras());


        public class Recibo
        {
            public string id { get; set; }
            public string idLectura { get; set; }
            public double litros { get; set; }
            public string precioUnitario { get; set; }
            public double total { get; set; }
            public double timbrado { get; set; }
            public double pagado { get; set; }
            public string barcode { get; set; }
        }
    }
}

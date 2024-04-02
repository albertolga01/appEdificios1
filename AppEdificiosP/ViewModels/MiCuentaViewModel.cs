using AppEdificiosP.Models;
using AppEdificiosP.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppEdificiosP.ViewModels
{
    public class MiCuentaViewModel : BaseViewModel
    {

        string _Nombre;
        string _CalleNumero;
        string _Telefono;
        string _Rfc;
        string _Cfdi;
        string _RegimenFiscal;
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
        public string Telefono
        {
            get { return _Telefono; }
            set { SetValue(ref _Telefono, value); }
        }
        public string Rfc
        {
            get { return _Rfc; }
            set { SetValue(ref _Rfc, value); }
        }
        public string Cfdi
        {
            get { return _Cfdi; }
            set { SetValue(ref _Cfdi, value); }
        }
        public string RegimenFiscal
        {
            get { return _RegimenFiscal; }
            set { SetValue(ref _RegimenFiscal, value); }
        }
        private static readonly HttpClient client = new HttpClient();
        public MiCuentaViewModel()
        {
            Title = "Mi Cuenta";
          

        }

        public async Task datosCliente()
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
            Telefono = res.telefono;
            Rfc = "RFC: " + res.rfc;
            Cfdi = "CFDI: " + res.cfdi;
            RegimenFiscal = "Regimen Fiscal: " + res.regimenFiscal;
            CalleNumero = res.calleNumero + " " +res.colonia;
        }



    }
}
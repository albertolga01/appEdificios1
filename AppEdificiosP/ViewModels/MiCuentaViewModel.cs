using AppEdificiosP.Models;
using AppEdificiosP.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppEdificiosP.ViewModels
{
    public class MiCuentaViewModel : BaseViewModel
    {
        string _Identificador;
        string _Nombre;
        string _CalleNumero;
        string _Telefono;
        string _Rfc;
        string _Cfdi;
        string _RegimenFiscal;
        public string Identificador
        {
            get { return _Identificador; }
            set { SetValue(ref _Identificador, value); }
        }
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
        public string NuevaContraseña { get; set; }
        public string RNuevaContraseña { get; set; }

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
            Identificador = res.id;
            Nombre = res.nombre;
            Telefono = res.telefono;
            Rfc = "RFC: " + res.rfc;
            Cfdi = "CFDI: " + res.cfdi;
            RegimenFiscal = "Regimen Fiscal: " + res.regimenFiscal;
            CalleNumero = res.calleNumero + " " +res.colonia;
        }

        public async Task CambioContra()
        {
            if (string.IsNullOrEmpty(NuevaContraseña) || string.IsNullOrEmpty(RNuevaContraseña))
            {
                await DisplayAlert("Campos Vacios", "FAVOR DE LLENAR LOS CAMPOS", "OK");
            }
            else
            {
                if(NuevaContraseña == RNuevaContraseña)
                {
                    var values = new Dictionary<string, string>
                    {
                        { "identificador", Identificador},
                        { "contrasena", NuevaContraseña },
                        { "id","cambiarContra" }
                    };
                    var content = new FormUrlEncodedContent(values);
                    var response = await client.PostAsync("https://app.petromargas.com/api/indexapp.php", content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    Console.Write(responseString);
                    await DisplayAlert("Contraseña", responseString, "OK");
                    if(responseString == "Error.") { } 
                    else
                    {
                        Preferences.Clear();

                        await Shell.Current.GoToAsync("//LoginPage");
            
                    }
                }
                else
                {
                    await DisplayAlert("Contraseña", "Las contraseñas no coinciden", "OK");
                }
            }


        }

        public ICommand CambioContracommand => new Command(async () => await CambioContra());
    }
}
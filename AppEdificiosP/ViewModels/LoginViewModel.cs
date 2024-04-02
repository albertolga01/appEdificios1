using AppEdificiosP.Views;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using AppEdificiosP.Models;
using Xamarin.Essentials;

namespace AppEdificiosP.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
   
        public Command LoginCommand { get; }
        public string Contraseña{get; set;}
        public string Correo { get; set; }
        private static readonly HttpClient client = new HttpClient();

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            if (string.IsNullOrEmpty(Correo) || string.IsNullOrEmpty(Contraseña))
            {

                await DisplayAlert("Campos Vacios", "FAVOR DE LLENAR LOS CAMPOS", "OK");
            }
            else
            {
                var values = new Dictionary<string, string>
                {
                    { "correo", Correo},
                    { "contrasena", Contraseña },
                    { "id","loggin" }
                };
                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync("https://app.petromargas.com/api/indexapp.php", content);
                var responseString = await response.Content.ReadAsStringAsync();
                Console.Write(responseString);
                if (responseString.Length < 10)
                {
                    await DisplayAlert("Error al iniciar sesion", "Datos de acceso incorrectos", "OK");
                    return;
                }
                var res = JsonConvert.DeserializeObject<MLogin>(responseString);
                string email = res.correo;
                string pass = res.contrasena;
                string identificador = res.id;

                Preferences.Set("isLoggedIn", true);
                Preferences.Set("correo", email);
                Preferences.Set("pass", pass);
                Preferences.Set("id", identificador);

                if (Correo == email && Contraseña == pass)
                {
                    await Shell.Current.GoToAsync($"//{nameof(PaginaPrincipal)}");
                }
            }
        }


        public async Task datosSesion()
        {
            if (Preferences.Get("isLoggedIn", false) == true)
            {
                await Shell.Current.GoToAsync($"//{nameof(PaginaPrincipal)}");
            }

        }

    }
}

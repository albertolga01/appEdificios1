using AppEdificiosP.Models;
using Newtonsoft.Json;
using Openpay;
using Openpay.Entities;
using Openpay.Entities.Request;
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
    public class openPayViewModel : BaseViewModel
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string CardExpirationDate { get; set; }
        public string CardCvv { get; set; }



        public async void cargo()
        {
            if (string.IsNullOrEmpty(CardName) || string.IsNullOrEmpty(CardNumber) || string.IsNullOrEmpty(CardExpirationDate) || string.IsNullOrEmpty(CardCvv))
            {
                await DisplayAlert("Campos Vacios", "Complete todo los campos ","OK");
            }
            else
            {
                CardNumber = CardNumber.Replace("-", "");

                string[] parts = CardExpirationDate.Split('/');
                string month = parts[0]; // "12"
                string year = parts[1];  // "25"
                string json = @"{
                    ""card_number"":""" + CardNumber + @""",
                    ""holder_name"":""" + CardName + @""",
                    ""expiration_year"":""" + year + @""",
                    ""expiration_month"":""" + month + @""",
                    ""cvv2"":""" + CardCvv + @"""
                }";
                /*                
                        ""address"":{
                            ""city"":""Querétaro"",
                            ""country_code"":""MX"",
                            ""postal_code"":""76900"",
                            ""line1"":""Av 5 de Febrero"",
                            ""line2"":""Roble 207"",
                            ""line3"":""col carrillo"",
                            ""state"":""Queretaro""
                        }
                 */
                try
                {
                    HttpMethod method = HttpMethod.Post;
                    string responseToken = await SendJsonRequest("https://sandbox-api.openpay.mx/v1/mq63xhurcngj88wral19/tokens?", method, json);
                    var jsonObjectToken = JsonConvert.DeserializeObject<MToken>(responseToken);

                    OpenpayAPI api = new OpenpayAPI("sk_9f13ea88b52c471d9bc49465bb24fcd4", "mq63xhurcngj88wral19");
                    api.Production = false; // Default value = false 

                    ChargeRequest requestC = new ChargeRequest();
                    Customer customer = new Customer();

                    string email = Preferences.Get("correo", "");

                    customer.Name = jsonObjectToken.card.holder_name;
                    customer.Email = email; //correo del cliente
                    requestC.Method = "card";
                    requestC.Amount = new Decimal(10.00); //Cantidad
                    requestC.Description = "Cargo inicial a mi merchant"; // cambiar descripcion
                    requestC.OrderId = "oid-00056"; //unico  
                    requestC.SendEmail = false;
                    requestC.RedirectUrl = "http://www.openpay.mx/index.html";
                    requestC.Customer = customer;
                    requestC.DeviceSessionId = Guid.NewGuid().ToString().Replace("-", "");
                    requestC.SourceId = jsonObjectToken.id;

                    Charge charge = api.ChargeService.Create(requestC);
                    await DisplayAlert("Error al iniciar sesion", "Id: " + charge.Id + " cantidad: " + charge.Amount, "OK");
                }
                catch (Exception e)
                {
                    await DisplayAlert("Error al iniciar sesion", e.ToString(), "OK");
                }

            }
                

        }


        public async Task<string> SendJsonRequest(string url, HttpMethod method, string jsonContent)
        {
            try
            {
                using (var httpClient = new HttpClient())
                { 
                    var request = new HttpRequestMessage(method, url);
                     
                    if (!string.IsNullOrEmpty(jsonContent))
                    {
                        request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    }

                    string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes("sk_9f13ea88b52c471d9bc49465bb24fcd4" + ":" + ""));
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);
                     
                    HttpResponseMessage response = await httpClient.SendAsync(request);
                     
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine("Error: " + ex.Message);
                return "Error: " + ex.Message;
            }
        }



        public ICommand Pagarcommand => new Command( () => cargo());
    }
}

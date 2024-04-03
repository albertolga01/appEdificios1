using Openpay;
using Openpay.Entities;
using Openpay.Entities.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;




namespace AppEdificiosP.ViewModels
{
    public class openPayViewModel : BaseViewModel
    {
        public async void datosSesion()
        {
            try
            {
                OpenpayAPI openpayAPI = new OpenpayAPI("sk_9f13ea88b52c471d9bc49465bb24fcd4", "mq63xhurcngj88wral19");
                openpayAPI.Production = false; // Default value = false 
                OpenpayAPI api = new OpenpayAPI("sk_9f13ea88b52c471d9bc49465bb24fcd4", "mq63xhurcngj88wral19");
                ChargeRequest requestC = new ChargeRequest();
                Customer customer = new Customer();

                Customer requestCustomer = new Customer();
                requestCustomer.ExternalId = "idExterno0101127";
                requestCustomer.Name = "Julian Gerardo";
                requestCustomer.LastName = "López Martínez";
                requestCustomer.Email = "julian.martinez@gmail.com";
                requestCustomer.PhoneNumber = "4421432915";
                requestCustomer.RequiresAccount = false;
                Address address = new Address();
                address.City = "Queretaro";
                address.CountryCode = "MX";
                address.State = "Queretaro";
                address.PostalCode = "79125";
                address.Line1 = "Av. Pie de la cuesta #12";
                address.Line2 = "Desarrollo San Pablo";
                address.Line3 = "Qro. Qro.";
                requestCustomer.Address = address;

               // requestCustomer = api.CustomerService.Create(requestCustomer);


                
                 
                
                Card request = new Card();
                request.HolderName = "Juan Perez Ramirez";
                request.CardNumber = "4111111111111111";
                request.Cvv2 = "110";
                request.ExpirationMonth = "12";
                request.ExpirationYear = "25";
                request.DeviceSessionId = "kR1MiQhz2otdIuUlQkbEyitIqVMiI16f";
                Address addressC = new Address();
                addressC.City = "Queretaro";
                addressC.CountryCode = "MX";
                addressC.State = "Queretaro";
                addressC.PostalCode = "79125";
                addressC.Line1 = "Av. Pie de la cuesta #12";
                addressC.Line2 = "Desarrollo San Pablo";
                addressC.Line3 = "Qro. Qro.";
                request.Address = address;

               // request = api.CardService.Create(requestCustomer.Id, request);

               // await DisplayAlert("Error al iniciar sesion", "id tarjeta: " +request.Id, "OK");

                /*
                Card card = new Card();
                card.CardNumber = "4111111111111111";
                card.ExpirationMonth = "12";
                card.ExpirationYear = "25";
                card.HolderName = "John Doe";
                card.Cvv2 = "123";  */

                customer.Name = "Juan";
                customer.LastName = "Vazquez Juarez";
                customer.PhoneNumber = "4423456723";
                customer.Email = "juan.vazquez@empresa.com.mx";
                customer.Id = "aggngmdshleczwlzmquk";

                requestC.Method = "card";
                requestC.Amount = new Decimal(100.00);
                requestC.Description = "Cargo inicial a mi merchant";
                requestC.OrderId = "oid-00051";
                //request.Confirm = false;
                requestC.SendEmail = false;
                requestC.RedirectUrl = "http://www.openpay.mx/index.html";
                requestC.Customer = customer;

                
                requestC.DeviceSessionId = Guid.NewGuid().ToString().Replace("-", "");
                requestC.SourceId = 




                requestC.SourceId = "k4acjwqr3cs5purr2jrq"; 

                Charge charge = api.ChargeService.Create(requestC);
                await DisplayAlert("Error al iniciar sesion", "Datos de acceso incorrectos", "OK");
            }
            catch (Exception e)
            {
                await DisplayAlert("Error al iniciar sesion", e.ToString(), "OK");
            }

        }



        public ICommand DatosSesioncommand => new Command( () =>  datosSesion());
    }
}

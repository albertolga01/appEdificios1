using AppEdificiosP.Models;
using AppEdificiosP.ViewModels;
using AppEdificiosP.Views;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppEdificiosP
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(PaginaPrincipal), typeof(PaginaPrincipal));
           
            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            GoToAsync("//LoginPage");
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Desea Cerrar Sesión?", "Desea Cerrar Sesión", "Si", "No");
            if (answer == true)
            {
                Preferences.Clear();
              
                await Shell.Current.GoToAsync("//LoginPage");
                await Navigation.PushAsync(new LoginPage());
            }
        }
    }
}

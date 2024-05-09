using AppEdificiosP.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppEdificiosP.Views
{
    public partial class PaginaPrincipal : ContentPage
    {
        public PaginaPrincipal()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();            
            _ = ((PaginaPrincipalViewModel)BindingContext).datosSesion();
            _ = ((PaginaPrincipalViewModel)BindingContext).lecturas(RecibosList);
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
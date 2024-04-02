using AppEdificiosP.Models;
using AppEdificiosP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppEdificiosP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        //LoginViewModel viewModel;
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //_ = viewModel.datosSesion();
            // Llamamos a la función en el ViewModel
            _ = ((LoginViewModel)BindingContext).datosSesion();
        }
    }
}
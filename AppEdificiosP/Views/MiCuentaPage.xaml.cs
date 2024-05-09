using AppEdificiosP.Models;
using AppEdificiosP.ViewModels;
using AppEdificiosP.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppEdificiosP.Views
{
    public partial class MiCuentaPage : ContentPage
    {
        MiCuentaViewModel _viewModel;

        public MiCuentaPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new MiCuentaViewModel();
            var list = new DashboardModel().GetBannerList();
            cvBanner.ItemsSource = list;
            Device.StartTimer(TimeSpan.FromSeconds(5), (Func<bool>)(() =>
            {

                cvBanner.Position = (cvBanner.Position + 1) % list.Count;

                return true;
            }));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ = ((MiCuentaViewModel)BindingContext).datosCliente();

        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }



    }
}
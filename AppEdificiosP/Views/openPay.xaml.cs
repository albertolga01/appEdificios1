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
    public partial class openPay : ContentPage
    {
        openPayViewModel _viewModel;

        public openPay()
        {
            InitializeComponent();
            BindingContext = _viewModel = new openPayViewModel();
          
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
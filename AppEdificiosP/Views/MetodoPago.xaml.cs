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
    public partial class MetodoPago : ContentPage
    {
        PaginaPrincipalViewModel _viewModel;
        public MetodoPago()
        {
            InitializeComponent();
            BindingContext = _viewModel = new PaginaPrincipalViewModel();
        }
    }
}
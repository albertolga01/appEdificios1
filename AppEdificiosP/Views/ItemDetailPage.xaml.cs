using AppEdificiosP.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace AppEdificiosP.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
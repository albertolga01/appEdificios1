using AppEdificiosP.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppEdificiosP.ViewModels
{
    public class MetodoPagosViewModel : BaseViewModel
    {
        public MetodoPagosViewModel()
        {
            Title = "Metodos de Pagos";

           
        }
        public async Task vistaOpenPay()
        {
            await Shell.Current.GoToAsync($"//{nameof(openPay)}");
        }
        public async Task atras()
        {
            await Shell.Current.GoToAsync($"//{nameof(PaginaPrincipal)}");
        }

        public ICommand vistaOpenpaycommand => new Command(async () => await vistaOpenPay());
        public ICommand BackCommand => new Command(async () => await atras());
    }
}

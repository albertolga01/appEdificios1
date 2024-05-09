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
    public partial class AvisoPrivacidadPage : ContentPage
    {
        public AvisoPrivacidadPage()
        {
            Title = "Aviso de Privacidad";
            InitializeComponent();
        }
    }
}
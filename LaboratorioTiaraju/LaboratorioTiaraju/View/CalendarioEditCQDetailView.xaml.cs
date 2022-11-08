using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace LaboratorioTiaraju.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarioEditCQDetailView : ContentPage
    {
        public CalendarioEditCQDetailView()
        {
            InitializeComponent();

            //datePicker.Date = DateTime.Now;
        }
    }
}
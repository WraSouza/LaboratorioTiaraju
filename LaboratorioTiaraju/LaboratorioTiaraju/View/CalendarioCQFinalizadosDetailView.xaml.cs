using LaboratorioTiaraju.FirebaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LaboratorioTiaraju.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarioCQFinalizadosDetailView : ContentPage
    {
        public CalendarioCQFinalizadosDetailView()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            var dia = Preferences.Get("DiaCalendario", "default_value");
            var mes = Preferences.Get("MesCalendario", "default_value");
            var descricao = Preferences.Get("DescricaoCalendario", "default_value");
            CalendarioCQServices calendarios = new CalendarioCQServices();
            collectionView.ItemsSource = await calendarios.RetornaCalendarioFinalizadoEspecifico(dia, mes, descricao);
        }
    }
}
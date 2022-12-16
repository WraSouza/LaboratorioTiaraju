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
    public partial class CalendarioGQVisitasDetailView : ContentPage
    {
        public CalendarioGQVisitasDetailView()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            var mes = Preferences.Get("MesCalendario", "default_value");
            int dia = Preferences.Get("DiaCalendario", 0);


            var descricao = Preferences.Get("DescricaoCalendario", "default_value");
            CalendarioGQServices calendarios = new CalendarioGQServices();
            collectionView.ItemsSource = await calendarios.RetornaCalendarioVisitasEspecifico(dia, mes, descricao);

            var dadosEvento = await calendarios.RetornaCalendarioVisitasEspecifico(dia, mes, descricao);

            //foreach (var itens in dadosEvento)
            //{
            //    // TextoEdicao.Text = itens.Descricao;
            //}


        }
    }
}
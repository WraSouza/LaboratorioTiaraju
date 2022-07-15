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
    public partial class CalendarioCQDetailView : ContentPage
    {
        public CalendarioCQDetailView()
        {
            InitializeComponent();

            datePicker.Date = DateTime.Now;
        }

        protected async override void OnAppearing()
        {
            var mes = Preferences.Get("MesCalendario", "default_value");
            int dia = Preferences.Get("DiaCalendario", 0);  

            
            var descricao = Preferences.Get("DescricaoCalendario", "default_value");            
            CalendarioCQServices calendarios = new CalendarioCQServices();
            collectionView.ItemsSource = await calendarios.RetornaCalendarioEspecifico(dia,mes,descricao);

            var dadosEvento = await calendarios.RetornaCalendarioEspecifico(dia, mes, descricao);

            foreach(var itens in dadosEvento)
            {
                TextoEdicao.Text = itens.Descricao;
            }

            
        }
    }
}
using LaboratorioTiaraju.FirebaseServices;
using LaboratorioTiaraju.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LaboratorioTiaraju.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarioCQView : ContentPage
    {
        //List<int> todosDias = new List<int>();
        public CalendarioCQView()
        {
            InitializeComponent();           
        }        
       

        //protected async override void OnAppearing()
        //{
        //    DateTime data = DateTime.Today;
        //    DateTime ultimoDiaDoMes = new DateTime(data.Year, data.Month, DateTime.DaysInMonth(data.Year, data.Month));
        //    int ultimoDia = ultimoDiaDoMes.Day;

        //    CalendarioCQServices calendario = new CalendarioCQServices();
        //    var lista = await calendario.RetornaInformacoes();
            

        //    collectionview.ItemsSource = await calendario.RetornaCalendarioAbril();
        //}
    }

    //public class CalendariosCQ : List<CalendarioCQ>
    //{
    //    public string Title { get; set; }

    //    public CalendariosCQ(string title)
    //    {
    //        Title = title;
    //    }
    //}
}
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
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(GLPIView),typeof(GLPIView));

            Routing.RegisterRoute(nameof(EnviaImagemView), typeof(EnviaImagemView));

            Routing.RegisterRoute(nameof(RHView), typeof(RHView));

            Routing.RegisterRoute(nameof(VisualizaImagemView), typeof(VisualizaImagemView));

            Routing.RegisterRoute(nameof(CalendarioCQView), typeof(CalendarioCQView));

            Routing.RegisterRoute(nameof(CadastroCalendarioCQView), typeof(CadastroCalendarioCQView));

            Routing.RegisterRoute(nameof(CalendarioCQDetailView), typeof(CalendarioCQDetailView));            

            Routing.RegisterRoute(nameof(CalendarioCQExcluidos), typeof(CalendarioCQExcluidos));

            Routing.RegisterRoute(nameof(CalendarioCQTabbedView), typeof(CalendarioCQTabbedView));

            Routing.RegisterRoute(nameof(BibliotecaView), typeof(BibliotecaView));

            Routing.RegisterRoute(nameof(TIView), typeof(TIView));

            Routing.RegisterRoute(nameof(GQTabbedView), typeof(GQTabbedView));

            Routing.RegisterRoute(nameof(SalaReunioesView), typeof(SalaReunioesView));

            Routing.RegisterRoute(nameof(CalendarioCQExcluidosDetailView), typeof(CalendarioCQExcluidosDetailView));

            Routing.RegisterRoute(nameof(CalendarioCQFinalizadosDetailView), typeof(CalendarioCQFinalizadosDetailView));
        }
    }
}
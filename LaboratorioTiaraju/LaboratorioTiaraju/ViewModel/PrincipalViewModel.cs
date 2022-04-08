using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LaboratorioTiaraju.ViewModel
{
    internal class PrincipalViewModel
    {
        public Command OpenImagemCardapio { get; set; }
        public Command OpenCalendarioCQ { get; set; }
        public Command OpenRHInforma { get; set; }
        public Command OpenGLPI { get; set; }
        public Command OpenRH {  get; set; }
        public INavigation Navigation { get; set; }

        public PrincipalViewModel()
        {
           
        }

        public PrincipalViewModel(INavigation navigation)
        {
            OpenGLPI = new Command(async () => await OpenGLPIView());

            OpenRH = new Command(async () => await OpenRHView());

            OpenImagemCardapio = new Command(async () => await OpenImagemCardapioView());

            OpenRHInforma = new Command(async () => await OpenRHInformaView());

            OpenCalendarioCQ = new Command(async () => await OpenCalendarioCQView());
        }

        private async Task OpenCalendarioCQView()
        {
            var route = $"{nameof(View.TabbedPageCalendarioCQ)}";
            await Shell.Current.GoToAsync(route);
        }

        private async Task OpenRHInformaView()
        {
            const string rhinforma = "RHInforma";
            Preferences.Set("Imagem", rhinforma);
            var route = $"{nameof(View.VisualizaImagemView)}";
            await Shell.Current.GoToAsync(route);
        }

        private async Task OpenImagemCardapioView()
        {
            const string cardapio = "Cardapio";
            Preferences.Set("Imagem", cardapio);
            var route = $"{nameof(View.VisualizaImagemView)}";
            await Shell.Current.GoToAsync(route);
        }

        private async Task OpenGLPIView()
        {            
            var route = $"{nameof(View.GLPIView)}";
            await Shell.Current.GoToAsync(route);
        }

        private async Task OpenRHView()
        {
            var route = $"{nameof(View.RHView)}";
            await Shell.Current.GoToAsync(route);
        }

        private async Task OpenEnviaImagemView()
        {           
            var route = $"{nameof(View.EnviaImagemView)}";
            await Shell.Current.GoToAsync(route);
        }

    }
}

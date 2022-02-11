using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LaboratorioTiaraju.ViewModel
{
    internal class PrincipalViewModel
    {
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
        }

        private async Task OpenGLPIView()
        {
            //await Navigation.PushModalAsync(new View.GLPIView());
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

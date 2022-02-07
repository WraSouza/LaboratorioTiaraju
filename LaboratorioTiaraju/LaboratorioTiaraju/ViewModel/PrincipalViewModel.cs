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
        public INavigation Navigation { get; set; }

        public PrincipalViewModel()
        {
           
        }

        public PrincipalViewModel(INavigation navigation)
        {
            OpenGLPI = new Command(async () => await OpenGLPIView());
        }

        private async Task OpenGLPIView()
        {
            //await Navigation.PushModalAsync(new View.GLPIView());
            var route = $"{nameof(View.GLPIView)}";
            await Shell.Current.GoToAsync(route);
        }


    }
}

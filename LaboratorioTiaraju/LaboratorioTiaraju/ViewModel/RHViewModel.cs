using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LaboratorioTiaraju.ViewModel
{
    internal class RHViewModel : BaseViewModel
    {
        
        public Command OpenEnviaImagem { get; set; }

        public RHViewModel()
        {
            OpenEnviaImagem = new Command(async () => await OpenEnviaImagemIView());
        }

        private async Task OpenEnviaImagemIView()
        {
            var route = $"{nameof(View.EnviaImagemView)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}

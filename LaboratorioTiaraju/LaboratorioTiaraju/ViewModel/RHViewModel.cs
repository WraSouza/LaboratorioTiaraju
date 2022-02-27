using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LaboratorioTiaraju.ViewModel
{
    internal class RHViewModel : BaseViewModel
    {
        
        public Command OpenEnviaImagemCardapio { get; set; }
        public Command OpenEnviaImagemRHInforma { get; set; }

        public RHViewModel()
        {
            OpenEnviaImagemCardapio = new Command(async () => await OpenEnviaImagemCardapioView());
            OpenEnviaImagemRHInforma = new Command(async () => await OpenEnviaImagemRHInformarView());
        }

        private async Task OpenEnviaImagemRHInformarView()
        {
            const string rhInforma = "RHInforma";
            Preferences.Set("Imagem", rhInforma);
            var route = $"{nameof(View.EnviaImagemView)}";
            await Shell.Current.GoToAsync(route);
        }

        private async Task OpenEnviaImagemCardapioView()
        {
            const string cardapio = "Cardapio";
            Preferences.Set("Imagem", cardapio);
            var route = $"{nameof(View.EnviaImagemView)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}

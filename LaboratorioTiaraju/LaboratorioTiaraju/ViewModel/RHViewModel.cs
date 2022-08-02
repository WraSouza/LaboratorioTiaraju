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
        public Command OpenEnviaImagemInformativoCovid { get; set; }
        public Command OpenEnviaImagemPautaFixa { get; set; }
        public Command OpenEnviaImagemBonusTiaraju { get; set; }
        public Command OpenEnviaImagemTempoEmpresa { get; set; }
        public Command OpenEnviaImagemDiaT { get; set; }

        public RHViewModel()
        {
            OpenEnviaImagemCardapio = new Command(async () => await OpenEnviaImagemCardapioView());
            OpenEnviaImagemRHInforma = new Command(async () => await OpenEnviaImagemRHInformaView());
            OpenEnviaImagemInformativoCovid = new Command(async () => await OpenEnviaImagemInformativoCovidView());
            OpenEnviaImagemPautaFixa = new Command(async () => await OpenEnviaImagemPautaFixaView());
            OpenEnviaImagemBonusTiaraju = new Command(async () => await OpenEnviaImagemBonusTiarajuView());
            OpenEnviaImagemTempoEmpresa = new Command(async () => await OpenEnviaImagemTempoEmpresaView());
            OpenEnviaImagemDiaT = new Command(async () => await OpenEnviaImagemDiaTView());
        }

        private async Task OpenEnviaImagemDiaTView()
        {
            const string diaT = "DiaT";
            Preferences.Set("Imagem", diaT);
            var route = $"{nameof(View.EnviaImagemView)}";
            await Shell.Current.GoToAsync(route);
        }

        private async Task OpenEnviaImagemTempoEmpresaView()
        {
            const string tempoEmpresa = "TempoEmpresa";
            Preferences.Set("Imagem", tempoEmpresa);
            var route = $"{nameof(View.EnviaImagemView)}";
            await Shell.Current.GoToAsync(route);
        }

        private async Task OpenEnviaImagemBonusTiarajuView()
        {
            const string bonusTiaraju = "BonusTiaraju";
            Preferences.Set("Imagem", bonusTiaraju);
            var route = $"{nameof(View.EnviaImagemView)}";
            await Shell.Current.GoToAsync(route);
        }

        private async Task OpenEnviaImagemInformativoCovidView()
        {
            const string informativoCovid = "InformativoCovid";
            Preferences.Set("Imagem", informativoCovid);
            var route = $"{nameof(View.EnviaImagemView)}";
            await Shell.Current.GoToAsync(route);
        }

        private async Task OpenEnviaImagemPautaFixaView()
        {
            const string pautaFixa = "PautaFixa";
            Preferences.Set("Imagem", pautaFixa);
            var route = $"{nameof(View.EnviaImagemView)}";
            await Shell.Current.GoToAsync(route);
        }

        private async Task OpenEnviaImagemRHInformaView()
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

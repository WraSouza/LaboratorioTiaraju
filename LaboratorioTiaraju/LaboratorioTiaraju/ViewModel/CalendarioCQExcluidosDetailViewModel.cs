using LaboratorioTiaraju.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LaboratorioTiaraju.ViewModel
{
    internal class CalendarioCQExcluidosDetailViewModel : BaseViewModel
    {
        public Command IrParaCalendarioCQExcluidosDetailView { get; set; }

        public CalendarioCQExcluidosDetailViewModel()
        {
            IrParaCalendarioCQExcluidosDetailView = new Command<CalendarioCQ>((model) => AbrirCalendarioExcluidosDetailView(model));
        }

        private async void AbrirCalendarioExcluidosDetailView(CalendarioCQ model)
        {
            if (model is null)
            {
                return;
            }

            Preferences.Set("DiaCalendario", model.Dia);
            Preferences.Set("MesCalendario", model.Mes);
            Preferences.Set("DescricaoCalendario", model.Descricao);
            var route = $"{nameof(View.CalendarioCQExcluidosDetailView)}";
            await Shell.Current.GoToAsync(route);
        }
    }
}

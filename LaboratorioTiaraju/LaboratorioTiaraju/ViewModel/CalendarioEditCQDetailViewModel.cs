using LaboratorioTiaraju.FirebaseServices;
using LaboratorioTiaraju.Model;
using LaboratorioTiaraju.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using Xamarin.Essentials;

namespace LaboratorioTiaraju.ViewModel
{
    internal class CalendarioEditCQDetailViewModel : BaseViewModel
    {
        public ObservableCollection<CalendarioCQ> Calendarios { get; private set; } = new ObservableCollection<CalendarioCQ>();

        public CalendarioEditCQDetailViewModel()
        {
            BuscaAtividade();
        }

        private async void BuscaAtividade()
        {
            bool verificaConexao = Conectividade.VerificaConectividade();

            if (verificaConexao)
            {
                var dia = Preferences.Get("DiaCalendario", 0);
                var mes = Preferences.Get("MesCalendario", "default_value");
                var descricao = Preferences.Get("DescricaoCalendario", "default_value");

                CalendarioCQServices dados = new CalendarioCQServices();
                var dadosCalendario = await dados.RetornaCalendarioEspecifico(dia, mes, descricao);
                ObservableCollection<CalendarioCQ> novoCalendarioJaneiro = new ObservableCollection<CalendarioCQ>();

                foreach (var c in dadosCalendario)
                {
                    Calendarios.Add(c);
                    //yield return c;
                }
            }
            else
            {
                Mensagem.MensagemErroConexao();
            }
        }
    }
}

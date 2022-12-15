using LaboratorioTiaraju.FirebaseServices;
using LaboratorioTiaraju.Model;
using LaboratorioTiaraju.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LaboratorioTiaraju.ViewModel
{
    internal class CalendarioEditCQDetailViewModel : BaseViewModel
    {
        public ObservableCollection<CalendarioCQ> Calendarios { get; private set; } = new ObservableCollection<CalendarioCQ>();
        private string _mes;
        private DateTime _dataColeta;
        private string _observacao;

        public Command AtualizarCalendario { get; set; }

        public CalendarioEditCQDetailViewModel()
        {
            BuscaAtividade();
            AtualizarCalendario = new Command(async () => await AtualizarCalendarioCommand());
        }

        public DateTime DataColeta
        {
            get => _dataColeta;
            set
            {
                _dataColeta = value;
                OnPropertyChanged();
            }
        }

        public string Descricao
        {
            get => _observacao;
            set
            {
                _observacao = value;
                OnPropertyChanged();
            }
        }

        private async Task AtualizarCalendarioCommand()
        {
            bool verificaConexao = Conectividade.VerificaConectividade();

            bool verificaData = DataHora.VerificaData(DataColeta);

            if (verificaConexao)
            {
                if (verificaData)
                {
                    CalendarioCQServices calendarioServices = new CalendarioCQServices();
                    string descricao = Descricao;
                    int dia = DataColeta.Day;
                    _mes = DataColeta.ToString("MMMM").ToUpper();

                    bool confirmaStatusAlterado = await calendarioServices.AtualizaDadosCalendario(dia, _mes, descricao);

                    if (confirmaStatusAlterado)
                    {
                        await Application.Current.MainPage.DisplayAlert("Sucesso", "Evento Atualizado Com Sucesso", "OK");
                    }
                }
                else
                {
                    Mensagem.MensagemDataDeveSerMaior();
                }
            }
            else
            {
                Mensagem.MensagemErroConexao();
            }
        }

        private async void BuscaAtividade()
        {
            bool verificaConexao = Conectividade.VerificaConectividade();

            if (verificaConexao)
            {
                var dia = Preferences.Get("DiaCalendario", 0);
                var mes = Preferences.Get("MesCalendario", "default_value");
                var descricao = Preferences.Get("DescricaoCalendario", "default_value");

                Descricao = descricao;

                CalendarioCQServices dados = new CalendarioCQServices();
                var dadosCalendario = await dados.RetornaCalendarioEspecifico(dia, mes, descricao);
                ObservableCollection<CalendarioCQ> novoCalendarioJaneiro = new ObservableCollection<CalendarioCQ>();

                //foreach (var c in dadosCalendario)
                //{
                //    Calendarios.Add(c);                    
                //}
            }
            else
            {
                Mensagem.MensagemErroConexao();
            }
        }
    }
}

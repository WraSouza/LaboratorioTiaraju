using LaboratorioTiaraju.FirebaseServices;
using LaboratorioTiaraju.Model;
using LaboratorioTiaraju.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LaboratorioTiaraju.ViewModel
{
    internal class CalendarioCQViewModel : BaseViewModel
    {
        public ObservableCollection<CalendarioGroup> Calendarios { get; private set; } = new ObservableCollection<CalendarioGroup>();

        public DateTime _year;
        public Command AtualizaTela { get; }
        public Command IrParaCadastroCalendarioView { get; set; }
        public Command IrParaCalendarioCQDetailView { get; set; }
        public Command BuscarCalendariosAno { get; set; }

        public DateTime AnoDesejado
        {
            get => _year;
            set
            {
                _year = value;
                OnPropertyChanged();
            }
        }

        public CalendarioCQViewModel()
        {
            BuscaCalendario();            
            AtualizaTela = new Command(AtualizarTela);
            BuscarCalendariosAno = new Command(() => BuscaCalendarioAno());
            IrParaCadastroCalendarioView = new Command(async () => await AbrirCadastroCalendarioView());
            IrParaCalendarioCQDetailView = new Command<CalendarioCQ>( (model) => AbrirCalendarioDetailView(model));
        }

        private async void AbrirCalendarioDetailView(CalendarioCQ model)
        {
            if (model is null)
            {
                return;
            }

            Preferences.Set("DiaCalendario", model.Dia);
            Preferences.Set("MesCalendario", model.Mes);
            Preferences.Set("DescricaoCalendario", model.Descricao);
            Preferences.Set("StatusFinalizado", model.IsFinished);
            Preferences.Set("StatusExcluido", model.IsExcluded);
            var route = $"{nameof(View.CalendarioCQDetailView)}";
            await Shell.Current.GoToAsync(route);
        }

        private async Task AbrirCadastroCalendarioView()
        {
            var route = $"{nameof(View.CadastroCalendarioCQView)}";
            await Shell.Current.GoToAsync(route);
        }

        bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        void AtualizarTela()
        {
            bool verificaConexao = Conectividade.VerificaConectividade();

            if (verificaConexao)
            {
                Calendarios.Clear();
                BuscaCalendario();


                IsRefreshing = false;
            }
            else
            {
                Mensagem.MensagemErroConexao();               

                IsRefreshing = false;
            }
            

        }

        async void BuscaCalendario()
        {
            bool verificaConexao = Conectividade.VerificaConectividade();

            if (verificaConexao)
            {
                CalendarioCQServices dados = new CalendarioCQServices();
                var dadosCalendario = await dados.RetornaCalendariosNaoFinalizados();
                ObservableCollection<CalendarioCQ> novoCalendarioJaneiro = new ObservableCollection<CalendarioCQ>();
                ObservableCollection<CalendarioCQ> novoCalendarioFevereiro = new ObservableCollection<CalendarioCQ>();
                ObservableCollection<CalendarioCQ> novoCalendarioMarco = new ObservableCollection<CalendarioCQ>();
                ObservableCollection<CalendarioCQ> novoCalendarioAbril = new ObservableCollection<CalendarioCQ>();
                ObservableCollection<CalendarioCQ> novoCalendarioMaio = new ObservableCollection<CalendarioCQ>();
                ObservableCollection<CalendarioCQ> novoCalendarioJunho = new ObservableCollection<CalendarioCQ>();
                ObservableCollection<CalendarioCQ> novoCalendarioJulho = new ObservableCollection<CalendarioCQ>();
                ObservableCollection<CalendarioCQ> novoCalendarioAgosto = new ObservableCollection<CalendarioCQ>();
                ObservableCollection<CalendarioCQ> novoCalendarioSetembro = new ObservableCollection<CalendarioCQ>();
                ObservableCollection<CalendarioCQ> novoCalendarioOutubro = new ObservableCollection<CalendarioCQ>();
                ObservableCollection<CalendarioCQ> novoCalendarioNovembro = new ObservableCollection<CalendarioCQ>();
                ObservableCollection<CalendarioCQ> novoCalendarioDezembro = new ObservableCollection<CalendarioCQ>();

                foreach (var item in dadosCalendario.OrderByDescending(x => x.Dia).Reverse())
                {
                    switch (item.Mes)
                    {
                        case "JANEIRO":
                            novoCalendarioJaneiro.Add(item);
                            break;

                        case "FEVEREIRO":
                            novoCalendarioFevereiro.Add(item);
                            break;

                        case "MARÇO":
                            novoCalendarioMarco.Add(item);
                            break;

                        case "ABRIL":
                            novoCalendarioAbril.Add(item);
                            break;

                        case "MAIO":
                            novoCalendarioMaio.Add(item);
                            break;

                        case "JUNHO":
                            novoCalendarioJunho.Add(item);
                            break;

                        case "JULHO":
                            novoCalendarioJulho.Add(item);
                            break;

                        case "AGOSTO":
                            novoCalendarioAgosto.Add(item);
                            break;

                        case "SETEMBRO":
                            novoCalendarioSetembro.Add(item);
                            break;

                        case "OUTUBRO":
                            novoCalendarioOutubro.Add(item);
                            break;

                        case "NOVEMBRO":
                            novoCalendarioNovembro.Add(item);
                            break;

                        default:
                            novoCalendarioDezembro.Add(item);
                            break;

                    }

                }

                if (novoCalendarioJaneiro.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroup("Janeiro", novoCalendarioJaneiro));
                }

                if (novoCalendarioFevereiro.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroup("Fevereiro", novoCalendarioFevereiro));
                }

                if (novoCalendarioMarco.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroup("Março", novoCalendarioMarco));
                }

                if (novoCalendarioAbril.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroup("Abril", novoCalendarioAbril));
                }

                if (novoCalendarioMaio.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroup("Maio", novoCalendarioMaio));
                }

                if (novoCalendarioJunho.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroup("Junho", novoCalendarioJunho));
                }

                if (novoCalendarioJulho.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroup("Julho", novoCalendarioJulho));
                }

                if (novoCalendarioAgosto.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroup("Agosto", novoCalendarioAgosto));
                }

                if (novoCalendarioSetembro.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroup("Setembro", novoCalendarioSetembro));
                }

                if (novoCalendarioOutubro.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroup("Outubro", novoCalendarioOutubro));
                }

                if (novoCalendarioNovembro.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroup("Novembro", novoCalendarioNovembro));
                }

                if (novoCalendarioDezembro.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroup("Dezembro", novoCalendarioDezembro));
                }
            }
            else
            {
                Mensagem.MensagemErroConexao();
                // await Application.Current.MainPage.DisplayAlert("Erro", "Verifique Sua Conexão de Internet.", "OK");
            }



        }

        //Buscando calendário do ano selecionado
        async void BuscaCalendarioAno()
        {
            bool verificaConexao = Conectividade.VerificaConectividade();

            if (verificaConexao)
            {
                Calendarios.Clear();
                CalendarioCQServices dados = new CalendarioCQServices();
                var dadosCalendario = await dados.RetornaCalendariosNaoFinalizadosAno(AnoDesejado.Year);
                ObservableCollection<CalendarioCQ> novoCalendarioJaneiro = new ObservableCollection<CalendarioCQ>();
                ObservableCollection<CalendarioCQ> novoCalendarioFevereiro = new ObservableCollection<CalendarioCQ>();
                ObservableCollection<CalendarioCQ> novoCalendarioMarco = new ObservableCollection<CalendarioCQ>();
                ObservableCollection<CalendarioCQ> novoCalendarioAbril = new ObservableCollection<CalendarioCQ>();
                ObservableCollection<CalendarioCQ> novoCalendarioMaio = new ObservableCollection<CalendarioCQ>();
                ObservableCollection<CalendarioCQ> novoCalendarioJunho = new ObservableCollection<CalendarioCQ>();
                ObservableCollection<CalendarioCQ> novoCalendarioJulho = new ObservableCollection<CalendarioCQ>();
                ObservableCollection<CalendarioCQ> novoCalendarioAgosto = new ObservableCollection<CalendarioCQ>();
                ObservableCollection<CalendarioCQ> novoCalendarioSetembro = new ObservableCollection<CalendarioCQ>();
                ObservableCollection<CalendarioCQ> novoCalendarioOutubro = new ObservableCollection<CalendarioCQ>();
                ObservableCollection<CalendarioCQ> novoCalendarioNovembro = new ObservableCollection<CalendarioCQ>();
                ObservableCollection<CalendarioCQ> novoCalendarioDezembro = new ObservableCollection<CalendarioCQ>();

                foreach (var item in dadosCalendario.OrderByDescending(x => x.Dia).Reverse())
                {
                    switch (item.Mes)
                    {
                        case "JANEIRO":
                            novoCalendarioJaneiro.Add(item);
                            break;

                        case "FEVEREIRO":
                            novoCalendarioFevereiro.Add(item);
                            break;

                        case "MARÇO":
                            novoCalendarioMarco.Add(item);
                            break;

                        case "ABRIL":
                            novoCalendarioAbril.Add(item);
                            break;

                        case "MAIO":
                            novoCalendarioMaio.Add(item);
                            break;

                        case "JUNHO":
                            novoCalendarioJunho.Add(item);
                            break;

                        case "JULHO":
                            novoCalendarioJulho.Add(item);
                            break;

                        case "AGOSTO":
                            novoCalendarioAgosto.Add(item);
                            break;

                        case "SETEMBRO":
                            novoCalendarioSetembro.Add(item);
                            break;

                        case "OUTUBRO":
                            novoCalendarioOutubro.Add(item);
                            break;

                        case "NOVEMBRO":
                            novoCalendarioNovembro.Add(item);
                            break;

                        default:
                            novoCalendarioDezembro.Add(item);
                            break;

                    }

                }

                if (novoCalendarioJaneiro.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroup("Janeiro", novoCalendarioJaneiro));
                }

                if (novoCalendarioFevereiro.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroup("Fevereiro", novoCalendarioFevereiro));
                }

                if (novoCalendarioMarco.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroup("Março", novoCalendarioMarco));
                }

                if (novoCalendarioAbril.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroup("Abril", novoCalendarioAbril));
                }

                if (novoCalendarioMaio.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroup("Maio", novoCalendarioMaio));
                }

                if (novoCalendarioJunho.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroup("Junho", novoCalendarioJunho));
                }

                if (novoCalendarioJulho.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroup("Julho", novoCalendarioJulho));
                }

                if (novoCalendarioAgosto.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroup("Agosto", novoCalendarioAgosto));
                }

                if (novoCalendarioSetembro.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroup("Setembro", novoCalendarioSetembro));
                }

                if (novoCalendarioOutubro.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroup("Outubro", novoCalendarioOutubro));
                }

                if (novoCalendarioNovembro.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroup("Novembro", novoCalendarioNovembro));
                }

                if (novoCalendarioDezembro.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroup("Dezembro", novoCalendarioDezembro));
                }
            }
            else
            {
                Mensagem.MensagemErroConexao();
                
            }
        }
    }
}

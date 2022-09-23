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
    internal class CalendarioGQVisitasViewModel : BaseViewModel
    {
        public ObservableCollection<CalendarioGroupGQ> Calendarios { get; private set; } = new ObservableCollection<CalendarioGroupGQ>();

        public Command AtualizaTela { get; }
        public Command IrParaCadastroCalendarioGQVisitasView { get; set; }
        

        public CalendarioGQVisitasViewModel()
        {
            BuscaCalendario();
            AtualizaTela = new Command(AtualizarTela);
            IrParaCadastroCalendarioGQVisitasView = new Command(async () => await AbrirCadastroCalendarioView());
            
        }

        private async void AbrirCalendarioDetailView(CalendarioVisitasGQ model)
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
            var route = $"{nameof(View.CadastroCalendarioVisitaGQView)}";
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

        async void AtualizarTela()
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
                await Application.Current.MainPage.DisplayAlert("Erro", "Verifique Sua Conexão de Internet.", "OK");

                IsRefreshing = false;
            }


        }

        async void BuscaCalendario()
        {
            bool verificaConexao = Conectividade.VerificaConectividade();

            if (verificaConexao)
            {
                CalendarioGQServices dados = new CalendarioGQServices();
                var dadosCalendario = await dados.RetornaCalendariosNaoFinalizados();
                ObservableCollection<CalendarioVisitasGQ> novoCalendarioJaneiro = new ObservableCollection<CalendarioVisitasGQ>();
                ObservableCollection<CalendarioVisitasGQ> novoCalendarioFevereiro = new ObservableCollection<CalendarioVisitasGQ>();
                ObservableCollection<CalendarioVisitasGQ> novoCalendarioMarco = new ObservableCollection<CalendarioVisitasGQ>();
                ObservableCollection<CalendarioVisitasGQ> novoCalendarioAbril = new ObservableCollection<CalendarioVisitasGQ>();
                ObservableCollection<CalendarioVisitasGQ> novoCalendarioMaio = new ObservableCollection<CalendarioVisitasGQ>();
                ObservableCollection<CalendarioVisitasGQ> novoCalendarioJunho = new ObservableCollection<CalendarioVisitasGQ>();
                ObservableCollection<CalendarioVisitasGQ> novoCalendarioJulho = new ObservableCollection<CalendarioVisitasGQ>();
                ObservableCollection<CalendarioVisitasGQ> novoCalendarioAgosto = new ObservableCollection<CalendarioVisitasGQ>();
                ObservableCollection<CalendarioVisitasGQ> novoCalendarioSetembro = new ObservableCollection<CalendarioVisitasGQ>();
                ObservableCollection<CalendarioVisitasGQ> novoCalendarioOutubro = new ObservableCollection<CalendarioVisitasGQ>();
                ObservableCollection<CalendarioVisitasGQ> novoCalendarioNovembro = new ObservableCollection<CalendarioVisitasGQ>();
                ObservableCollection<CalendarioVisitasGQ> novoCalendarioDezembro = new ObservableCollection<CalendarioVisitasGQ>();

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
                    Calendarios.Add(new CalendarioGroupGQ("Janeiro", novoCalendarioJaneiro));
                }

                if (novoCalendarioFevereiro.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroupGQ("Fevereiro", novoCalendarioFevereiro));
                }

                if (novoCalendarioMarco.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroupGQ("Março", novoCalendarioMarco));
                }

                if (novoCalendarioAbril.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroupGQ("Abril", novoCalendarioAbril));
                }

                if (novoCalendarioMaio.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroupGQ("Maio", novoCalendarioMaio));
                }

                if (novoCalendarioJunho.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroupGQ("Junho", novoCalendarioJunho));
                }

                if (novoCalendarioJulho.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroupGQ("Julho", novoCalendarioJulho));
                }

                if (novoCalendarioAgosto.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroupGQ("Agosto", novoCalendarioAgosto));
                }

                if (novoCalendarioSetembro.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroupGQ("Setembro", novoCalendarioSetembro));
                }

                if (novoCalendarioOutubro.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroupGQ("Outubro", novoCalendarioOutubro));
                }

                if (novoCalendarioNovembro.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroupGQ("Novembro", novoCalendarioNovembro));
                }

                if (novoCalendarioDezembro.Count > 0)
                {
                    Calendarios.Add(new CalendarioGroupGQ("Dezembro", novoCalendarioDezembro));
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Verifique Sua Conexão de Internet.", "OK");
            }



        }
    }
}

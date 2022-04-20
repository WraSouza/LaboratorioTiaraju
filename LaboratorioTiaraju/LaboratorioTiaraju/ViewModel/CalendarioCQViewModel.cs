using LaboratorioTiaraju.FirebaseServices;
using LaboratorioTiaraju.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace LaboratorioTiaraju.ViewModel
{
    internal class CalendarioCQViewModel : BaseViewModel
    {
        public ObservableCollection<CalendarioGroup> Calendarios { get; private set; } = new ObservableCollection<CalendarioGroup>();

        public CalendarioCQViewModel()
        {
            BuscaCalendario();
        }

        async void BuscaCalendario()
        {
            CalendarioCQServices dados = new CalendarioCQServices();
            var dadosCalendario = await dados.RetornaInformacoes();
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

            foreach (var item in dadosCalendario)
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

                    default :
                        novoCalendarioDezembro.Add(item);
                        break;

                }
                //if (item.Mes == "ABRIL")
                //{
                //    novoCalendarioAbril.Add(item);
                //}else if( item.Mes == "MAIO")
                //{
                //    novoCalendarioMaio.Add(item);
                //}
            }


            Calendarios.Add(new CalendarioGroup("Janeiro", novoCalendarioJaneiro));

            Calendarios.Add(new CalendarioGroup("Fevereiro", novoCalendarioFevereiro));

            Calendarios.Add(new CalendarioGroup("Março", novoCalendarioMarco));

            Calendarios.Add(new CalendarioGroup("Abril", novoCalendarioAbril));

            Calendarios.Add(new CalendarioGroup("Maio", novoCalendarioMaio));

            Calendarios.Add(new CalendarioGroup("Junho", novoCalendarioJunho));

            Calendarios.Add(new CalendarioGroup("Julho", novoCalendarioJulho));

            Calendarios.Add(new CalendarioGroup("Agosto", novoCalendarioAgosto));

            Calendarios.Add(new CalendarioGroup("Setembro", novoCalendarioSetembro));

            Calendarios.Add(new CalendarioGroup("Outubro", novoCalendarioOutubro));

            Calendarios.Add(new CalendarioGroup("Novembro", novoCalendarioNovembro));

            Calendarios.Add(new CalendarioGroup("Dezembro", novoCalendarioDezembro));
        }
    }
}

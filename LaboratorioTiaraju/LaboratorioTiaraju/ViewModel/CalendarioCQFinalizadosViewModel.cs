﻿using LaboratorioTiaraju.FirebaseServices;
using LaboratorioTiaraju.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LaboratorioTiaraju.ViewModel
{
    internal class CalendarioCQFinalizadosViewModel : BaseViewModel
    {
        public ObservableCollection<CalendarioGroup> Calendarios { get; private set; } = new ObservableCollection<CalendarioGroup>();

        public Command AtualizarTelaCommand { get; }
        public Command IrParaFinalizadosDetail { get; set; }

        public CalendarioCQFinalizadosViewModel()
        {
            BuscaCalendario();
            AtualizarTelaCommand = new Command(AtualizarTela);
            IrParaFinalizadosDetail = new Command<CalendarioCQ>((model) => AbrirCalendarioFinalizadosExcluidosDetailView(model));
        }

        private async void AbrirCalendarioFinalizadosExcluidosDetailView(CalendarioCQ model)
        {
            if (model is null)
            {
                return;
            }

            Preferences.Set("DiaCalendario", model.Dia);
            Preferences.Set("MesCalendario", model.Mes);
            Preferences.Set("DescricaoCalendario", model.Descricao);
            var route = $"{nameof(View.CalendarioCQFinalizadosDetailView)}";
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
            Calendarios.Clear();
            BuscaCalendario();


            IsRefreshing = false;

        }

        async void BuscaCalendario()
        {
            CalendarioCQServices dados = new CalendarioCQServices();
            var dadosCalendario = await dados.RetornaCalendariosFinalizados();
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
    }
}

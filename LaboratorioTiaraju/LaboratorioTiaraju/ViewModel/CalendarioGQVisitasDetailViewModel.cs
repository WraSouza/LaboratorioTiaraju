using LaboratorioTiaraju.FirebaseServices;
using LaboratorioTiaraju.Services;
using LaboratorioTiaraju.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace LaboratorioTiaraju.ViewModel
{
    internal class CalendarioGQVisitasDetailViewModel : BaseViewModel
    {
        private bool _Result;
        private bool _IsBusy;
        CalendarioGQServices calendarioServices = new CalendarioGQServices();         

        public ObservableCollection<CalendarioVisitasGQ> Calendarios { get; private set; } = new ObservableCollection<CalendarioVisitasGQ>();

        public CalendarioGQVisitasDetailViewModel()
        {
            //BuscaCalendarioEspecifico();
        }

        public bool Result
        {
            get => _IsBusy;
            set
            {
                _IsBusy = value;
                OnPropertyChanged();
            }
        }

        //Método para verificar se o login está sendo realizado para evitar concorrência
        public bool IsBusy
        {
            get => _Result;
            set
            {
                _Result = value;
                OnPropertyChanged();
            }
        }

        //private async Task BuscaCalendarioEspecifico()
        //{
        //    var dados = calendarioServices.
        //}

        private async Task AlterarStatusCalendarioCommand(CalendarioVisitasGQ model)
        {
            bool verificaConexao = Conectividade.VerificaConectividade();

            if (verificaConexao)
            {
                if (IsBusy)
                    return;

                try
                {
                    CalendarioGQServices calendarioServices = new CalendarioGQServices();
                    string descricao = model.Descricao;
                    int dia = model.Dia;
                    string mes = model.Mes;
                    string finalizadoPor = Preferences.Get("Nome", "default_value");
                    string diaFinalizacao = DateTime.Today.ToShortDateString();

                    //TO DO

                    //bool verificaStatusCalendario = await calendarioServices.GetCalendarioCQStatus(dia, mes, descricao);

                    //if (verificaStatusCalendario)
                    //{

                    //    await Application.Current.MainPage.DisplayAlert("Info", "Evento Já Foi Finalizado", "OK");
                    //}
                    //else
                    //{
                    //    //bool confirmaStatusAlterado = await calendarioServices.FinalizarCalendario(dia, mes, descricao, finalizadoPor, diaFinalizacao);

                    //    //if (confirmaStatusAlterado)
                    //    //{
                    //    //    await Application.Current.MainPage.DisplayAlert("Sucesso", "Evento Finalizado Com Sucesso.", "OK");
                    //    //}
                    //}
                }
                catch (Exception e)
                {
                    await Application.Current.MainPage.DisplayAlert("Ops!", e.Message, "OK");
                }
                finally
                {
                    IsBusy = false;
                }
            }
            else
            {
                Mensagem.MensagemErroConexao();
            }

        }
    }
}

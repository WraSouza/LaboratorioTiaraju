using LaboratorioTiaraju.FirebaseServices;
using LaboratorioTiaraju.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LaboratorioTiaraju.ViewModel
{
    internal class CalendarioCQDetailViewModel : BaseViewModel
    {
        private DateTime _dataColeta;
        private string _observacao;
        private string _mes;
        private bool _isFinished = true;

        public Command AlterarStatusCalendario { get; set; }

        public DateTime DataColeta
        {
            get => _dataColeta;
            set
            {
                _dataColeta = value;
                OnPropertyChanged();
            }
        }

        public string Observacao
        {
            get => _observacao;
            set
            {
                _observacao = value;
                OnPropertyChanged();
            }
        }

        public CalendarioCQDetailViewModel()
        {
            AlterarStatusCalendario = new Command<CalendarioCQ>((model) => AlterarStatusCalendarioCommand(model));
        }

        private async Task AlterarStatusCalendarioCommand(CalendarioCQ model)
        {
            CalendarioCQServices calendarioServices = new CalendarioCQServices();
            string descricao = model.Descricao;
            string dia = model.Dia;
            string mes = model.Mes;
            string finalizadoPor = Preferences.Get("Nome", "default_value");

            bool verificaStatusCalendario = await calendarioServices.GetCalendarioCQStatus(dia, mes, descricao);

            if (verificaStatusCalendario)
            {
                await Application.Current.MainPage.DisplayAlert("Info", "Calendário Já Foi Finalizado", "OK");
            }
            else
            {
                bool confirmaStatusAlterado = await calendarioServices.FinalizarCalendario(dia, mes, descricao, finalizadoPor);

                if (confirmaStatusAlterado)
                {
                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Serviço Finalizado Com Sucesso", "OK");
                }
            }

            
        }
    }
}

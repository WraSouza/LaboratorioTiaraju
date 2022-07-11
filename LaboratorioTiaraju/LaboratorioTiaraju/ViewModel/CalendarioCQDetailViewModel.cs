using LaboratorioTiaraju.FirebaseServices;
using LaboratorioTiaraju.Model;
using LaboratorioTiaraju.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Extensions;

namespace LaboratorioTiaraju.ViewModel
{
    internal class CalendarioCQDetailViewModel : BaseViewModel
    {
        private DateTime _dataColeta;
        private string _observacao;
        private string _mes;
        private bool _Result;
        private bool _IsBusy;        

        public Command AlterarStatusCalendario { get; set; }
        public Command ExcluirCalendario { get; set; }
        public Command AtualizarCalendario { get; set; }

        //Método para verificar se o login foi realizado com sucesso
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
            AlterarStatusCalendario = new Command<CalendarioCQ>(async (model) => await AlterarStatusCalendarioCommand(model));
            ExcluirCalendario = new Command<CalendarioCQ>(async (model) => await ExcluirCalendarioCommand(model));
            AtualizarCalendario = new Command(async () => await AtualizarCalendarioCommand());
        }

        private async Task AtualizarCalendarioCommand()
        {
            bool verificaConexao = Conectividade.VerificaConectividade();

            if (verificaConexao)
            {
                CalendarioCQServices calendarioServices = new CalendarioCQServices();
                string descricao = Observacao;
                int dia = DataColeta.Day;
                _mes = DataColeta.ToString("MMMM").ToUpper();

                if (DataColeta.ToString("MMMM").ToUpper() == "JANUARY")
                {
                    _mes = "JANEIRO";
                }

                if (DataColeta.ToString("MMMM").ToUpper() == "FEBRUARY")
                {
                    _mes = "FEVEREIRO";
                }

                if (DataColeta.ToString("MMMM").ToUpper() == "MARCH")
                {
                    _mes = "MARÇO";
                }

                if (DataColeta.ToString("MMMM").ToUpper() == "APRIL")
                {
                    _mes = "ABRIL";
                }

                if (DataColeta.ToString("MMMM").ToUpper() == "MAY")
                {
                    _mes = "MAIO";
                }

                if (DataColeta.ToString("MMMM").ToUpper() == "JUNE")
                {
                    _mes = "JUNHO";
                }

                if (DataColeta.ToString("MMMM").ToUpper() == "JULY")
                {
                    _mes = "JULHO";
                }

                if (DataColeta.ToString("MMMM").ToUpper() == "AUGUST")
                {
                    _mes = "AGOSTO";
                }

                if (DataColeta.ToString("MMMM").ToUpper() == "SEPTEMBER")
                {
                    _mes = "SETEMBRO";
                }

                if (DataColeta.ToString("MMMM").ToUpper() == "OCTOBER")
                {
                    _mes = "OUTUBRO";
                }

                if (DataColeta.ToString("MMMM").ToUpper() == "NOVEMBER")
                {
                    _mes = "NOVEMBRO";
                }

                if (DataColeta.ToString("MMMM").ToUpper() == "DECEMBER")
                {
                    _mes = "DEZEMBRO";
                }

                bool confirmaStatusAlterado = await calendarioServices.AtualizaDadosCalendario(dia, _mes, descricao);

                if (confirmaStatusAlterado)
                {
                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Evento Atualizado Com Sucesso", "OK");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Ops!", "Algo deu errado.Verifique Sua Conexão de Internet.", "OK");
            }

        }

        private async Task ExcluirCalendarioCommand(CalendarioCQ model)
        {
            CalendarioCQServices calendarioServices = new CalendarioCQServices();
            string descricao = model.Descricao;
            int dia = model.Dia;
            string mes = model.Mes;
            string finalizadoPor = Preferences.Get("Nome", "default_value");

            bool verificaStatusCalendario = await calendarioServices.GetCalendarioCQStatusExcluded(dia, mes, descricao);

            if (verificaStatusCalendario)
            {
                await Application.Current.MainPage.DisplayAlert("Info", "Evento Já Foi Excluído", "OK");
            }
            else
            {
                string motivo = await Application.Current.MainPage.DisplayPromptAsync("Cuidado", "Informe Motivo da Exclusão","OK","Cancel");

                if(!(motivo == null))
                {
                    if (String.IsNullOrEmpty(motivo))
                    {
                        await Application.Current.MainPage.DisplayAlert("Ops", "É necessário Informar um Motivo.", "OK");
                    }
                    else
                    {
                                
                        bool confirmaStatusAlterado = await calendarioServices.ExcluirCalendario(dia, mes, descricao, finalizadoPor, motivo);

                                    if (confirmaStatusAlterado)
                                    {
                                        await Application.Current.MainPage.DisplayAlert("Sucesso", "Evento Excluído Com Sucesso", "OK");
                                    }
                                
                                else
                                {
                                    await Application.Current.MainPage.DisplayAlert("Erro", "Dados Não Conferem", "OK");
                                }                                     

                            
                    }
                    
                }         
            }
        }

        private async Task AlterarStatusCalendarioCommand(CalendarioCQ model)
        {
            bool verificaConexao = Conectividade.VerificaConectividade();

            if (verificaConexao)
            {
                if (IsBusy)
                    return;

                try
                {
                    CalendarioCQServices calendarioServices = new CalendarioCQServices();
                    string descricao = model.Descricao;
                    int dia = model.Dia;
                    string mes = model.Mes;
                    string finalizadoPor = Preferences.Get("Nome", "default_value");

                    bool verificaStatusCalendario = await calendarioServices.GetCalendarioCQStatus(dia, mes, descricao);

                    if (verificaStatusCalendario)
                    {
                        await Application.Current.MainPage.DisplayAlert("Info", "Evento Já Foi Finalizado", "OK");
                    }
                    else
                    {
                        bool confirmaStatusAlterado = await calendarioServices.FinalizarCalendario(dia, mes, descricao, finalizadoPor);

                        if (confirmaStatusAlterado)
                        {
                            await Application.Current.MainPage.DisplayAlert("Sucesso", "Evento Finalizado Com Sucesso", "OK");
                        }
                    }
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
                await Application.Current.MainPage.DisplayAlert("Erro", "Verifique Sua Conexão de Internet.", "OK");
            }

            

            

            
        }
    }
}

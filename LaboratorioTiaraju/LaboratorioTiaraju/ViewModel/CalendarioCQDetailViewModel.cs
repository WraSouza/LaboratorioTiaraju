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
        public Command AbrirCalendarioCQEditView { get; set; }

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
            AbrirCalendarioCQEditView = new Command<CalendarioCQ>((model) => IrCalendarioEditView(model));
            
        }

        private async void IrCalendarioEditView(CalendarioCQ model)
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
            var route = $"{nameof(View.CalendarioEditCQDetailView)}";
            await Shell.Current.GoToAsync(route);
        }

        private async Task AtualizarCalendarioCommand()
        {
            bool verificaConexao = Conectividade.VerificaConectividade();

            if (verificaConexao)
            {

                bool verificaData = DataHora.VerificaData(DataColeta);

                if (verificaData)
                {
                    CalendarioCQServices calendarioServices = new CalendarioCQServices();
                    string descricao = Observacao;
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
                    string diaFinalizacao = DateTime.Today.ToShortDateString();

                    bool verificaStatusCalendario = await calendarioServices.GetCalendarioCQStatus(dia, mes, descricao);

                    if (verificaStatusCalendario)
                    {
                        
                        await Application.Current.MainPage.DisplayAlert("Info", "Evento Já Foi Finalizado", "OK");
                    }
                    else
                    {
                        bool confirmaStatusAlterado = await calendarioServices.FinalizarCalendario(dia, mes, descricao, finalizadoPor, diaFinalizacao);

                        if (confirmaStatusAlterado)
                        {
                            await Application.Current.MainPage.DisplayAlert("Sucesso", "Evento Finalizado Com Sucesso.", "OK");
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
                Mensagem.MensagemErroConexao();
            }
            
        }
    }
}

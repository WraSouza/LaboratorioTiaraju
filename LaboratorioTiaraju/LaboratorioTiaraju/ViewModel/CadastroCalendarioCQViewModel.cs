using LaboratorioTiaraju.FirebaseServices;
using LaboratorioTiaraju.Model;
using LaboratorioTiaraju.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LaboratorioTiaraju.ViewModel
{
    internal class CadastroCalendarioCQViewModel : BaseViewModel
    {
        private DateTime _dataColeta;
        private string _observacao;
        private string _mes;
        private string _titulo;
        private bool _Result;
        private bool _IsBusy;

        public Command SalvarCalendario { get; set; }


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

        public string Titulo
        {
            get => _titulo;
            set
            {
                _titulo = value;
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

        public CadastroCalendarioCQViewModel()
        {
            SalvarCalendario = new Command(async () => await SalvarCalendarioDados());
        }

        private async Task SalvarCalendarioDados()
        {

            if (IsBusy)
                return;

            try
            {
                bool verificaConexao = Conectividade.VerificaConectividade();

                if (verificaConexao)
                {
                    var calendarioService = new CalendarioCQServices();

                    _mes = DataColeta.ToString("MMMM").ToUpper();
                    
                    if(Observacao == null)
                    {
                        var novoCalendario = new CalendarioCQ()
                        {

                            Dia = DataColeta.Day,
                            Mes = _mes,
                            Descricao = " ",
                            IsFinished = false,
                            IsExcluded = false,
                            FinalizadoPor = " ",
                            MotivoExclusao = " ",
                            Titulo = Titulo,
                            DataFinalizacao = DateTime.Today.ToShortDateString()

                        };

                        bool verificaData = DataHora.VerificaData(DataColeta);

                        if (verificaData)
                        {

                            bool verificaSeExiste = await calendarioService.IsCalendarioCQExists(novoCalendario);

                            if (verificaSeExiste)
                            {
                                Mensagem.MensagemEventoJaCadastrado();                                
                            }
                            else
                            {
                                bool confirmaCadastro = await CalendarioCQ.CadastraCalendario(novoCalendario);                                

                                if (confirmaCadastro)
                                {
                                    Mensagem.MensagemCadastroSucesso();
                                }

                            }

                        }
                        else
                        {
                            Mensagem.MensagemDataDeveSerMaior();
                        }

                    }
                    else
                    {
                        var novoCalendario = new CalendarioCQ()
                        {

                            Dia = DataColeta.Day,
                            Mes = _mes,
                            Descricao = Observacao,
                            IsFinished = false,
                            IsExcluded = false,
                            FinalizadoPor = " ",
                            MotivoExclusao = " ",
                            Titulo = Titulo,
                            DataFinalizacao = DateTime.Today.ToShortDateString()

                        };

                        bool verificaData = DataHora.VerificaData(DataColeta);

                        if (verificaData)
                        {

                            bool verificaSeExiste = await calendarioService.IsCalendarioCQExists(novoCalendario);

                            if (verificaSeExiste)
                            {
                                Mensagem.MensagemEventoJaCadastrado();                                
                            }
                            else
                            {
                                bool confirmaCadastro = await CalendarioCQ.CadastraCalendario(novoCalendario);                                

                                if (confirmaCadastro)
                                {
                                    Mensagem.MensagemCadastroSucesso();
                                }

                            }

                        }
                        else
                        {
                            Mensagem.MensagemDataDeveSerMaior();
                        }
                    }                   
                    
                }
                else
                {
                    Mensagem.MensagemErroConexao();                    
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
    }
}

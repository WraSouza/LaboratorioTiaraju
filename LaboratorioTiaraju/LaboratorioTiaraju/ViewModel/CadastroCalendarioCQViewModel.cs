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

        public Command SalvarCalendario { get; set; }

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

        public CadastroCalendarioCQViewModel()
        {
            SalvarCalendario = new Command(async () => await SalvarCalendarioDados());
        }

        private async Task SalvarCalendarioDados()
        {
            bool verificaConexao = Conectividade.VerificaConectividade();

            if (verificaConexao)
            {
                var calendarioService = new CalendarioCQServices();

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

                var novoCalendario = new CalendarioCQ()
                {
                    Dia = DataColeta.Day.ToString(),
                    Mes = _mes,                    
                    Descricao = Observacao
                };

                bool confirmaCadastro = await calendarioService.CadastrarDadosCalendario(novoCalendario);

                if (confirmaCadastro)
                {
                    await Application.Current.MainPage.DisplayAlert("", "Informações Salvas Com Sucesso.", "OK");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Ops!", "Algo deu errado.Verifique Sua Conexão de Internet.", "OK");
            }
        }
    }
}

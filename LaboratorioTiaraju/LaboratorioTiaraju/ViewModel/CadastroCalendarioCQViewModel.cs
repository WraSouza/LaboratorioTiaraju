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

                var novoCalendario = new CalendarioCQ()
                {
                    Dia = DataColeta.Day.ToString(),
                    Mes = DataColeta.ToString("MMMM").ToUpper(),
                    
                    //DataColeta = DataColeta.Date.ToString("dd-MM-yyyy"),
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

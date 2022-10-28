using LaboratorioTiaraju.FirebaseServices;
using LaboratorioTiaraju.Model;
using LaboratorioTiaraju.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LaboratorioTiaraju.ViewModel
{
    internal class CadastroCalendarioGQVisitasViewModel : BaseViewModel
    {
        private string _nomeParceiro;
        private object _rdRefeicaoButton;
        private object _rdHospedagemButton;
        private DateTime _dataChegada;
        private DateTime _dataFinal;
        private string _descricao;
        CalendarioGQServices calendarioVisitasGQ = new CalendarioGQServices();


        public Command SalvarCommand { get; set; }


        public CadastroCalendarioGQVisitasViewModel()
        {
            SalvarCommand = new Command(async () => await SalvarCalendarioVisitante());
        }

        public string Descricao
        {
            get => _descricao;
            set
            {
                _descricao = value;
                OnPropertyChanged();
            }
        }

        public DateTime DataFinal
        {
            get => _dataFinal;
            set
            {
                _dataFinal = value;
                OnPropertyChanged();
            }
        }

        public DateTime DataChegada
        {
            get => _dataChegada;
            set
            {
                _dataChegada = value;
                OnPropertyChanged();
            }
        }

        public object RefeicaoButton
        {
            get => _rdRefeicaoButton;
            set
            {
                _rdRefeicaoButton = value;
                OnPropertyChanged();
            }
        }

        public object HospedagemButton
        {
            get => _rdHospedagemButton;
            set
            {
                _rdHospedagemButton = value;
                OnPropertyChanged();
            }
        }

        public string Nome
        {
            get => _nomeParceiro;
            set
            {
                _nomeParceiro = value;
                OnPropertyChanged();
            }
        }

        private async Task SalvarCalendarioVisitante()
        {
            var novaVisita = new CalendarioVisitasGQ()
            {
                ResponsabilityMeal = RefeicaoButton.ToString(),
                ResponsabilityHotel = HospedagemButton.ToString(),
                Titulo = Nome,
                DataChegada = DataChegada.ToShortDateString(),
                DataFinal = DataFinal.ToShortDateString(),
                Descricao = Descricao,
                IsFinished = false,
                IsExcluded = false,
                FinalizadoPor = " ",
                MotivoExclusao = " ",
                DataFinalizacao = DateTime.Today,
                Dia = DataChegada.Day,
                Mes = DataChegada.ToString("MMMM").ToUpper(),
                Comentarios = new List<string>()
        };

            bool verificaConexao = Conectividade.VerificaConectividade();

            if (verificaConexao)
            {
                bool confirmaCadastro = await calendarioVisitasGQ.CadastrarDadosCalendario(novaVisita);

                if (confirmaCadastro)
                {
                    Mensagem.MensagemCadastroSucesso();
                }
                
            }
            else
            {
                Mensagem.MensagemErroConexao();                
            }

        }
    }
}

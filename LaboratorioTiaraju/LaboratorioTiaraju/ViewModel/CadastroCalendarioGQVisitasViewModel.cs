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

        public Command SalvarCommand { get; set; }


        public CadastroCalendarioGQVisitasViewModel()
        {
            SalvarCommand = new Command(async () => await SalvarCalendarioVisitante());
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
                _nomeParceiro = value.ToUpper();
                OnPropertyChanged();
            }
        }

        private async Task SalvarCalendarioVisitante()
        {

        }
    }
}

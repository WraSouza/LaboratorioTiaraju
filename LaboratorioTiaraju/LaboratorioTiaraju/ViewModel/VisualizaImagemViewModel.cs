using LaboratorioTiaraju.FirebaseServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LaboratorioTiaraju.ViewModel
{
    internal class VisualizaImagemViewModel : BaseViewModel
    {
        private ImageSource _caminhoImagem;

        public ImageSource CaminhoImagem
        {
            get { return _caminhoImagem; }
            set { _caminhoImagem = value; OnPropertyChanged(); }
        }

        public VisualizaImagemViewModel()
        {
            BuscaImagem();
        }

        private async Task BuscaImagem()
        {
            ImageServices referenciaImagem = new ImageServices();
            string opcaoDesejada = Preferences.Get("Imagem", "default_value");

            var imagem = await referenciaImagem.RetornaImagem(opcaoDesejada);
            
        }
    }
}

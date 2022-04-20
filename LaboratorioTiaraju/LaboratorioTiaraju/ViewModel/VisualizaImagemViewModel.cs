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
        private string _caminhoImagem;

        ImageServices referenciaImagem = new ImageServices();
        string opcaoDesejada = Preferences.Get("Imagem", "default_value");

        public string CaminhoImagem
        {
            get {  return referenciaImagem.RetornaImagem(opcaoDesejada).ToString(); }
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

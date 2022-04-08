using LaboratorioTiaraju.FirebaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LaboratorioTiaraju.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VisualizaImagemView : ContentPage
    {
        public VisualizaImagemView()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            ImageServices referenciaImagem = new ImageServices();
            string opcaoDesejada = Preferences.Get("Imagem", "default_value");

            collectionview.ItemsSource = await referenciaImagem.RetornaImagem(opcaoDesejada);

            //Image imagem = await referenciaImagem.RetornaImagem(opcaoDesejada);
            //_caminhoImagem = imagem;
        }
    }
}
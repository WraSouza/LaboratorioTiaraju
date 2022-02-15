using Firebase.Storage;
using LaboratorioTiaraju.FirebaseServices;
using LaboratorioTiaraju.Model;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LaboratorioTiaraju.ViewModel
{
    internal class EnviaImagemViewModel : BaseViewModel
    {
        MediaFile file;
        private ImageSource _caminhoImagem;
        private bool _IsBusy;
        public Command SelecionarImagem { get; set; }
        public Command CadastrarImagem { get; set; }

        public bool IsBusy
        {
            get => _IsBusy;
            set
            {
                _IsBusy = value;
                OnPropertyChanged();
            }
        }

        public EnviaImagemViewModel()
        {
            SelecionarImagem = new Command(async () => await SelecionarImagemGaleria());
            CadastrarImagem = new Command(async () => await EnviarImagem());
        }

        public ImageSource CaminhoImagem
        {
            get { return _caminhoImagem; }
            set { _caminhoImagem = value; OnPropertyChanged(); }
        }

        private async Task SelecionarImagemGaleria()
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
                if (file == null)
                    return;
                CaminhoImagem = ImageSource.FromStream(() => file.GetStream());
                //await StoreImages(file.GetStream());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async Task<string> StoreImages(Stream imageStream)
        {
            var imageServices = new ImageServices();

            IsBusy = true;

            List<Cardapio> verificaCardapio = await imageServices.VerificaCardapio();

            if (verificaCardapio.Count == 0)
            {
                
                string imagemSelecionada = CaminhoImagem.ToString();
                string imgurl = null;
                string storageImage = null;
                
                    storageImage = await new FirebaseStorage("tiaraju-afa0a.appspot.com")
                  .Child("Cardapio")
                  .Child(imagemSelecionada + ".jpg")
                  .PutAsync(imageStream);
                    imgurl = storageImage;

                
                return imgurl;

                
            }
            else
            {
                return "";
            }
           
        }       

        private async Task EnviarImagem()
        {
            IsBusy = true;
            var imagemURL = await StoreImages(file.GetStream());
            
            string referenciaImagem = imagemURL.ToString();

            ImageServices cardapioImage = new ImageServices();

            bool confirmaCadastroLanche = await cardapioImage.EnviarCardapio(referenciaImagem);

            if (confirmaCadastroLanche)
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Sucesso", "Cardápio Cadastrado Com Sucesso", "OK");
            }
        }

        async Task GetBack()
        {
            await Shell.Current.GoToAsync("..");
        }        
    }
}

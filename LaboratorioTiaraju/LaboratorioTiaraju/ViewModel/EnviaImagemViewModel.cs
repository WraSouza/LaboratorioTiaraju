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
using Xamarin.Essentials;
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

            //List<Imagem> verificaCardapio = await imageServices.VerificaCardapio();

            string imagemSelecionada = CaminhoImagem.ToString();
            var pastaImagem = Preferences.Get("Imagem", "default_value");
            string imgurl = null;
            string storageImage = null;

            storageImage = await new FirebaseStorage("tiaraju-afa0a.appspot.com")
          .Child(pastaImagem)
          .Child(imagemSelecionada + ".jpg")
          .PutAsync(imageStream);
            imgurl = storageImage;


            return imgurl;

            //if (verificaCardapio.Count == 0)
            //{
            //    string imagemSelecionada = CaminhoImagem.ToString();
            //    var pastaImagem = Preferences.Get("Imagem", "default_value");
            //    string imgurl = null;
            //    string storageImage = null;

            //    storageImage = await new FirebaseStorage("tiaraju-afa0a.appspot.com")
            //  .Child(pastaImagem)
            //  .Child(imagemSelecionada + ".jpg")
            //  .PutAsync(imageStream);
            //    imgurl = storageImage;


            //    return imgurl;

            //}
            //else
            //{
            //    string imagemSelecionada = CaminhoImagem.ToString();
            //    var pastaImagem = Preferences.Get("Imagem", "default_value");
            //    string imgurl = null;
            //    string storageImage = null;

            //    storageImage = await new FirebaseStorage("tiaraju-afa0a.appspot.com")
            //  .Child(pastaImagem)
            //  .Child(imagemSelecionada + ".jpg")
            //  .PutAsync(imageStream);
            //    imgurl = storageImage;


            //    return imgurl;
            //}

        }       

        private async Task EnviarImagem()
        {
            IsBusy = true;
            var imagemURL = await StoreImages(file.GetStream());
            var imageServices = new ImageServices();

            string referenciaImagem = imagemURL.ToString();

            ImageServices cardapioImage = new ImageServices();

            List<Imagem> verificaCardapio = await imageServices.VerificaCardapio();

            if(verificaCardapio.Count == 0)
            {
                bool confirmaCadastro = await cardapioImage.EnviarImagem(referenciaImagem);

                if (confirmaCadastro)
                {
                    IsBusy = false;
                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Imagem Cadastrada Com Sucesso", "OK");
                }
            }
            else
            {
                bool confirmaCadastro = await cardapioImage.AtualizarImagem(referenciaImagem);

                if (confirmaCadastro)
                {
                    IsBusy = false;
                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Imagem Atuailzada Com Sucesso", "OK");
                }
            }

            
        }

        async Task GetBack()
        {
            await Shell.Current.GoToAsync("..");
        }        
    }
}

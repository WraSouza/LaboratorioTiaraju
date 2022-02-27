using Firebase.Database;
using Firebase.Database.Query;
using LaboratorioTiaraju.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace LaboratorioTiaraju.FirebaseServices
{
    internal class ImageServices
    {
        FirebaseClient firebase;

        public ImageServices()
        {
            firebase = new FirebaseClient("https://tiaraju-afa0a-default-rtdb.firebaseio.com/");
        }
        public async Task<bool> EnviarImagem(string referenciaImage)
        {
            var pastaImagem = Preferences.Get("Imagem", "default_value");

            await firebase.Child(pastaImagem)
                    .PostAsync(new Imagem()
                    {                        
                        CaminhoImagem = referenciaImage,
                        
                    });

            return true;
        }

        public async Task<List<Imagem>> VerificaCardapio()
        {
            var pastaImagem = Preferences.Get("Imagem", "default_value");
            return (await firebase
                .Child(pastaImagem)
                .OnceAsync<Imagem>()).Select(item => new Imagem
                {
                    CaminhoImagem = item.Object.CaminhoImagem,
                }).ToList();
            //var cardapioExiste = (await firebase.Child("Cardapio")
            //    .OnceAsync<Cardapio>())
            //    .Where(u => u.Object.CaminhoImagem == null)
            //    .FirstOrDefault();

            //return (cardapioExiste != null);
        }


        public async Task<bool> EnviarPrevencaoCovid(string referenciaImage)
        {
            await firebase.Child("Prevencao Covid")
                    .PostAsync(new Imagem()
                    {
                        CaminhoImagem = referenciaImage,

                    });

            return true;
        }

        public async Task<bool> EnviarDiaT(string referenciaImage)
        {
            await firebase.Child("DiaT")
                    .PostAsync(new Imagem()
                    {
                        CaminhoImagem = referenciaImage,

                    });

            return true;
        }
    }
}

using Firebase.Database;
using Firebase.Database.Query;
using LaboratorioTiaraju.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioTiaraju.FirebaseServices
{
    internal class ImageServices
    {
        FirebaseClient firebase;

        public ImageServices()
        {
            firebase = new FirebaseClient("https://tiaraju-afa0a-default-rtdb.firebaseio.com/");
        }
        public async Task<bool> EnviarCardapio(string referenciaImage)
        {
            await firebase.Child("Cardapio")
                    .PostAsync(new Cardapio()
                    {                        
                        CaminhoImagem = referenciaImage,
                        
                    });

            return true;
        }

        public async Task<List<Cardapio>> VerificaCardapio()
        {
            return (await firebase
                .Child("Cardapio")
                .OnceAsync<Cardapio>()).Select(item => new Cardapio
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
                    .PostAsync(new Cardapio()
                    {
                        CaminhoImagem = referenciaImage,

                    });

            return true;
        }

        public async Task<bool> EnviarDiaT(string referenciaImage)
        {
            await firebase.Child("DiaT")
                    .PostAsync(new Cardapio()
                    {
                        CaminhoImagem = referenciaImage,

                    });

            return true;
        }
    }
}

﻿using Firebase.Database;
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
                        PastaImagem = pastaImagem,
                        CaminhoImagem = referenciaImage,

                    });

            return true;
        }

        public async Task<bool> AtualizarImagem(string referenciaImagem)
        {
            var pastaImagem = Preferences.Get("Imagem", "default_value");

            var toUpdateImagem = (await firebase
                .Child(pastaImagem)
                .OnceAsync<Imagem>()).Where(x => x.Object.PastaImagem == pastaImagem).FirstOrDefault();            

            toUpdateImagem.Object.CaminhoImagem = referenciaImagem;

            await firebase
           .Child(pastaImagem)

           .Child(toUpdateImagem.Key)
           .PutAsync(toUpdateImagem.Object);

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

        public async Task<List<Imagem>> RetornaImagem(string opcaoDesejada)
        {
            return (await firebase.Child(opcaoDesejada)
                .OnceAsync<Imagem>()).Select(item => new Imagem
                {
                    CaminhoImagem = item.Object.CaminhoImagem,
                }).ToList();
        }
    }
}

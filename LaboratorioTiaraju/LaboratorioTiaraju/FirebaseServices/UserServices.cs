﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using LaboratorioTiaraju.Model;

namespace LaboratorioTiaraju.FirebaseServices
{
    internal class UserServices
    {
        FirebaseClient firebase;

        public UserServices()
        {
            firebase = new FirebaseClient("https://tiaraju-afa0a-default-rtdb.firebaseio.com/");
        }

        public async Task<bool> LoginUser(string name, string passwd)
        {
            var user = (await firebase.Child("Usuario")
                .OnceAsync<Usuario>())
                .Where(u => u.Object.NomeUsuario == name)
                .Where(u => u.Object.Senha == passwd)
                .FirstOrDefault();


            return (user != null);
        }

        public async Task<string> GetUserResponsability(string name)
        {
            var user = (await firebase.Child("Usuario")
               .OnceAsync<Usuario>())
               .Where(u => u.Object.NomeUsuario == name)
               .FirstOrDefault();

            return user.Object.Responsabilidade;
        }

        public async Task<string> GetUserDept(string name)
        {
            var user = (await firebase.Child("Usuario")
               .OnceAsync<Usuario>())
               .Where(u => u.Object.NomeUsuario == name)
               .FirstOrDefault();

            return user.Object.Departamento;
        }

        public async Task<string> GetUserStatus(string name)
        {
            var user = (await firebase.Child("Usuario")
               .OnceAsync<Usuario>())
               .Where(u => u.Object.NomeUsuario == name)
               .FirstOrDefault();

            return user.Object.Status;
        }

        public async Task<string> GetUserSenha(string name)
        {
            var user = (await firebase.Child("Usuario")
               .OnceAsync<Usuario>())
               .Where(u => u.Object.NomeUsuario == name)
               .FirstOrDefault();

            return user.Object.Senha;
        }
    }
}

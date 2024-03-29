﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LaboratorioTiaraju.Services
{
    public class Mensagem
    {
        public static void MensagemErroConexao()
        {
            Application.Current.MainPage.DisplayAlert("Ops!", "Algo Deu Errado. Verifique Sua Conexão de Internet.", "OK");
        }

        public static void MensagemSenhaInvalida()
        {
            Application.Current.MainPage.DisplayAlert("Ops!", "Usuário ou Senha Inválida", "OK");
        }

        public static void MensagemCadastroSucesso()
        {
            Application.Current.MainPage.DisplayAlert("", "Cadastro Realizado Com Sucesso.", "OK");
        }

        public static void MensagemUsuarioInvalido()
        {
            Application.Current.MainPage.DisplayAlert("Ops!", "Usuário Informado Não Existe", "OK");
        }

        public static void MensagemEventoJaCadastrado()
        {
            Application.Current.MainPage.DisplayAlert("Ops!", "Evento Já Cadastrado.", "OK");
        }

        public static void MensagemDataDeveSerMaior()
        {
            Application.Current.MainPage.DisplayAlert("Ops!", "A Data Informada Deve Ser Posterior ou Igual a Hoje.", "OK");
        }

        public static void MensagemUsuarioSemAutorizacao()
        {
            Application.Current.MainPage.DisplayAlert("Ops!", "Usuário Sem Autorização de Acesso", "OK");
        }
    }
}

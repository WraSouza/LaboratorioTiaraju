using LaboratorioTiaraju.FirebaseServices;
using LaboratorioTiaraju.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LaboratorioTiaraju.ViewModel
{
    internal class TrocarSenhaViewModel : BaseViewModel
    {
        private string _senhaAntiga;
        private string _senhaNova;
        private bool _Result;
        private bool _IsBusy;

        public Command AtualizarSenhaCommand { get; set; }

        public string SenhaAntiga
        {
            get { return _senhaAntiga; }
            set { _senhaAntiga = value;
                OnPropertyChanged();
            }
        }

        public string SenhaNova
        {
            get { return _senhaNova; }
            set
            {
                _senhaNova = value;
                OnPropertyChanged();
            }
        }

        //Método para verificar se o login foi realizado com sucesso
        public bool Result
        {
            get => _IsBusy;
            set
            {
                _IsBusy = value;
                OnPropertyChanged();
            }
        }

        //Método para verificar se o login está sendo realizado para evitar concorrência
        public bool IsBusy
        {
            get => _Result;
            set
            {
                _Result = value;
                OnPropertyChanged();
            }
        }

        public TrocarSenhaViewModel()
        {
            AtualizarSenhaCommand = new Command(async () => await AtualizaCommandAsync());
        }

        private async Task AtualizaCommandAsync()
        {
            string nomeUsuario = Preferences.Get("Nome", "default_value");

            UserServices userServices = new UserServices();

            try
            {
                bool verificaConexao = Conectividade.VerificaConectividade();

                if (verificaConexao)
                {
                    IsBusy = true;
                    string novaSenha = Criptografia.CriptografaSenha(SenhaNova);
                    bool confirmaTrocaSenha = await userServices.AtualizarSenha(nomeUsuario, novaSenha);

                    if (confirmaTrocaSenha)
                    {
                        await Application.Current.MainPage.DisplayAlert("Sucesso", "Senha Alterada Com Sucesso.", "OK");
                        Application.Current.MainPage = new View.AppShell();
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Não Foi Possível Verificar Credenciais. Verifique Sua Conexão de Internet.", "OK");
                }

            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }

            
        }
    }
}

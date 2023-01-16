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
    internal class LoginViewModel : BaseViewModel
    {
        private string _Nome;
        private string _Senha;
        private bool _Result;
        private bool _IsBusy;

        public Command LoginCommand { get; set; }        

        public string Nome
        {
            get => _Nome;
            set
            {
                _Nome = value.ToLower();
                OnPropertyChanged();
            }
        }

        public string Senha
        {
            get => _Senha;
            set
            {
                _Senha = value;
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

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await LoginCommandAsync()); //Nome do comando, qualquer um nome de preferência            
        }

        //Login
        private async Task LoginCommandAsync()
        {
            if (IsBusy)
                return;

            try
            {
                bool verificaConexao = Conectividade.VerificaConectividade();

                if (verificaConexao)
                {
                    IsBusy = true;                    
                    var userService = new UserServices();
                    var calendarioCQService = new CalendarioCQServices();
                    string senhaDigitada = Criptografia.CriptografaSenha(Senha);
                    Result = await userService.IsUSerExists(Nome);
                    

                    if (Result)
                    {
                        
                        Preferences.Set("Nome", Nome);                        

                        string responsabilidade = await userService.GetUserResponsability(Nome);

                        string departamento = await userService.GetUserDept(Nome);

                        string status = await userService.GetUserStatus(Nome);

                        string senhaUsuario = await userService.GetUserSenha(Nome);

                        if (status != "ativo")
                        {                            
                            Mensagem.MensagemUsuarioSemAutorizacao();                           
                        }
                        else
                        {
                            Preferences.Set("Departamento", departamento);

                            Preferences.Set("Responsabilidade", responsabilidade);

                            string senhaNoBanco = await userService.GetUserSenha(Nome);

                            //Verificar se a senha é 1234
                            if (senhaNoBanco == "1234")
                            {
                                Application.Current.MainPage = new View.TrocarSenhaView();
                            }
                            else
                            {
                                Result = await userService.LoginUser(Nome, senhaDigitada);

                                if (Result)
                                {
                                    //await calendarioCQService.ApagarCalendario();
                                    Application.Current.MainPage = new View.AppShell();
                                }
                                else
                                {
                                    Mensagem.MensagemSenhaInvalida();
                                }
                                
                            }                           

                        }

                    }else
                    {
                        Mensagem.MensagemUsuarioInvalido();
                    }
                }
                else
                {
                    Mensagem.MensagemErroConexao();                    
                }


            }
            catch (Exception ex)
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

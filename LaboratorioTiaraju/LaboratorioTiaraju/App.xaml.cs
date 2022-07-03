using Microsoft.AppCenter;
using Microsoft.AppCenter.Distribute;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LaboratorioTiaraju
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new View.LoginView();
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=5597ba11 - b4af - 44e7 - a7f9 - 2b180d8d185b}", typeof(Distribute));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

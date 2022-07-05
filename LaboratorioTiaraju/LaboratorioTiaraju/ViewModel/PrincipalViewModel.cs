﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LaboratorioTiaraju.ViewModel
{
    internal class PrincipalViewModel
    {
        public Command OpenImagemCardapio { get; set; }
        public Command OpenCalendarioCQ { get; set; }
        public Command OpenRHInforma { get; set; }
        public Command OpenGLPI { get; set; }
        public Command OpenRH {  get; set; }
        public Command OpenTI { get; set; }
        public Command OpenBiblioteca { get; set; }
        public INavigation Navigation { get; set; }

        public PrincipalViewModel()
        {
           
        }

        public PrincipalViewModel(INavigation navigation)
        {
            OpenGLPI = new Command(async () => await OpenGLPIView());

            OpenRH = new Command(async () => await OpenRHView());

            OpenImagemCardapio = new Command(async () => await OpenImagemCardapioView());

            OpenRHInforma = new Command(async () => await OpenRHInformaView());

            OpenCalendarioCQ = new Command(async () => await OpenCalendarioCQView());

            OpenBiblioteca = new Command(async () => await OpenBibliotecaView());

            OpenTI = new Command(async () => await OpenTIView());
        }

        private async Task OpenBibliotecaView()
        {            
                await Application.Current.MainPage.DisplayAlert("Ops!", "Página Em Construção...", "OK");
                //var route = $"{nameof(View.BibliotecaView)}";

                //await Shell.Current.GoToAsync(route);           
        }

        private async Task OpenTIView()
        {
            const string ti = "TI";

            string departamento = Preferences.Get("Departamento", "default_value");
            if (departamento == ti)
            {
                await Application.Current.MainPage.DisplayAlert("Ops!","Página Em Construção...","OK");
                //var route = $"{nameof(View.TIView)}";

                //await Shell.Current.GoToAsync(route);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("", "Acesso Não Autorizado", "OK");
            }
        }

        private async Task OpenCalendarioCQView()
        {            
            const string cq = "CQ";
            const string micro = "MICRO";
            string departamento = Preferences.Get("Departamento", "default_value");
            if ((departamento == cq) || (departamento == micro))
            {
                var route = $"{nameof(View.CalendarioCQTabbedView)}";
                //var route = $"{nameof(View.CalendarioCQView)}";
                await Shell.Current.GoToAsync(route);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("", "Acesso Não Autorizado", "OK");
            }
        }

        private async Task OpenRHInformaView()
        {
            const string rhinforma = "RHInforma";
            Preferences.Set("Imagem", rhinforma);
            var route = $"{nameof(View.VisualizaImagemView)}";
            await Shell.Current.GoToAsync(route);
        }

        private async Task OpenImagemCardapioView()
        {
            const string cardapio = "Cardapio";
            Preferences.Set("Imagem", cardapio);
            var route = $"{nameof(View.VisualizaImagemView)}";
            await Shell.Current.GoToAsync(route);
        }

        private async Task OpenGLPIView()
        {            
            var route = $"{nameof(View.GLPIView)}";
            await Shell.Current.GoToAsync(route);
        }

        private async Task OpenRHView()
        {
            const string rh = "RH";
            string departamento = Preferences.Get("Departamento", "default_value");
            if(departamento == rh)
            {
                var route = $"{nameof(View.RHView)}";
                await Shell.Current.GoToAsync(route);
            }
            else
            {
               await App.Current.MainPage.DisplayAlert("", "Acesso Não Autorizado", "OK");
            }
           
        }

        private async Task OpenEnviaImagemView()
        {           
            var route = $"{nameof(View.EnviaImagemView)}";
            await Shell.Current.GoToAsync(route);
        }

    }
}

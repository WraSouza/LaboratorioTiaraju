﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LaboratorioTiaraju.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastroCalendarioVisitaGQView : ContentPage
    {
        public CadastroCalendarioVisitaGQView()
        {
            InitializeComponent();

            datePickerChegada.Date = DateTime.Now;

            datePickerSaida.Date = DateTime.Now;
        }
    }
}
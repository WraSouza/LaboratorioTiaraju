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
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(GLPIView),typeof(GLPIView));

            Routing.RegisterRoute(nameof(EnviaImagemView), typeof(EnviaImagemView));

            Routing.RegisterRoute(nameof(RHView), typeof(RHView));

            Routing.RegisterRoute(nameof(VisualizaImagemView), typeof(VisualizaImagemView));

            Routing.RegisterRoute(nameof(TabbedPageCalendarioCQ), typeof(TabbedPageCalendarioCQ));
        }
    }
}
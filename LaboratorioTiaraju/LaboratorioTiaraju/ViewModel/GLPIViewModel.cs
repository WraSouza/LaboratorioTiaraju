﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LaboratorioTiaraju.ViewModel
{
    internal class GLPIViewModel : BaseViewModel
    {
        async Task GetBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}

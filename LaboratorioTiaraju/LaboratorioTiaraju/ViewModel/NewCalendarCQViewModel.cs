using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace LaboratorioTiaraju.ViewModel
{
    class NewCalendarCQViewModel : BaseViewModel
    {
        readonly List<DiaMes> source;
        public ObservableCollection<DiaMes> DiasMes { get; set; }

        public NewCalendarCQViewModel()
        {
            source = new List<DiaMes>();
            CriarDias();
        }

        void CriarDias()
        {
            for (int i = 0; i < 31; i++)
            {
                source.Add(new DiaMes
                {
                    diaMes = (i + 1)
                }) ;
            }

            DiasMes = new ObservableCollection<DiaMes>(source);
        }
    }

    public class DiaMes
    {
        public int diaMes { get; set; }
    }
}

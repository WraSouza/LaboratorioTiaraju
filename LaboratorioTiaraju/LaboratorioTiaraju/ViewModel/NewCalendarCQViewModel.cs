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
            //Vamos considerar que a data seja o dia de hoje, mas pode ser qualquer data.
            DateTime data = DateTime.Today;

            //DateTime com o primeiro dia do mês
            DateTime primeiroDiaDoMes = new DateTime(data.Year, data.Month, 1);

            DateTime ultimoDiaDoMes = new DateTime(data.Year, data.Month, DateTime.DaysInMonth(data.Year, data.Month));
            for (int i = 0; i < ultimoDiaDoMes.Day; i++)
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

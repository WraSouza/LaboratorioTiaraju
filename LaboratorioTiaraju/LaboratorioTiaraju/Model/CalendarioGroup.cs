using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LaboratorioTiaraju.Model
{
    internal class CalendarioGroup : ObservableCollection<CalendarioCQ>
    {
        public string Mes { get; private set; }

        public CalendarioGroup(string mes, ObservableCollection<CalendarioCQ> calendario) : base(calendario)
        {
            Mes = mes;
        }

        public override string ToString()
        {
            return Mes;
        }
    }
}

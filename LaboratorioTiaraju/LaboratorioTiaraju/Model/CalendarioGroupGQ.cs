using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LaboratorioTiaraju.Model
{
    internal class CalendarioGroupGQ : ObservableCollection<CalendarioVisitasGQ>
    {
        public string Mes { get; private set; }

        public CalendarioGroupGQ(string mes, ObservableCollection<CalendarioVisitasGQ> calendario) : base(calendario)
        {
            Mes = mes;
        }

        public override string ToString()
        {
            return Mes;
        }
    }
}

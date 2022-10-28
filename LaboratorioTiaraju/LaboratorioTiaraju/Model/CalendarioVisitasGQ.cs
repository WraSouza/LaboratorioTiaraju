using System;
using System.Collections.Generic;
using System.Text;

namespace LaboratorioTiaraju.Model
{
    public class CalendarioVisitasGQ : Calendario
    {
        public string ResponsabilityHotel { get; set; }
        public string ResponsabilityMeal { get; set; }
        public string DataFinal { get; set; }
        public string DataChegada { get; set; }
        public List<string> Comentarios { get; set; }
    }
}

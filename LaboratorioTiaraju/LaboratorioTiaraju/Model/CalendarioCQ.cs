using System;
using System.Collections.Generic;
using System.Text;

namespace LaboratorioTiaraju.Model
{
    public class CalendarioCQ
    {        
        public string Mes { get; set; }
        public string Dia { get; set; }
        public string Descricao { get; set; }
        public bool IsFinished { get; set; }
        public bool IsExcluded { get; set; }
        public string FinalizadoPor { get; set; }
        public string MotivoExclusao { get; set; }
    }
}

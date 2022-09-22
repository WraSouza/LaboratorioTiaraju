using System;
using System.Collections.Generic;
using System.Text;

namespace LaboratorioTiaraju.Model
{
    public class CalendarioCQ
    {        
        public string Mes { get; set; }
        public int Dia { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public bool IsFinished { get; set; }
        public bool IsExcluded { get; set; }
        public string FinalizadoPor { get; set; }
        public string MotivoExclusao { get; set; }
        public DateTime DataFinalizacao { get; set; }


    }
}

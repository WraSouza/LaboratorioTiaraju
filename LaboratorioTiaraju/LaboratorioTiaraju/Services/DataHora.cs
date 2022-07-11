using System;
using System.Collections.Generic;
using System.Text;

namespace LaboratorioTiaraju.Services
{
    internal class DataHora
    {
        public static bool VerificaData(DateTime dataDesejada)
        {
            bool confirmacao = false;

            if( dataDesejada >= DateTime.Today)
            {
                confirmacao = true;
            }

            return confirmacao;
        }
    }
}

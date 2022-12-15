using LaboratorioTiaraju.FirebaseServices;
using LaboratorioTiaraju.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioTiaraju.Model
{
    public class CalendarioCQ
    {        
        public string Mes { get; set; }
        public int Dia { get; set; }
        public int Ano { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public bool IsFinished { get; set; }
        public bool IsExcluded { get; set; }
        public string FinalizadoPor { get; set; }
        public string MotivoExclusao { get; set; }
        public string DataFinalizacao { get; set; }


        public async static Task<bool> CadastraCalendario(CalendarioCQ calendario)
        {
            var calendarioService = new CalendarioCQServices();

            bool confirmaCadastro = await calendarioService.CadastrarDadosCalendario(calendario);

            if (confirmaCadastro)
            {
                Mensagem.MensagemCadastroSucesso();
            }

            return true;
        }


    }
    
}

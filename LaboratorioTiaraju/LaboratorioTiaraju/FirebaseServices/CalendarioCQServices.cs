using Firebase.Database;
using Firebase.Database.Query;
using LaboratorioTiaraju.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioTiaraju.FirebaseServices
{
    internal class CalendarioCQServices
    {
        FirebaseClient firebase;

        public CalendarioCQServices()
        {
            firebase = new FirebaseClient("https://tiaraju-afa0a-default-rtdb.firebaseio.com/");
        }

        public async Task<bool> CadastrarDadosCalendario(CalendarioCQ calendario)
        {
            await firebase.Child("CalendarioCQ")
                .PostAsync(new CalendarioCQ()
                {
                    Dia = calendario.Dia,
                    Mes = calendario.Mes,
                    //DataColeta = calendario.DataColeta,
                    Descricao = calendario.Descricao
                });

            return true;
        }

        public async Task<List<CalendarioCQ>> RetornaInformacoes()
        {
            return (await firebase.Child("CalendarioCQ")
                .OnceAsync<CalendarioCQ>()).Select(item => new CalendarioCQ
                {
                    Dia = item.Object.Dia,
                    Mes = item.Object.Mes,                    
                    Descricao = item.Object.Descricao

                }).ToList();
        }

        public async Task<List<CalendarioCQ>> RetornaCalendarioAbril()
        {
            var todosCalendarios = await RetornaInformacoes();

            await firebase
                .Child("CalendarioCQ")
                .OnceAsync<CalendarioCQ>();

            return todosCalendarios.Where(m => m.Mes == "Abril").ToList();
        }



    }
}

using Firebase.Database;
using LaboratorioTiaraju.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioTiaraju.FirebaseServices
{
    internal class CalendarioGQServices
    {
        FirebaseClient firebase;

        public CalendarioGQServices()
        {
            firebase = new FirebaseClient("https://tiaraju-afa0a-default-rtdb.firebaseio.com/");
        }

        public async Task<List<CalendarioVisitasGQ>> RetornaInformacoes()
        {
            return (await firebase.Child("CalendarioGQ")
                .OnceAsync<CalendarioVisitasGQ>()).Select(item => new CalendarioVisitasGQ
                {
                    Dia = item.Object.Dia,
                    Mes = item.Object.Mes,
                    Descricao = item.Object.Descricao,
                    IsFinished = item.Object.IsFinished,
                    IsExcluded = item.Object.IsExcluded,
                    FinalizadoPor = item.Object.FinalizadoPor,
                    MotivoExclusao = item.Object.MotivoExclusao,
                    Titulo = item.Object.Titulo

                }).ToList();
        }

        public async Task<List<CalendarioVisitasGQ>> RetornaCalendariosNaoFinalizados()
        {
            var todosCalendarios = await RetornaInformacoes();

            await firebase
                .Child("CalendarioGQ")
                .OnceAsync<CalendarioVisitasGQ>();

            return todosCalendarios.Where(m => m.IsFinished == false && m.IsExcluded == false).ToList();
        }
    }
}

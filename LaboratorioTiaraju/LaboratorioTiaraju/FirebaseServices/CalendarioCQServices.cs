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

        public async Task<bool> GetCalendarioCQStatus(string dia, string mes, string descricao)
        {
            var user = (await firebase.Child("CalendarioCQ")
               .OnceAsync<CalendarioCQ>())
               .Where(u => u.Object.Dia == dia && u.Object.Mes == mes && u.Object.Descricao == descricao)
               .FirstOrDefault();

            return user.Object.IsFinished;
        }

        public async Task<List<CalendarioCQ>> RetornaCalendarioEspecifico(string dia, string mes, string descricao)
        {
            var calendarios = await RetornaInformacoes();
            await firebase
                .Child("CalendarioCQ")
                .OnceAsync<CalendarioCQ>();

            return calendarios.Where(a => a.Dia == dia && a.Mes == mes && a.Descricao == descricao).ToList();
        }

        public async Task<bool> ApagarDadosCalendario(CalendarioCQ calendario)
        {
            var dados = RetornaInformacoes();

            var toDeleteCalendar = (await firebase
              .Child("CalendrioCQ")
              .OnceAsync<CalendarioCQ>()).Where(a => a.Object.Mes == calendario.Mes
              && a.Object.Dia == calendario.Dia
              && a.Object.Descricao == calendario.Descricao).FirstOrDefault();

            await firebase.Child("CalendarioCQ").Child(toDeleteCalendar.Key).DeleteAsync();

            return true;
        }

        public async Task<bool> CadastrarDadosCalendario(CalendarioCQ calendario)
        {
            await firebase.Child("CalendarioCQ")
                .PostAsync(new CalendarioCQ()
                {
                    Dia = calendario.Dia,
                    Mes = calendario.Mes,                    
                    Descricao = calendario.Descricao,
                    FinalizadoPor = calendario.FinalizadoPor
                });

            return true;
        }

        public async Task<bool> FinalizarCalendario(string dia, string mes, string descricao,string finalizadoPor)
        {
            var calendarios = await RetornaInformacoes();
            var toUpdatePerson = (await firebase
              .Child("CalendarioCQ")
              .OnceAsync<CalendarioCQ>()).Where(a => a.Object.Dia == dia && a.Object.Mes == mes && a.Object.Descricao == descricao).FirstOrDefault();

            toUpdatePerson.Object.IsFinished = true;
            toUpdatePerson.Object.FinalizadoPor = finalizadoPor;

            await firebase
           .Child("CalendarioCQ")
           .Child(toUpdatePerson.Key)
           .PutAsync(toUpdatePerson.Object);

            return true;
        }

        public async Task<List<CalendarioCQ>> RetornaInformacoes()
        {
            return (await firebase.Child("CalendarioCQ")
                .OnceAsync<CalendarioCQ>()).Select(item => new CalendarioCQ
                {
                    Dia = item.Object.Dia,
                    Mes = item.Object.Mes,                    
                    Descricao = item.Object.Descricao,
                    IsFinished = item.Object.IsFinished

                }).ToList();
        }

        public async Task<List<CalendarioCQ>> RetornaCalendariosNaoFinalizados()
        {
            var todosCalendarios = await RetornaInformacoes();

            await firebase
                .Child("CalendarioCQ")
                .OnceAsync<CalendarioCQ>();

            return todosCalendarios.Where(m => m.IsFinished == false).ToList();
        }



    }
}

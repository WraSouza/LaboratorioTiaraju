using Firebase.Database;
using Firebase.Database.Query;
using LaboratorioTiaraju.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

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

        public async Task<bool> GetCalendarioCQStatusExcluded(string dia, string mes, string descricao)
        {
            var user = (await firebase.Child("CalendarioCQ")
               .OnceAsync<CalendarioCQ>())
               .Where(u => u.Object.Dia == dia && u.Object.Mes == mes && u.Object.Descricao == descricao)
               .FirstOrDefault();

            return user.Object.IsExcluded;
        }

        public async Task<List<CalendarioCQ>> RetornaCalendarioEspecifico(string dia, string mes, string descricao)
        {
            var calendarios = await RetornaInformacoes();
            await firebase
                .Child("CalendarioCQ")
                .OnceAsync<CalendarioCQ>();

            return calendarios.Where(a => a.Dia == dia && a.Mes == mes && a.Descricao == descricao && a.IsExcluded == false && a.IsFinished == false).ToList();
        }

        public async Task<List<CalendarioCQ>> RetornaCalendarioFinalizadoEspecifico(string dia, string mes, string descricao)
        {
            var calendarios = await RetornaInformacoes();
            await firebase
                .Child("CalendarioCQ")
                .OnceAsync<CalendarioCQ>();

            return calendarios.Where(a => a.Dia == dia && a.Mes == mes && a.Descricao == descricao && a.IsFinished == true).ToList();
        }

        public async Task<List<CalendarioCQ>> RetornaCalendarioExcluidoEspecifico(string dia, string mes, string descricao)
        {
            var calendarios = await RetornaInformacoes();
            await firebase
                .Child("CalendarioCQ")
                .OnceAsync<CalendarioCQ>();

            return calendarios.Where(a => a.Dia == dia && a.Mes == mes && a.Descricao == descricao && a.IsExcluded == true).ToList();
        }

        public async Task<bool> ExcluirCalendario(string dia, string mes, string descricao, string finalizadoPor, string motivoExclusao)
        {
            var calendarios = await RetornaInformacoes();
            var toUpdateCalendario = (await firebase
              .Child("CalendarioCQ")
              .OnceAsync<CalendarioCQ>()).Where(a => a.Object.Dia == dia && a.Object.Mes == mes && a.Object.Descricao == descricao).FirstOrDefault();

            toUpdateCalendario.Object.IsExcluded = true;
            toUpdateCalendario.Object.FinalizadoPor = finalizadoPor;
            toUpdateCalendario.Object.MotivoExclusao = motivoExclusao;

            await firebase
           .Child("CalendarioCQ")
           .Child(toUpdateCalendario.Key)
           .PutAsync(toUpdateCalendario.Object);

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
                    IsFinished = calendario.IsFinished,
                    IsExcluded = calendario.IsExcluded,
                    FinalizadoPor = calendario.FinalizadoPor,
                    MotivoExclusao = " ",
                    Titulo = calendario.Titulo
                });

            return true;
        }

        public async Task<bool> AtualizaDadosCalendario(string dia, string mes, string descricao)
        {
            var calendarios = await RetornaInformacoes();
            string diaCalendario = Preferences.Get("DiaCalendario", "default_value");
            string mesCalendario = Preferences.Get("MesCalendario", "default_value");
            string descricaoCalendario = Preferences.Get("DescricaoCalendario", "default_value");

            var toUpdateCalendar = (await firebase
              .Child("CalendarioCQ")
              .OnceAsync<CalendarioCQ>()).Where(a => a.Object.Dia == diaCalendario && a.Object.Mes == mesCalendario && a.Object.Descricao == descricaoCalendario).FirstOrDefault();

            toUpdateCalendar.Object.Dia = dia;
            toUpdateCalendar.Object.Mes = mes;
            toUpdateCalendar.Object.Descricao = descricao;

            await firebase
           .Child("CalendarioCQ")
           .Child(toUpdateCalendar.Key)
           .PutAsync(toUpdateCalendar.Object);

            return true;

        }

        public async Task<bool> FinalizarCalendario(string dia, string mes, string descricao,string finalizadoPor)
        {
            var calendarios = await RetornaInformacoes();
            var toUpdateCalendar = (await firebase
              .Child("CalendarioCQ")
              .OnceAsync<CalendarioCQ>()).Where(a => a.Object.Dia == dia && a.Object.Mes == mes && a.Object.Descricao == descricao).FirstOrDefault();

            toUpdateCalendar.Object.IsFinished = true;
            toUpdateCalendar.Object.FinalizadoPor = finalizadoPor;

            await firebase
           .Child("CalendarioCQ")
           .Child(toUpdateCalendar.Key)
           .PutAsync(toUpdateCalendar.Object);

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
                    IsFinished = item.Object.IsFinished,
                    IsExcluded = item.Object.IsExcluded,
                    FinalizadoPor = item.Object.FinalizadoPor,
                    MotivoExclusao = item.Object.MotivoExclusao,
                    Titulo = item.Object.Titulo

                }).ToList();
        }        

        public async Task<List<CalendarioCQ>> RetornaCalendariosNaoFinalizados()
        {
            var todosCalendarios = await RetornaInformacoes();

            await firebase
                .Child("CalendarioCQ")
                .OnceAsync<CalendarioCQ>();

            return todosCalendarios.Where(m => m.IsFinished == false && m.IsExcluded == false).ToList();
        }

        public async Task<List<CalendarioCQ>> RetornaCalendariosFinalizados()
        {
            var todosCalendarios = await RetornaInformacoes();

            await firebase
                .Child("CalendarioCQ")
                .OnceAsync<CalendarioCQ>();

            return todosCalendarios.Where(m => m.IsFinished == true && m.IsExcluded == false).ToList();
        }

        public async Task<List<CalendarioCQ>> RetornaCalendariosExcluidos()
        {
            var todosCalendarios = await RetornaInformacoes();

            await firebase
                .Child("CalendarioCQ")
                .OnceAsync<CalendarioCQ>();

            return todosCalendarios.Where(m => m.IsExcluded == true).ToList();
        }



    }
}

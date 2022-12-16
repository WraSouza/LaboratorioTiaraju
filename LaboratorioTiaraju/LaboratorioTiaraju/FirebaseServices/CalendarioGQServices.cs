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
    internal class CalendarioGQServices
    {
        FirebaseClient firebase;

        public CalendarioGQServices()
        {
            firebase = new FirebaseClient("https://tiaraju-afa0a-default-rtdb.firebaseio.com/");
        }

        public async Task<bool> CadastrarDadosCalendario(CalendarioVisitasGQ calendario)
        {
            await firebase.Child("CalendarioVisitasGQ")
                .PostAsync(new CalendarioVisitasGQ()
                {                    
                    Descricao = calendario.Descricao,
                    IsFinished = calendario.IsFinished,
                    IsExcluded = calendario.IsExcluded,
                    FinalizadoPor = calendario.FinalizadoPor,
                    MotivoExclusao = calendario.MotivoExclusao,
                    Titulo = calendario.Titulo,
                    DataFinal = calendario.DataFinal,
                    DataChegada = calendario.DataChegada,
                    ResponsabilityMeal = calendario.ResponsabilityMeal,
                    ResponsabilityHotel = calendario.ResponsabilityHotel,
                    DataFinalizacao = calendario.DataFinalizacao,
                    Dia = calendario.Dia,
                    Mes = calendario.Mes,
                    Comentarios = calendario.Comentarios
                    
                });

            return true;
        }

        public async Task<List<CalendarioVisitasGQ>> RetornaCalendarioVisitasEspecifico(int dia, string mes, string descricao)
        {
            var calendarios = await RetornaInformacoes();
            await firebase
                .Child("CalendarioVisitasGQ")
                .OnceAsync<CalendarioVisitasGQ>();

            return calendarios.Where(a => a.Dia == dia && a.Mes == mes && a.Descricao == descricao && a.IsExcluded == false && a.IsFinished == false).ToList();
        }

        public async Task<List<CalendarioVisitasGQ>> RetornaInformacoes()
        {
            return (await firebase.Child("CalendarioVisitasGQ")
                .OnceAsync<CalendarioVisitasGQ>()).Select(item => new CalendarioVisitasGQ
                {
                    DataChegada = item.Object.DataChegada,
                    DataFinal = item.Object.DataFinal,
                    Descricao = item.Object.Descricao,
                    Titulo = item.Object.Titulo,
                    ResponsabilityHotel = item.Object.ResponsabilityHotel,
                    ResponsabilityMeal = item.Object.ResponsabilityMeal,
                    Dia = item.Object.Dia,
                    Mes = item.Object.Mes,
                    //Descricao = item.Object.Descricao,
                    //IsFinished = item.Object.IsFinished,
                    //IsExcluded = item.Object.IsExcluded,
                    //FinalizadoPor = item.Object.FinalizadoPor,
                    //MotivoExclusao = item.Object.MotivoExclusao,
                    //Titulo = item.Object.Titulo

                }).ToList();
        }

        public async Task<List<CalendarioVisitasGQ>> RetornaCalendariosNaoFinalizados()
        {
            var todosCalendarios = await RetornaInformacoes();

            await firebase
                .Child("CalendarioVisitasGQ")
                .OnceAsync<CalendarioVisitasGQ>();

            return todosCalendarios.Where(m => m.IsFinished == false && m.IsExcluded == false).ToList();
        }
    }
}

using LaboratorioTiaraju.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace LaboratorioTiaraju.ViewModel
{
    class NewCalendarCQViewModel : BaseViewModel
    {
        public List<DiaMes> source;

        readonly List<Mes> mesesAno;

        int count = (DateTime.Today.Month)-1;

        private string _mes;
        public ObservableCollection<DiaMes> DiasMes { get; set; } = new ObservableCollection<DiaMes>();

        public Command IrProximoMes { get; set; }

        public Command IrMesAnterior { get; set; }        

        public string Mes
        {
            get => _mes;
            set
            {
                _mes = value;
                OnPropertyChanged();
            }
        }

        public NewCalendarCQViewModel()
        {
            source = new List<DiaMes>();
            mesesAno = new List<Mes>();
            IrProximoMes = new Command( () => MostrarProximoMes());
            IrMesAnterior = new Command(() => MostrarMesAnterior());
            
            CriarDias();
            CriarMeses();

        }

        void MostrarProximoMes()
        {
            
            source.Clear();
            DiasMes.Clear();

            count++;

            if (count < 12)
            {
                string mesSelecionado = mesesAno[count].Meses;

                Mes = mesSelecionado;
            }
            else
            {
                count = 12;
            }

            //source = new List<DiaMes>();

            //Mostrar os dias do mês selecionado
            DateTime data = DateTime.Today;

            //DateTime com o primeiro dia do mês
            DateTime primeiroDiaDoMes = new DateTime(data.Year, data.Month, 1);

            DateTime ultimoDiaDoMes = new DateTime(data.Year, (count+1), DateTime.DaysInMonth(data.Year, (count+1)));
            for (int i = 0; i < ultimoDiaDoMes.Day; i++)
            {
                
                source.Add(new DiaMes
                {
                    diaMes = (i + 1)
                });
            }           

            DiasMes = new ObservableCollection<DiaMes>(source);

        }

        void CriarDiasMeses(int mesDesejado)
        {

        }

        void MostrarMesAnterior()
        {
            count--;
           
            if (count == 11)
            {
                string mesSelecionado = mesesAno[count-1].Meses;

                count = 10;

                Mes = mesSelecionado;
            }
            else if(count >= 0)
            {
                string mesSelecionado = mesesAno[count].Meses;

                Mes = mesSelecionado;
            }
            else
            {
                count = 0;
            }

            
        }

        void CriarMeses()
        {
            mesesAno.Add(new Mes()
            {
                Meses = "Janeiro"
            });

            mesesAno.Add(new Mes()
            {
                Meses = "Fevereiro"
            });

            mesesAno.Add(new Mes()
            {
                Meses = "Março"
            });

            mesesAno.Add(new Mes()
            {
                Meses = "Abril"
            });

            mesesAno.Add(new Mes()
            {
                Meses = "Maio"
            });

            mesesAno.Add(new Mes()
            {
                Meses = "Junho"
            });

            mesesAno.Add(new Mes()
            {
                Meses = "Julho"
            });

            mesesAno.Add(new Mes()
            {
                Meses = "Agosto"
            });

            mesesAno.Add(new Mes()
            {
                Meses = "Setembro"
            });

            mesesAno.Add(new Mes()
            {
                Meses = "Outubro"
            });

            mesesAno.Add(new Mes()
            {
                Meses = "Novembro"
            });

            mesesAno.Add(new Mes()
            {
                Meses = "Dezembro"
            });

            int count = DateTime.Now.Month;

            string mesSelecionado = mesesAno[count-1].Meses;

            Mes = mesSelecionado;
        }

        void CriarDias()
        {
            //Vamos considerar que a data seja o dia de hoje, mas pode ser qualquer data.
            DateTime data = DateTime.Today;

            //DateTime com o primeiro dia do mês
            DateTime primeiroDiaDoMes = new DateTime(data.Year, data.Month, 1);

            DateTime ultimoDiaDoMes = new DateTime(data.Year, data.Month, DateTime.DaysInMonth(data.Year, data.Month));
            for (int i = 0; i < ultimoDiaDoMes.Day; i++)
            {
                source.Add(new DiaMes
                {
                    diaMes = (i + 1)
                }) ;
            }
            

            DiasMes = new ObservableCollection<DiaMes>(source);
        }
    }

    public class Mes
    {
        public string Meses { get; set; }
        
    }

    public class DiaMes
    {
        public int diaMes { get; set; }
    }
}

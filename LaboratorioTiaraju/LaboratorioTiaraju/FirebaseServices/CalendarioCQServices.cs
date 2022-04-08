using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaboratorioTiaraju.FirebaseServices
{
    internal class CalendarioCQServices
    {
        FirebaseClient firebase;

        public CalendarioCQServices()
        {
            firebase = new FirebaseClient("https://tiaraju-afa0a-default-rtdb.firebaseio.com/");
        }
    }
}

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TelasColetor
{
    public class Mensagens
    {
        public void MostraMensagem(int icon, string mensagem, AlertDialog.Builder builder, string titulo = "Atenção")
        {           
            AlertDialog alerta = builder.Create();
            alerta.SetTitle(titulo);
            alerta.SetIcon(icon);
            alerta.SetMessage(mensagem);
            alerta.SetButton("OK", (s, ev) =>
            {
                Toast.MakeText(Application.Context,"OK", ToastLength.Short).Show();
            });
            alerta.Show();
        }
    }
}
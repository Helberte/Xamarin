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
    [Activity(Label = "MainClassApplication")]
    public class MainClassApplication : Activity
    {
        public void MensagemSairDoAplicativo()
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            AlertDialog alertDialog = builder.Create();
            alertDialog.SetTitle("Pergunta");
            alertDialog.SetMessage("Deseja sair da Aplicação?");
            alertDialog.SetIcon(Resource.Drawable.icons8_question_48);
            alertDialog.SetButton("NÂO", (a, b) => { });
            alertDialog.SetButton2("SIM", (a, b) => System.Diagnostics.Process.GetCurrentProcess().Kill());
            alertDialog.Show();
        }
    }
}
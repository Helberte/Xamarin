using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TelasColetor.Fonte
{
    public static class TransferenciaInformacoes
    {
        public static List<Produtos> produtos;

        public static bool FinalizouCarregamento = false;
        public static ProgressDialog ProgressBar;

        public static void ExibirMensagemCarregando(ProgressDialog progress)
        {
            TransferenciaInformacoes.ProgressBar = progress;

            progress.SetCancelable(true);
            progress.SetMessage("Carregando");
            progress.SetProgressStyle(ProgressDialogStyle.Spinner);
            progress.Progress = 0;
            progress.Max = 100;
            progress.Show();
        }        

        public static string GetDataAleatoria()
        {
            System.Random random = new System.Random();
            string mes = random.Next(1, 12).ToString().PadLeft(2, '0');
            int diasNoMes = DateTime.DaysInMonth(DateTime.Now.Year, Convert.ToInt32(mes));
            string dia = random.Next(1, diasNoMes).ToString().PadLeft(2, '0');

            return $"{dia}/{mes}/{DateTime.Now.Year}";
        }

        public static void MensagemSairDoAplicativo()
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(Application.Context);
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
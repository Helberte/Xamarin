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
    }        
}           
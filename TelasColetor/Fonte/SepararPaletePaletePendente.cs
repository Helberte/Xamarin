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
    [Activity(Label = "SepararPaletePaletePendente")]
    public class SepararPaletePaletePendente : Activity
    {
        TextView textView_filial_palete;
        EditText editText_palete;
        Button separar_palete_pendente_botao_sair;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SepararPaletePaletePendente);

            textView_filial_palete             = FindViewById<TextView>(Resource.Id.textView_filial_palete);
            editText_palete                    = FindViewById<EditText>(Resource.Id.editText_palete);
            separar_palete_pendente_botao_sair = FindViewById<Button>(Resource.Id.separar_palete_pendente_botao_sair);

            textView_filial_palete.Text = Intent.GetStringExtra("filial");
            editText_palete.Text        = Intent.GetStringExtra("etiqueta");

            separar_palete_pendente_botao_sair.Click += Separar_palete_pendente_botao_sair_Click;
        }

        private void Separar_palete_pendente_botao_sair_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            AlertDialog alerta = builder.Create();
            alerta.SetTitle("Pergunta");
            alerta.SetIcon(Resource.Drawable.icons8_question_48);
            alerta.SetMessage("Deseja sair da aplicação?");
            alerta.SetButton("SIM", (s, ev) =>
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            });
            alerta.SetButton2("NÃO", (s, ev) => { });
            alerta.Show();
        }
    }
}
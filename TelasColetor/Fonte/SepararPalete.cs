﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Android.Icu.Text.CaseMap;

namespace TelasColetor.Fonte
{
    [Activity(Label = "SepararPalete")]
    public class SepararPalete : Activity
    {
        DatePicker datePicker;
        Button botao_confirmar;
        Button separar_palete_botao_sair;
        EditText editText_filial;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Faz a ligação deste fonte com a tela de design
            SetContentView(Resource.Layout.Separar_palete);

            datePicker                      = FindViewById<DatePicker>(Resource.Id.datePickerAnoDocumento);
            botao_confirmar                 = FindViewById<Button>(Resource.Id.botao_confirmar);
            separar_palete_botao_sair       = FindViewById<Button>(Resource.Id.separar_palete_botao_sair);
            editText_filial                 = FindViewById<EditText>(Resource.Id.editText_filial);
                                    
            botao_confirmar.Click           += Botao_confirmar_Click;
            datePicker.DateChanged          += DatePicker_DateChanged;
            separar_palete_botao_sair.Click += Separar_palete_botao_sair_Click;
        }

        private void Separar_palete_botao_sair_Click(object sender, EventArgs e)
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

        private void Botao_confirmar_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SeparacaoPaleteDocumentos));
            intent.PutExtra("data", datePicker.DateTime.ToString("dd/MM/yyyy"));
            intent.PutExtra("filial", editText_filial.Text);

            TransferenciaInformacoes.FinalizouCarregamento = false;
            ProgressDialog progress = new ProgressDialog(this);
            TransferenciaInformacoes.ExibirMensagemCarregando(progress);

            StartActivity(intent);
        }

        private void DatePicker_DateChanged(object sender, DatePicker.DateChangedEventArgs e)
        {
            //Toast.MakeText(this, (sender as DatePicker).DateTime.ToString(), ToastLength.Long).Show();
        }
    }
}

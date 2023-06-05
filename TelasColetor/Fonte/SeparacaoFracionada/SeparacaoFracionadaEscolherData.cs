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

namespace TelasColetor.Fonte.SeparacaoFracionada
{
    [Activity(Label = "SeparacaoFracionadaEscolherData")]
    public class SeparacaoFracionadaEscolherData : Activity
    {
        DatePicker separacao_fracionada_escolher_data_date_picker;
        Button     separacao_fracionada_escolher_data_botao_confirmar;
        Button     separacao_fracionada_escolher_data_botao_sair;
        Button     separacao_fracionada_escolher_data_botao_opcoes;
        EditText   separacao_fracionada_escolher_data_filial;
         
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SeparacaoFracionadaEscolherData);

            separacao_fracionada_escolher_data_date_picker     = FindViewById<DatePicker>(Resource.Id.separacao_fracionada_escolher_data_date_picker);
            separacao_fracionada_escolher_data_botao_confirmar = FindViewById<Button>(Resource.Id.separacao_fracionada_escolher_data_botao_confirmar);
            separacao_fracionada_escolher_data_filial          = FindViewById<EditText>(Resource.Id.separacao_fracionada_escolher_data_filial);

            // define data minima e máxima do DatePicker
            separacao_fracionada_escolher_data_date_picker.MinDate = (long)(new DateTime(2023, 01, 01, 0, 0, 0).Date - new DateTime(1970, 01, 01, 0, 0, 0).Date).TotalMilliseconds;
            separacao_fracionada_escolher_data_date_picker.MaxDate = (long)(new DateTime(2023, 12, 31, 0, 0, 0).Date - new DateTime(1970, 01, 01, 0, 0, 0).Date).TotalMilliseconds;

            separacao_fracionada_escolher_data_botao_confirmar.Click += Separacao_fracionada_escolher_data_botao_confirmar_Click;
        }

        private void Separacao_fracionada_escolher_data_botao_confirmar_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SeparacaoFracionadaListaDocumentos));
            intent.PutExtra("filial", separacao_fracionada_escolher_data_filial.Text);
            intent.PutExtra("data", separacao_fracionada_escolher_data_date_picker.DateTime.ToString("dd/MM/yyyy"));

            StartActivity(intent);
        }
    }
}
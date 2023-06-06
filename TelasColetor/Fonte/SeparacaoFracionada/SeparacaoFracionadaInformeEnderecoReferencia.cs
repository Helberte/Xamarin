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
    [Activity(Label = "SeparacaoFracionadaInformeEnderecoReferencia")]
    public class SeparacaoFracionadaInformeEnderecoReferencia : Activity
    {
        TextView separacao_fracionada_informe_endereco_referencia_filial;
        TextView separacao_fracionada_informe_endereco_referencia_etiqueta;
        EditText separacao_fracionada_informe_endereco_referencia_edit_endereco;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SeparacaoFracionadaInformeEnderecoReferencia);

            separacao_fracionada_informe_endereco_referencia_filial        = FindViewById<TextView>(Resource.Id.separacao_fracionada_informe_endereco_referencia_filial);
            separacao_fracionada_informe_endereco_referencia_etiqueta      = FindViewById<TextView>(Resource.Id.separacao_fracionada_informe_endereco_referencia_etiqueta);
            separacao_fracionada_informe_endereco_referencia_edit_endereco = FindViewById<EditText>(Resource.Id.separacao_fracionada_informe_endereco_referencia_edit_endereco);

            separacao_fracionada_informe_endereco_referencia_filial.Text   = Intent.GetStringExtra("filial");
            separacao_fracionada_informe_endereco_referencia_etiqueta.Text = (new Random()).Next(10000, 55555).ToString() + (new Random()).Next(5000, 9555).ToString();

            separacao_fracionada_informe_endereco_referencia_edit_endereco.TextChanged += Separacao_fracionada_informe_endereco_referencia_edit_endereco_TextChanged;
        }

        private void Separacao_fracionada_informe_endereco_referencia_edit_endereco_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if ((sender as EditText).Text.ToUpper().Trim().Replace(" ","") == separacao_fracionada_informe_endereco_referencia_etiqueta.Text.ToUpper())
            {
                Intent intent = new Intent(this, typeof(SeparacaoFracionadaListaBaias));
                intent.PutExtra("filial", separacao_fracionada_informe_endereco_referencia_filial.Text);
                intent.PutExtra("etiqueta", separacao_fracionada_informe_endereco_referencia_etiqueta.Text);

                StartActivity(intent);
            }
        }
    }
}
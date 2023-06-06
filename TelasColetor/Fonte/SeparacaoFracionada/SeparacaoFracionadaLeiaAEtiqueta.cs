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
    [Activity(Label = "SeparacaoFracionadaLeiaAEtiqueta")]
    public class SeparacaoFracionadaLeiaAEtiqueta : Activity
    {
        TextView separacao_fracionada_leia_a_etiqueta_filial;
        Button separacao_fracionada_leia_a_etiqueta_imprimir;

        string filial = "";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SeparacaoFracionadaLeiaAEtiqueta);

            separacao_fracionada_leia_a_etiqueta_filial   = FindViewById<TextView>(Resource.Id.separacao_fracionada_leia_a_etiqueta_filial);
            separacao_fracionada_leia_a_etiqueta_imprimir = FindViewById<Button>(Resource.Id.separacao_fracionada_leia_a_etiqueta_imprimir);

            filial = Intent.GetStringExtra("filial");

            separacao_fracionada_leia_a_etiqueta_filial.Text = Intent.GetStringExtra("filial");

            separacao_fracionada_leia_a_etiqueta_imprimir.Click += Separacao_fracionada_leia_a_etiqueta_imprimir_Click;
        }

        private void Separacao_fracionada_leia_a_etiqueta_imprimir_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SeparacaoFracionadaInformeEnderecoReferencia));
            intent.PutExtra("filial", filial);

            StartActivity(intent);
        }
    }
}
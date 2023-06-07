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

namespace TelasColetor.Fonte.Descarregamento
{
    [Activity(Label = "DescarregamentoLeiaDoca")]
    public class DescarregamentoLeiaDoca : Activity
    {
        EditText descarregamento_leia_doca_leia_uma_doca;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.DescarregamentoLeiaDoca);

            descarregamento_leia_doca_leia_uma_doca = FindViewById<EditText>(Resource.Id.descarregamento_leia_doca_leia_uma_doca);

            descarregamento_leia_doca_leia_uma_doca.RequestFocus();
            descarregamento_leia_doca_leia_uma_doca.TextChanged += Descarregamento_leia_doca_leia_uma_doca_TextChanged;
        }

        private void Descarregamento_leia_doca_leia_uma_doca_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if ((sender as EditText).Text.Replace(" ", "") == "1234")
            {
                Intent intent = new Intent(this, typeof(DescarregamentoListaPlacas));
                StartActivity(intent);
            }
        }
    }
}
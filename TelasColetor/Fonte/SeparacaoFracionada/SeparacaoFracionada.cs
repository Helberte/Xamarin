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
    [Activity(Label = "SeparacaoFracionada")]
    public class SeparacaoFracionada : Activity
    {
        EditText editText_separacao_fracionada_leia_endereco;
        EditText editText_separacao_fracionada_palete;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SeparacaoFracionada);

            editText_separacao_fracionada_leia_endereco = FindViewById<EditText>(Resource.Id.editText_separacao_fracionada_leia_endereco);
            editText_separacao_fracionada_palete        = FindViewById<EditText>(Resource.Id.editText_separacao_fracionada_palete);

            editText_separacao_fracionada_leia_endereco.TextChanged += EditText_separacao_fracionada_leia_endereco_TextChanged;
        }

        private void EditText_separacao_fracionada_leia_endereco_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if ((sender as TextView).Text.ToUpper().ToString().Trim().Replace(" ", "") == "1234")
            {
                Intent intent = new Intent(this, typeof(SeparacaoFracionadaEscolherData));
                
                StartActivity(intent);
            }
        }
    }
}
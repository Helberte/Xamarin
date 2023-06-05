using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using static Xamarin.Essentials.Platform;

namespace TelasColetor.Fonte.SeparacaoFracionada
{
    [Activity(Label = "SeparacaoFracionadaLeituraPorCaixa")]
    public class SeparacaoFracionadaLeituraPorCaixa : Activity
    {
        TextView separacao_fracionada_produtos_por_caixa_endereco;
        EditText separacao_fracionada_produtos_por_caixa_leia_endereco;

        Produtos produto;
        string filial, data;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SeparacaoFracionadaLeituraPorCaixa);

            separacao_fracionada_produtos_por_caixa_endereco = FindViewById<TextView>(Resource.Id.separacao_fracionada_produtos_por_caixa_endereco);
            separacao_fracionada_produtos_por_caixa_leia_endereco = FindViewById<EditText>(Resource.Id.separacao_fracionada_produtos_por_caixa_leia_endereco);

            produto = JsonConvert.DeserializeObject<Produtos>(Intent.GetStringExtra("produto"));

            separacao_fracionada_produtos_por_caixa_endereco.Text = produto.Localizacao[..2] + "-" + 
                                                                    produto.Localizacao.Substring(2, 2) + "-" + 
                                                                    produto.Localizacao.Substring(4, 2) + "-" + 
                                                                    produto.Localizacao[6..];

            separacao_fracionada_produtos_por_caixa_leia_endereco.TextChanged += Separacao_fracionada_produtos_por_caixa_leia_endereco_TextChanged;


            filial = Intent.GetStringExtra("filial");
            data   = Intent.GetStringExtra("data");
        }

        private void Separacao_fracionada_produtos_por_caixa_leia_endereco_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if ((sender as EditText).Text.Trim().Replace(" ", "") == produto.Localizacao)
            {
                string produto = JsonConvert.SerializeObject(this.produto);

                Intent intent = new Intent(this, typeof(SeparacaoFracionadaInformacoesDoProduto));
                intent.PutExtra("produto", produto);
                intent.PutExtra("filial", filial);
                intent.PutExtra("data",   data);

                StartActivity(intent);
            }
        }
    }
}
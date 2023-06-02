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

namespace TelasColetor.Fonte
{
    [Activity(Label = "SepararPaleteEtiqueta")]
    public class SepararPaleteEtiqueta : Activity
    {
        TextView textView_produto_etiqueta;
        TextView textView_produto_descricao;
        TextView textView_filial;
        TextView textView_data;
        EditText editText_produto_codigo;
        EditText editText_produto_referencia;
        EditText editText_produto_qtd_embalagem;
        EditText editText_produto_lote;
        EditText editText_produto_validade;
        EditText editText_produto_endereco;
        Button   bt_bloquear;
        Produtos produtos; 

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SepararPaleteEtiqueta);

            string produto = Intent.GetStringExtra("produto");
            produtos = JsonConvert.DeserializeObject<Produtos>(produto);

            textView_produto_etiqueta      = FindViewById<TextView>(Resource.Id.textView_produto_etiqueta);
            textView_produto_descricao     = FindViewById<TextView>(Resource.Id.textView_produto_descricao);
            textView_filial                = FindViewById<TextView>(Resource.Id.textView_filial);
            textView_data                  = FindViewById<TextView>(Resource.Id.textView_data);
            editText_produto_codigo        = FindViewById<EditText>(Resource.Id.editText_produto_codigo);
            editText_produto_referencia    = FindViewById<EditText>(Resource.Id.editText_produto_referencia);
            editText_produto_qtd_embalagem = FindViewById<EditText>(Resource.Id.editText_produto_qtd_embalagem);
            editText_produto_lote          = FindViewById<EditText>(Resource.Id.editText_produto_lote);
            editText_produto_validade      = FindViewById<EditText>(Resource.Id.editText_produto_validade);
            editText_produto_endereco      = FindViewById<EditText>(Resource.Id.editText_produto_endereco);
            bt_bloquear                    = FindViewById<Button>(Resource.Id.bt_bloquear);

            textView_produto_etiqueta.Text      = produtos.Etiqueta;
            textView_produto_descricao.Text     = produtos.Descricao.ToUpper();
            editText_produto_codigo.Text        = produtos.Codigo;
            editText_produto_referencia.Text    = produtos.Referencia;
            editText_produto_qtd_embalagem.Text = produtos.QuantidadeEmbalagem.ToString();
            editText_produto_lote.Text          = produtos.Lote;
            editText_produto_validade.Text      = produtos.Validade;
            editText_produto_endereco.Text      = produtos.Localizacao[..2] + "-" + produtos.Localizacao.Substring(2, 2) + "-" + produtos.Localizacao.Substring(4, 2) + "-" + produtos.Localizacao[6..];
            textView_filial.Text                = Intent.GetStringExtra("filial");
            textView_data.Text                  = Intent.GetStringExtra("data");

            bt_bloquear.Click += Bt_bloquear_Click;
        }

        private void Bt_bloquear_Click(object sender, EventArgs e)
        {
            string strProdutos = JsonConvert.SerializeObject(produtos);

            Intent intent = new Intent(this, typeof(SepararPaleteEtiquetaBloquear));
            intent.PutExtra("produto", strProdutos);
            intent.PutExtra("filial", textView_filial.Text);
            intent.PutExtra("data", textView_data.Text);

            StartActivity(intent);
        }
    }
}
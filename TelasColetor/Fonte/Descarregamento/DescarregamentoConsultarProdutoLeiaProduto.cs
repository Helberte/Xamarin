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
    [Activity(Label = "DescarregamentoConsultarProdutoLeiaProduto")]
    public class DescarregamentoConsultarProdutoLeiaProduto : Activity
    {
        TextView descarregamento_consultar_produto_leia_produto_baia;
        EditText descarregamento_consultar_produto_leia_produto_edit_leia_item;
        Button   descarregamento_consultar_produto_leia_produto_botao_voltar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.DescarregamentoConsultarProdutoLeiaProduto);

            descarregamento_consultar_produto_leia_produto_baia           = FindViewById<TextView>(Resource.Id.descarregamento_consultar_produto_leia_produto_baia);
            descarregamento_consultar_produto_leia_produto_edit_leia_item = FindViewById<EditText>(Resource.Id.descarregamento_consultar_produto_leia_produto_edit_leia_item);
            descarregamento_consultar_produto_leia_produto_botao_voltar   = FindViewById<Button>(Resource.Id.descarregamento_consultar_produto_leia_produto_botao_voltar);

            descarregamento_consultar_produto_leia_produto_baia.Text      = Intent.GetStringExtra("baia");
            descarregamento_consultar_produto_leia_produto_botao_voltar.Click += Descarregamento_consultar_produto_leia_produto_botao_voltar_Click;
        }

        private void Descarregamento_consultar_produto_leia_produto_botao_voltar_Click(object sender, EventArgs e)
        {
            this.Finish();
        }
    }
}
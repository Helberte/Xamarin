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
using static Android.Icu.Text.CaseMap;

namespace TelasColetor.Fonte.SeparacaoFracionada
{
    [Activity(Label = "SeparacaoFracionadaLeiaItem")]
    public class SeparacaoFracionadaLeiaItem : Activity
    {

        #region Declaração de variáveis

        TextView separacao_fracionada_leia_item_endereco;
        EditText separacao_fracionada_leia_item_edit_leia_item;
        EditText separacao_fracionada_leia_item_descricao;
        EditText separacao_fracionada_leia_item_codigo;
        EditText separacao_fracionada_leia_item_referencia;
        EditText separacao_fracionada_leia_item_qtd_embalagem;
        EditText separacao_fracionada_leia_item_lote;
        EditText separacao_fracionada_leia_item_validade;
        EditText separacao_fracionada_leia_item_cod_barras;
        TextView separacao_fracionada_leia_item_unidades;
        TextView separacao_fracionada_leia_item_caixas;
        TextView separacao_fracionada_leia_item_unidades_pendentes;
        TextView separacao_fracionada_leia_item_caixas_pendentes;
        Button separacao_fracionada_leia_item_botao_mais_opcoes;
        Button separacao_fracionada_leia_item_botao_paletizar;

        Produtos produto;

        private string filial;
        private string data;
        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SeparacaoFracionadaLeiaItem);

            separacao_fracionada_leia_item_endereco           = FindViewById<TextView>(Resource.Id.separacao_fracionada_leia_item_endereco);
            separacao_fracionada_leia_item_edit_leia_item     = FindViewById<EditText>(Resource.Id.separacao_fracionada_leia_item_edit_leia_item);
            separacao_fracionada_leia_item_descricao          = FindViewById<EditText>(Resource.Id.separacao_fracionada_leia_item_descricao);
            separacao_fracionada_leia_item_codigo             = FindViewById<EditText>(Resource.Id.separacao_fracionada_leia_item_codigo);
            separacao_fracionada_leia_item_referencia         = FindViewById<EditText>(Resource.Id.separacao_fracionada_leia_item_referencia);
            separacao_fracionada_leia_item_qtd_embalagem      = FindViewById<EditText>(Resource.Id.separacao_fracionada_leia_item_qtd_embalagem);
            separacao_fracionada_leia_item_lote               = FindViewById<EditText>(Resource.Id.separacao_fracionada_leia_item_lote);
            separacao_fracionada_leia_item_validade           = FindViewById<EditText>(Resource.Id.separacao_fracionada_leia_item_validade);
            separacao_fracionada_leia_item_cod_barras         = FindViewById<EditText>(Resource.Id.separacao_fracionada_leia_item_cod_barras);
            separacao_fracionada_leia_item_unidades           = FindViewById<TextView>(Resource.Id.separacao_fracionada_leia_item_unidades);
            separacao_fracionada_leia_item_caixas             = FindViewById<TextView>(Resource.Id.separacao_fracionada_leia_item_caixas);
            separacao_fracionada_leia_item_unidades_pendentes = FindViewById<TextView>(Resource.Id.separacao_fracionada_leia_item_unidades_pendentes);
            separacao_fracionada_leia_item_caixas_pendentes   = FindViewById<TextView>(Resource.Id.separacao_fracionada_leia_item_caixas_pendentes);
            separacao_fracionada_leia_item_botao_mais_opcoes  = FindViewById<Button>(Resource.Id.separacao_fracionada_leia_item_botao_mais_opcoes);
            separacao_fracionada_leia_item_botao_paletizar    = FindViewById<Button>(Resource.Id.separacao_fracionada_leia_item_botao_paletizar);

            produto = JsonConvert.DeserializeObject<Produtos>(Intent.GetStringExtra("produto"));

            separacao_fracionada_leia_item_endereco.Text           = $"{produto.Localizacao[..2]}-{produto.Localizacao.Substring(2,2)}-{produto.Localizacao.Substring(4,2)}-{produto.Localizacao[6..]}";           
            separacao_fracionada_leia_item_descricao.Text          = produto.Descricao.ToUpper();
            separacao_fracionada_leia_item_codigo.Text             = produto.Codigo;
            separacao_fracionada_leia_item_referencia.Text         = produto.Referencia;
            separacao_fracionada_leia_item_qtd_embalagem.Text      = produto.QuantidadeEmbalagem.ToString();
            separacao_fracionada_leia_item_lote.Text               = produto.Lote;
            separacao_fracionada_leia_item_validade.Text           = produto.Validade;
            separacao_fracionada_leia_item_cod_barras.Text         = produto.Ean;
            separacao_fracionada_leia_item_unidades.Text           = produto.Unidade.ToString();
            separacao_fracionada_leia_item_caixas.Text             = produto.QuantidadeEmbalagem.ToString();
            separacao_fracionada_leia_item_unidades_pendentes.Text = produto.Pendente.ToString();
            separacao_fracionada_leia_item_caixas_pendentes.Text   = (produto.Pendente + (new Random()).Next(1,10)).ToString();

            filial = Intent.GetStringExtra("filial");
            data   = Intent.GetStringExtra("data");

            separacao_fracionada_leia_item_botao_paletizar.Click += Separacao_fracionada_leia_item_botao_paletizar_Click;
            separacao_fracionada_leia_item_botao_mais_opcoes.Click += Separacao_fracionada_leia_item_botao_mais_opcoes_Click;
        }

        private void Separacao_fracionada_leia_item_botao_paletizar_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);

            AlertDialog alerta = builder.Create();
            alerta.SetTitle("Pergunta");
            alerta.SetIcon(Resource.Drawable.icons8_question_48);
            alerta.SetMessage("Deseja fechar o palete com os produtos separados?");
            alerta.SetButton("NÂO", (s, ev) =>
            {
                Toast.MakeText(Application.Context, "NÂO", ToastLength.Short).Show();
            });
            alerta.SetButton2("SIM", (s, ev) =>
            {
                // chamar tela de ultima leitura
                string produtoUltimaLeitura = JsonConvert.SerializeObject(produto);

                Intent intent = new Intent(this, typeof(SeparacaoFracionadaUltimaLeitura));
                intent.PutExtra("produto", produtoUltimaLeitura);
                intent.PutExtra("filial", filial);
                intent.PutExtra("data", data);

                StartActivity(intent);
            });
            alerta.Show();
        }

        private void Separacao_fracionada_leia_item_botao_mais_opcoes_Click(object sender, EventArgs e)
        {
            FragmentTransaction fragment = FragmentManager.BeginTransaction();
            DialogLeiaMenu dialogLeiaMenu = new DialogLeiaMenu(filial, data);
            dialogLeiaMenu.Show(fragment, "Dialog Fragment");
        }
    }
}

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

namespace TelasColetor.Fonte.SeparacaoFracionada
{
    [Activity(Label = "SeparacaoFracionadaUltimaLeitura")]
    public class SeparacaoFracionadaUltimaLeitura : Activity
    {
        TextView separacao_fracionada_ultima_leitura_filial;
        EditText separacao_fracionada_ultima_leitura_descricao;
        EditText separacao_fracionada_ultima_leitura_codigo;
        EditText separacao_fracionada_ultima_leitura_referencia;
        EditText separacao_fracionada_ultima_leitura_qtd_embalagem;
        EditText separacao_fracionada_ultima_leitura_cod_barras_unidade;
        EditText separacao_fracionada_ultima_leitura_cod_barras_caixa;
        EditText separacao_fracionada_ultima_leitura_data_leitura;
        EditText separacao_fracionada_ultima_leitura_unidade;
        EditText separacao_fracionada_ultima_leitura_caixa;
        EditText separacao_fracionada_ultima_leitura_validade;

        Produtos produto;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SeparacaoFracionadaUltimaLeitura);

            separacao_fracionada_ultima_leitura_filial             = FindViewById<TextView>(Resource.Id.separacao_fracionada_ultima_leitura_filial);
            separacao_fracionada_ultima_leitura_descricao          = FindViewById<EditText>(Resource.Id.separacao_fracionada_ultima_leitura_descricao);
            separacao_fracionada_ultima_leitura_codigo             = FindViewById<EditText>(Resource.Id.separacao_fracionada_ultima_leitura_codigo);
            separacao_fracionada_ultima_leitura_referencia         = FindViewById<EditText>(Resource.Id.separacao_fracionada_ultima_leitura_referencia);
            separacao_fracionada_ultima_leitura_qtd_embalagem      = FindViewById<EditText>(Resource.Id.separacao_fracionada_ultima_leitura_qtd_embalagem);
            separacao_fracionada_ultima_leitura_cod_barras_unidade = FindViewById<EditText>(Resource.Id.separacao_fracionada_ultima_leitura_cod_barras_unidade);
            separacao_fracionada_ultima_leitura_cod_barras_caixa   = FindViewById<EditText>(Resource.Id.separacao_fracionada_ultima_leitura_cod_barras_caixa);
            separacao_fracionada_ultima_leitura_data_leitura       = FindViewById<EditText>(Resource.Id.separacao_fracionada_ultima_leitura_data_leitura);
            separacao_fracionada_ultima_leitura_unidade            = FindViewById<EditText>(Resource.Id.separacao_fracionada_ultima_leitura_unidade);
            separacao_fracionada_ultima_leitura_caixa              = FindViewById<EditText>(Resource.Id.separacao_fracionada_ultima_leitura_caixa);
            separacao_fracionada_ultima_leitura_validade           = FindViewById<EditText>(Resource.Id.separacao_fracionada_ultima_leitura_validade);

            produto = JsonConvert.DeserializeObject<Produtos>(Intent.GetStringExtra("produto"));

            separacao_fracionada_ultima_leitura_filial.Text             = Intent.GetStringExtra("filial");
            separacao_fracionada_ultima_leitura_descricao.Text          = produto.Descricao.ToUpper();
            separacao_fracionada_ultima_leitura_codigo.Text             = produto.Codigo;
            separacao_fracionada_ultima_leitura_referencia.Text         = produto.Referencia;
            separacao_fracionada_ultima_leitura_qtd_embalagem.Text      = produto.QuantidadeEmbalagem.ToString();
            separacao_fracionada_ultima_leitura_cod_barras_unidade.Text = produto.Ean;
            separacao_fracionada_ultima_leitura_cod_barras_caixa.Text   = produto.Ean + (new Random()).Next(0,9).ToString();
            separacao_fracionada_ultima_leitura_data_leitura.Text       = DateTime.Now.ToString("dd/MM/yyyy");
            separacao_fracionada_ultima_leitura_unidade.Text            = produto.Unidade.ToString();
            separacao_fracionada_ultima_leitura_caixa.Text              = (produto.QuantidadeEmbalagem / 2).ToString();
            separacao_fracionada_ultima_leitura_validade.Text           = produto.Validade;
        }
    }
}
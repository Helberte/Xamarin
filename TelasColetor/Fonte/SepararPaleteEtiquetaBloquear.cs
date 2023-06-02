using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Android.Icu.Text.CaseMap;

namespace TelasColetor.Fonte
{
    [Activity(Label = "SepararPaleteEtiquetaBloquear")]
    public class SepararPaleteEtiquetaBloquear : Activity
    {
        Spinner spinner_motivo_bloqueio;
        Button bt_confirmar_bloqueio;

        TextView separar_palete_etiqueta_bloquear_filial;
        TextView separar_palete_etiqueta_bloquear_data;
        TextView separar_palete_etiqueta_bloquear_produto_etiqueta;
        TextView separar_palete_etiqueta_bloquear_produto_descricao;
        EditText separar_palete_etiqueta_bloquear_produto_codigo;
        EditText separar_palete_etiqueta_bloquear_produto_referencia;
        EditText separar_palete_etiqueta_bloquear_qtd_embalagem;
        EditText separar_palete_etiqueta_bloquear_produto_lote;
        EditText separar_palete_etiqueta_bloquear_produto_validade;
        EditText separar_palete_etiqueta_bloquear_produto_endereco;
        Button   separar_palete_bloquear_botao_voltar;

        Produtos produto;
        int posicao = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SepararPaleteEtiquetaBloquear);

            // Deserializa a string 
            produto = JsonConvert.DeserializeObject<Produtos>(Intent.GetStringExtra("produto"));

            spinner_motivo_bloqueio = FindViewById<Spinner>(Resource.Id.spinner_motivo_bloqueio);
            bt_confirmar_bloqueio   = FindViewById<Button>(Resource.Id.bt_confirmar_bloqueio);

            separar_palete_etiqueta_bloquear_filial             = FindViewById<TextView>(Resource.Id.separar_palete_etiqueta_bloquear_filial);
            separar_palete_etiqueta_bloquear_data               = FindViewById<TextView>(Resource.Id.separar_palete_etiqueta_bloquear_data);
            separar_palete_etiqueta_bloquear_produto_etiqueta   = FindViewById<TextView>(Resource.Id.separar_palete_etiqueta_bloquear_produto_etiqueta);
            separar_palete_etiqueta_bloquear_produto_descricao  = FindViewById<TextView>(Resource.Id.separar_palete_etiqueta_bloquear_produto_descricao);
            separar_palete_etiqueta_bloquear_produto_codigo     = FindViewById<EditText>(Resource.Id.separar_palete_etiqueta_bloquear_produto_codigo);
            separar_palete_etiqueta_bloquear_produto_referencia = FindViewById<EditText>(Resource.Id.separar_palete_etiqueta_bloquear_produto_referencia);
            separar_palete_etiqueta_bloquear_qtd_embalagem      = FindViewById<EditText>(Resource.Id.separar_palete_etiqueta_bloquear_qtd_embalagem);
            separar_palete_etiqueta_bloquear_produto_lote       = FindViewById<EditText>(Resource.Id.separar_palete_etiqueta_bloquear_produto_lote);
            separar_palete_etiqueta_bloquear_produto_validade   = FindViewById<EditText>(Resource.Id.separar_palete_etiqueta_bloquear_produto_validade);
            separar_palete_etiqueta_bloquear_produto_endereco   = FindViewById<EditText>(Resource.Id.separar_palete_etiqueta_bloquear_produto_endereco);
            separar_palete_bloquear_botao_voltar                = FindViewById<Button>(Resource.Id.separar_palete_bloquear_botao_voltar);

            separar_palete_etiqueta_bloquear_filial.Text             = Intent.GetStringExtra("filial");
            separar_palete_etiqueta_bloquear_data.Text               = Intent.GetStringExtra("data");
            separar_palete_etiqueta_bloquear_produto_etiqueta.Text   = produto.Etiqueta;
            separar_palete_etiqueta_bloquear_produto_descricao.Text  = produto.Descricao.ToUpper();
            separar_palete_etiqueta_bloquear_produto_codigo.Text     = produto.Codigo;
            separar_palete_etiqueta_bloquear_produto_referencia.Text = produto.Referencia;
            separar_palete_etiqueta_bloquear_qtd_embalagem.Text      = produto.QuantidadeEmbalagem.ToString();
            separar_palete_etiqueta_bloquear_produto_lote.Text       = produto.Lote;
            separar_palete_etiqueta_bloquear_produto_validade.Text   = produto.Validade;
            separar_palete_etiqueta_bloquear_produto_endereco.Text   = produto.Localizacao[..2] + "-" + produto.Localizacao.Substring(2, 2) + "-" + produto.Localizacao.Substring(4, 2) + "-" + produto.Localizacao[6..];

            PopulaSpinner(spinner_motivo_bloqueio);

            spinner_motivo_bloqueio.ItemSelected       += Spinner_motivo_bloqueio_ItemSelected;
            bt_confirmar_bloqueio.Click                += Bt_confirmar_bloqueio_Click;
            separar_palete_bloquear_botao_voltar.Click += Separar_palete_bloquear_botao_voltar_Click;
        }

        private void Separar_palete_bloquear_botao_voltar_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void Bt_confirmar_bloqueio_Click(object sender, EventArgs e)
        {
            ArrayAdapter<string> arrayAdapter = spinner_motivo_bloqueio.Adapter as ArrayAdapter<string>;

            string mensagem = "Deseja realmente bloquear o estoque deste produto?\n\nMotivo: " + arrayAdapter.GetItem(posicao);

            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            AlertDialog alerta = builder.Create();
            alerta.SetTitle("Pergunta");
            alerta.SetIcon(Resource.Drawable.icons8_question_48);
            alerta.SetMessage(mensagem);
            alerta.SetButton("SIM", (s, ev) =>
            {
                // chama o layout de lista de baias
                Intent intent = new Intent(this, typeof(SepararPaleteListaDeBaias));
                intent.PutExtra("filial", separar_palete_etiqueta_bloquear_filial.Text);
                intent.PutExtra("etiqueta", separar_palete_etiqueta_bloquear_produto_etiqueta.Text);

                StartActivity(intent);
            });
            alerta.SetButton2("NÂO", (s, ev) =>
            {
                Toast.MakeText(Application.Context, "NÂO", ToastLength.Short).Show();
            });
            alerta.Show();
        }

        /// <summary>
        /// Este método é chamado quando um item é selecionado no spinner
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>Helberte Costa Arruda, 01/06/2023</remarks>
        private void Spinner_motivo_bloqueio_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            posicao = e.Position;
        }

        private void PopulaSpinner(Spinner spinner)
        {            
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, MotivosBloqueio());
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
        }

        private string[] MotivosBloqueio()
        {
            string[] temparray = {   "Estoque não encontrado"                                   
                                    ,"quantidade de produtos perdidos"
                                    ,"valor total dessas mercadorias"
                                    ,"percentual de perdas em relação ao estoque total"
                                    ,"percentual do valor em relação ao valor total do estoque."
                                    ,"Fator de conversão incorreto"
                                    ,"Não registrar todas as movimentações" 
                                    ,"Mau armazenamento" 
                                    ,"Falha na segurança" 
                                    ,"Utilização de 'cadastro genérico'" 
                                    ,"Falta de inventários periódicos" 
                                    ,"Não realizar conferências nas entradas" 
                                    ,"Vinculações incorretas" 
                                    ,"Comprar mais que o necessário" };        
            
            return temparray;
        }
    }
}
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TelasColetor.Fonte.SeparacaoFracionada
{
    [Activity(Label = "SeparacaoFracionaProdutosDoDocumento")]
    public class SeparacaoFracionaProdutosDoDocumento : Activity
    {
        #region Declaração de Variáveis

        List<Produtos> produtos;
        RecyclerView separacao_fraciona_produtos_do_documento_recyclerView_documentos;
        Button separacao_fraciona_produtos_do_documento_botao_opcoes;

        TextView separacao_fraciona_produtos_do_documento_filial;
        TextView separacao_fraciona_produtos_do_documento_data;

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SeparacaoFracionaProdutosDoDocumento);

            separacao_fraciona_produtos_do_documento_recyclerView_documentos = FindViewById<RecyclerView>(Resource.Id.separacao_fraciona_produtos_do_documento_recyclerView_documentos);
            separacao_fraciona_produtos_do_documento_botao_opcoes            = FindViewById<Button>(Resource.Id.separacao_fraciona_produtos_do_documento_botao_opcoes);

            separacao_fraciona_produtos_do_documento_filial = FindViewById<TextView>(Resource.Id.separacao_fraciona_produtos_do_documento_filial);
            separacao_fraciona_produtos_do_documento_data   = FindViewById<TextView>(Resource.Id.separacao_fraciona_produtos_do_documento_data);

            separacao_fraciona_produtos_do_documento_filial.Text = Intent.GetStringExtra("filial");
            separacao_fraciona_produtos_do_documento_data.Text   = Intent.GetStringExtra("data");

            separacao_fraciona_produtos_do_documento_botao_opcoes.Click += Separacao_fraciona_produtos_do_documento_botao_opcoes_Click;

            produtos = JsonConvert.DeserializeObject<List<Produtos>>(Intent.GetStringExtra("produtos"));
            GridLayoutManager gridLayoutManager = new GridLayoutManager(this, 1);

            RecyclerAdapter adapter = new RecyclerAdapter(produtos);

            adapter.ItemClick += Adapter_ItemClick;

            separacao_fraciona_produtos_do_documento_recyclerView_documentos.SetLayoutManager(gridLayoutManager);
            separacao_fraciona_produtos_do_documento_recyclerView_documentos.HasFixedSize = true;
            separacao_fraciona_produtos_do_documento_recyclerView_documentos.SetAdapter(adapter);
        }

        /// <summary>
        /// Metodo acionado ao clicar no botão "+Opções", exibe uma caixa de dialogo por cima da tela atual, 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>Helberte Costa Arruda, 03/06/2023</remarks>
        private void Separacao_fraciona_produtos_do_documento_botao_opcoes_Click(object sender, EventArgs e)
        {
            FragmentTransaction fragment = FragmentManager.BeginTransaction();
            Dialog dialog = new Dialog();

            dialog.Show(fragment, "Dialog Fragment");
        }

        private void Adapter_ItemClick(object sender, int e)
        {
            int posicao = e;

            RecyclerAdapter adapter = separacao_fraciona_produtos_do_documento_recyclerView_documentos.GetAdapter() as RecyclerAdapter;
            Produtos produtos = adapter.itens[posicao];

            string produtoSerializado = JsonConvert.SerializeObject(produtos);

            Intent intent = new Intent(this, typeof(SeparacaoFracionadaLeituraPorCaixa));
            intent.PutExtra("produto", produtoSerializado);
            intent.PutExtra("filial", separacao_fraciona_produtos_do_documento_filial.Text);
            intent.PutExtra("data", separacao_fraciona_produtos_do_documento_data.Text);

            StartActivity(intent);
        }

        public class RecyclerAdapter : RecyclerView.Adapter
        {
            public readonly List<Produtos> itens;

            public event EventHandler<int> ItemClick;

            public RecyclerAdapter(List<Produtos> itens)
            {
                this.itens = itens;
            }

            public override int ItemCount
            {
                get
                {
                    return this.itens.Count;
                }
            }

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                Produtos prod = itens[position];

                RecyclerHolder recycler = holder as RecyclerHolder;

                recycler.separacao_fraciona_produtos_do_documento_item_descricao.Text            = prod.Descricao.ToUpper();
                recycler.separacao_fraciona_produtos_do_documento_item_numero_etiqueta.Text      = prod.Etiqueta;
                recycler.separacao_fraciona_produtos_do_documento_item_referencia.Text           = prod.Referencia;
                recycler.separacao_fraciona_produtos_do_documento_item_quantidade_embalagem.Text = prod.QuantidadeEmbalagem.ToString();
                recycler.separacao_fraciona_produtos_do_documento_item_caixas_pendentes.Text     = prod.Pendente.ToString();
                recycler.separacao_fraciona_produtos_do_documento_item_caixas_ean.Text           = prod.Ean;
                recycler.separacao_fraciona_produtos_do_documento_item_caixas_dun.Text           = prod.Dun;
                recycler.separacao_fraciona_produtos_do_documento_item_caixas_rua.Text           = prod.Localizacao[..2];
                recycler.separacao_fraciona_produtos_do_documento_item_caixas_predio.Text        = prod.Localizacao.Substring(2, 2);
                recycler.separacao_fraciona_produtos_do_documento_item_caixas_andar.Text         = prod.Localizacao.Substring(4, 2);
                recycler.separacao_fraciona_produtos_do_documento_item_caixas_posicao.Text       = prod.Localizacao[6..];
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.SeparacaoFracionaProdutosDoDocumentoItem, parent, false);

                RecyclerHolder vh = new RecyclerHolder(itemView, OnClick);
                return vh; ;
            }
            public void OnClick(int position)
            {
                ItemClick?.Invoke(this, position);
            }
        }

        public class RecyclerHolder : RecyclerView.ViewHolder
        {
            public TextView separacao_fraciona_produtos_do_documento_item_descricao             { get; private set; }
            public TextView separacao_fraciona_produtos_do_documento_item_numero_etiqueta       { get; private set; }
            public TextView separacao_fraciona_produtos_do_documento_item_referencia            { get; private set; }
            public TextView separacao_fraciona_produtos_do_documento_item_quantidade_embalagem  { get; private set; }
            public TextView separacao_fraciona_produtos_do_documento_item_caixas_pendentes      { get; private set; }
            public TextView separacao_fraciona_produtos_do_documento_item_caixas_ean            { get; private set; }
            public TextView separacao_fraciona_produtos_do_documento_item_caixas_dun            { get; private set; }
            public TextView separacao_fraciona_produtos_do_documento_item_caixas_rua            { get; private set; }
            public TextView separacao_fraciona_produtos_do_documento_item_caixas_predio         { get; private set; }
            public TextView separacao_fraciona_produtos_do_documento_item_caixas_andar          { get; private set; }
            public TextView separacao_fraciona_produtos_do_documento_item_caixas_posicao        { get; private set; }
            
            public RecyclerHolder(View itemView, Action<int> listener) : base(itemView)
            {
                separacao_fraciona_produtos_do_documento_item_descricao            = itemView.FindViewById<TextView>(Resource.Id.separacao_fraciona_produtos_do_documento_item_descricao);
                separacao_fraciona_produtos_do_documento_item_numero_etiqueta      = itemView.FindViewById<TextView>(Resource.Id.separacao_fraciona_produtos_do_documento_item_numero_etiqueta);
                separacao_fraciona_produtos_do_documento_item_referencia           = itemView.FindViewById<TextView>(Resource.Id.separacao_fraciona_produtos_do_documento_item_referencia);
                separacao_fraciona_produtos_do_documento_item_quantidade_embalagem = itemView.FindViewById<TextView>(Resource.Id.separacao_fraciona_produtos_do_documento_item_quantidade_embalagem);
                separacao_fraciona_produtos_do_documento_item_caixas_pendentes     = itemView.FindViewById<TextView>(Resource.Id.separacao_fraciona_produtos_do_documento_item_caixas_pendentes);
                separacao_fraciona_produtos_do_documento_item_caixas_ean           = itemView.FindViewById<TextView>(Resource.Id.separacao_fraciona_produtos_do_documento_item_caixas_ean);
                separacao_fraciona_produtos_do_documento_item_caixas_dun           = itemView.FindViewById<TextView>(Resource.Id.separacao_fraciona_produtos_do_documento_item_caixas_dun);
                separacao_fraciona_produtos_do_documento_item_caixas_rua           = itemView.FindViewById<TextView>(Resource.Id.separacao_fraciona_produtos_do_documento_item_caixas_rua);
                separacao_fraciona_produtos_do_documento_item_caixas_predio        = itemView.FindViewById<TextView>(Resource.Id.separacao_fraciona_produtos_do_documento_item_caixas_predio);
                separacao_fraciona_produtos_do_documento_item_caixas_andar         = itemView.FindViewById<TextView>(Resource.Id.separacao_fraciona_produtos_do_documento_item_caixas_andar);
                separacao_fraciona_produtos_do_documento_item_caixas_posicao       = itemView.FindViewById<TextView>(Resource.Id.separacao_fraciona_produtos_do_documento_item_caixas_posicao);
               
                itemView.Click += (sender, e) => listener(base.AdapterPosition);
            }
        }
    }
}
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
    [Activity(Label = "SeparacaoFracionadaInformacoesDoProduto")]
    public class SeparacaoFracionadaInformacoesDoProduto : Activity
    {
        RecyclerView separacao_fracionada_informacoes_do_produto_recyclerview;
        TextView separacao_fracionada_informacoes_do_produto_filial;
        TextView separacao_fracionada_informacoes_do_produto_data;

        EditText separacao_fracionada_informacoes_do_produto_descricao;
        EditText separacao_fracionada_informacoes_do_produto_codigo;
        EditText separacao_fracionada_informacoes_do_produto_referencia;
        EditText separacao_fracionada_informacoes_do_produto_qtd_embalagem;
        EditText separacao_fracionada_informacoes_do_produto_endereco;
        EditText separacao_fracionada_informacoes_do_produto_cod_barra;
        TextView separacao_fracionada_informacoes_do_produto_qtd_registrar;
        TextView separacao_fracionada_informacoes_do_produto_unidade;

        Produtos produto;
        Random random;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SeparacaoFracionadaInformacoesDoProduto);
            random = new Random();

            separacao_fracionada_informacoes_do_produto_recyclerview  = FindViewById<RecyclerView>(Resource.Id.separacao_fracionada_informacoes_do_produto_recyclerview);
            separacao_fracionada_informacoes_do_produto_filial        = FindViewById<TextView>(Resource.Id.separacao_fracionada_informacoes_do_produto_filial);
            separacao_fracionada_informacoes_do_produto_data          = FindViewById<TextView>(Resource.Id.separacao_fracionada_informacoes_do_produto_data);
            separacao_fracionada_informacoes_do_produto_descricao     = FindViewById<EditText>(Resource.Id.separacao_fracionada_informacoes_do_produto_descricao);
            separacao_fracionada_informacoes_do_produto_codigo        = FindViewById<EditText>(Resource.Id.separacao_fracionada_informacoes_do_produto_codigo);
            separacao_fracionada_informacoes_do_produto_referencia    = FindViewById<EditText>(Resource.Id.separacao_fracionada_informacoes_do_produto_referencia);
            separacao_fracionada_informacoes_do_produto_qtd_embalagem = FindViewById<EditText>(Resource.Id.separacao_fracionada_informacoes_do_produto_qtd_embalagem);
            separacao_fracionada_informacoes_do_produto_endereco      = FindViewById<EditText>(Resource.Id.separacao_fracionada_informacoes_do_produto_endereco);
            separacao_fracionada_informacoes_do_produto_cod_barra     = FindViewById<EditText>(Resource.Id.separacao_fracionada_informacoes_do_produto_cod_barra);
            separacao_fracionada_informacoes_do_produto_qtd_registrar = FindViewById<TextView>(Resource.Id.separacao_fracionada_informacoes_do_produto_qtd_registrar);
            separacao_fracionada_informacoes_do_produto_unidade       = FindViewById<TextView>(Resource.Id.separacao_fracionada_informacoes_do_produto_unidade);

            separacao_fracionada_informacoes_do_produto_filial.Text   = Intent.GetStringExtra("filial");
            separacao_fracionada_informacoes_do_produto_data.Text     = Intent.GetStringExtra("data");

            produto = JsonConvert.DeserializeObject<Produtos>(Intent.GetStringExtra("produto"));

            separacao_fracionada_informacoes_do_produto_descricao.Text     = produto.Descricao.ToUpper();
            separacao_fracionada_informacoes_do_produto_codigo.Text        = produto.Codigo;
            separacao_fracionada_informacoes_do_produto_referencia.Text    = produto.Referencia;
            separacao_fracionada_informacoes_do_produto_qtd_embalagem.Text = produto.QuantidadeEmbalagem.ToString();
            separacao_fracionada_informacoes_do_produto_endereco.Text      = produto.Localizacao[..2]            + "-" +
                                                                             produto.Localizacao.Substring(2, 2) + "-" +
                                                                             produto.Localizacao.Substring(4, 2) + "-" +
                                                                             produto.Localizacao[6..];

            separacao_fracionada_informacoes_do_produto_cod_barra.Text     = produto.Ean;
            separacao_fracionada_informacoes_do_produto_qtd_registrar.Text = produto.Quantidade_a_registrar.ToString();
            separacao_fracionada_informacoes_do_produto_unidade.Text       = produto.Unidade.ToString();
            
            List<Lote> lotes = new List<Lote>();
            lotes.Add(new Lote() { Nome = "SL", Validade = "08/08/2023" } );

            GridLayoutManager gridLayoutManager = new GridLayoutManager(this, 3);
            RecyclerAdapter adapter = new RecyclerAdapter(lotes);
            adapter.ItemClick += Adapter_ItemClick;
            separacao_fracionada_informacoes_do_produto_recyclerview.SetLayoutManager(gridLayoutManager);
            separacao_fracionada_informacoes_do_produto_recyclerview.HasFixedSize = true;
            separacao_fracionada_informacoes_do_produto_recyclerview.SetAdapter(adapter);
        }

        private void Adapter_ItemClick(object sender, int e)
        {
            string produto = JsonConvert.SerializeObject(this.produto);
            Intent intent = new Intent(this, typeof(SeparacaoFracionadaLeiaItem));

            intent.PutExtra("produto", produto);
            intent.PutExtra("filial", separacao_fracionada_informacoes_do_produto_filial.Text);
            intent.PutExtra("data", separacao_fracionada_informacoes_do_produto_data.Text);

            StartActivity(intent);
        }

        public class RecyclerAdapter : RecyclerView.Adapter
        {
            public readonly List<Lote> itens;

            public event EventHandler<int> ItemClick;

            public RecyclerAdapter(List<Lote> itens)
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
                Lote prod = itens[position];

                RecyclerHolder recycler = holder as RecyclerHolder;

                recycler.separacao_fracionada_informacoes_do_produto_lote.Text = prod.Nome;
                recycler.separacao_fracionada_informacoes_do_produto_validade.Text = prod.Validade;               
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.SeparacaoFracionadaInformacoesDoProdutoItemLote, parent, false);

                RecyclerHolder vh = new RecyclerHolder(itemView, OnClick);
                return vh; 
            }
            public void OnClick(int position)
            {
                ItemClick?.Invoke(this, position);
            }
        }

        public class RecyclerHolder : RecyclerView.ViewHolder
        {
            public TextView separacao_fracionada_informacoes_do_produto_lote { get; private set; }
            public TextView separacao_fracionada_informacoes_do_produto_validade { get; private set; }
            
            public RecyclerHolder(View itemView, Action<int> listener) : base(itemView)
            {
                separacao_fracionada_informacoes_do_produto_lote     = itemView.FindViewById<TextView>(Resource.Id.separacao_fracionada_informacoes_do_produto_lote);
                separacao_fracionada_informacoes_do_produto_validade = itemView.FindViewById<TextView>(Resource.Id.separacao_fracionada_informacoes_do_produto_validade);
                
                itemView.Click += (sender, e) => listener(base.AdapterPosition);
            }
        }

        public class Lote
        {
            public string Nome { get; set; } 
            public string Validade { get; set; }
        }
    }
}
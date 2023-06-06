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
    [Activity(Label = "SeparacaoFracionadaListaProdutosSeparados")]
    public class SeparacaoFracionadaListaProdutosSeparados : Activity
    {
        TextView     separacao_fracionada_lista_produtos_separados_filial;
        RecyclerView separacao_fracionada_lista_produtos_separados_recyclerView_produtos;
        Button       separacao_fracionada_lista_produtos_separados_botao_voltar;
        Button       separacao_fracionada_lista_produtos_separados_botao_avancar;

        List<Produtos> produtos;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SeparacaoFracionadaListaProdutosSeparados);

            separacao_fracionada_lista_produtos_separados_filial                = FindViewById<TextView>(Resource.Id.separacao_fracionada_lista_produtos_separados_filial);
            separacao_fracionada_lista_produtos_separados_recyclerView_produtos = FindViewById<RecyclerView>(Resource.Id.separacao_fracionada_lista_produtos_separados_recyclerView_produtos);
            separacao_fracionada_lista_produtos_separados_botao_voltar          = FindViewById<Button>(Resource.Id.separacao_fracionada_lista_produtos_separados_botao_voltar);
            separacao_fracionada_lista_produtos_separados_botao_avancar         = FindViewById<Button>(Resource.Id.separacao_fracionada_lista_produtos_separados_botao_avancar);

            produtos = JsonConvert.DeserializeObject<List<Produtos>>(Intent.GetStringExtra("produtos"));

            separacao_fracionada_lista_produtos_separados_filial.Text = Intent.GetStringExtra("filial");

            GridLayoutManager gridLayoutManager = new GridLayoutManager(this, 1);

            RecyclerAdapter adapter = new RecyclerAdapter(produtos);
            adapter.ItemClick       += Adapter_ItemClick;
            separacao_fracionada_lista_produtos_separados_botao_avancar.Click += Separacao_fracionada_lista_produtos_separados_botao_avancar_Click;

            separacao_fracionada_lista_produtos_separados_recyclerView_produtos.SetLayoutManager(gridLayoutManager);
            separacao_fracionada_lista_produtos_separados_recyclerView_produtos.HasFixedSize = true;
            separacao_fracionada_lista_produtos_separados_recyclerView_produtos.SetAdapter(adapter);
        }

        private void Separacao_fracionada_lista_produtos_separados_botao_avancar_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SeparacaoFracionadaLeiaAEtiqueta));
            intent.PutExtra("filial", separacao_fracionada_lista_produtos_separados_filial.Text);

            StartActivity(intent);
        }

        private void Adapter_ItemClick(object sender, int e)
        {

        }

        public class RecyclerAdapter : RecyclerView.Adapter
        {
            List<Produtos> produtos;

            public RecyclerAdapter(List<Produtos> produtos)
            {
                this.produtos = produtos;
            }

            public override int ItemCount
            {
                get
                {
                    return produtos.Count;
                }
            }

            public event EventHandler<int> ItemClick;

            public void OnClick(int position)
            {
                ItemClick?.Invoke(this, position);
            }

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                Produtos produto = produtos[position];

                RecyclerHolder recycler = holder as RecyclerHolder;

                recycler.Separacao_fracionada_lista_produtos_separados_descricao.Text            = produto.Descricao.ToUpper();
                recycler.Separacao_fracionada_lista_produtos_separados_codigo.Text               = produto.Codigo;
                recycler.Separacao_fracionada_lista_produtos_separados_quantidade_embalagem.Text = produto.QuantidadeEmbalagem.ToString();
                recycler.Separacao_fracionada_lista_produtos_separados_ean.Text                  = produto.Ean;
                recycler.Separacao_fracionada_lista_produtos_separados_quantidade_unidade.Text   = produto.Unidade.ToString();
                recycler.Separacao_fracionada_lista_produtos_separados_referencia.Text           = produto.Referencia[..5];
                recycler.Separacao_fracionada_lista_produtos_separados_status.Text               = char.ToUpper(produto.Status[0]) + produto.Status[1..];
                recycler.Separacao_fracionada_lista_produtos_separados_dun.Text                  = produto.Dun;
                recycler.Separacao_fracionada_lista_produtos_separados_caixa.Text                = (produto.QuantidadeEmbalagem / 2).ToString();
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.SeparacaoFracionadaListaProdutosSeparadosItem, parent, false);

                RecyclerHolder vh = new RecyclerHolder(itemView, OnClick);
                return vh;
            }
        }

        public class RecyclerHolder : RecyclerView.ViewHolder
        {
            public TextView Separacao_fracionada_lista_produtos_separados_descricao { get; private set; }
            public TextView Separacao_fracionada_lista_produtos_separados_codigo { get; private set; }
            public TextView Separacao_fracionada_lista_produtos_separados_quantidade_embalagem { get; private set; }
            public TextView Separacao_fracionada_lista_produtos_separados_ean { get; private set; }
            public TextView Separacao_fracionada_lista_produtos_separados_quantidade_unidade { get; private set; }
            public TextView Separacao_fracionada_lista_produtos_separados_referencia { get; private set; }
            public TextView Separacao_fracionada_lista_produtos_separados_status { get; private set; }
            public TextView Separacao_fracionada_lista_produtos_separados_dun { get; private set; }
            public TextView Separacao_fracionada_lista_produtos_separados_caixa { get; private set; }

            public RecyclerHolder(View itemView, Action<int> listener) : base(itemView)
            {
                Separacao_fracionada_lista_produtos_separados_descricao            = itemView.FindViewById<TextView>(Resource.Id.separacao_fracionada_lista_produtos_separados_descricao);
                Separacao_fracionada_lista_produtos_separados_codigo               = itemView.FindViewById<TextView>(Resource.Id.separacao_fracionada_lista_produtos_separados_codigo);
                Separacao_fracionada_lista_produtos_separados_quantidade_embalagem = itemView.FindViewById<TextView>(Resource.Id.separacao_fracionada_lista_produtos_separados_quantidade_embalagem);
                Separacao_fracionada_lista_produtos_separados_ean                  = itemView.FindViewById<TextView>(Resource.Id.separacao_fracionada_lista_produtos_separados_ean);
                Separacao_fracionada_lista_produtos_separados_quantidade_unidade   = itemView.FindViewById<TextView>(Resource.Id.separacao_fracionada_lista_produtos_separados_quantidade_unidade);
                Separacao_fracionada_lista_produtos_separados_referencia           = itemView.FindViewById<TextView>(Resource.Id.separacao_fracionada_lista_produtos_separados_referencia);
                Separacao_fracionada_lista_produtos_separados_status               = itemView.FindViewById<TextView>(Resource.Id.separacao_fracionada_lista_produtos_separados_status);
                Separacao_fracionada_lista_produtos_separados_dun                  = itemView.FindViewById<TextView>(Resource.Id.separacao_fracionada_lista_produtos_separados_dun);
                Separacao_fracionada_lista_produtos_separados_caixa                = itemView.FindViewById<TextView>(Resource.Id.separacao_fracionada_lista_produtos_separados_caixa);

                itemView.Click += (sender, e) => listener(base.AdapterPosition);
            }
        }
    }
}
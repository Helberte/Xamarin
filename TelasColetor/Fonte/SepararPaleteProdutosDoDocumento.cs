using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static TelasColetor.Fonte.SeparacaoPaleteDocumentos;

namespace TelasColetor.Fonte
{
    [Activity(Label = "SepararPaleteProdutosDoDocumento")]
    public class SepararPaleteProdutosDoDocumento : Activity
    {        
        RecyclerView recyclerView;
        TextView textView_filial;
        TextView textView_data;
        TextView textview_documentos_numero;
   
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SepararPaleteProdutosDoDocumento);

            recyclerView               = FindViewById<RecyclerView>(Resource.Id.recyclerView_documentos);
            textView_filial            = FindViewById<TextView>(Resource.Id.textView_filial);
            textView_data              = FindViewById<TextView>(Resource.Id.textView_data);
            textview_documentos_numero = FindViewById<TextView>(Resource.Id.textview_documentos_numero);
         
           
            textView_filial.Text = Intent.GetStringExtra("filial");
            textView_data.Text = Intent.GetStringExtra("data");
            textview_documentos_numero.Text = Intent.GetStringExtra("numeroDocumento");

            GridLayoutManager gridLayoutManager = new GridLayoutManager(this, 1);

            RecyclerAdapter adapter = new RecyclerAdapter(TransferenciaInformacoes.produtos);
            adapter.ItemClick += Adapter_ItemClick;

            recyclerView.SetLayoutManager(gridLayoutManager);
            recyclerView.HasFixedSize = true;
            recyclerView.SetAdapter(adapter);
        }

        private void Adapter_ItemClick(object sender, int e)
        {
            StartActivity(typeof(SepararPaleteEtiqueta));
        }

        public class RecyclerAdapter : RecyclerView.Adapter
        {
            List<Produtos> items;

            public RecyclerAdapter(List<Produtos> items)
            {
                this.items = items;
            }

            public event EventHandler<int> ItemClick;

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                Produtos item = items[position];

                RecyclerHolder recycler = holder as RecyclerHolder;
                recycler.textView_descricao_produto.Text   = item.Descricao.Length > 30 ? item.Descricao[..30].ToUpper() : item.Descricao.ToUpper();
                recycler.textView_numero_etiqueta.Text     = item.Etiqueta;
                recycler.textView_caixas_pendentes.Text    = item.QuantidadeEmbalagem.ToString();
                recycler.textView_localizacao_rua.Text     = item.Localizacao[..2];
                recycler.textView_localizacao_predio.Text  = item.Localizacao.Substring(2, 2);
                recycler.textView_localizacao_andar.Text   = item.Localizacao.Substring(4, 2);
                recycler.textView_localizacao_posicao.Text = item.Localizacao[6..];
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.SepararPaleteProdutosDoDocumentoItem, parent, false);

                RecyclerHolder vh = new RecyclerHolder(itemView, OnClick);
                return vh;
            }

            public void OnClick(int position)
            {
                ItemClick?.Invoke(this, position);
            }

            public override int ItemCount
            {
                get
                {
                    return items.Count;
                }
            }
        }

        public class RecyclerHolder : RecyclerView.ViewHolder
        {
            public TextView textView_descricao_produto { get; private set; }
            public TextView textView_numero_etiqueta { get; private set; }
            public TextView textView_caixas_pendentes { get; private set; }
            public TextView textView_localizacao_rua { get; private set; }
            public TextView textView_localizacao_predio { get; private set; }
            public TextView textView_localizacao_andar { get; private set; }
            public TextView textView_localizacao_posicao { get; private set; }
             
            public RecyclerHolder(View itemView, Action<int> listener) : base(itemView)
            {
                textView_descricao_produto   = itemView.FindViewById<TextView>(Resource.Id.textView_descricao_produto);
                textView_numero_etiqueta     = itemView.FindViewById<TextView>(Resource.Id.textView_numero_etiqueta);
                textView_caixas_pendentes    = itemView.FindViewById<TextView>(Resource.Id.textView_caixas_pendentes);
                textView_localizacao_rua     = itemView.FindViewById<TextView>(Resource.Id.textView_localizacao_rua);
                textView_localizacao_predio  = itemView.FindViewById<TextView>(Resource.Id.textView_localizacao_predio);
                textView_localizacao_andar   = itemView.FindViewById<TextView>(Resource.Id.textView_localizacao_andar);
                textView_localizacao_posicao = itemView.FindViewById<TextView>(Resource.Id.textView_localizacao_posicao);

                itemView.Click += (sender, e) => listener(base.LayoutPosition);
            }
        }
    }
}
﻿using Android.App;
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
        Button   separar_palete_produtos_documento_botao_voltar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SepararPaleteProdutosDoDocumento);

            recyclerView                                   = FindViewById<RecyclerView>(Resource.Id.recyclerView_documentos);
            textView_filial                                = FindViewById<TextView>(Resource.Id.textView_filial);
            textView_data                                  = FindViewById<TextView>(Resource.Id.textView_data);
            textview_documentos_numero                     = FindViewById<TextView>(Resource.Id.textview_documentos_numero);
            separar_palete_produtos_documento_botao_voltar = FindViewById<Button>(Resource.Id.separar_palete_produtos_documento_botao_voltar);

            textView_filial.Text = Intent.GetStringExtra("filial");
            textView_data.Text   = Intent.GetStringExtra("data");

            textview_documentos_numero.Text = Intent.GetStringExtra("numeroDocumento");

            GridLayoutManager gridLayoutManager = new GridLayoutManager(this, 1);

            RecyclerAdapter adapter = new RecyclerAdapter(TransferenciaInformacoes.produtos);
            adapter.ItemClick += Adapter_ItemClick;

            //adapter.ItemClick += Adapter_ItemClick;
            separar_palete_produtos_documento_botao_voltar.Click += Separar_palete_produtos_documento_botao_voltar_Click;

            recyclerView.SetLayoutManager(gridLayoutManager);
            recyclerView.HasFixedSize = true;
            recyclerView.SetAdapter(adapter);
        }

        private void Separar_palete_produtos_documento_botao_voltar_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void Adapter_ItemClick(object sender, int e)
        {
            // transferir informações através do json.net - newtonsoft
            // para separarPaleteEtiqueta

            int posicao = e;

            // pega o objeto recyclerview para obter a lista dos produtos contidos nele
            RecyclerAdapter recycler = recyclerView.GetAdapter() as RecyclerAdapter;

            // recupera o produto que foi clicado
            Produtos produto = recycler.items[posicao];

            // serializa o objeto produto para uma string com notação Json
            string produtoJson = JsonConvert.SerializeObject(produto);

            // cria o intent para transferir estas informações do produto para a proxima view
            Intent intent = new Intent(this, typeof(SepararPaleteEtiqueta));
            intent.PutExtra("produto", produtoJson);
            intent.PutExtra("filial", textView_filial.Text);
            intent.PutExtra("data", textView_data.Text);

            StartActivity(intent);
        }

        public class RecyclerAdapter : RecyclerView.Adapter
        {
            public List<Produtos> items;

            public RecyclerAdapter(List<Produtos> items)
            {
                this.items = items;
            }

            public event EventHandler<int> ItemClick;

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                Produtos item = items[position];

                RecyclerHolder recycler = holder as RecyclerHolder;
                recycler.textView_descricao_produto.Text = item.Descricao.Length > 30 ? item.Descricao[..30].ToUpper() : item.Descricao.ToUpper();
                recycler.textView_numero_etiqueta.Text = item.Etiqueta;
                recycler.textView_caixas_pendentes.Text = item.QuantidadeEmbalagem.ToString();
                recycler.textView_localizacao_rua.Text = item.Localizacao[..2];
                recycler.textView_localizacao_predio.Text = item.Localizacao.Substring(2, 2);
                recycler.textView_localizacao_andar.Text = item.Localizacao.Substring(4, 2);
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
                    return this.items.Count;
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
                textView_descricao_produto = itemView.FindViewById<TextView>(Resource.Id.textView_descricao_produto);
                textView_numero_etiqueta = itemView.FindViewById<TextView>(Resource.Id.textView_numero_etiqueta);
                textView_caixas_pendentes = itemView.FindViewById<TextView>(Resource.Id.textView_caixas_pendentes);
                textView_localizacao_rua = itemView.FindViewById<TextView>(Resource.Id.textView_localizacao_rua);
                textView_localizacao_predio = itemView.FindViewById<TextView>(Resource.Id.textView_localizacao_predio);
                textView_localizacao_andar = itemView.FindViewById<TextView>(Resource.Id.textView_localizacao_andar);
                textView_localizacao_posicao = itemView.FindViewById<TextView>(Resource.Id.textView_localizacao_posicao);

                itemView.Click += (sender, e) => listener(base.LayoutPosition);
            }
        }
    }
}

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

namespace TelasColetor.Fonte
{
    [Activity(Label = "SeparacaoPaleteDocumentos")]
    public class SeparacaoPaleteDocumentos : Activity
    {

        TextView textView_filial;
        TextView textView_data;

        RecyclerView recyclerView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SepararPaleteDocumentos);

            textView_filial = FindViewById<TextView>(Resource.Id.textView_filial);
            textView_data = FindViewById<TextView>(Resource.Id.textView_data);
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView_documentos);

            textView_filial.Text = Intent.GetStringExtra("filial");
            textView_data.Text = Intent.GetStringExtra("data");

            GridLayoutManager gridLayoutManager = new GridLayoutManager(this, 3);

            RecyclerAdapter adapter = new RecyclerAdapter(GetDocumentos());
            adapter.ItemClick += Adapter_ItemClick;

            recyclerView.SetLayoutManager(gridLayoutManager);
            recyclerView.HasFixedSize = true;

            recyclerView.SetAdapter(adapter);
        }

        private void Adapter_ItemClick(object sender, int e)
        {
            int posicao = e;

            RecyclerAdapter adapter = recyclerView.GetAdapter() as RecyclerAdapter;
            string data = adapter.items.documentosIndividual[posicao].Data;

            Toast.MakeText(this, data, ToastLength.Long).Show();
        }

        private class RecyclerAdapter : RecyclerView.Adapter
        {
            public Documentos items;

            public RecyclerAdapter(Documentos items)
            {
                this.items = items;
            }

            public event EventHandler<int> ItemClick; 

            public override int ItemCount
            {
                get
                {
                    return items.documentosIndividual.Count;
                }
            }

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                DocumentoIndividual item = items.documentosIndividual[position];

                RecyclerHolder recycler = holder as RecyclerHolder;
                recycler.imageSeta.Visibility = (item.Iniciado ? ViewStates.Visible : ViewStates.Gone );
                recycler.textView_documento.Text = item.Numero;
                recycler.progressBar.Progress = item.Progresso;
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Separar_Palete_Documentos_Item, parent, false);

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
            public ImageView imageSeta { get; private set; }
            public TextView textView_documento { get; private set; }
            public ProgressBar progressBar { get; private set; }

            public RecyclerHolder(View itemView, Action<int> listener) : base(itemView)
            {
                imageSeta = itemView.FindViewById<ImageView>(Resource.Id.imageView_seta);
                textView_documento = itemView.FindViewById<TextView>(Resource.Id.textView_documento_numero);
                progressBar = itemView.FindViewById<ProgressBar>(Resource.Id.progressBar_Documento);

                itemView.Click += (sender, e) => listener(base.LayoutPosition);
            }
        }

        // simula os dados vindos do back-end

        public Documentos GetDocumentos()
        {
            // cria a lista de documentos em filiais aleatorias e em datas aleatorias

            Documentos documentos = new Documentos();
            documentos.documentosIndividual = new List<DocumentoIndividual>();
            Random random = new Random();

            int acumula = 5;

            // simula um intervalo de 60 dias
            for (int i = 1; i <= 40000; i++)
            {
                int progress = 0;
                int filialNumero = 0;
               
                DocumentoIndividual doc = new DocumentoIndividual();
                doc.Numero = random.Next(1, 99999999).ToString();

                progress = random.Next(0, 100);
                filialNumero = random.Next(0, 30);

                if (acumula == i)
                {
                    doc.Iniciado = false;
                    progress = 0;
                    acumula += 5;
                }
                else
                {
                    doc.Iniciado = ((random.Next(0, 1) == 0) ? false : true) ? true : (progress == 0) ? false : true;
                    doc.Progresso = progress;
                }                                                                              
                
                doc.Filial = filialNumero;
                
                // gera uma data aleatoria

                int ano = DateTime.Now.Year; 
                int mes = Convert.ToInt32(random.Next(1, 12).ToString().PadLeft(2, '0'));
                int diasNoMesEscolhido = DateTime.DaysInMonth(ano, mes);
                int dia = Convert.ToInt32(random.Next(1, diasNoMesEscolhido).ToString().PadLeft(2, '0'));

                doc.Data = dia.ToString().PadLeft(2,'0') + "/" + mes.ToString().PadLeft(2,'0') + "/" + ano;

                documentos.documentosIndividual.Add(doc);
            }

            // consulta comente os documentos da filial e na data ambos especificados

            List<DocumentoIndividual> documentosNaData = new List<DocumentoIndividual>();
            List<DocumentoIndividual> documentosNaFilial = new List<DocumentoIndividual>();

            int filial = Convert.ToInt32(textView_filial.Text.Substring(1));

            documentosNaData = documentos.documentosIndividual.FindAll(a => a.Data == textView_data.Text);
            documentosNaFilial = documentosNaData.FindAll(a => a.Filial == filial);

            documentos.documentosIndividual = documentosNaFilial;

            return documentos;
        }
        public class Documentos
        {
            public List<DocumentoIndividual> documentosIndividual { get; set; }
        }
        public class DocumentoIndividual
        {
            public string Numero { get; set; }
            public bool Iniciado { get; set; }
            public int Progresso { get; set; }
            public int Filial { get; set; }
            public string Data { get; set; }
        }
    }  
} 
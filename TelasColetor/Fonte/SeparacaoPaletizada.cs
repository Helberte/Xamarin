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
using static Android.Graphics.ColorSpace;

namespace TelasColetor.Fonte
{
    [Activity(Label = "SeparacaoPaletizada")]
    public class SeparacaoPaletizada : Activity
    {
        RecyclerView recyclerView;
        List<Movimentacoes> objetosMovimentacoes;

      
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // vincula o layout com este codigo fonte
            SetContentView(Resource.Layout.Separacao_Paletizada);

            // pega o recycleView do front
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView1);
                       
            // define quantas colunas o recycleView vai ter
            var gridLayoutManager = new GridLayoutManager(this, 2);

            // pega os dados do back e adapta eles para colocar no recycleView
            var adapter = new RecyclerAdapter(DefineReservas());
            //                                      +---- Retorna um objeto contendo um atributo lista com vários outros objetos


            // Registre o manipulador de cliques de item (abaixo) com o adaptador:
            adapter.ItemClick += Adapter_ItemClick;

            recyclerView.SetLayoutManager(gridLayoutManager);
            recyclerView.HasFixedSize = true;

            // Conecte o adaptador no RecyclerView:
            recyclerView.SetAdapter(adapter);
        }

        private void Adapter_ItemClick(object sender, int e)
        {
            Toast.MakeText(this, "Voce clicou em um item.", ToastLength.Long).Show();
        }

       
        // ADAPTER

        // Adaptador para conectar o conjunto de dados(vindos do banco, retorno da API) ao RecyclerView:
        public class RecyclerAdapter : RecyclerView.Adapter
        {
            // referencia para o objeto contendo a lista de dados
            public Movimentacoes items;

            // Manipulador de eventos para cliques de itens:
            public event EventHandler<int> ItemClick;

            // recebe no construtor, o conjunto de dados vindo do retorno da API
            public RecyclerAdapter(Movimentacoes data)
            {
                items = data;
            }

            // Crie uma nova CardView (chamada pelo gerenciador de layout). Aqui, o layout de visualização personalizado já deverá estar criado, o FrameLayout
            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                // Inflar o CardView para mostrar o card:

                View itemView = LayoutInflater.From(parent.Context).
                Inflate(Resource.Layout.Menu_Coletor_Item, parent, false);
                // referencia o layout card que foi criado na pasta Resource -> Layout


                // Crie um ViewHolder para localizar e manter essas referências de exibição e
                // registra OnClick com o viewholder:
                RecyclerHolder vh = new RecyclerHolder(itemView, OnClick);
                return vh;
            }

            // Preencha o conteúdo do card com os objetos passados (chamado pelo gerenciador de layout) :
            public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
            {
                var item = items.movimento[position];

                // define a descrição e o icone
                var holder = viewHolder as RecyclerHolder;
                holder.Descricao.Text = items.movimento[position].NomeCliente;

                // holder.icone.SetImageResource(ImagemMenu.ObtemImagemMenu(item.icone));
                holder.Icone.SetImageResource(Resource.Drawable.icons8_breakable_96_1);
            }

            // Gera um evento quando ocorre o clique do item:
            public void OnClick(int position)
            {
                ItemClick?.Invoke(this, position);
            }

            // retorna a quantidade de itens na lista
            public override int ItemCount
            {
                get
                {
                    return items.movimento.Count;
                }
            }
        }

        // VIEW HOLDER

        // Implementa o padrão ViewHolder: cada ViewHolder contém referências
        // para os componentes de interface do usuário (ImageView e TextView) dentro do CardView
        // que é exibido em uma linha do RecyclerView:

        // aqui é onde são criados os atributos que serão mostrados
        public class RecyclerHolder : RecyclerView.ViewHolder
        {
            public TextView Descricao { get; private set; }
            public ImageView Icone { get; private set; }

            public RecyclerHolder(View itemView, Action<int> listener) : base(itemView)
            {
                Descricao = (TextView)itemView.FindViewById(Resource.Id.Menu_Coletor_Item_Descricao);
                //                                                          +--- pega estes itens exatamente da nova view de card personalizada que foi criada, FrameLayout
                Icone = (ImageView)itemView.FindViewById(Resource.Id.Menu_Coletor_Item_Icone);
                //                                                          +--- pega estes itens exatamente da nova view de card personalizada que foi criada, FrameLayout


                // cria o evento de click para cada item

                // Detecte cliques do usuário na exibição do item e informe qual item
                // foi clicado (por posição de layout) para o ouvinte:
                itemView.Click += (sender, e) => listener(base.LayoutPosition);
            }
        }

        // simula dados vindo do banco de dados
        public Movimentacoes DefineReservas()
        {
            string nomesPessoas = "Maria,José,Antônio,João,Francisca,Ana,Luiz,Paulo,Carlos,Manoel,Pedro,Francisca,Marcos,Raimundo,Sebastião,Antônia,Marcelo,Jorge,Márcia,Geraldo,Adriana,Sandra,Luis,Fernando,Fabio,Roberta,Márcio,Edson,André,Sérgio,Josefa,Patrícia,Daniel,Rodrigo,Rafael,Joaquim,Vera,Ricardo,Eduardo";
            string[] nomes = nomesPessoas.Split(',');
            Random random = new Random();

            Movimentacoes movimentacoes = new Movimentacoes();
            movimentacoes.movimento = new List<Movimento>();

            for (int i = 1; i <= 31; i++)
            {
                Movimento movimento = new Movimento();

                movimento.DiaReserva = random.Next(1, 31);
                movimento.NomeCliente = nomes[random.Next(1, 39)];
                movimento.NumeroReserva = random.Next(1, 300);

                movimentacoes.movimento.Add(movimento);
            }                   
            return movimentacoes;
        }

        public class Movimentacoes
        { 
            public List<Movimento> movimento { get; set; }
        }

        public class Movimento
        {
            public int DiaReserva { get; set; }
            public int NumeroReserva { get; set; }
            public string NomeCliente { get; set; }
        }
    }
}
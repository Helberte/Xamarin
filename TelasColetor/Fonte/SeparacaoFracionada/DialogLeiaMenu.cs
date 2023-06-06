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
    public class DialogLeiaMenu : DialogFragment
    {
        Button separacao_fracionada_leia_um_item_menu_produtos_separados;
        string[] status = { "separado", "pendente" };

        private string filial;
        private string data; 

        public DialogLeiaMenu(string filial, string data)
        {
            this.filial = filial;
            this.data = data;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.SeparacaoFracionadaLeiaUmItemMenu, container, false);

            separacao_fracionada_leia_um_item_menu_produtos_separados = view.FindViewById<Button>(Resource.Id.separacao_fracionada_leia_um_item_menu_produtos_separados);

            separacao_fracionada_leia_um_item_menu_produtos_separados.Click += Separacao_fracionada_leia_um_item_menu_produtos_separados_Click; ;
            return view;
        }

        private void Separacao_fracionada_leia_um_item_menu_produtos_separados_Click(object sender, EventArgs e)
        {
            // chamar próximo layout produtos separados
            List<Produtos> produtos                    = GeraProdutosSeparados(30);
            List<Produtos> produtosSeparadosFinalizado = new List<Produtos>();

            // pega todos os produtos separados
            IEnumerable<Produtos> produtosSeparados = from a 
                                                        in produtos 
                                                     where a.Status == "separado" 
                                                   orderby a.Descricao 
                                                    select a;

            foreach (Produtos item in produtosSeparados)
            {
                produtosSeparadosFinalizado.Add(item);
            }

            // serializa a lista de objetos
            string stringProdutos = JsonConvert.SerializeObject(produtosSeparadosFinalizado);

            Intent intent = new Intent(Application.Context, typeof(SeparacaoFracionadaListaProdutosSeparados));
            intent.PutExtra("produtos", stringProdutos);
            intent.PutExtra("filial", filial);
            intent.PutExtra("data", data);

            StartActivity(intent);
        }

        // gera ods produtos
        public List<Produtos> GeraProdutosSeparados(int qtdLimiteProdutos)
        { 
            System.Random random = new System.Random();
            List<Produtos> produtos = new List<Produtos>();

            int qtdprodutos = random.Next(0, qtdLimiteProdutos);

            // gera data vencimento aleatoria
            string ano = (DateTime.Now.Year + 2).ToString();
            string anoAleatorio = random.Next(DateTime.Now.Year, Convert.ToInt32(ano)).ToString();
            string mes = random.Next(1, 12).ToString().PadLeft(2, '0');
            string diaNoMes = DateTime.DaysInMonth(Convert.ToInt32(anoAleatorio), Convert.ToInt32(mes)).ToString().PadLeft(2, '0');
            string dia = random.Next(DateTime.Now.Day, Convert.ToInt32(diaNoMes)).ToString().PadLeft(2, '0');
            
            for (int i = 0; i < qtdLimiteProdutos; i++)
            {
                int quantidadeEmbalagem = random.Next(2, 50);

                Produtos produto = new Produtos()
                {
                    Descricao = GetNomesProdutos()[random.Next(0, GetNomesProdutos().Length - 1)].Trim(),
                    Etiqueta = random.Next(1, 2500000).ToString().PadLeft(8, '0'),
                    Gramas = random.Next(20, 500),
                    QuantidadeEmbalagem = quantidadeEmbalagem,
                    Localizacao = random.Next(1, 99).ToString().PadLeft(2, '0') + random.Next(1, 50).ToString().PadLeft(2, '0') + random.Next(1, 7).ToString().PadLeft(2, '0') + random.Next(1, 3).ToString().PadLeft(4, '0'),
                    Codigo = random.Next(10000000, 20000000).ToString(),
                    Referencia = random.Next(10000, 99999).ToString() + random.Next(100000, 999999),
                    Lote = "SL",
                    Validade = dia + "/" + mes + "/" + anoAleatorio,
                    Pendente = random.Next(1, quantidadeEmbalagem - 1),
                    Ean = random.Next(1000000, 9999999).ToString() + random.Next(100000, 999999).ToString(),
                    Dun = random.Next(10000000, 99999999).ToString() + random.Next(1000000, 9999999).ToString(),
                    Quantidade_a_registrar = random.Next(1, 10),
                    Unidade = random.Next(1, 10),
                    Status = status[random.Next(0,1)]
                };
                produtos.Add(produto);
            }
            return produtos;
        }

        // nomes dos produtos
        public string[] GetNomesProdutos()
        {
            string[] arrayProdutos;
            string nomesProdutos = "Biscoito ADRIA Gergelim                                               " +
                        ",Biscoito BAUDUCCO Amanteigado Cereais, Iogurte e Mel                            " +
                        ",Biscoito MONTEVÉRGINE Champanhe com Açúcar Cristal                              " +
                        ",Biscoito TRAKINAS Recheado de Chocolate e Chocolate Branco                      " +
                        ",Biscoito Wafer BAUDUCCO de Chocolate Branco                                     " +
                        ",Amandita LACTA de Chocolate                                                     " +
                        ",Biscoito ADRIA Água e Sal                                                       " +
                        ",Biscoito ADRIA Crackers Original                                                " +
                        ",Biscoito ADRIA Cream Cracker Folhata                                            " +
                        ",Biscoito ADRIA Maisena                                                          " +
                        ",Biscoito ADRIA Mousse Recheado de Chocolate                                     " +
                        ",Biscoito ADRIA Mousse Recheado de Coco com Chocolate                            " +
                        ",Biscoito ADRIA Mousse Recheado de Limão com Chocolate                           " +
                        ",Biscoito ADRIA Mousse Recheado de Morango com Chocolate                         " +
                        ",Biscoito ADRIA Plug@dos Recheado de Chocolate                                   " +
                        ",Biscoito ADRIA Plug@dos Recheado de Chocolate Branco                            " +
                        ",Biscoito ADRIA Plug@dos Recheado de Flocos                                      " +
                        ",Biscoito ADRIA Plug@dos Recheado de Morango                                     " +
                        ",Biscoito ADRIA Tortinhas Due Chocolate Branco + Geléia de Frutas Vermelhas      " +
                        ",Biscoito ADRIA Tortinhas Due Trufa + Geléia de Morango                          " +
                        ",Biscoito ADRIA Tortinhas Recheado Black de Baunilha                             " +
                        ",Biscoito ADRIA Tortinhas Recheado de Cappuccino                                 " +
                        ",Biscoito ADRIA Tortinhas Recheado de Chocolate                                  " +
                        ",Biscoito ADRIA Tortinhas Recheado de Chocolate Branco                           " +
                        ",Biscoito ADRIA Tortinhas Recheado de Chocolate e Cereja                         " +
                        ",Biscoito ADRIA Tortinhas Recheado de Coco                                       " +
                        ",Biscoito ADRIA Tortinhas Recheado de Limão                                      " +
                        ",Biscoito ADRIA Tortinhas Recheado de Morango                                    " +
                        ",Biscoito Água Light PIRAQUÊ                                                     " +
                        ",Biscoito Alemão Sabor Chocolate com Avelã Diet SCHNEEKOPPE                      " +
                        ",Biscoito Amanteigado                                                            " +
                        ",Biscoito Aveia e Mel Diet KOBBER                                                " +
                        ",Biscoito BAUDUCCO Água e Gergelim                                               " +
                        ",Biscoito BAUDUCCO Água e Sal                                                    " +
                        ",Biscoito BAUDUCCO Amanteigado Banana e Canela                                   " +
                        ",Biscoito BAUDUCCO Amanteigado Chocolate                                         " +
                        ",Biscoito BAUDUCCO Amanteigado Coco                                              " +
                        ",Biscoito BAUDUCCO Amanteigado Leite                                             " +
                        ",Biscoito BAUDUCCO Amanteigado Leite com Chocolate                               " +
                        ",Biscoito BAUDUCCO Champanhe com Açúcar Cristal                                  " +
                        ",Biscoito BAUDUCCO Champanhe com Açúcar Fino                                     " +
                        ",Biscoito BAUDUCCO Cream Cracker                                                 " +
                        ",Biscoito BAUDUCCO Integral com Gergelim                                         " +
                        ",Biscoito BAUDUCCO Integral com Gergelim                                         " +
                        ",Biscoito BAUDUCCO Toda Hora Integral                                            " +
                        ",Biscoito BITS CHIPITS Cebola e Salsa                                            " +
                        ",Biscoito BITS CHIPITS de Queijo                                                 " +
                        ",Biscoito BITS CHIPITS Original                                                  " +
                        ",Biscoito BREAK UP Recheado de Chocolate                                         " +
                        ",Biscoito BREAK UP Recheado de Chocolate ao Leite                                " +
                        ",Biscoito BREAK UP Recheado de Coco                                              " +
                        ",Biscoito BREAK UP Recheado de Morango                                           " +
                        ",Biscoito Casadinho de Chocolate Diet SVELTE                                     " +
                        ",Biscoito Casadinho de Doce de Leite Diet SVELTE                                 " +
                        ",Biscoito Casadinho de Goiaba Diet SVELTE                                        " +
                        ",Biscoito CASSINI de Polvilho Doce                                               " +
                        ",Biscoito CASSINI de Polvilho Salgado                                            " +
                        ",Biscoito Champanhe                                                              " +
                        ",Biscoito Classic Diet FIBROCRAC                                                 " +
                        ",Biscoito CLUB SOCIAL Pizza                                                      " +
                        ",Biscoito Coberto Deditos NESTLÉ                                                 " +
                        ",Biscoito Cream Cracker Light BAUDUCCO                                           " +
                        ",Biscoito DANIX Recheado Choc+Choc                                               " +
                        ",Biscoito DANIX Recheado de Chocolate                                            " +
                        ",Biscoito DANIX Recheado de Morango                                              " +
                        ",Biscoito de Aveia com Passas                                                    " +
                        ",Biscoito de Aveia Diet FIBROCRAC                                                " +
                        ",Biscoito de Aveia e Mel                                                         " +
                        ",Biscoito de Chocolate Diet GERBEAUD                                             " +
                        ",Biscoito de Chocolate Light FIBROCRAC                                           " +
                        ",Biscoito de Creme de Amendoim                                                   " +
                        ",Biscoito de Leite Diet GERBEAUD                                                 " +
                        ",Biscoito Dinamarquês DANISH DIVINE Amanteigado                                  " +
                        ",Farinha de trigo                                                                " +
                        ",Biscoito FIBRAX Fibra Natural                                                   " +
                        ",Biscoito FIBRAX Salgado                                                         " +
                        ",Biscoito LEVINA Amanteigado Bichinhos Chocolate 160g                            " +
                        ",Biscoito LEVINA Amanteigado Bichinhos Coco                                      " +
                        ",Biscoito MABEL Cream Cracker                                                    " +
                        ",Biscoito MABEL Maizena                                                          " +
                        ",Biscoito MABEL Rosquinha de Coco                                                " +
                        ",Biscoito Maça e Canela                                                          " +
                        ",Biscoito Maizena                                                                " +
                        ",Biscoito Maria                                                                  " +
                        ",Biscoito MARILAN Amanteigado                                                    " +
                        ",Biscoito MARILAN Cracker Tostmant                                               " +
                        ",Biscoito MARILAN Magic Toast                                                    " +
                        ",Biscoito MARILAN Magic Toast Integral                                           " +
                        ",Biscoito MARILAN Pit Stop                                                       " +
                        ",Biscoito MARILAN Pit Stop com Gergelim e Sementes de Papoula                    " +
                        ",Biscoito MARILAN Pit Stop Integral                                              " +
                        ",Biscoito MARILAN Recheado de Baunilha                                           " +
                        ",Biscoito MARILAN Recheado de Chocolate                                          " +
                        ",Biscoito MARILAN Salgatost Temperado                                            " +
                        ",Biscoito MARILAN Teens Recheado de Chocolate                                    " +
                        ",Biscoito MARILAN Teens Recheado de Chocolate Branco                             " +
                        ",Biscoito MARILAN Tortinha de Morango                                            " +
                        ",Biscoito MARILAN Tortinha Mousse de Chocolate                                   " +
                        ",Biscoito MARILAN Vitaminado Maizena                                             " +
                        ",Biscoito NABISCO Bon Gouter de Provolone                                        " +
                        ",Biscoito NABISCO Bon Gouter de Queijo e Tomate Seco                             " +
                        ",Biscoito NABISCO Bon Gouter Sabor Parmesão                                      " +
                        ",Biscoito NABISCO Bon Gouter Tipo Suíço                                          " +
                        ",Biscoito NABISCO Chocolícia Recheado de Chocolate                               " +
                        ",Biscoito NABISCO Chocolícia Recheado de Chocolate Branco                        " +
                        ",Biscoito NABISCO Chocooky Baunilha                                              " +
                        ",Biscoito NABISCO Chocooky Chocolate                                             " +
                        ",Biscoito NABISCO Club Social Frutas Vermelhas                                   " +
                        ",Biscoito NABISCO Club Social Mel e Cereal                                       " +
                        ",Biscoito NABISCO Club Social Salgado                                            " +
                        ",Biscoito NABISCO Club Social Salgado Integral                                   " +
                        ",Biscoito NABISCO Óreo Recheado de Baunilha                                      " +
                        ",Biscoito NABISCO Óreo Recheado de Chocolate                                     " +
                        ",Biscoito NESTLÉ Aveia e Mel                                                     " +
                        ",Biscoito NESTLÉ Bono Recheado de Chocolate                                      " +
                        ",Biscoito NESTLÉ Bono Recheado de Morango                                        " +
                        ",Biscoito NESTLÉ Bono Recheado Doce de Leite                                     " +
                        ",Biscoito NESTLÉ Calipso Chocolate Branco                                        " +
                        ",Biscoito NESTLÉ Calipso Original                                                " +
                        ",Biscoito NESTLÉ Classic Choco Snack                                             " +
                        ",Biscoito NESTLÉ Lanche Passatempo leite                                         " +
                        ",Biscoito NESTLÉ Leite e Mel                                                     " +
                        ",Biscoito NESTLÉ Negresco Recheado                                               " +
                        ",Biscoito NESTLÉ Negresco Recheado Invertido                                     " +
                        ",Biscoito NESTLÉ Passatempo Bichos Leite                                         " +
                        ",Biscoito NESTLÉ Passatempo Bichos Recheado de Chocolate                         " +
                        ",Biscoito NESTLÉ Passatempo Cobertura de Chocolate                               " +
                        ",Biscoito NESTLÉ Passatempo Recheado Chocolate                                   " +
                        ",Biscoito NESTLÉ Passatempo Recheado Leite Glacê                                 " +
                        ",Biscoito NESTLÉ Passatempo Sabor Chocolate Recheado Morango                     " +
                        ",Biscoito NESTLÉ Recheado Galak                                                  " +
                        ",Biscoito NESTLÉ Recheado Nescau                                                 " +
                        ",Biscoito NESTLÉ Salclic Aperitivo                                               " +
                        ",Biscoito NESTLÉ Tostines Acqua Gergelim                                         " +
                        ",Biscoito NESTLÉ Tostines Água                                                   " +
                        ",Biscoito NESTLÉ Tostines Água e Sal                                             " +
                        ",Biscoito NESTLÉ Tostines Cream Cracker                                          " +
                        ",Biscoito NESTLÉ Tostines Leite                                                  " +
                        ",Biscoito NESTLÉ Tostines Recheado de Chocolate                                  " +
                        ",Biscoito NESTLÉ Tostines Recheado de Morango                                    " +
                        ",Biscoito NESTLÉ Vitaminado Coco                                                 " +
                        ",Biscoito PIRAQUÊ Água e Gergelim                                                " +
                        ",Biscoito PIRAQUÊ Pizzaquê                                                       " +
                        ",Biscoito PIRAQUÊ Roladinho Goiaba                                               " +
                        ",Biscoito PIRAQUÊ Sabor Gergelim                                                 " +
                        ",Biscoito PIRAQUÊ Sabor Presunto                                                 " +
                        ",Biscoito PIRAQUÊ Sabor Queijo                                                   " +
                        ",Biscoito PIRAQUÊ Salgadinho                                                     " +
                        ",Biscoito Pit Stop Light MARILAN                                                 " +
                        ",Biscoito Recheado                                                               " +
                        ",Biscoito Recheado com Nozes                                                     " +
                        ",Biscoito Recheado de Chocolate Diet DOCE VIDA                                   " +
                        ",Biscoito Salgado                                                                " +
                        ",Biscoito Toda Hora Light BAUDUCCO                                               " +
                        ",Biscoito TRAKINAS Carinhas Recheado de Chocolate                                " +
                        ",Biscoito TRAKINAS Carinhas Recheado de Morango                                  " +
                        ",Biscoito TRAKINAS Mais Recheado de Chocolate                                    " +
                        ",Biscoito TRAKINAS Carinhas Recheado de Morango                                  " +
                        ",Biscoito TRAKINAS Mais Recheado de Chocolate                                    " +
                        ",Biscoito TRAKINAS Mais Recheado de Morango                                      " +
                        ",Biscoito TRAKINAS Mini Recheado de Chocolate                                    " +
                        ",Biscoito TRAKINAS Mini Recheado de Morango                                      " +
                        ",Biscoito TRAKINAS Recheado de Chocolate e Morango                               " +
                        ",Biscoito TRAKINAS Trakmix Sabor Chocolate                                       " +
                        ",Biscoito TRAKINAS Trakmix Sabor Leite                                           " +
                        ",Biscoito TRIUNFO Água e Sal                                                     " +
                        ",Biscoito TRIUNFO Cream Cracker                                                  " +
                        ",Biscoito TRIUNFO Maisena                                                        " +
                        ",Biscoito TRIUNFO Maizena Sabor Chocolate                                        " +
                        ",Biscoito TRIUNFO Recheado de Chocolate                                          " +
                        ",Biscoito TRIUNFO Recheado de Morango                                            " +
                        ",Biscoito TRIUNFO Salpet Salgado                                                 " +
                        ",Biscoito TRIUNFO Tortini Chocolate                                              " +
                        ",Biscoito TRIUNFO Tortini Chocolate Branco                                       " +
                        ",Biscoito TRIUNFO Tortini Coco                                                   " +
                        ",Biscoito TRIUNFO Tortini Morango                                                " +
                        ",Biscoito TRIUNFO Tortini Torta de Limão                                         " +
                        ",Biscoito TRIUNFO Tortini Torta de Maracujá                                      " +
                        ",Biscoito TRIUNFO Tortini Trufa                                                  " +
                        ",Biscoito Wafer BAUDUCCO Crocante                                                " +
                        ",Biscoito Wafer BAUDUCCO de Brigadeiro                                           " +
                        ",Biscoito Wafer BAUDUCCO de Chocolate                                            " +
                        ",Biscoito Wafer BAUDUCCO de Chocolate com Avelã                                  " +
                        ",Biscoito Wafer BAUDUCCO de Flocos                                               " +
                        ",Biscoito Wafer BAUDUCCO de Morango                                              " +
                        ",Biscoito Wafer BAUDUCCO de Nozes                                                " +
                        ",Biscoito Wafer de Chocolate Light GERBEAUD                                      " +
                        ",Biscoito Wafer de Morango Diet GERBEAUD                                         " +
                        ",Biscoito Wafer NESTLÉ Bono de Chocolate                                         " +
                        ",Biscoito Wafer NESTLÉ Bono de Morango                                           " +
                        ",Biscoito Wafer NESTLÉ Galak                                                     " +
                        ",Biscoito Wafer NESTLÉ Prestígio                                                 " +
                        ",Biscoito Wafer NESTLÉ Tostines de Chocolate                                     " +
                        ",Biscoito Wafer NESTLÉ Tostines de Morango                                       " +
                        ",Biscoito Wafer TRIUNFO de Chocolate                                             " +
                        ",Biscoito Wafer TRIUNFO de Chocolate Branco                                      " +
                        ",Biscoito Wafer TRIUNFO de Chocolate com Avelã                                   " +
                        ",Biscoito Wafer TRIUNFO de Maça e Canela                                         " +
                        ",Biscoito Wafer TRIUNFO de Morango                                               " +
                        ",Biscoitos de farinha integral                                                   " +
                        ",Biscoitos de glúten a 40%                                                       " +
                        ",Biscoitos de Glúten Puro                                                        " +
                        ",Biscoitos de Polvilho                                                           " +
                        ",Biscoitos Doces                                                                 " +
                        ",Bolacha d' Água                                                                 " +
                        ",Bolacha de Aveia                                                                " +
                        ",Bolacha de Chocolate                                                            " +
                        ",Bolacha de Queijo                                                               " +
                        ",Bolacha de Trigo Integral                                                       " +
                        ",Bolachinha Salgada                                                              " +
                        ",Cookie Baunilha com Gotas de Chocolate TAEQ                                     " +
                        ",Cookie Chocolate com Gotas de Chocolate TAEQ                                    " +
                        ",Cookie Integral Aveia e Mel TAEQ                                                " +
                        ",Cookie Integral Banana com Gotas de Chocolate TAEQ                              " +
                        ",Cookie Integral Cereal Matinal MÃE TERRA                                        " +
                        ",Cookie Integral Gengibre MÃE TERRA                                              " +
                        ",Cookie Integral Soja MÃE TERRA                                                  " +
                        ",Cookies JASMINE Integral de Café                                                " +
                        ",Cookies JASMINE Integral de Frutas Cítricas                                     " +
                        ",Cookies JASMINE Integral de Limão                                               " +
                        ",Lanchinho Wafer NESTLÉ Passatempo de Chocolate                                  " +
                        ",Lanchinho Wafer NESTLÉ Passatempo de Morango                                    " +
                        ",Rolinhos de Waffer TUBETES Cobertos                                             " +
                        ",Rolinhos de Waffer TUBETES Recheados                                            " +
                        ",Rolinhos de Waffer TUBETES Simples                                              " +
                        ",Rosquinha (Rosquinhas)                                                          " +
                        ",Rosquinha (Rosquinhas) de Polvilho                                              " +
                        ",Rosquinhas, com recheio de geléia                                               " +
                        ",Bolo Amarelo / Glacê de Chocolate                                               " +
                        ",Bolo Batido                                                                     " +
                        ",Bolo Branco / Glacê Branco                                                      " +
                        ",Bolo Branco de Côco                                                             " +
                        ",Bolo de Café                                                                    " +
                        ",Bolo de Cenoura                                                                 " +
                        ",Bolo de Chocolate                                                               " +
                        ",Bolo de Fruta, escuro                                                           " +
                        ",Bolo de Gengibre                                                                " +
                        ",Bolo de Maçã                                                                    " +
                        ",Bolo de Milho                                                                   " +
                        ",Bolo de Pêssego                                                                 " +
                        ",Bolo de Queijo                                                                  " +
                        ",Bolo de Tapioca                                                                 " +
                        ",Bolo de Trigo                                                                   " +
                        ",Massa de Bolo Branca Glacê                                                      " +
                        ",Massa de Bolo Simples                                                           " +
                        ",Mistura para Bolo Sabor Abacaxi DONA BENTA                                      " +
                        ",Mistura para Bolo Sabor Banana com Canela DONA BENTA                            " +
                        ",Bolo de 40g Pulmann                                                             " +
                        ",Mistura para Bolo com Soja Sabor Laranja SOY CAKE                               " +
                        ",Mistura para Bolo de Baunilha SOL                                               " +
                        ",Mistura para Bolo de Chocolate DR OETKER                                        " +
                        ",Mistura para Bolo de Festa SOL                                                  " +
                        ",Mistura para Bolo de Mármore Dr OETKER                                          " +
                        ",Mistura para Bolo Nega Maluca DR OETKER                                         " +
                        ",Mistura para Bolo Sabor Aipim DONA BENTA                                        " +
                        ",Mistura para Bolo Sabor Aipim SOL                                               " +
                        ",Mistura para Bolo Sabor Baunilha DONA BENTA                                     " +
                        ",Mistura para Bolo Sabor Cenoura DONA BENTA                                      " +
                        ",Mistura para Bolo Sabor Chocolate DONA BENTA                                    " +
                        ",Mistura para Bolo Sabor Chocolate SOL                                           " +
                        ",Mistura para Bolo Sabor Coco DONA BENTA                                         " +
                        ",Mistura para Bolo Sabor Coco Gelado Dr OETKER                                   " +
                        ",Mistura para Bolo Sabor Fubá DONA BENTA                                         " +
                        ",Mistura para Bolo Sabor Laranja DONA BENTA                                      " +
                        ",Mistura para Bolo Sabor Laranja SOL                                             " +
                        ",Mistura para Bolo Sabor Limão DONA BENTA                                        " +
                        ",Mistura para Bolo Sabor Maracujá DONA BENTA                                     " +
                        ",Mistura para Bolo Sabor Milho Cremoso                                           " +
                        ",Mistura para Bolo Sabor Milho Verde DONA BENTA                                  " +
                        ",Mistura para Bolo Sabor Panetone SOL                                            " +
                        ",Mistura para Bolo Sabor Pão de Mel Dr OETKER                                    " +
                        ",Mistura para bolos, industrializada                                             " +
                        ",Mistura para Brownie Sabor Chocolate Dr OETKER                                  ";
            arrayProdutos = nomesProdutos.Split(",");

            return arrayProdutos;
        }
    }
}
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Android.Content.ClipData;

namespace hotel_pousadas.fonte
{
    [Activity(Label = "movimentacoes")]
    public class movimentacoes : Activity
    {

        CalendarView calendario_mes;
        LinearLayout layout_bt_listar_todos;

        List<Movimentacoes> objetosMovimentacoes;


        ListView lista_movimentacoes;
        List<string> list;
        List<string> lista_gambiarra;

        string dataEscolhida;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // linca a tela ao codigo fonte
            SetContentView(Resource.Layout.movimentacoes);

            calendario_mes = FindViewById<CalendarView>(Resource.Id.calendario_mes);
            lista_movimentacoes = FindViewById<ListView>(Resource.Id.lista_reservas);
            layout_bt_listar_todos = FindViewById<LinearLayout>(Resource.Id.layout_bt_listar_todos);

            calendario_mes.DateChange += Calendario_mes_DateChange;
            layout_bt_listar_todos.Click += Layout_bt_listar_todos_Click;

            list = new List<string>();
            lista_gambiarra = new List<string>();
            objetosMovimentacoes = new List<Movimentacoes>();

            DefineReservas();
        }

        private void Layout_bt_listar_todos_Click(object sender, EventArgs e)
        {
            ArrayAdapter<string> arrayAdapter = new ArrayAdapter<string>(this,
                                                        Android.Resource.Layout.SimpleListItem1
                                                        , lista_gambiarra);
            lista_movimentacoes.Adapter = arrayAdapter;
        }

        private void Calendario_mes_DateChange(object sender, CalendarView.DateChangeEventArgs e)
        {
            //dataEscolhida = e.DayOfMonth.ToString() + "/" + e.Month + "/" + e.Year;
            AtualizaLista(e.DayOfMonth);
        }

        private void AtualizaLista(int dia)
        {
            List<string> listaDiaEscolhido = new List<string>();
            ArrayAdapter<string> arrayAdapter;

            try
            {
                var res = from a in objetosMovimentacoes where a.DiaReserva == dia select a;

                Movimentacoes retorno = objetosMovimentacoes.Find(a => a.DiaReserva == dia);

                if (res.Count() == 0)
                {
                    listaDiaEscolhido.Add("Nenhuma reserva para este dia.");

                    arrayAdapter = new ArrayAdapter<string>(this,
                                                           Android.Resource.Layout.SimpleListItem1
                                                           , listaDiaEscolhido);
                    lista_movimentacoes.Adapter = arrayAdapter;
                    return;
                }                

                foreach (var item in res)
                {
                    listaDiaEscolhido.Add("RESERVA: " + item.NumeroReserva + " - CLIENTE: " + item.NomeCliente + " - DIA: " + item.DiaReserva);
                }
                arrayAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1
                                                            , listaDiaEscolhido);
                lista_movimentacoes.Adapter = arrayAdapter;
            }
            catch (Exception e)
            {
                // criar caixas de mensagem de alerta 
                // https://www.macoratti.net/16/07/xamand_dial1.htm

                MostrarMensagem("Erro inesperado, contate o suporte.", e.Message.ToString());
            }           
        }
        public void MostrarMensagem(string titulo, string texto)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            AlertDialog alerta = builder.Create();
            alerta.SetTitle(titulo);
            alerta.SetIcon(Android.Resource.Drawable.IcDialogAlert);
            alerta.SetMessage(texto);
            alerta.SetButton("OK", (s, ev) =>
            {
                Toast.MakeText(this, "Tudo bem, pode continuar.", ToastLength.Short).Show();
            });
            alerta.Show();
        }

        private void DefineReservas()
        {
            string nomesPessoas = "Maria,José,Antônio,João,Francisca,Ana,Luiz,Paulo,Carlos,Manoel,Pedro,Francisca,Marcos,Raimundo,Sebastiã,Antônia,Marcelo,Jorge,Márcia,Geraldo,Adriana,Sandra,Luis,Fernando,Fabio,Roberta,Márcio,Edson,André,Sérgio,Josefa,Patrícia,Daniel,Rodrigo,Rafael,Joaquim,Vera,Ricardo,Eduardo";
            string[] nomes = nomesPessoas.Split(',');
            Random random = new Random();
                        
            for (int i = 1; i <= 31; i++)
            {
                objetosMovimentacoes.Add(new Movimentacoes() { 
                                                                DiaReserva = random.Next(1, 31), 
                                                                NomeCliente = nomes[random.Next(1, 39)], 
                                                                NumeroReserva = random.Next(1,300) 
                                                             });
                                
                lista_gambiarra.Add("RESERVA: " + objetosMovimentacoes[i - 1].NumeroReserva + " - CLIENTE: " + objetosMovimentacoes[i - 1].NomeCliente + " - DIA: " + objetosMovimentacoes[i - 1].DiaReserva);                                
            }
            ArrayAdapter<string> arrayAdapter = new ArrayAdapter<string>(this,
                                                        Android.Resource.Layout.SimpleListItem1
                                                        , lista_gambiarra);
            lista_movimentacoes.Adapter = arrayAdapter;
        }

        private class Movimentacoes {
             
            public int DiaReserva { get; set; }
            public int NumeroReserva { get; set; }
            public string NomeCliente { get; set; }
        }

    }
}
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

namespace TelasColetor.Fonte.Descarregamento
{
    [Activity(Label = "DescarregamentoCancelarConfirmaInformacoes")]
    public class DescarregamentoCancelarConfirmaInformacoes : Activity
    {
        #region Decalação de variáveis

        EditText descarregamento_cancelar_confirma_informacoes_baia;
        EditText descarregamento_cancelar_confirma_informacoes_carga;
        EditText descarregamento_cancelar_confirma_informacoes_data;
        EditText descarregamento_cancelar_confirma_informacoes_status;
        TextView descarregamento_cancelar_confirma_informacoes_descricao;
        Button   descarregamento_cancelar_confirma_informacoes_botao_confirmar;
        Button   descarregamento_cancelar_confirma_informacoes_botao_voltar;

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.DescarregamentoCancelarConfirmaInformacoes);

            descarregamento_cancelar_confirma_informacoes_baia            = FindViewById<EditText>(Resource.Id.descarregamento_cancelar_confirma_informacoes_baia);
            descarregamento_cancelar_confirma_informacoes_carga           = FindViewById<EditText>(Resource.Id.descarregamento_cancelar_confirma_informacoes_carga);
            descarregamento_cancelar_confirma_informacoes_data            = FindViewById<EditText>(Resource.Id.descarregamento_cancelar_confirma_informacoes_data);
            descarregamento_cancelar_confirma_informacoes_status          = FindViewById<EditText>(Resource.Id.descarregamento_cancelar_confirma_informacoes_status);
            descarregamento_cancelar_confirma_informacoes_descricao       = FindViewById<TextView>(Resource.Id.descarregamento_cancelar_confirma_informacoes_descricao);
            descarregamento_cancelar_confirma_informacoes_botao_confirmar = FindViewById<Button>(Resource.Id.descarregamento_cancelar_confirma_informacoes_botao_confirmar);
            descarregamento_cancelar_confirma_informacoes_botao_voltar    = FindViewById<Button>(Resource.Id.descarregamento_cancelar_confirma_informacoes_botao_voltar);

            System.Random random = new System.Random();
            string[] status      = { "AGUARDANDO", "CONFERINDO", "ARMAZENANDO" };

            string[] descricao   = { "CARGA DE ALIMENTOS PERECÍVEIS, PERDEU", 
                                     "PRODUTOS VIERAM FALTANDO E PRECISAM SER REVISTOS", 
                                     "CARGA ESTRAVIOU PELO CAMINHO E INFELIZMENTE NÃO FOI POSSÍVEL RECUPERAR" };

            descarregamento_cancelar_confirma_informacoes_baia.Text      = Intent.GetStringExtra("baia");
            descarregamento_cancelar_confirma_informacoes_carga.Text     = random.Next(10000,70000).ToString().PadLeft(8,'0');
            descarregamento_cancelar_confirma_informacoes_data.Text      = TransferenciaInformacoes.GetDataAleatoria();
            descarregamento_cancelar_confirma_informacoes_status.Text    = status[random.Next(0, status.Length - 1)];
            descarregamento_cancelar_confirma_informacoes_descricao.Text = descricao[random.Next(0, descricao.Length - 1)];

            descarregamento_cancelar_confirma_informacoes_botao_confirmar.Click += Descarregamento_cancelar_confirma_informacoes_botao_confirmar_Click;

            descarregamento_cancelar_confirma_informacoes_botao_voltar.Click += (sender, e) => this.Finish();
        }

        private void Descarregamento_cancelar_confirma_informacoes_botao_confirmar_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            AlertDialog msg = builder.Create();
            msg.SetTitle("Atenção");
            msg.SetMessage("Deseja cancelar o descarregamento? Este processo desfaz todo recebimento e cadastro da carga. Se houver escaneamento de conferência" +
                ", esta conferência será excluída. Deseja continuar?");
            msg.SetIcon(Resource.Drawable.icons8_question_48);
            msg.SetButton("CANCELAR", (ev, teste) =>
            {
               
            });
            msg.SetButton2("OK", (a,b) => 
            {

                SegundaConfirmacao();
            });
            msg.Show();
        }

        public void SegundaConfirmacao()
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            AlertDialog msg = builder.Create();
            msg.SetTitle("Atenção");
            msg.SetMessage("Notas fiscais da carga terão seus eventos de recebimento cancelados. " +
                "Para receber estas notas, deverá cadastrá-las em uma nova carga. Deseja continuar?");
            msg.SetIcon(Resource.Drawable.icons8_question_48);
            msg.SetButton("CANCELAR", (ev, teste) =>
            {

            });
            msg.SetButton2("OK", (ev, teste) =>
            {
                TerceiraConfirmacao();
            });
            msg.Show();
        }

        public void TerceiraConfirmacao()
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            AlertDialog msg = builder.Create();
            msg.SetTitle("Atenção");
            msg.SetMessage("Tem certeza que deseja desfazer este descarregamento? Uma vez que desfazer este descarregamento, não tem como" +
                " voltar atrás. Deseja continuar?");
            msg.SetIcon(Resource.Drawable.icons8_question_48);
            msg.SetButton("CANCELAR", (ev, teste) =>
            {

            });
            msg.SetButton2("OK", (ev, teste) =>
            {
                MensagemSucessoCancelamento();
            });
            msg.Show(); 
        }

        public void MensagemSucessoCancelamento()
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            AlertDialog msg = builder.Create();
            msg.SetTitle("Sucesso");
            msg.SetMessage("Cancelamento do descarregamento efetuado com sucesso.");
            msg.SetIcon(Resource.Drawable.Icones_Mensagem_Sucesso);
            msg.SetButton("OK", (ev, teste) =>
            {
                StartActivity(typeof(DescarregamentoMenuPrincipal));
            });
            msg.Show();
        }
    }
} 
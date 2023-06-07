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
    public class DialogFragmentClass : DialogFragment
    {
        EditText descarregamento_finaliza_usuario_login_codigo_usuario;
        EditText descarregamento_finaliza_usuario_login_senha_usuario;
        Button   descarregamento_finaliza_usuario_login_botao_confirma;
        Button   descarregamento_finaliza_usuario_login_botao_sair;

        #pragma warning disable CS0672 // O membro substitui o membro obsoleto
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        #pragma warning restore CS0672 // O membro substitui o membro obsoleto
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.DescarregamentoFinalizaUsuarioLogin, container, false);

            descarregamento_finaliza_usuario_login_codigo_usuario = view.FindViewById<EditText>(Resource.Id.descarregamento_finaliza_usuario_login_codigo_usuario);
            descarregamento_finaliza_usuario_login_senha_usuario  = view.FindViewById<EditText>(Resource.Id.descarregamento_finaliza_usuario_login_senha_usuario);
            descarregamento_finaliza_usuario_login_botao_confirma = view.FindViewById<Button>(Resource.Id.descarregamento_finaliza_usuario_login_botao_confirma);
            descarregamento_finaliza_usuario_login_botao_sair     = view.FindViewById<Button>(Resource.Id.descarregamento_finaliza_usuario_login_botao_sair);

            descarregamento_finaliza_usuario_login_botao_sair.Click     += Descarregamento_finaliza_usuario_login_botao_sair_Click;
            descarregamento_finaliza_usuario_login_botao_confirma.Click += Descarregamento_finaliza_usuario_login_botao_confirma_Click;

            return view;
        }

        private void Descarregamento_finaliza_usuario_login_botao_confirma_Click(object sender, EventArgs e)
        {
            if (descarregamento_finaliza_usuario_login_codigo_usuario.Text.Replace(" ", "") == "")
            {
                descarregamento_finaliza_usuario_login_codigo_usuario.RequestFocus();
                Toast.MakeText(Application.Context, "Informe um usuário.", ToastLength.Long).Show();
                return;
            }

            if (descarregamento_finaliza_usuario_login_senha_usuario.Text.Replace(" ", "") == "")
            {
                descarregamento_finaliza_usuario_login_senha_usuario.RequestFocus();
                Toast.MakeText(Application.Context, "Informe uma senha.", ToastLength.Long).Show();
                return;
            }

            if (!string.IsNullOrEmpty(descarregamento_finaliza_usuario_login_codigo_usuario.Text) && descarregamento_finaliza_usuario_login_codigo_usuario.Text == "64429")
            {
                if (!string.IsNullOrEmpty(descarregamento_finaliza_usuario_login_senha_usuario.Text) && descarregamento_finaliza_usuario_login_senha_usuario.Text == "1234")
                {
                    Toast.MakeText(Application.Context, "Descarregamento Finalizado com sucesso.", ToastLength.Long).Show();

                    Intent intent = new Intent(Application.Context, typeof(DescarregamentoMenuPrincipal));
                    StartActivity(intent);
                    return;
                }                
            }
            Toast.MakeText(Application.Context, "Usuário não encontrado ou sem permissão.", ToastLength.Long).Show();
        }

        private void Descarregamento_finaliza_usuario_login_botao_sair_Click(object sender, EventArgs e)
        {
            this.Dismiss();
        }
    }
}
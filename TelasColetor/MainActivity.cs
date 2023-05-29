using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using TelasColetor.Fonte;

namespace TelasColetor
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        LinearLayout layout_bt_separacao_paletizada;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);


            layout_bt_separacao_paletizada = FindViewById<LinearLayout>(Resource.Id.layout_bt_separacao_paletizada);

            layout_bt_separacao_paletizada.Click += Layout_bt_separacao_paletizada_Click;
        }

        private void Layout_bt_separacao_paletizada_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(SeparacaoPaletizada));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
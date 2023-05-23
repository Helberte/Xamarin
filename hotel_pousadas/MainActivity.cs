using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;

namespace hotel_pousadas
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        ImageView imgLogo;
        ImageView imgLogoUser;
        ImageView imgPassword;
        EditText  edUser;
        EditText  edPassword;
        Button    btEntrar;

        Conection conection;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            imgLogo     = FindViewById<ImageView>(Resource.Id.imgLogo);
            imgLogoUser = FindViewById<ImageView>(Resource.Id.imgLogoUser);
            imgPassword = FindViewById<ImageView>(Resource.Id.imgPassword);
            edUser      = FindViewById<EditText>(Resource.Id.edUser);
            edPassword  = FindViewById<EditText>(Resource.Id.edPassword);
            btEntrar    = FindViewById<Button>(Resource.Id.btEntrar);

            imgLogo.SetImageResource(Resource.Drawable.logo);
            imgLogoUser.SetImageResource(Resource.Drawable.usuarios);
            imgPassword.SetImageResource(Resource.Drawable.senha);            

            btEntrar.Click += BtEntrar_Click;
        }

        private void BtEntrar_Click(object sender, System.EventArgs e)
        {
            
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
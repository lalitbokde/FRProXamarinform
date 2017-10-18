using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Refractored.XamForms.PullToRefresh.Droid;

namespace FR.Droid
{
    [Activity(Label = "FR", Icon = "@drawable/fr_logo", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            Forms9Patch.Droid.Settings.LicenseKey = "NZPK-RMP4-PJVV-Z7LP-78JF-GNXB-CDJZ-SRYA-BLR2-WBZC-G64K-QJZW-65DB";
            //MR.Gestures.Android.Settings.LicenseKey = "ALZ9-BPVU-XQ35-CEBG-5ZRR-URJQ-ED5U-TSY8-6THP-3GVU-JW8Z-RZGE-CQW6";
            UserDialogs.Init(this);
            PullToRefreshLayoutRenderer.Init();
            LoadApplication(new App());
            
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using FR.CustomViews;
using FR.Droid.Renderers;
using Android.Graphics;

[assembly: ExportRenderer(typeof(CustomLabelUnderline), typeof(CustomLabelUnderlineRenderer))]
namespace FR.Droid.Renderers
{
    class CustomLabelUnderlineRenderer : LabelRenderer
    {
        public CustomLabelUnderlineRenderer()
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var label = (TextView)Control; // for example
                label.PaintFlags = PaintFlags.UnderlineText;
                label.SetTextColor(Resources.GetColor(Resource.Color.Blue));
                label.TextFormatted = Android.Text.Html.FromHtml("Click: " + "a href=\"http://www.google.com\">www.google.com a");
                label.MovementMethod = Android.Text.Method.LinkMovementMethod.Instance;
                label.LinksClickable = true;
            }
        }
    }
}
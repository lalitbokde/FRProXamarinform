using Xamarin.Forms;

namespace FR.Common
{
    public static class FontResources
    {
        //public static readonly Font LargeHeader = Font.SystemFontOfSize(Device.OnPlatform(30, 30, 50));
        //public static readonly Font Header = Font.SystemFontOfSize(Device.OnPlatform(16, 16, 26));
        //public static readonly Font Subheader = Font.SystemFontOfSize(Device.OnPlatform(12, 14, 24));
        public static readonly Font Standard = Font.SystemFontOfSize(Device.OnPlatform(15, 15, 22));
        public static readonly Font StandardBold = Font.SystemFontOfSize(Device.OnPlatform(15, 15, 22)).WithAttributes(FontAttributes.Bold);
        public static readonly Font Small = Font.SystemFontOfSize(Device.OnPlatform(10, 10, 16));
    }
}

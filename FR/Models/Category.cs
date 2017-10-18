using System;
using Xamarin.Forms;

namespace FR
{
    public class Category
    {
        public string Name
        {
            get;
            set;
        }
        public string Icon
        {
            get;
            set;
        }

        public Func<ContentPage> PageFn
        {
            get;
            set;
        }

        public Category(string name, Func<ContentPage> pageFn, string icon)
        {
            Name = name;
            PageFn = pageFn;
            Icon = icon;
        }
    }
}

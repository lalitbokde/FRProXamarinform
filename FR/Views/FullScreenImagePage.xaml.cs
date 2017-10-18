using Xamarin.Forms;

namespace FR
{
    public partial class FullScreenImagePage : ContentPage
    {

        public FullScreenImagePage(string imageUrl)
        {
            InitializeComponent();
            ppImage.Source = imageUrl;
        }
    }
}

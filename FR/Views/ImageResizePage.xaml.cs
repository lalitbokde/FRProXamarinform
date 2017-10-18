using Xamarin.Forms;

namespace FR
{
    public partial class ImageResizePage : ContentPage
    {
        public ImageResizePage()
        {
            InitializeComponent();

            btnResizeImage.Clicked +=  (sender, args) =>
            {
                ffilImage.Source = "http://loremflickr.com/600/600/nature?filename=simple.jpg";
            };
        }
    }
}

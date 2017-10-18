using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace FR.Component
{
    public partial class Pagination : StackLayout
    {
        static int mTotalPage = 0;
        static int mCurrentPage = 0;
        static string mTarget = "";

        public Pagination()
        {
            InitializeComponent();
        }

        #region CurrentPage
        public string CurrentPage { get; set; }
        public static readonly BindableProperty CurrentPageProperty =
           BindableProperty.Create<Pagination, string>(
           p => p.CurrentPage,
           "",
           BindingMode.OneWay,
           null,
           new BindableProperty.BindingPropertyChangedDelegate<string>(HandleCurrentPageChanged));

        static void HandleCurrentPageChanged(BindableObject bindable, object oldValue, object newValue)
        {
            int.TryParse(newValue.ToString(), out mCurrentPage);

            int prev = mCurrentPage - 1;
            int next = mCurrentPage + 1;

            if (prev < 1)
            {
                prev = 1;
                ((Pagination)bindable).lblPrev.IsVisible = false;
            }
            else
            {
                ((Pagination)bindable).lblPrev.IsVisible = true;
                ((Pagination)bindable).lblPrev.Text = "<<";
            }

            if (next > mTotalPage)
            {
                next = mTotalPage;
                ((Pagination)bindable).lblNext.IsVisible = false;
            }
            else
            {
                ((Pagination)bindable).lblNext.IsVisible = true;
                ((Pagination)bindable).lblNext.Text = ">>";
            }

            ((Pagination)bindable).tgrlblPrev.CommandParameter = prev.ToString();
            ((Pagination)bindable).tgrlblNext.CommandParameter = next.ToString();

            #region Number Display 

            int currentPageleft4 = 1;
            int currentPageleft3 = 2;
            int currentPageleft2 = 3;
            int currentPageleft1 = 4;
            int currentPage = 5;
            int currentPageright1 = 6;
            int currentPageright2 = 7;
            int currentPageright3 = 8;
            int currentPageright4 = 9;

            if (mTotalPage >= 10 && mCurrentPage > 5 )
            {
                currentPageleft4 = mCurrentPage - 4;
                currentPageleft3 = mCurrentPage - 3;
                currentPageleft2 = mCurrentPage - 2;
                currentPageleft1 = mCurrentPage - 1;
                currentPage = mCurrentPage;
                currentPageright1 = mCurrentPage + 1;
                currentPageright2 = mCurrentPage + 2;
                currentPageright3 = mCurrentPage + 3;
                currentPageright4 = mCurrentPage + 4;
            }
            #endregion

            #region Set Label Number And Parameter 
            if (mTotalPage >= 1)
            {
                ((Pagination)bindable).lblItem1.IsVisible = true;
                ((Pagination)bindable).lblItem1.Text = currentPageleft4.ToString();
                ((Pagination)bindable).tgrlblItem1.CommandParameter = currentPageleft4.ToString();
            }

            if (mTotalPage >= 2)
            {

                ((Pagination)bindable).lblItem2.IsVisible = true;
                ((Pagination)bindable).lblItem2.Text = currentPageleft3.ToString();
                ((Pagination)bindable).tgrlblItem2.CommandParameter = currentPageleft3.ToString();

            }

            if (mTotalPage >= 3)
            {
                ((Pagination)bindable).lblItem3.IsVisible = true;
                ((Pagination)bindable).lblItem3.Text = currentPageleft2.ToString();
                ((Pagination)bindable).tgrlblItem3.CommandParameter = currentPageleft2.ToString();

            }

            if (mTotalPage >= 4)
            {
                ((Pagination)bindable).lblItem4.IsVisible = true;
                ((Pagination)bindable).lblItem4.Text = currentPageleft1.ToString();
                ((Pagination)bindable).tgrlblItem4.CommandParameter = currentPageleft1.ToString();
            }

            if (mTotalPage >= 5)
            {
                ((Pagination)bindable).lblItem5.IsVisible = true;
                ((Pagination)bindable).lblItem5.Text = currentPage.ToString();
                ((Pagination)bindable).tgrlblItem5.CommandParameter = currentPage.ToString();
            }

            if (mTotalPage >= 6 && mTotalPage >= currentPageright1)
            {
                ((Pagination)bindable).lblItem6.IsVisible = true;
                ((Pagination)bindable).lblItem6.Text = currentPageright1.ToString();
                ((Pagination)bindable).tgrlblItem6.CommandParameter = currentPageright1.ToString();
            }

            if (mTotalPage >= 7 && mTotalPage >= currentPageright2)
            {
                ((Pagination)bindable).lblItem7.IsVisible = true;
                ((Pagination)bindable).lblItem7.Text = currentPageright2.ToString();
                ((Pagination)bindable).tgrlblItem7.CommandParameter = currentPageright2.ToString();
            }

            if (mTotalPage >= 8 && mTotalPage >= currentPageright3)
            {
                ((Pagination)bindable).lblItem8.IsVisible = true;
                ((Pagination)bindable).lblItem8.Text = currentPageright3.ToString();
                ((Pagination)bindable).tgrlblItem8.CommandParameter = currentPageright3.ToString();
            }

            if (mTotalPage >= 9 && mTotalPage >= currentPageright4 )
            {
                ((Pagination)bindable).lblItem9.IsVisible = true;
                ((Pagination)bindable).lblItem9.Text = currentPageright4.ToString();
                ((Pagination)bindable).tgrlblItem9.CommandParameter = currentPageright4.ToString();
            }

            #endregion

            #region Color Current Page Number
            if (mTotalPage == mCurrentPage)
            {
                ((Pagination)bindable).lblNext.IsVisible = false;
            }

            if (newValue.ToString() == ((Pagination)bindable).lblItem1.Text)
            {
                ((Pagination)bindable).lblItem1.TextColor = Color.Aqua;
            }
            else if (newValue.ToString() == ((Pagination)bindable).lblItem2.Text)
            {
                ((Pagination)bindable).lblItem2.TextColor = Color.Aqua;
            }
            else if (newValue.ToString() == ((Pagination)bindable).lblItem3.Text)
            {
                ((Pagination)bindable).lblItem3.TextColor = Color.Aqua;
            }
            else if (newValue.ToString() == ((Pagination)bindable).lblItem4.Text)
            {
                ((Pagination)bindable).lblItem4.TextColor = Color.Aqua;
            }
            else if (newValue.ToString() == ((Pagination)bindable).lblItem5.Text)
            {
                ((Pagination)bindable).lblItem5.TextColor = Color.Aqua;
            }
            else if (newValue.ToString() == ((Pagination)bindable).lblItem6.Text)
            {
                ((Pagination)bindable).lblItem6.TextColor = Color.Aqua;
            }
            else if (newValue.ToString() == ((Pagination)bindable).lblItem7.Text)
            {
                ((Pagination)bindable).lblItem7.TextColor = Color.Aqua;
            }
            else if (newValue.ToString() == ((Pagination)bindable).lblItem8.Text)
            {
                ((Pagination)bindable).lblItem8.TextColor = Color.Aqua;
            }
            else if (newValue.ToString() == ((Pagination)bindable).lblItem9.Text)
            {
                ((Pagination)bindable).lblItem9.TextColor = Color.Aqua;
            }
            #endregion

        }
        #endregion

        #region TotalPage
        public string TotalPage { get; set; }
        public static readonly BindableProperty TotalPageProperty =
           BindableProperty.Create<Pagination, string>(
           p => p.TotalPage,
           "",
           BindingMode.OneWay,
           null,
           new BindableProperty.BindingPropertyChangedDelegate<string>(HandleTotalPageChanged));

        static void HandleTotalPageChanged(BindableObject bindable, object oldValue, object newValue)
        {
            int.TryParse(newValue.ToString(), out mTotalPage);
        }
        #endregion

        #region Target
        public string Target { get; set; }
        public static readonly BindableProperty TargetProperty =
           BindableProperty.Create<Pagination, string>(
           p => p.Target,
           "",
           BindingMode.OneWay,
           null,
           new BindableProperty.BindingPropertyChangedDelegate<string>(HandleTargetChanged));

        static void HandleTargetChanged(BindableObject bindable, object oldValue, object newValue)
        {
            mTarget = newValue.ToString();
        }
        #endregion

        void OnTappedItem(object sender, EventArgs args)
        {
            TappedEventArgs tea = (TappedEventArgs)args;

            int currentPage = 0;
            int.TryParse(tea.Parameter.ToString(), out currentPage);

            if (mTarget == "Enroll")
            {
                try
                {
                   
                    var mainPage = new MenuPage();
                    var drawerPage = new DrawerPage();
                    drawerPage.OnMenuSelect = (categoryPage) =>
                    {
                        mainPage.Detail = new NavigationPage(categoryPage)
                        {
                            BarBackgroundColor = Color.FromHex("#ffffff"),
                            BarTextColor = Color.Black
                        };
                        mainPage.IsPresented = false;
                    };
                    mainPage.Master = drawerPage;

                    mainPage.Detail = new NavigationPage(new EnrollListPage(currentPage))
                    {
                        BarBackgroundColor = Color.FromHex("#ffffff"),//your color here
                        BarTextColor = Color.Black
                    };
                    App.Current.MainPage = mainPage;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"				ERROR {0}", ex.Message);
                };
            }
            
        }

    }
}

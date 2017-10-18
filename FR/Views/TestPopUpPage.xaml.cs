using System;
using System.Text;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace FR
{
    public partial class TestPopUpPage : ContentPage
    {
        public TestPopUpPage()
        {
            InitializeComponent();
        }

        private async void OnUserAnimationPupup(object sender, EventArgs e)
        {
            var page = new ModalChatMessageActionPage("Hello World","123");
            await Navigation.PushPopupAsync(page);
        }

        private async void OnClickedChatRoomLoginModal(object sender, EventArgs e)
        {
            ChatRoom chatRoom = new ChatRoom(12, "Hello Chat Room", "Chat Room Description", 1, "123");

            var page = new ModalPrivateChatRoomLoginPage(chatRoom, Navigation);
            await Navigation.PushPopupAsync(page);
        }

        private void OnClickedReg(object sender, EventArgs e)
        {
            string test = "0125552222";

            string result = HidePhoneNo(test, 'X');
            string test1 = "";
        }

        public string HidePhoneNo(string input, char target)
        {
            //input = 012 234 1234 
            //output =012XXX2324
            string strNoWhiteSpace = RemoveWhitespace(input);
            StringBuilder sb = new StringBuilder(strNoWhiteSpace.Length);
            int totalLength = sb.Length;
            string first3Char = strNoWhiteSpace.Substring(0, 3);
            string last4Char = strNoWhiteSpace.Substring((strNoWhiteSpace.Length - 4), 4);

            if (strNoWhiteSpace.Length > 7)
            {
                for (int i = 0; i < (strNoWhiteSpace.Length - 7); i++)
                {
                    sb.Append(target);
                }
            }
          

            return first3Char + sb.ToString() + last4Char;
        }

        public string RemoveWhitespace(string str)
        {
            return string.Join("", str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }
    }
}

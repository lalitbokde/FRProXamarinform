using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FR.ViewModels
{
    public class ChatMessagePMViewModel : BaseViewModel
    {
        public ObservableCollection<ChatMessage> Items { get; set; }
        public int RelationshipID { get; set; }
        public int LastDisplayHistoryChatMessageID { get; set; }
        public int HistoryChatMessagePage { get; set; }
        Page page;

        public ChatMessagePMViewModel(Page page, ObservableCollection<ChatMessage> chatMessages, int relationshipID)
        {
            this.page = page;
            Items = chatMessages;

            int count = chatMessages.Count;
            this.LastDisplayHistoryChatMessageID = chatMessages[count - 1].ID;

            this.HistoryChatMessagePage = 1;
            this.RelationshipID = relationshipID;
        }

        bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetField(ref isBusy, value); }
        }

        ICommand refreshCommand;
        public ICommand RefreshCommand
        {
            get {

                    return refreshCommand ?? (
                        refreshCommand = new Command(
                            async () => await ExecuteRefreshCommand()
                        )
                    );
            }
        }

        async Task ExecuteRefreshCommand()
        {
            
            if (IsBusy)
                return;

            IsBusy = true;
            int newPage = HistoryChatMessagePage + 1;
            ChatMessageListResponseResult chatMessagesResponseResult = await App.ChatMessageManager.GetPrivateMessagesResultPage(RelationshipID, newPage, 15, LastDisplayHistoryChatMessageID);
 
            int i = 0;
            foreach (var item in chatMessagesResponseResult.ChatMessages)
            {
                Items.Insert(i, item);
                i++;
            }

            this.HistoryChatMessagePage = newPage;
            IsBusy = false;

        }
    }
}

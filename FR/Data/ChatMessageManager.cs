using Plugin.Media.Abstractions;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FR
{
    public class ChatMessageManager
    {
        IChatMessageRestService restService;

        public ChatMessageManager(IChatMessageRestService service)
        {
            restService = service;
        }

        public Task<ObservableCollection<ChatMessage>> GetAll(int chatRoomID)
        {
            return restService.GetAll(chatRoomID);
        }

        public Task<ChatMessageListResponseResult> GetResultPageByChatRoomID(int chatRoomID, int pageNumber, int itemPerPage, int lastDisplayHistoryChatMessageID)
        {
            return restService.GetResultPageByChatRoomID(chatRoomID, pageNumber, itemPerPage, lastDisplayHistoryChatMessageID);
        }

        public Task<ChatMessage> Create(ChatMessage chatMessage) {
            return restService.Create(chatMessage);
        }

        public Task<ObservableCollection<ChatMessage>> GetNewChatMessage(int chatRoomID, int requestUserID)
        {
            return restService.GetNewChatMessage(chatRoomID, requestUserID);
        }

        public Task<ChatMessage> UploadImage(MediaFile file, int chatRoomID, int senderUserID)
        {
            return restService.UploadImage(file, chatRoomID, senderUserID);
        }

        public Task<ChatMessage> UploadVoice(byte[] audio, int chatRoomID, int senderUserID)
        {
            return restService.UploadVoice(audio, chatRoomID, senderUserID);
        }

        public Task<ObservableCollection<ChatMessage>> GetAllPrivateMessages(int relationshipID)
        {
            return restService.GetAllPrivateMessages(relationshipID);
        }

        public Task<ChatMessageListResponseResult> GetPrivateMessagesResultPage(int relationshipID, int pageNumber, int itemPerPage, int lastDisplayHistoryChatMessageID)
        {
            return restService.GetPrivateMessagesResultPage(relationshipID, pageNumber, itemPerPage, lastDisplayHistoryChatMessageID);
        }

        public Task<ChatMessage> CreatePrivateMessage(ChatMessage chatMessage)
        {
            return restService.CreatePrivateMessage(chatMessage);
        }

        public Task<ObservableCollection<ChatMessage>> GetNewPrivateMessages(int relationshipID, int requestUserID)
        {
            return restService.GetNewPrivateMessages(relationshipID, requestUserID);
        }

        public Task<ChatMessage> UploadPMImage(MediaFile file, int senderUserID, int receiverUserID)
        {
            return restService.UploadPMImage(file, senderUserID, receiverUserID);
        }

        public Task<ChatMessage> UploadPMVoice(byte[] audio, int senderUserID, int receiverUserID)
        {
            return restService.UploadPMVoice(audio, senderUserID, receiverUserID);
        }

    }
}

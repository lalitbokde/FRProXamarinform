using Plugin.Media.Abstractions;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FR
{
    public interface IChatMessageRestService
    {
        Task<ObservableCollection<ChatMessage>> GetAll(int chatRoomID);

        Task<ChatMessageListResponseResult> GetResultPageByChatRoomID(int chatRoomID, int pageNumber, int itemPerPage, int lastDisplayHistoryChatMessageID);

        Task<ChatMessage> Create(ChatMessage chatMessage);

        Task<ObservableCollection<ChatMessage>> GetNewChatMessage(int chatRoomID, int requestUserID);

        Task<ChatMessage> UploadImage(MediaFile file, int chatRoomID, int senderUserID);

        Task<ChatMessage> UploadVoice(byte[] audio, int chatRoomID, int senderUserID);

        Task<ObservableCollection<ChatMessage>> GetAllPrivateMessages(int relationshipID);

        Task<ChatMessageListResponseResult> GetPrivateMessagesResultPage(int relationshipID, int pageNumber, int itemPerPage, int lastDisplayHistoryChatMessageID);

        Task<ChatMessage> CreatePrivateMessage(ChatMessage chatMessage);

        Task<ObservableCollection<ChatMessage>> GetNewPrivateMessages(int relationshipID, int requestUserID);

        Task<ChatMessage> UploadPMImage(MediaFile file, int senderUserID, int receiverUserID);

        Task<ChatMessage> UploadPMVoice(byte[] audio, int senderUserID, int receiverUserID);
    }
}

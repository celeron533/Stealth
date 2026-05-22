using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Stealth.ViewModel
{
    public sealed class ShowAboutViewMessage : ValueChangedMessage<string>
    {
        public ShowAboutViewMessage() : base("ShowAboutView")
        {
        }
    }
}

using System.Collections.Generic;

namespace SanitizeChatApp.Client.Services
{
    // Holds the current state of the chat messages in memory
    public class ChatState
    {
        // List of messages exchanged in the chat
        public List<string> Messages { get; private set; } = new List<string>();

        // Adds a new message to the chat
        public void AddMessage(string message)
        {
            Messages.Add(message);
        }

        // Clears all messages from the chat
        public void ClearMessages()
        {
            Messages.Clear();
        }
    }
}

using System;
using System.Collections.Generic;


namespace Thankies.Core.Entity
{
    public class Chat
    {
        /// <summary>
        /// Id of the chat.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Optional, available when the type of the chat is private.
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Optional, available when the type of the chat is a group, supergroup or channel.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Type of the chat.
        /// </summary>
        public ChatType Type { get; set; }
        /// <summary>
        /// List of conversations that this chat has.
        /// </summary>
        public List<Conversation> Conversations { get; set; }
        /// <summary>
        /// Date of first interaction with the chat.
        /// </summary>
        public DateTime CreatedAt { get; set; }
        
        public enum ChatType
        {
            Private,
            Group,
            Channel,
            Supergroup,
        }
    }
}
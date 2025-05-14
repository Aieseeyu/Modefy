using System;
using System.ComponentModel.DataAnnotations;

namespace ModefyEcommerce.Models
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }

        // foreign key to Chat
        [Required]
        public int ChatId { get; set; }

        [Required]
        [StringLength(20)]
        public string SenderType { get; set; }

        [Required]
        public int SenderId { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public bool Read { get; set; } = false;

        [Required]
        public DateTime Date { get; set; }

        // default constructor
        public ChatMessage() { }

        // full constructor
        public ChatMessage(int id, int chatId, string senderType, int senderId, string content, bool read, DateTime date)
        {
            Id = id;
            ChatId = chatId;
            SenderType = senderType;
            SenderId = senderId;
            Content = content;
            Read = read;
            Date = date;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StateInterface.Model;

namespace SunGardStateInterface.API.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string MessageKey { get; set; }
        public string Description { get; set; }
        public List<MessageVariationModel> MessageVariations { get; set; }
        public MessageModel()
        {
            MessageVariations = new List<MessageVariationModel>();
        }
        public MessageModel(int parentId, Message message): this()
        {
            Id = message.Id;
            ParentId = parentId;
            MessageKey = message.MessageKey;
            Description = message.Description;
        }
        public Message ToDomain()
        {
            return new Message()
            {
                Id = Id,
                MessageKey = MessageKey,
                Description = Description,
            };
        }
    }
}
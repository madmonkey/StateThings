using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StateInterface.Model;

namespace SunGardStateInterface.API.Models
{
    public class MessageVariationModel
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public MessageVariationModel()
        {
        }
        public MessageVariationModel(int parentId, MessageVariation messageVariation)
            : this()
        {
            Id = messageVariation.Id;
            ParentId = parentId;
            Text = messageVariation.MessageText;
            Description = messageVariation.Description;
        }

        public MessageVariation ToDomain()
        {
            return new MessageVariation()
            {
                Id = Id,
                MessageText = Text,
                Description = Description,
            };
        }
    }
}
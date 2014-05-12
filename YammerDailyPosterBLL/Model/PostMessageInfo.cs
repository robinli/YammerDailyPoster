using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace YammerDailyPosterBLL.Model
{
    public class PostMessageInfo
    {
        public List<MessageInfo> Items;

        public PostMessageInfo()
        {
            this.Items = new List<MessageInfo>();
        }
    }

    public class MessageInfo
    {
        public Guid Id { get; set; }

        [DataType("Date")]
        public DateTime? Schedule { get; set; }

        public string Subject { get; set; }

        public string Context { get; set; }

        [DataType("Date")]
        public DateTime? SentTime { get; set; }

        public MessageInfo()
        {
            this.Id = Guid.NewGuid();
        }
    } 
}

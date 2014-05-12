using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YammerDailyPosterBLL.Model;

namespace YammerDailyPosterBLL
{
    public class PostMessager
    {
        public PostMessageInfo GetData()
        {
            StorageMan<PostMessageInfo> sm = new StorageMan<PostMessageInfo>();
            PostMessageInfo data = sm.Read();
            //if (data == null)
            //    data = new PostMessageInfo() { Items = new List<MessageInfo>() };

            return data;
        }

        

        public bool AddNew(DateTime? schedule, string subject, string context)
        {
            PostMessageInfo data = this.GetData();
            data.Items.Add(new MessageInfo()
            {
                Schedule = schedule
                ,
                Subject = subject
                ,
                Context = context
            });

            StorageMan<PostMessageInfo> sm = new StorageMan<PostMessageInfo>();
            sm.Save(data);
            return true;
        }
        public MessageInfo GetItem(Guid id)
        {
            PostMessageInfo data = this.GetData();
            return data.Items.FirstOrDefault(r => r.Id == id);
        }

        public bool UpdateItem(Guid id,DateTime? schedule, string subject, string context)
        {
            PostMessageInfo data = this.GetData();
            MessageInfo item = data.Items.FirstOrDefault(r => r.Id == id);
            item.Schedule = schedule;
            item.Subject = subject;
            item.Context = context;

            StorageMan<PostMessageInfo> sm = new StorageMan<PostMessageInfo>();
            sm.Save(data);
            return true;
        }

        public bool DeleteItem(Guid id)
        {
            PostMessageInfo data = this.GetData();
            MessageInfo item = data.Items.FirstOrDefault(r => r.Id == id);
            data.Items.Remove(item);
            StorageMan<PostMessageInfo> sm = new StorageMan<PostMessageInfo>();
            sm.Save(data);
            return true;
        }

        public MessageInfo GetMessageToPost()
        {
            PostMessageInfo data = this.GetData();
            if (data.Items.Count == 0)
                return null;

            //MessageInfo item = data.Items.Find(r => r.SentTime == null);

            MessageInfo item = data.Items.FirstOrDefault(r => r.SentTime == null);

            return item;
        }


        public void SentMessageComplete(Guid id)
        {
            PostMessageInfo data = this.GetData();
            MessageInfo item = data.Items.FirstOrDefault(r => r.Id == id);
            item.SentTime = System.DateTime.Now;
            StorageMan<PostMessageInfo> sm = new StorageMan<PostMessageInfo>();
            sm.Save(data);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YammerDailyPosterBLL.Model;

namespace YammerDailyPosterBLL
{
    public class YammerDailyPoster
    {
        //public void NewMessage(DateTime? schedule, string subject, string context)
        //{
        //    PostMessager PM = new PostMessager();
        //    PM.AddNew(schedule, subject, context);
        //}

        public void DoDailyPost()
        {
            PostMessager PM = new PostMessager();
            MessageInfo item = PM.GetMessageToPost();
            if(item==null)
                return;


            EmailHelper email = new EmailHelper();
            string email_addr = System.Configuration.ConfigurationManager.AppSettings["mail_to_address"];
            bool result = email.Send(email_addr, item.Subject, item.Context);

            if (result)
            {
                PM.SentMessageComplete(item.Id);
            }
                
        }

    }
}

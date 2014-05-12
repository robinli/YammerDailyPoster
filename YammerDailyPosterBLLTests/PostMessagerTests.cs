using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YammerDailyPosterBLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YammerDailyPosterBLL.Model;
namespace YammerDailyPosterBLL.Tests
{
    [TestClass()]
    public class PostMessagerTests
    {
        PostMessager PM = new PostMessager();

        [TestMethod()]
        public void GetDataTest()
        {
            
            PostMessageInfo data=PM.GetData();
            Assert.IsTrue(data!=null);
        }

        [TestMethod()]
        public void AddNewTest()
        {
            bool result = PM.AddNew(DateTime.Now, "Hello", "Hello subject");

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void GetMessageToPostTest_CompleteTest()
        {
            MessageInfo item = PM.GetMessageToPost();

            PM.SentMessageComplete(item.Id);

            Assert.IsTrue(item != null);
        }

    }
}

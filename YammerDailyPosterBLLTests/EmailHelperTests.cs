using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YammerDailyPosterBLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace YammerDailyPosterBLL.Tests
{
    [TestClass()]
    public class EmailHelperTests
    {
        [TestMethod()]
        public void SendTest()
        {
            EmailHelper email = new EmailHelper();
            bool result = email.Send("im@robinks.net", "Hello", "This is a test email by YammerDailyPosterBLL");
            Assert.IsTrue(result);
        }
    }
}

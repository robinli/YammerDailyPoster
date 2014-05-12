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
    public class YammerDailyPosterTests
    {
        [TestMethod()]
        public void DoDailyPostTest()
        {
            YammerDailyPoster ydp = new YammerDailyPoster();
            ydp.DoDailyPost();
            Assert.IsTrue(true);
        }
    }
}

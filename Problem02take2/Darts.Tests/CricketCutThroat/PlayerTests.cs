using Darts.CricketCutThroat;
using NUnit.Framework;

namespace Darts.Tests.CricketCutThroat
{
    [TestFixture]
    public class PlayerTests
    {
        private IPlayer player;

        [SetUp]
        public void SetUp()
        {
            this.player = new Player();
        }

        [TearDown]
        public void CleanUp()
        {
            this.player = null;
        }

        [Test]
        public void WhenPlayerThrownNumber3Times_ShouldReturn3()
        {
            player.Threw(18).Threw(18).Threw(18);

            var result = player.TimesPlayerThrown(18);

            Assert.AreEqual(3, result);
        }

        [Test]
        public void WhenNmberWasNeverThrown_ShouldReturn0()
        {
            player.Threw(20);

            var result = player.TimesPlayerThrown(19);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void WhenNumberThrownLessThan3Times_ShouldReturn1()
        {
            player.Threw(12);

            var result = player.TimesPlayerThrown(12);

            Assert.AreEqual(1, result);
        }
    }
}

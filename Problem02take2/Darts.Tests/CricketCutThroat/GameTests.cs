using Darts.CricketCutThroat;
using Darts.CricketCutThroat.Exceptions;
using NUnit.Framework;
using System;

namespace Darts.Tests.CricketCutThroat
{
    [TestFixture]
    public class GameTests
    {
        private IGame game;
        private TestPlayerFactory testPlayerFactory;

        [SetUp]
        public void SetUp()
        {
            testPlayerFactory = new TestPlayerFactory();
            game = new Game(testPlayerFactory);
        }

        [TearDown]
        public void TearDown()
        {
            game = null;
            testPlayerFactory = null;
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(1)]
        public void WhenInitialisingGameWithNotAllowedNumberOfPlayers_ShouldThrowException(
            int playersNumber)
        {
            var exception = Assert.Throws<InvalidPlayersNumberException>(
                () => game.InitiateGame(playersNumber));
        }

        [Test]
        public void WhenPlayerThrowsInNotCountingNumber_PointsShouldNotChange()
        {
            game.InitiateGame(playersNumber: 4);
            game.Threw(12);

            var points = game.GetPoints();

            Assert.AreEqual(new int[] { 0, 0, 0, 0 }, points);
        }

        [Test]
        public void WhenPlayerThrowsHisClosedNumber_ShouldAddPointsToOtherPlayers()
        {
            testPlayerFactory.IncludePlayerWithClosed(timesRequiredToClose: 3, numbers: 20);
            game.InitiateGame(playersNumber: 4);

            game.Threw(20);

            var points = game.GetPoints();

            Assert.AreEqual(new int[] { 0, 20, 20, 20 }, points);
        }
    }
}

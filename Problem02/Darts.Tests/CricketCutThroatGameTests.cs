using NUnit.Framework;
using System;

namespace Darts.Tests
{
    [TestFixture]
    public class CricketCutThroatGameTests
    {
        private CricketCutThroatGame game;

        private void CreateFourPlayerGame()
        {
            game = new CricketCutThroatGame(4);
        }

        private void EachPlayerThrow(int players, int number, int forRounds)
        {
            for (int t = 0; t < forRounds; t++)
            {
                for (int player = 0; player < players; player++)
                {
                    game.ThrowDart(number).ThrowDart(number).ThrowDart(number);
                }
            }
        }

        [TearDown]
        public void TearDown()
        {
            game = null;
        }

        [Test]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void WhenCreatingGameWithAllowedNumberOfPlayers_ShouldNotThrowException(
            int playersNumber)
        {
            new CricketCutThroatGame(playersNumber);
        }

        [Test]
        [TestCase(1)]
        [TestCase(5)]
        public void WhenCreatingGameWithNotAllowedNumberOfPlayers_ShouldThrowException(
            int playersNumber)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(
                () => new CricketCutThroatGame(playersNumber));
            Assert.AreEqual(
                "Allowed number of players is 2, 3, 4.\r\nParameter name: playersNumber",
                exception.Message);
        }

        [Test]
        public void WhenFirstThrowNotInScoringNumber_ScoresShouldNotChange()
        {
            var expected = new int[] { 0, 0, 0, 0 };
            CreateFourPlayerGame();

            game.ThrowDart(14);
            var result = game.Scores;

            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase(0)]
        [TestCase(21)]
        [TestCase(-1)]
        public void WhenThrowingNotAllowedNumber_ShouldThrowException(int numberHit)
        {
            var game = new CricketCutThroatGame(4);

            var exception = Assert.Throws<ArgumentOutOfRangeException>(
                () => game.ThrowDart(numberHit));
            Assert.AreEqual(
                "Numbers allowed to hit are form 1 to 20 and 25.\r\nParameter name: numberHit",
                exception.Message);
        }

        [Test]
        public void WhenFirstIsThrowingTrippleAndOne20_ShouldAdd20ToOthers()
        {
            var expected = new int[] { 0, 20, 20, 20 };
            CreateFourPlayerGame();

            game
                .ThrowDart(20, 3)
                .ThrowDart(20);

            var result = game.Scores;

            Assert.AreEqual(expected, result);
        }

        [Test]
        [TestCase(19, 3)]
        [TestCase(25, 1)]
        [TestCase(25, 2)]
        public void WhenThrowingWithAllowedMulitiplier_ShouldNotThrowException(
            int numberHit,
            int multiplier)
        {
            CreateFourPlayerGame();
            game.ThrowDart(numberHit, multiplier);
        }

        [Test]
        [TestCase(16, 4)]
        [TestCase(25, 3)]
        [TestCase(25, 6)]
        [TestCase(18, 0)]
        public void WhenThrowingWithNotAllowedMultiplier_ShouldThrowException(
            int numberHit,
            int multiplier)
        {
            CreateFourPlayerGame();

            var exception = Assert.Throws<ArgumentOutOfRangeException>(
                () => game.ThrowDart(numberHit, multiplier));
            Assert.AreEqual(
                "Allowed multipliers for number 25 are 1 and 2, for other numbers 1, 2, and 3.\r\nParameter name: multiplier",
                exception.Message);
        }

        [Test]
        public void WhenPlayerHitsClosedNumber_ShouldAddPointsToPlayersThatDidntClose()
        {
            var expected = new int[] { 0, 0, 17, 17 };
            CreateFourPlayerGame();

            game
                .ThrowDart(17).ThrowDart(17).ThrowDart(17)
                .ThrowDart(17, 3).ThrowDart(17);

            Assert.AreEqual(expected, game.Scores);
        }

        [Test]
        public void WhenGameIsNotFinished_WinnerShouldBeNull()
        {
            CreateFourPlayerGame();

            game.ThrowDart(20);

            var winner = game.Winner;

            Assert.AreEqual(null, winner);
        }

        [Test]
        public void After20RoundsWhenWinnerIsClear_TheGameShouldEnd()
        {
            CreateFourPlayerGame();
            EachPlayerThrow(players: 4, number: 20, forRounds: 19);

            game.ThrowDart(19, 3).ThrowDart(19).ThrowDart(19);
            game.ThrowDart(19).ThrowDart(19).ThrowDart(19);
            game.ThrowDart(19).ThrowDart(19).ThrowDart(19);
            game.ThrowDart(19).ThrowDart(19).ThrowDart(19);

            var winner = game.Winner;

            Assert.AreEqual(38, winner.Points);
        }

        [Test]
        public void IfGameHasFinished_ThrowShouldThrowException()
        {
            CreateFourPlayerGame();
            EachPlayerThrow(players: 4, number: 15, forRounds: 19);
            game.ThrowDart(19, 3).ThrowDart(19).ThrowDart(19);
            game.ThrowDart(19).ThrowDart(19).ThrowDart(19);
            game.ThrowDart(19).ThrowDart(19).ThrowDart(19);
            game.ThrowDart(19).ThrowDart(19).ThrowDart(19);

            var exception = Assert.Throws<InvalidOperationException>(
                () => game.ThrowDart(17));
            Assert.AreEqual(
                "Game has finished. Create a new game.",
                exception.Message);
        }

        [Test]
        public void WhenPlayerClosesAllAndHasSmallestNumberOfPoints_GameShouldEnd()
        {
            CreateFourPlayerGame();
            game.ThrowDart(20, 3).ThrowDart(19, 3).ThrowDart(18, 3);
            game.ThrowDart(19).ThrowDart(19).ThrowDart(19);
            game.ThrowDart(19).ThrowDart(19).ThrowDart(19);
            game.ThrowDart(19).ThrowDart(19).ThrowDart(19);
            game.ThrowDart(17, 3).ThrowDart(16, 3).ThrowDart(15, 3);
            game.ThrowDart(19).ThrowDart(19).ThrowDart(19);
            game.ThrowDart(19).ThrowDart(19).ThrowDart(19);
            game.ThrowDart(19).ThrowDart(19).ThrowDart(19);
            game.ThrowDart(25, 2).ThrowDart(25, 2);

            var finished = game.IsGameFinished;
            var winner = game.Winner;
            
            Assert.IsTrue(finished);
            Assert.AreEqual(0, winner.Points);
        }
    }
}

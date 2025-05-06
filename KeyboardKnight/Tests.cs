using NUnit.Framework;

namespace RPGGame.Tests
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void Player_InitialHealth_ShouldBe100()
        {
            var player = new Player();
            Assert.AreEqual(100, player.Health);
        }

        [Test]
        public void Player_TakeDamage_ShouldReduceHealth()
        {
            var player = new Player();
            player.Health -= 20;
            Assert.AreEqual(80, player.Health);
        }

        [Test]
        public void Player_HealthShouldNotGoBelowZero()
        {
            var player = new Player();
            player.Health = 0;
            player.Health -= 20;
            Assert.AreEqual(0, player.Health);
        }

        [Test]
        public void Player_Damage_ShouldBe10()
        {
            var player = new Player();
            Assert.AreEqual(10, player.Damage);
        }
    }

    [TestFixture]
    public class EnemyTests
    {
        [Test]
        public void Enemy_InitialHealth_ShouldBeCalculatedCorrectly()
        {
            var enemy = new Enemy(DifficultyLevel.Easy, 1);
            Assert.AreEqual(55, enemy.Health);
        }

        [Test]
        public void Enemy_InitialDamage_ShouldBeCalculatedCorrectly()
        {
            var enemy = new Enemy(DifficultyLevel.Easy, 1);
            Assert.AreEqual(9, enemy.Damage);
        }

        [Test]
        public void Enemy_HealthShouldNotGoBelowZero()
        {
            var enemy = new Enemy(DifficultyLevel.Easy, 1);
            enemy.Health = 0;
            enemy.Health -= 20;
            Assert.AreEqual(0, enemy.Health);
        }
    }

    [TestFixture]
    public class AbilityTests
    {
        [Test]
        public void Ability_ShouldHaveCorrectNameAndDescription()
        {
            var ability = new Ability("Double Damage", "Doubles the damage for one attack.");
            Assert.AreEqual("Double Damage", ability.Name);
            Assert.AreEqual("Doubles the damage for one attack.", ability.Description);
        }
    }

    [TestFixture]
    public class WordBankTests
    {
        [Test]
        public void GetWords_ShouldReturnCorrectNumberOfWordsForEasyDifficulty()
        {
            var words = WordBank.GetWords(0);
            Assert.AreEqual(20, words.Count);
        }

        [Test]
        public void GetWords_ShouldReturnCorrectNumberOfWordsForMediumDifficulty()
        {
            var words = WordBank.GetWords(1);
            Assert.AreEqual(20, words.Count);
        }

        [Test]
        public void GetWords_ShouldReturnCorrectNumberOfWordsForHardDifficulty()
        {
            var words = WordBank.GetWords(2);
            Assert.AreEqual(20, words.Count);
        }
    }
}
using NUnit.Framework;

namespace RPGGame.Tests
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void Player_InitialHealth_ShouldBe100()
        {
            var player = new Player();
            Assert.AreEqual(100, player.Health);
        }

        [Test]
        public void Player_TakeDamage_ShouldReduceHealth()
        {
            var player = new Player();
            player.Health -= 20;
            Assert.AreEqual(80, player.Health);
        }

        [Test]
        public void Player_HealthShouldNotGoBelowZero()
        {
            var player = new Player();
            player.Health = 0;
            player.Health -= 20;
            Assert.AreEqual(0, player.Health);
        }

        [Test]
        public void Player_Damage_ShouldBe10()
        {
            var player = new Player();
            Assert.AreEqual(10, player.Damage);
        }
    }

    [TestFixture]
    public class EnemyTests
    {
        [Test]
        public void Enemy_InitialHealth_ShouldBeCalculatedCorrectly()
        {
            var enemy = new Enemy(DifficultyLevel.Easy, 1);
            Assert.AreEqual(55, enemy.Health);
        }

        [Test]
        public void Enemy_InitialDamage_ShouldBeCalculatedCorrectly()
        {
            var enemy = new Enemy(DifficultyLevel.Easy, 1);
            Assert.AreEqual(9, enemy.Damage);
        }

        [Test]
        public void Enemy_HealthShouldNotGoBelowZero()
        {
            var enemy = new Enemy(DifficultyLevel.Easy, 1);
            enemy.Health = 0;
            enemy.Health -= 20;
            Assert.AreEqual(0, enemy.Health);
        }
    }

    [TestFixture]
    public class AbilityTests
    {
        [Test]
        public void Ability_ShouldHaveCorrectNameAndDescription()
        {
            var ability = new Ability("Double Damage", "Doubles the damage for one attack.");
            Assert.AreEqual("Double Damage", ability.Name);
            Assert.AreEqual("Doubles the damage for one attack.", ability.Description);
        }
    }

    [TestFixture]
    public class WordBankTests
    {
        [Test]
        public void GetWords_ShouldReturnCorrectNumberOfWordsForEasyDifficulty()
        {
            var words = WordBank.GetWords(0);
            Assert.AreEqual(20, words.Count);
        }

        [Test]
        public void GetWords_ShouldReturnCorrectNumberOfWordsForMediumDifficulty()
        {
            var words = WordBank.GetWords(1);
            Assert.AreEqual(20, words.Count);
        }

        [Test]
        public void GetWords_ShouldReturnCorrectNumberOfWordsForHardDifficulty()
        {
            var words = WordBank.GetWords(2);
            Assert.AreEqual(20, words.Count);
        }
    }
}

using Philocivil.Models;
using System;

namespace Philocivil.Services
{
    public class GameEngineService
    {
        // Method to calculate damage based on the score
        public int CalculateDamage(int score)
        {
            // Example logic to calculate damage: the score directly maps to damage
            int damage = score * 10; // Each point of score equals 10 damage
            return damage;
        }

        // Method to update enemy health based on damage
        public void UpdateEnemyHealth(Enemy enemy, int damage)
        {
            enemy.TakeDamage(damage);
        }

        // Method to handle game over conditions
        public bool IsGameOver(Enemy enemy)
        {
            return enemy.IsDefeated();
        }

        // Method to award XP to the player when an enemy is defeated
        public void AwardXP(User player, int xpAmount)
        {
            player.GainXP(xpAmount);
        }
    }
}

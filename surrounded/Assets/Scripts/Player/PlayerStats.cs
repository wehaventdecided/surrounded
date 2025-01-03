using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerStats")]
    public class PlayerStats : ScriptableObject
    {
        public int
            MachineGunCount = 0,
            RocketBoosterCount = 0,
            divergeCount = 0,
            shieldCount = 0,
            forcefieldCount = 0,
            rouletteCount = 0,
            piercingRoundCount = 0, //upgrade not implemented yet
            pilotingEnhancementsCount = 0,
            currentLevel = 1;

        public float
            damage = 5f,
            defense = 3,
            moveSpeed = 10f,
            baseSpeed = 10f,
            maxHealth = 50,
            XP = 0,
            shield = 0,
            maxShield = 0,
            fireForce = 50f,
            fireRate = 5.0f,
            baseRate = 5.0f,
            score = 0;

        public bool
            forceFieldActivated,
            hasPiercing,
            divergeActivated;

        public void ResetStats()
        {
            MachineGunCount = 0;
            RocketBoosterCount = 0;
            divergeCount = 0;
            shieldCount = 0;
            forcefieldCount = 0;
            rouletteCount = 0;
            piercingRoundCount = 0;
            pilotingEnhancementsCount = 0;
            currentLevel = 1;

            currentLevel = 1;
            damage = 5f;
            defense = 3;
            moveSpeed = 10f;
            baseSpeed = 10f;
            maxHealth = 50;
            XP = 0;
            shield = 0;
            maxShield = 0;
        }
    }
}
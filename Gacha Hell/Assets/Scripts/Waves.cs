using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu]

public class Waves : ScriptableObject
{
    [SerializeField]
    public EnemyWave[] theWaves;

    [System.Serializable]
    public struct EnemyWave
    {
        public EnemyClump[] clumps;
    }

    [System.Serializable]
    public struct EnemyClump
    {
        public EnemyBase enemyType;
        public int count;
        public float spawncooldown;
        public float startDelay;
    }


}



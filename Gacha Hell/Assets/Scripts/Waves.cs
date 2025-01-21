using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(menuName = "Wave System/Waves")]
public class Waves : ScriptableObject
{
    [SerializeField]
    public EnemyWave[] theWaves;

    [System.Serializable]
    
    public struct EnemyWave
    {
        [HideInInspector]
        public string waveName;
        public EnemyClump[] clumps;
    }

    [System.Serializable]
    public struct EnemyClump
    {
        [HideInInspector]
        public string clumpName;
        public EnemyBase enemyType;
        public int count;
        public float spawncooldown;
        public float startDelay;
    }
    private void OnValidate()
    {

        // Loop through each wave and assign a name based on its index to name it properly
        if (theWaves != null)
        {
            for (int i = 0; i < theWaves.Length; i++)
            {
                theWaves[i].waveName = $"Wave {i + 1}";

                if (theWaves[i].clumps != null)
                {
                    for (int j = 0; j < theWaves[i].clumps.Length; j++)
                    {
                        theWaves[i].clumps[j].clumpName = $"Clump {j + 1}";
                    }
                }
            }
        }
    }
}

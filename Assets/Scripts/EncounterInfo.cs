using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EncounterInfo : ScriptableObject
{
    [System.Serializable]
    public class Encounter
    {
        public List<GameObject> enemyPrefabs;
        public string sceneCameraName; //camera position 

    }

    public List<Encounter> encounters;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    public string EnemyName;
    public Sprite BattleIcon; //or texture2d maybe
    public Sprite BattlePortrait;
    public int HP;
    public int MP;
    public int Level;


    public float scaleX = 1.0f;
    public float scaleY = 1.0f;

    public ScriptableObject[] SkillSlots;


    //just a little something to get you started on the battle system guys <3
    public void TakeDamage(int damage)
    {
        HP -= damage;
    }
}


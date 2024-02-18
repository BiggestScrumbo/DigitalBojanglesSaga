using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SkillData : ScriptableObject
{
    public enum SkillType
    {
        Strength,
        Magic,
        Support,
        Auto

    }

    public enum ElementType
    {
        Physical,
        Agi,
        Bufu,
        Zan,
        Zio,
        Almighty,
        Curse,
        Support,
        NULL
    }



    public enum AilmentType
    {
        None, //dumbass
        Freeze, //Cannot act in battle, evasion drops to 0, guaranteed critical on hit
        Shock, //Cannot act in battle, evasion drops to 0, guaranteed critical on hit
        Poison, //Loses HP overtime, persists outside of battle*
        Mute, //Cannot use MP skills, if casted after being muted MP is not refunded, persists outside of battle*
        Petrify, //Cannot act in battle, evasion drops to 0, PHYS and FORCE skills have chance to insta kill, probably persists outside of battle*
        Paralyze, //Chance to not act in battle, persists outside of battle*
        Curse, //Unable to recover HP/MP, persists outside of battle*
        KO //HP == 0, prioritises every other ailment. Cannot act in battle, persists outside of battle*
    }


    public string Name;
    public SkillType Type;
    public ElementType Element;
    public int BaseDamage;
    public float BaseAccuracy;
    public float BaseCritChance;
    public string BattleDescription;

    public AilmentType Ailment;
    public float BaseAilmentChance;

    public float ExtraTurnChance;

    public int MPCost;
    public int HPPercentageCost; //compare tags, if "strength" tag multiply this value by unit max HP to get skill cost


}

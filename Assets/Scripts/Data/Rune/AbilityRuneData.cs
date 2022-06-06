using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AbilityRuneData" , menuName = "RuneData/AbilityRuneData")]
public class AbilityRuneData : RuneData
{
    public RuneAbility ability;

    public override int GetMax() { return 1; }
    public override bool IsMax(int point) { return point >= 1; }

    public override void Apply(int point)
    {
        DungeonManager.instance.onWaveStart     += ability.OnWaveStart;
        DungeonManager.instance.onHeroDead      += ability.OnHeroDead;
        DungeonManager.instance.onMonsterDead   += ability.OnMonsterDead;
    }

    public override void Release(int point)
    {
        DungeonManager.instance.onWaveStart     -= ability.OnWaveStart;
        DungeonManager.instance.onHeroDead      -= ability.OnHeroDead;
        DungeonManager.instance.onMonsterDead   -= ability.OnMonsterDead;
    }

    public override int GetCurValue(int point)
    {
        return ability.GetCurValue();
    }
    public override int GetNextValue(int point)
    {
        return 0;
    }

}

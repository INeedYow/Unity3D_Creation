using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RuneAbility : MonoBehaviour
{
    public abstract void OnMonsterDead();
    public abstract void OnHeroDead();
    public abstract void OnWaveStart();

    public abstract int GetCurValue();
}

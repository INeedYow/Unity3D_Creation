using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BattleMacro : MonoBehaviour
{
    public BattleMacroData data;
    protected Hero owner;
    public BattleMacro(Hero hero){
        this.owner = hero;
    }
}
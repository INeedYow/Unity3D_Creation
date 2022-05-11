using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BattleMacro
{
    public string desc;
    protected Hero owner;
    public BattleMacro(string desc, Hero hero){
        this.desc = desc;
        this.owner = hero;
    }
}
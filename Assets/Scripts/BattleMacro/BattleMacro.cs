using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이콘으로 간단하게 볼 수 있게 해도 좋을 듯
public abstract class BattleMacro
{
    public string desc;
    protected Hero owner;
    public BattleMacro(string desc, Hero hero){
        this.desc = desc;
        this.owner = hero;
    }
}
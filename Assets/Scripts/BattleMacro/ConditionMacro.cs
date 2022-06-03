using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EMost { Least, Most };
public enum EGroup { Hero, Monster };
public abstract class ConditionMacro : BattleMacro
{
    Character _target;
    protected Character target{
        get { return _target; }
        set {
            _target = value;
            if (null != _target)
            { _target.onDeadGetThis += TargetDead;}
        }
    }
    public abstract bool IsSatisfy();

    public Character GetTarget() 
    {   // 도발
        if (owner.IsProvoked())
        {   //Debug.Log("도발 : " + owner.GetProvoker().name);
            return owner.GetProvoker();
        }

        return target; 
    }

    void TargetDead(Character character)
    {
        character.onDeadGetThis -= TargetDead;
        target = null;
    }
}

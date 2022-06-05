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
        set 
        {
            if (null != _target)
            {
                _target.onDeadGetThis -= NullTarget;
                _target.onUntouchableGetThis -= NullTarget;
            }

            _target = value;

            if (null != _target)
            { 
                _target.onDeadGetThis += NullTarget;
                _target.onUntouchableGetThis += NullTarget;
            }
        }
    }
    public abstract bool IsSatisfy();

    public virtual Character GetTarget() 
    {   // 도발
        if (owner.IsProvoked())
        {   
            return owner.GetProvoker();
        }

        return target; 
    }

    void NullTarget(Character character)
    {
        character.onDeadGetThis -= NullTarget;
        character.onUntouchableGetThis -= NullTarget;
        target = null;
    }
}

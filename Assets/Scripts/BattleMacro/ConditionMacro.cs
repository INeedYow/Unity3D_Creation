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

    public Character GetTarget() 
    {   
        if (owner.madness > 0)
        {
            if (owner.eGroup == EGroup.Hero)
            {
                return PartyManager.instance.GetAliveHero();
            }
            else{
                return DungeonManager.instance.curDungeon.GetAliveMonster();
            }
        }
        
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

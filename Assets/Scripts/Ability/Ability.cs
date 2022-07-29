using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Ability : MonoBehaviour
{
    protected Character owner;

    private void OnDestroy() { OnUnEquip(); }

    protected void SetOwner(Character owner) { this.owner = owner; }

    public abstract bool SetOptionText1to3(int optionNumber, ItemOptionUnit optionUnit);

    public abstract void OnEquip(Character owner);
    protected abstract void OnUnEquip();
    
    // 처음에 상황별 이벤트 함수 다 만들고 필요없는 함수는 비워두게 구현했으나
    // 상황이 많아지면 비어있는 함수가 많아지고 비효율적일 것 같아서 OnEquip()을 abstrct로 만들고 장착 시 필요한 함수만 등록하게 재정의로 바꿔서 구현
    // public abstract void OnAttackGetDamage(float damage);
    // public abstract void OnDamagedGetAttacker(Character attacker);
    // public abstract void OnKill();

}

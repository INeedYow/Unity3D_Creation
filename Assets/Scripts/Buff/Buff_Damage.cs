using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Damage : Buff
{
    private void Awake() { value = 1f; }
    public override void Add(float dura, float value)
    {
        if (isOn) { // 동일한 버프가 있던 경우 해제하고 적용
            CancelInvoke("Sub");
            Sub();
        }

        this.value = value;
        this.dura = dura;
        isOn = true;
        
        DungeonManager.instance.onChangeAnyPower?.Invoke();

        Invoke("Sub", dura);
    }

    public override void Sub()
    {
        if (!isOn) return;
        value = 1f;
        dura = 0f;
        isOn = false;
    }
}
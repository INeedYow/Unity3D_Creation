using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Character owner;
    public Image hpBar;

    public void SetOwner(Character ch)
    {
        owner = ch;
        owner.onHpChange += RenewHpBar;
        owner.onDead += Return;
        RenewHpBar();
    }

    private void FixedUpdate() {
        transform.position = owner.HpBarTF.position;
    }

    public void Return(Character ch)
    {
        owner.onHpChange -= RenewHpBar;
        owner.onDead -= Return;
        owner = null;
        ObjectPool.instance.ReturnObj(this.gameObject);
    }

    public void RenewHpBar()
    {
        hpBar.fillAmount = owner.curHp / owner.maxHp;
    }
}

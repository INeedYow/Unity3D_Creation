using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Character owner;
    public Image hpBar;
    bool m_isHero;

    // private void OnDisable() {
    //     owner.onHpChange -= RenewHpBar;
    //     owner.onDead -= Return;
    //     owner = null;
    //     transform.SetParent(null);
    //     ObjectPool.instance.ReturnObj(this.gameObject);
    // }
    // private void FixedUpdate() {
    //     transform.position = owner.transform.position + Vector3.up * 6f;
    // }
    // private void OnEnable() {
    //     DungeonManager.instance.onDungeonExit += Return;
    // }

    public void SetOwner(Character ch)
    {
        owner = ch;
        owner.onHpChange += RenewHpBar;
        //owner.onDead += Return;
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

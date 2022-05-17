using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Ally : Projectile
{
    public void SetInfo(Character target, float damage, float area)
    {
        this.target = target;
        this.damage = damage;
        this.area = area;
    }

    // TODO Update()에서 움직임, 데미지 처리, 삭제 등
}

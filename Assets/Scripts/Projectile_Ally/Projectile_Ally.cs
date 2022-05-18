using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Ally : Projectile
{
    public void Launch(Character target, float damage, float area)
    {
        this.target = target;
        this.damage = damage;
        this.area = area;

       InvokeRepeating("LookTarget", 0f, 0.2f);
    }

    private void Update() {
        Move();
    }

    void Move()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime);
    }

    void LookTarget()
    {
        if (null != target)
        {
            lastPos = target.transform.position;
        }
        else{
            
        }
        transform.LookAt(lastPos);
    }
}

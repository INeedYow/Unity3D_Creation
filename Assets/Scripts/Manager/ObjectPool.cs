using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance { get; private set; }


    [Header("Pool Prefabs")]
    List<Projectile> poolArrow; 
    public Projectile prfArrow;
    [Range(1, 100)] public int count;


    private void Awake() {
        instance = this;
        Init();
    }

    void Init(){
        if (null != prfArrow)
        {
            for(int i = 0; i < count; i++){
                poolArrow.Add(CreateNewArrow());
            }
        }
    }

    Projectile CreateNewArrow() {
        var obj = Instantiate(prfArrow);
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(transform);
        return obj;
    }

    public Projectile GetArrow(){
        foreach (Projectile arrow in poolArrow){
            if (!arrow.gameObject.activeSelf)
            {   Debug.Log("ObjPool, 기존 반환");
                arrow.transform.SetParent(null);
                arrow.gameObject.SetActive(true);
                return arrow;
            }
        }
        Debug.Log("ObjPool,, 새로 생성");
        // var obj = 
        // poolArrow.Add(CreateNewArrow());
        return poolArrow[poolArrow.Count - 1];
    }

    public void ReturnObj(GameObject obj){
        //switch (obj)
    }
}

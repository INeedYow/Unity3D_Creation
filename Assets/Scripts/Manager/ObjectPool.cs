using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EProjectile { Arrow, };
public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance { get; private set; }


    [Header("Pool Prefabs")]
    List<Projectile> poolArrow; 
    public Projectile prfArrow;
    [Range(1, 100)] public int count;


    private void Awake() {
        instance = this;
        InitPool();
    }

    void InitPool(){
        poolArrow = new List<Projectile>();

        if (null != prfArrow)
        {
            for(int i = 0; i < count; i++){
                poolArrow.Add(CreateNewArrow());
            }
        }
    }

    Projectile CreateNewArrow(bool isActive = false) { //Debug.Log("Pool.CreateNew()");
        var obj = Instantiate(prfArrow);
        obj.gameObject.SetActive(isActive);
        obj.transform.SetParent(transform);
        return obj;
    }

    public Projectile GetArrow(){
        foreach (Projectile arrow in poolArrow){
            if (!arrow.gameObject.activeSelf)
            {   //Debug.Log("Pool.GetObject()");
                arrow.transform.SetParent(null);
                arrow.gameObject.SetActive(true);
                return arrow;
            }
        }
        
        var newObj = CreateNewArrow(true);
        poolArrow.Add(newObj);
        //Debug.Log("Pool.Count : " + poolArrow.Count);
        return newObj;
    }

    public void ReturnObj(GameObject obj){
        //Debug.Log("Pool.ReturnObj()");
        obj.SetActive(false);
        obj.transform.SetParent(transform);
    }
}

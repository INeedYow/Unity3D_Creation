using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EProjectile { Arrow, };
public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance { get; private set; }


    [Header("Pool Prefabs")]
    List<BattleInfoText> poolBattleInfoText;
    public BattleInfoText prfBattleInfoText;
    [Range(1,100)] public int count_infoText;
    [Space(10f)]
    List<Projectile> poolArrow; 
    public Projectile prfArrow;
    [Range(1, 100)] public int count_arrow;


    private void Awake() {
        instance = this;
        InitPool();
    }

    void InitPool(){
        poolArrow = new List<Projectile>();
        poolBattleInfoText = new List<BattleInfoText>();

        if (null != prfBattleInfoText)
        { for(int i = 0; i < count_infoText; i++)   { poolBattleInfoText.Add(CreateNewInfoText()); } }

        if (null != prfArrow) 
        { for(int i = 0; i < count_arrow; i++)      { poolArrow.Add(CreateNewArrow()); } }
    }

    BattleInfoText CreateNewInfoText(bool isActive = false){
        var obj = Instantiate(prfBattleInfoText);
        obj.gameObject.SetActive(isActive);
        obj.transform.SetParent(transform);
        return obj;
    }

    Projectile CreateNewArrow(bool isActive = false){ //Debug.Log("Pool.CreateNew()");
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

    public BattleInfoText GetInfoText(){
        foreach (BattleInfoText text in poolBattleInfoText){
            if (!text.gameObject.activeSelf)
            {   //Debug.Log("Pool.GetObject()");
                text.transform.SetParent(null);
                text.gameObject.SetActive(true);
                return text;
            }
        }
        
        var newObj = CreateNewInfoText(true);
        poolBattleInfoText.Add(newObj);
        //Debug.Log("Pool.Count : " + poolArrow.Count);
        return newObj;
    }


    public void ReturnObj(GameObject obj){
        //Debug.Log("Pool.ReturnObj()");
        obj.SetActive(false);
        obj.transform.SetParent(transform);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EProjectile { Arrow, };
public enum EMonster { 
    RedSlime,   BlueSlime,  Spore,
}
public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance { get; private set; }

    [Header("BattleInfoText Pool")]
    public BattleInfoText prfBattleInfoText;
    [Range(1,100)] public int count_infoText;
    List<BattleInfoText> poolBattleInfoText;

    [Space(15f)]

    [Header("Arrow Pool")]
    public Projectile prfArrow;
    [Range(1, 30)] public int count_arrow;
    List<Projectile> poolArrow; 

    [Space(15f)]

    [Header("Monster Pool")]
    public Monster[] prfMonsters;
    [Range(1, 10)] public int[] count_monsters;
    List<Monster>[] poolMonsters;


    private void Awake() { instance = this; }

    private void Start() { InitPool(); }

    void InitPool(){
        poolArrow = new List<Projectile>();
        poolBattleInfoText = new List<BattleInfoText>();
        poolMonsters = new List<Monster>[prfMonsters.Length];
        for(int i = 0; i < prfMonsters.Length; i++){
            //Debug.Log(prfMonsters.Length);
            //Debug.Log(poolMonsters[i]);
            poolMonsters[i] = new List<Monster>();
        }

        if (null != prfBattleInfoText)
        { for(int i = 0; i < count_infoText; i++)   { poolBattleInfoText.Add(CreateNewInfoText()); } }

        if (null != prfArrow) 
        { for(int i = 0; i < count_arrow; i++)      { poolArrow.Add(CreateNewArrow()); } }

        for (int i = 0; i < prfMonsters.Length; i++){
            if (null != prfMonsters[i])
            {
                for(int j = 0; j < count_monsters[i]; j++) { poolMonsters[i].Add(CreateNewMonster(i)); }
            }
        }
    }

    public void ReturnObj(GameObject obj){
        //Debug.Log("Pool.ReturnObj()");
        obj.SetActive(false);
        obj.transform.SetParent(transform);
    }
    
    BattleInfoText CreateNewInfoText(bool isActive = false){
        var obj = Instantiate(prfBattleInfoText);
        obj.gameObject.SetActive(isActive);
        obj.transform.SetParent(transform);
        return obj;
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

    Projectile CreateNewArrow(bool isActive = false){ 
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
        newObj.transform.SetParent(null);
        //Debug.Log("Pool.Count : " + poolArrow.Count);
        return newObj;
    }

    Monster CreateNewMonster(int index, bool isActive = false){
        var obj = Instantiate(prfMonsters[index]);
        obj.gameObject.SetActive(isActive);
        obj.transform.SetParent(transform);

        //
        return obj;
    }

    public Monster GetMonster(int monsterId){
        foreach (Monster mons in poolMonsters[monsterId]){
            if (!mons.gameObject.activeSelf)
            {   
                mons.transform.SetParent(null);
                mons.gameObject.SetActive(true);
                mons.monsGFX.gameObject.SetActive(true);
                mons.curHp = mons.maxHp;
                mons.isDead = false;
                return mons;
            }
        }
        
        var newObj = CreateNewMonster(monsterId, true);
        newObj.transform.SetParent(null);
        poolMonsters[monsterId].Add(newObj);
        
        return newObj;
    }
}

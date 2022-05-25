﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EProjectile { 
    ArcherArrow, PoisonArrow, YellowMarble,
}
public enum EMonster { 
    RedSlime, BlueSlime, Spore,
}
public enum ESkillObj {
    Single,
}

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance { get; private set; }

    [Header("BattleInfoText Pool")]
    public BattleInfoText prfBattleInfoText;
    [Range(1,100)] public int count_infoText;
    List<BattleInfoText> poolBattleInfoText;

    [Space(15f)]

    [Header("Projectile Pools")]
    public Projectile[] prfProjectiles;
    [Range(1, 30)] public int[] count_projectiles;
    List<Projectile>[] poolProjectiles; 

    [Space(15f)]

    [Header("Monster Pools")]
    public Monster[] prfMonsters;
    [Range(1, 10)] public int[] count_monsters;
    List<Monster>[] poolMonsters;

    [Space(15f)]

    [Header("SkillObject Pools")]
    public SkillObject[] prfSkillObjs;
    [Range(1, 100)] public int[] count_skillObjs;
    List<SkillObject>[] poolSKillObjs; 


    private void Awake() { instance = this; }

    private void Start() { InitPool(); }

    void InitPool(){
        // list 초기화
        poolBattleInfoText      = new List<BattleInfoText>();
        poolProjectiles         = new List<Projectile>[prfProjectiles.Length];
        poolMonsters            = new List<Monster>[prfMonsters.Length];
        poolSKillObjs           = new List<SkillObject>[prfSkillObjs.Length];
        
        for(int i = 0; i < prfProjectiles.Length; i++){
            poolProjectiles[i] = new List<Projectile>();
        }

        for(int i = 0; i < prfMonsters.Length; i++){
            poolMonsters[i] = new List<Monster>();
        }

        for(int i = 0; i < prfSkillObjs.Length; i++){
            poolSKillObjs[i] = new List<SkillObject>();
        }

        // 생성
        if (null != prfBattleInfoText){ 
            for(int i = 0; i < count_infoText; i++)
            { 
                poolBattleInfoText.Add(CreateNewInfoText()); 
            } 
        }

        for (int i = 0; i < prfProjectiles.Length; i++){
            if (null != prfProjectiles[i])
            {
                for(int j = 0; j < count_projectiles[i]; j++) { poolProjectiles[i].Add(CreateNewProjectile(i)); }
            }
        }
        
        for (int i = 0; i < prfMonsters.Length; i++){
            if (null != prfMonsters[i])
            {
                for(int j = 0; j < count_monsters[i]; j++) { poolMonsters[i].Add(CreateNewMonster(i)); }
            }
        }

        for (int i = 0; i < prfSkillObjs.Length; i++){
            if (null != prfSkillObjs[i])
            {
                for(int j = 0; j < count_skillObjs[i]; j++) { poolSKillObjs[i].Add(CreateNewSkillObject(i)); }
            }
        }
    }

    public void ReturnObj(GameObject obj){
        //Debug.Log("Pool.ReturnObj()");
        obj.SetActive(false);
        obj.transform.SetParent(transform);
    }
    
    // BattleInfoText
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

    // Projectiles
    Projectile CreateNewProjectile(int index, bool isActive = false){   //Debug.Log("CreateNewProj" + index); Debug.Log(poolProjectiles[0].Count);
        var obj = Instantiate(prfProjectiles[index]);
        obj.gameObject.SetActive(isActive);
        obj.transform.SetParent(transform);
        return obj;
    }

    public Projectile GetProjectile(int ProjectileId){
        foreach (Projectile proj in poolProjectiles[ProjectileId]){
            if (!proj.gameObject.activeSelf)
            {   
                proj.transform.SetParent(null);
                proj.gameObject.SetActive(true);
                return proj;
            }
        }
        
        var newObj = CreateNewProjectile(ProjectileId, true);
        newObj.transform.SetParent(null);
        poolProjectiles[ProjectileId].Add(newObj);
        
        return newObj;
    }

    // Monsters
    Monster CreateNewMonster(int index, bool isActive = false){
        var obj = Monster.Create((EMonster)index);              // 팩토리패턴
        obj.gameObject.SetActive(isActive);
        obj.transform.SetParent(transform);
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

    // SkillObjects
    SkillObject CreateNewSkillObject(int index, bool isActive = false){
        var obj = Instantiate(prfSkillObjs[index]);
        obj.gameObject.SetActive(isActive);
        obj.transform.SetParent(transform);
        return obj;
    }

    public SkillObject GetSkillObject(int objId){
        foreach (SkillObject obj in poolSKillObjs[objId]){
            if (!obj.gameObject.activeSelf)
            {   
                obj.transform.SetParent(null);
                obj.gameObject.SetActive(true);
                return obj;
            }
        }
        
        var newObj = CreateNewSkillObject(objId, true);
        newObj.transform.SetParent(null);
        poolSKillObjs[objId].Add(newObj);
        
        return newObj;
    }

    
}

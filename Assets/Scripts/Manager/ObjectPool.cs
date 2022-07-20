using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EProjectile { 
    Archer_0, Archer_1, Angel_0, Archer_2, Chewer_1, EvilMage_0, EvilMage_1, Bard_0, Archer_4, 
    Golem_Fire_0, Golem_Fire_1, Golem_Red_2,

}
public enum EMonster { 
    RedSlime, BlueSlime, Spore, Pollen, Plant, OddPlant, PlantChewer, 
    Skeleton, BraveSkeleton, EvilMage, Golem_Earth, Golem_Ice, Golem_Fire, Golem_Blue, Golem_Red,
}

public enum EEffect {
    None = -1,
    Stun, Blood, 
    Knight_1_TargetEff, Knight_2_TargetEff, Knight_3_StartEff, Knight_4_StartEff,           // knight
    Angel_1_TargetEff, Angel_2_TargetEff, Angel_3_UserEff, Angel_4_TargetEff,               // angel
    Archer_1_TargetEff, Archer_2_TargetEff, Archer_3_StartEff, Archer_4_StartEff,           // archer
    Necro_1_TargetEff, Necro_2_UserEff, Necro_3_TargetEff, Necro_4_TargetEff,  
    Bard_1_TargetEff, Bard_2_TargetEff, Bard_3_TargetEff, Bard_4_StartEff,
    Templar_1_UserEff, Templar_2_TargetEff, Templar_3_StartEff, Templar_4_TargetEff,
    Rune_4_2_World, Rune_4_2_OnWorks, Rune_4_1_World, Rune_4_1_OnWorks, 
    Invincible, Bleed, Blind, Silence, Madness,
    Rune_1_2_OnWorks, Rune_3_1_OnWorks_UnLucky, Rune_3_1_OnWorks_Lucky,
    GolemEarth_1_TargetEff, Spawn, GolemIce_1_UserEff, GolemFire_1_TargetEff,
    Golem_Red_2_TargetEff, Golem_Red_1_TargetEff, Golem_Blue_1_TargetEff,
    Templar_1_StartEff, Templar_3_TargetEff,

    Size,
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

    // [Header("SkillObject Pools")]
    // public SkillObject[] prfSkillObjs;
    // [Range(1, 100)] public int[] count_skillObjs;
    // List<SkillObject>[] poolSKillObjs; 

    [Header("Hp bar")]
    public HpBar prfHeroHpBar;
    [Range(1, 8)] public int count_heroHp;
    public HpBar prfMonHpBar;
    [Range(1, 15)] public int count_monHp;
    List <HpBar> poolHeroHp;
    List <HpBar> poolMonHp;

    [Space(15f)]

    [Header("Buff Pools")]
    public Buff[] prfBuffs;
    [Range(1, 30)] public int[] count_buff;
    List<Buff>[] poolBuffs; 

    [Space(15f)]

    [Header("Effect Pools")]
    public Effect[] prfEffects;
    [Range(1, 30)] public int[] count_effect;
    List<Effect>[] poolEffects; 





    private void Awake() { instance = this; }

    private void Start() { InitPool(); }

    void InitPool(){
        // list 초기화
        poolBattleInfoText      = new List<BattleInfoText>();
        poolProjectiles         = new List<Projectile>[prfProjectiles.Length];
        poolMonsters            = new List<Monster>[prfMonsters.Length];
        // poolSKillObjs           = new List<SkillObject>[prfSkillObjs.Length];
        poolHeroHp              = new List<HpBar>();
        poolMonHp               = new List<HpBar>();
        poolBuffs               = new List<Buff>[prfBuffs.Length];
        poolEffects             = new List<Effect>[prfEffects.Length];

        
        for(int i = 0; i < prfProjectiles.Length; i++)  { poolProjectiles[i] = new List<Projectile>(); }

        for(int i = 0; i < prfMonsters.Length; i++)     { poolMonsters[i] = new List<Monster>(); }

        // for(int i = 0; i < prfSkillObjs.Length; i++)    { poolSKillObjs[i] = new List<SkillObject>(); }

        for(int i = 0; i < prfBuffs.Length; i++)        { poolBuffs[i] = new List<Buff>(); }

        for(int i = 0; i < prfEffects.Length; i++)      { poolEffects[i] = new List<Effect>(); }

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

        // for (int i = 0; i < prfSkillObjs.Length; i++){
        //     if (null != prfSkillObjs[i])
        //     {
        //         for(int j = 0; j < count_skillObjs[i]; j++) { poolSKillObjs[i].Add(CreateNewSkillObject(i)); }
        //     }
        // }

        if (null != prfHeroHpBar){ 
            for(int i = 0; i < count_heroHp; i++)
            { 
                poolHeroHp.Add(CreateNewHpBar(true)); 
            } 
        }

        if (null != prfMonHpBar){ 
            for(int i = 0; i < count_monHp; i++)
            { 
                poolMonHp.Add(CreateNewHpBar(false)); 
            } 
        }

        for (int i = 0; i < prfBuffs.Length; i++){
            if (null != prfBuffs[i])
            {
                for(int j = 0; j < count_buff[i]; j++) { poolBuffs[i].Add(CreateNewBuff(i)); }
            }
        }

        for (int i = 0; i < prfEffects.Length; i++){
            if (null != prfEffects[i])
            {
                for(int j = 0; j < count_effect[i]; j++) { poolEffects[i].Add(CreateNewEffect(i)); }
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

    // // SkillObjects
    // SkillObject CreateNewSkillObject(int index, bool isActive = false){
    //     var obj = Instantiate(prfSkillObjs[index]);
    //     obj.gameObject.SetActive(isActive);
    //     obj.transform.SetParent(transform);
    //     return obj;
    // }

    // public SkillObject GetSkillObject(int objId){
    //     foreach (SkillObject obj in poolSKillObjs[objId]){
    //         if (!obj.gameObject.activeSelf)
    //         {   
    //             obj.transform.SetParent(null);
    //             obj.gameObject.SetActive(true);
    //             return obj;
    //         }
    //     }
        
    //     var newObj = CreateNewSkillObject(objId, true);
    //     newObj.transform.SetParent(null);
    //     poolSKillObjs[objId].Add(newObj);
        
    //     return newObj;
    // }

    // Hp bars
    HpBar CreateNewHpBar(bool isHero, bool isActive = false)
    {
        if (isHero)
        {
            var obj = Instantiate(prfHeroHpBar);
            obj.gameObject.SetActive(isActive);
            obj.transform.SetParent(transform);
            return obj;
        }
       
        else
        {
            var obj = Instantiate(prfMonHpBar);
            obj.gameObject.SetActive(isActive);
            obj.transform.SetParent(transform);
            return obj;
        }
    }

    public HpBar GetHpBar(bool isHero){
        if (isHero)
        {
            foreach (HpBar bar in poolHeroHp)
            {
                if (!bar.gameObject.activeSelf)
                {   // Hp bar는 월드캔버스를 부모로 반환
                    bar.transform.SetParent(GameManager.instance.worldCanvas.transform);
                    bar.gameObject.SetActive(true);
                    return bar;
                }
            }

            var newObj = CreateNewHpBar(isHero, true);
            newObj.transform.SetParent(GameManager.instance.worldCanvas.transform);
            poolHeroHp.Add(newObj);
            return newObj;
        }
        else
        {
            foreach (HpBar bar in poolMonHp)
            {
                if (!bar.gameObject.activeSelf)
                {   
                    bar.transform.SetParent(GameManager.instance.worldCanvas.transform);
                    bar.gameObject.SetActive(true);
                    return bar;
                }
            }

            var newObj = CreateNewHpBar(isHero, true);
            newObj.transform.SetParent(GameManager.instance.worldCanvas.transform);
            poolMonHp.Add(newObj);
            return newObj;
        }
    }

    // Buff
    Buff CreateNewBuff(int index, bool isActive = false){   //Debug.Log("CreateNewProj" + index); Debug.Log(poolProjectiles[0].Count);
        var obj = Instantiate(prfBuffs[index]);
        obj.gameObject.SetActive(isActive);
        obj.transform.SetParent(transform);
        return obj;
    }

    public Buff GetBuff(int buffId)
    {    //Debug.Log("buffId : " + buffId);
        if (buffId <= (int)EBuff.None || buffId >= (int)EBuff.Size) return null;
        
        foreach (Buff buff in poolBuffs[buffId]){
            if (!buff.gameObject.activeSelf)
            {   
                buff.transform.SetParent(null);
                buff.gameObject.SetActive(true);
                return buff;
            }
        }
        
        var newObj = CreateNewBuff(buffId, true);
        newObj.transform.SetParent(null);
        poolBuffs[buffId].Add(newObj);
        
        return newObj;
    }

    // Effect
    Effect CreateNewEffect(int index, bool isActive = false){   //Debug.Log("CreateNewProj" + index); Debug.Log(poolProjectiles[0].Count);
        var obj = Instantiate(prfEffects[index]);
        obj.gameObject.SetActive(isActive);
        obj.transform.SetParent(transform);
        return obj;
    }

    public Effect GetEffect(int effectId)
    {    
        if (effectId <= (int)EEffect.None || effectId >= (int)EEffect.Size) return null;
        
        foreach (Effect eff in poolEffects[effectId]){
            if (!eff.gameObject.activeSelf)
            {   
                eff.transform.SetParent(null);
                eff.gameObject.SetActive(true);
                return eff;
            }
        }
        
        var newObj = CreateNewEffect(effectId, true);
        newObj.transform.SetParent(null);
        poolEffects[effectId].Add(newObj);
        
        return newObj;
    }
}

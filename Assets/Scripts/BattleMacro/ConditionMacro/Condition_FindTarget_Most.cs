// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// // 최대 최소는 owner 뿐아니라 모든 객체의 info 변동과 관련 있으니까 Manager통해서 갱신명령 받도록
// public class Condition_FindTarget_Most : ConditionMacro
// {
//     public enum EMost { Least, Most };

//     public EMost eMost;
//     float m_temp;

//     public Condition_FindTarget_Most(string desc, Hero hero, EGroup eGroup, EMost eMost) 
//         : base(desc, hero)
//     {
//         this.eMost = eMost;
//         Init();
//     }

//     private void OnDestroy() {
//         switch (eValue){
//         case EValue.HP:             DungeonManager.instance.onChangeAnyHP -= UpdateHp; break;
//         case EValue.Power:          DungeonManager.instance.onChangeAnyPower -= UpdatePower; break;
//         case EValue.Armor:          DungeonManager.instance.onChangeAnyArmor -= UpdateArmor; break;
//         case EValue.MagicPower:     DungeonManager.instance.onChangeAnyMagicPower -= UpdateMagicPower; break;
//         case EValue.MagicArmor:     DungeonManager.instance.onChangeAnyMagicArmor -= UpdateMagicArmor; break;
//         case EValue.AttackRange:    DungeonManager.instance.onChangeAnyAttackRange -= UpdateAttackRange; break;
//         case EValue.AttackSpeed:    DungeonManager.instance.onChangeAnyAttackSpeed -= UpdateAttackSpeed; break;
//         case EValue.MoveSpeed:      DungeonManager.instance.onChangeAnyMoveSpeed -= UpdateMoveSpeed; break;
//         }
//     }

//     // TODO 각 함수 제대로 작동하는지 확인
//     public override bool IsSatisfy()
//     {   
//         if (eValue == EValue.Distance)
//         {   // 거리는 이벤트가 아니라 자꾸 확인해줘야 해서 따로 관리하는 게 나을 것 같음 // 일단 임시로..
//             UpdateDistance();
//             owner.target = target;
//             if (target == null) return false;
//             return true;
//         }

//         if (target == null) {               //Debug.Log("if");
//             FindTarget();
//             if (target != null) { 
//                 owner.target = target;
//                 return true; 
//             }
//             else { return false; }
//         }
//         else if (target != owner.target){   //Debug.Log("else if");
//             owner.target = target; 
//             return true; 
//         }
//         else {                              //Debug.Log("else"); 
//             return true; 
//         }
//     }

//     // TODO FindTarget 함수들이 target설정도 하고 반환도 함 // 수정 중에 꼬임
//     void UpdateHp()             { target = FindTargetHP(); }
//     void UpdatePower()          { target = FindTargetPower(); }
//     void UpdateArmor()          { target = FindTargetArmor(); }
//     void UpdateMagicPower()     { target = FindTargetMagicPower(); }
//     void UpdateMagicArmor()     { target = FindTargetMagicArmor(); }
//     void UpdateAttackRange()    { target = FindTargetAttackRange(); }
//     void UpdateAttackSpeed()    { target = FindTargetAttackSpeed(); }
//     void UpdateDistance()       { target = FindTargetDistance(); }
//     void UpdateMoveSpeed()      { target = FindTargetMoveSpeed(); }

//     void Init(){
//         switch (eValue){
//         case EValue.HP:             DungeonManager.instance.onChangeAnyHP += UpdateHp; break;
//         case EValue.Power:          DungeonManager.instance.onChangeAnyPower += UpdatePower; break;
//         case EValue.Armor:          DungeonManager.instance.onChangeAnyArmor += UpdateArmor; break;
//         case EValue.MagicPower:     DungeonManager.instance.onChangeAnyMagicPower += UpdateMagicPower; break;
//         case EValue.MagicArmor:     DungeonManager.instance.onChangeAnyMagicArmor += UpdateMagicArmor; break;
//         case EValue.AttackRange:    DungeonManager.instance.onChangeAnyAttackRange += UpdateAttackRange; break;
//         case EValue.AttackSpeed:    DungeonManager.instance.onChangeAnyAttackSpeed += UpdateAttackSpeed; break;
//         case EValue.MoveSpeed:      DungeonManager.instance.onChangeAnyMoveSpeed += UpdateMoveSpeed; break;
//         }
//     }

//     public void FindTarget()
//     {
//         switch (eValue){
//         case EValue.HP:             UpdateHp(); break;
//         case EValue.Power:          UpdatePower(); break;
//         case EValue.Armor:          UpdateArmor(); break;
//         case EValue.MagicPower:     UpdateMagicPower(); break;
//         case EValue.MagicArmor:     UpdateMagicArmor(); break;
//         case EValue.AttackRange:    UpdateAttackRange(); break;
//         case EValue.AttackSpeed:    UpdateAttackSpeed(); break;
//         case EValue.MoveSpeed:      UpdateMoveSpeed(); break;
//         }
//     }

//     Character FindTargetHP()
//     {   // HP비율
//         if (eGroup == EGroup.Ally)
//         {   // 아군
//             target = PartyManager.instance.GetAliveHero();
//             if (target == null) return null;

//             if (eMost == EMost.Least)
//             {   // 최소
//                 value = 1f;
//                 foreach (Character ch in PartyManager.instance.heroParty)
//                 {
//                     if (ch.isDead) continue;
//                     if (value > ch.curHp / ch.maxHp)
//                     {
//                         value = ch.curHp / ch.maxHp;
//                         target = ch;
//                     }
//                 }
//             }
//             else{   // 최대
//                 value = 0f;
//                 foreach (Character ch in PartyManager.instance.heroParty)
//                 {
//                     if (ch.isDead) continue;
//                     if (value < ch.curHp / ch.maxHp)
//                     {
//                         value = ch.curHp / ch.maxHp;
//                         target = ch;
//                     }
//                 }
//             }
//         }
//         else{   // 적군
//             target = DungeonManager.instance.curDungeon.GetAliveMonster();
//             if (target == null) return null;

//             if (eMost == EMost.Least)
//             {   // 최소
//                 value = 1f;
//                 foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
//                 {
//                     if (ch.isDead) continue;
//                     if (value > ch.curHp / ch.maxHp)
//                     {
//                         value = ch.curHp / ch.maxHp;
//                         target = ch;
//                     }
//                 }
//             }
//             else{   // 최대
//                 value = 0f;
//                 foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
//                 {
//                     if (ch.isDead) continue;
//                     if (value < ch.curHp / ch.maxHp)
//                     {
//                         value = ch.curHp / ch.maxHp;
//                         target = ch;
//                     }
//                 }
//             }
//         }
//         return target;
//     }

//     Character FindTargetPower()
//     {   //최소 최대 공격력 평균
//         if (eGroup == EGroup.Ally)
//         {   // 아군
//             target = PartyManager.instance.GetAliveHero();
//             if (target == null) return null;

//             if (eMost == EMost.Least)
//             {   // 최소
//                 value = Mathf.Infinity;
//                 foreach (Character ch in PartyManager.instance.heroParty)
//                 {
//                     if (ch.isDead) continue;
//                     if (value > ch.minDamage + ch.maxDamage)
//                     {
//                         value = ch.minDamage + ch.maxDamage;
//                         target = ch;
//                     }
//                 }
//             }
//             else{   // 최대
//                 value = 0f;
//                 foreach (Character ch in PartyManager.instance.heroParty)
//                 {
//                     if (ch.isDead) continue;
//                     if (value < ch.minDamage + ch.maxDamage)
//                     {
//                         value = ch.minDamage + ch.maxDamage;
//                         target = ch;
//                     }
//                 }
//             }
//         }
//         else{   // 적군
//            target = DungeonManager.instance.curDungeon.GetAliveMonster();
//             if (target == null) return null;

//             if (eMost == EMost.Least)
//             {   // 최소 최대 공격력 평균이 최소
//                 value = Mathf.Infinity;
//                 foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
//                 {
//                     if (ch.isDead) continue;
//                     if (value > ch.minDamage + ch.maxDamage)
//                     {
//                         value = ch.minDamage + ch.maxDamage;
//                         target = ch;
//                     }
//                 }
//             }
//             else{   // 최대
//                 value = 0f;
//                 foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
//                 {
//                     if (ch.isDead) continue;
//                     if (value < ch.minDamage + ch.maxDamage)
//                     {
//                         value = ch.minDamage + ch.maxDamage;
//                         target = ch;
//                     }
//                 }
//             }
//         }
//         return target;
//     }

//     Character FindTargetArmor()
//     {   // 아머
//         if (eGroup == EGroup.Ally)
//         {   // 아군
//             target = PartyManager.instance.GetAliveHero();
//             if (target == null) return null;
            
//             if (eMost == EMost.Least)
//             {   // 최소
//                 value = Mathf.Infinity;
//                 foreach (Character ch in PartyManager.instance.heroParty)
//                 {
//                     if (ch.isDead) continue;
//                     if (value > ch.armorRate)
//                     {
//                         value = ch.armorRate;
//                         target = ch;
//                     }
//                 }
//             }
//             else{   // 최대
//                 value = 0f;
//                 foreach (Character ch in PartyManager.instance.heroParty)
//                 {
//                     if (ch.isDead) continue;
//                     if (value < ch.armorRate)
//                     {
//                         value = ch.armorRate;
//                         target = ch;
//                     }
//                 }
//             }
//         }
//         else{   // 적군
//            target = DungeonManager.instance.curDungeon.GetAliveMonster();
//             if (target == null) return null;
            
//             if (eMost == EMost.Least)
//             {   // 최소
//                 value = Mathf.Infinity;
//                 foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
//                 {
//                     if (ch.isDead) continue;
//                     if (value > ch.armorRate)
//                     {
//                         value = ch.armorRate;
//                         target = ch;
//                     }
//                 }
//             }
//             else{   // 최대
//                 value = 0f;
//                 foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
//                 {
//                     if (ch.isDead) continue;
//                     if (value < ch.armorRate)
//                     {
//                         value = ch.armorRate;
//                         target = ch;
//                     }
//                 }
//             }
//         }
//         return target;
//     }

//     Character FindTargetMagicPower()
//     {   // 마법공격력
//         if (eGroup == EGroup.Ally)
//         {   // 아군
//             target = PartyManager.instance.GetAliveHero();
//             if (target == null) return null;
            
//             if (eMost == EMost.Least)
//             {   // 최소
//                 value = Mathf.Infinity;
//                 foreach (Character ch in PartyManager.instance.heroParty)
//                 {
//                     if (ch.isDead) continue;
//                     if (value > ch.magicPowerRate)
//                     {
//                         value = ch.magicPowerRate;
//                         target = ch;
//                     }
//                 }
//             }
//             else{   // 최대
//                 value = 0f;
//                 foreach (Character ch in PartyManager.instance.heroParty)
//                 {
//                     if (ch.isDead) continue;
//                     if (value < ch.magicPowerRate)
//                     {
//                         value = ch.magicPowerRate;
//                         target = ch;
//                     }
//                 }
//             }
//         }
//         else{   // 적군
//            target = DungeonManager.instance.curDungeon.GetAliveMonster();
//             if (target == null) return null;
            
//             if (eMost == EMost.Least)
//             {   // 최소
//                 value = Mathf.Infinity;
//                 foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
//                 {
//                     if (ch.isDead) continue;
//                     if (value > ch.magicPowerRate)
//                     {
//                         value = ch.magicPowerRate;
//                         target = ch;
//                     }
//                 }
//             }
//             else{   // 최대
//                 value = 0f;
//                 foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
//                 {
//                     if (ch.isDead) continue;
//                     if (value < ch.magicPowerRate)
//                     {
//                         value = ch.magicPowerRate;
//                         target = ch;
//                     }
//                 }
//             }
//         }
//         return target;
//     }

//     Character FindTargetMagicArmor()
//     {   // 마법방어력
//         if (eGroup == EGroup.Ally)
//         {   // 아군
//             target = PartyManager.instance.GetAliveHero();
//             if (target == null) return null;
            
//             if (eMost == EMost.Least)
//             {   // 최소
//                 value = Mathf.Infinity;
//                 foreach (Character ch in PartyManager.instance.heroParty)
//                 {
//                     if (ch.isDead) continue;
//                     if (value > ch.magicArmorRate)
//                     {
//                         value = ch.magicArmorRate;
//                         target = ch;
//                     }
//                 }
//             }
//             else{   // 최대
//                 value = 0f;
//                 foreach (Character ch in PartyManager.instance.heroParty)
//                 {
//                     if (ch.isDead) continue;
//                     if (value < ch.magicArmorRate)
//                     {
//                         value = ch.magicArmorRate;
//                         target = ch;
//                     }
//                 }
//             }
//         }
//         else{   // 적군
//            target = DungeonManager.instance.curDungeon.GetAliveMonster();
//             if (target == null) return null;
            
//             if (eMost == EMost.Least)
//             {   // 최소
//                 value = Mathf.Infinity;
//                 foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
//                 {
//                     if (ch.isDead) continue;
//                     if (value > ch.magicArmorRate)
//                     {
//                         value = ch.magicArmorRate;
//                         target = ch;
//                     }
//                 }
//             }
//             else{   // 최대
//                 value = 0f;
//                 foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
//                 {
//                     if (ch.isDead) continue;
//                     if (value < ch.magicArmorRate)
//                     {
//                         value = ch.magicArmorRate;
//                         target = ch;
//                     }
//                 }
//             }
//         }
//         return target;
//     }

//     Character FindTargetAttackRange()
//     {   // 사거리
//         if (eGroup == EGroup.Ally)
//         {   // 아군
//             target = PartyManager.instance.GetAliveHero();
//             if (target == null) return null;
            
//             if (eMost == EMost.Least)
//             {   // 최소
//                 value = Mathf.Infinity;
//                 foreach (Character ch in PartyManager.instance.heroParty)
//                 {
//                     if (ch.isDead) continue;
//                     if (value > ch.attackRange)
//                     {
//                         value = ch.attackRange;
//                         target = ch;
//                     }
//                 }
//             }
//             else{   // 최대
//                 value = 0f;
//                 foreach (Character ch in PartyManager.instance.heroParty)
//                 {
//                     if (ch.isDead) continue;
//                     if (value < ch.attackRange)
//                     {
//                         value = ch.attackRange;
//                         target = ch;
//                     }
//                 }
//             }
//         }
//         else{   // 적군
//            target = DungeonManager.instance.curDungeon.GetAliveMonster();
//             if (target == null) return null;
            
//             if (eMost == EMost.Least)
//             {   // 최소
//                 value = Mathf.Infinity;
//                 foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
//                 {
//                     if (ch.isDead) continue;
//                     if (value > ch.attackRange)
//                     {
//                         value = ch.attackRange;
//                         target = ch;
//                     }
//                 }
//             }
//             else{   // 최대
//                 value = 0f;
//                 foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
//                 {
//                     if (ch.isDead) continue;
//                     if (value < ch.attackRange)
//                     {
//                         value = ch.attackRange;
//                         target = ch;
//                     }
//                 }
//             }
//         }
//         return target;
//     }

//     Character FindTargetAttackSpeed()
//     {   // 공속
//         if (eGroup == EGroup.Ally)
//         {   // 아군
//             target = PartyManager.instance.GetAliveHero();
//             if (target == null) return null;
            
//             if (eMost == EMost.Least)
//             {   // 공속 최소 : delay가 가장 큰 경우 
//                 value = 0f;
//                 foreach (Character ch in PartyManager.instance.heroParty)
//                 {
//                     if (ch.isDead) continue;
//                     if (value < ch.attackDelay)
//                     {
//                         value = ch.attackDelay;
//                         target = ch;
//                     }
//                 }
//             }
//             else{   // 공속 최대 : delay가 가장 작은 경우
//                 value = Mathf.Infinity;
//                 foreach (Character ch in PartyManager.instance.heroParty)
//                 {
//                     if (ch.isDead) continue;
//                     if (value > ch.attackDelay)
//                     {
//                         value = ch.attackDelay;
//                         target = ch;
//                     }
//                 }
//             }
//         }
//         else{   // 적군
//            target = DungeonManager.instance.curDungeon.GetAliveMonster();
//             if (target == null) return null;
            
//             if (eMost == EMost.Least)
//             {   // 최소
//                 value = 0f;
//                 foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
//                 {
//                     if (ch.isDead) continue;
//                     if (value < ch.attackDelay)
//                     {
//                         value = ch.attackDelay;
//                         target = ch;
//                     }
//                 }
//             }
//             else{   // 최대
//                 value = Mathf.Infinity;
//                 foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
//                 {
//                     if (ch.isDead) continue;
//                     if (value > ch.attackDelay)
//                     {
//                         value = ch.attackDelay;
//                         target = ch;
//                     }
//                 }
//             }
//         }
//         return target;
//     }

//     Character FindTargetDistance()
//     {   // 거리 // TODO
//         if (eGroup == EGroup.Ally)
//         {   // 아군
//             target = PartyManager.instance.GetAliveHero();
//             if (target == null) return null;
            
//             if (eMost == EMost.Least)
//             {   // 최소
//                 value = Mathf.Infinity;
//                 foreach (Character ch in PartyManager.instance.heroParty)
//                 {
//                     if (ch.isDead) continue;
//                     m_temp = (ch.transform.position - owner.transform.position).sqrMagnitude;
//                     if (value * value > m_temp)
//                     {
//                         value = m_temp;
//                         target = ch;
//                     }
//                 }
//             }
//             else{   // 최대
//                 value = 0f;
//                 foreach (Character ch in PartyManager.instance.heroParty)
//                 {
//                     if (ch.isDead) continue;
//                     m_temp = (ch.transform.position - owner.transform.position).sqrMagnitude;
//                     if (value * value < m_temp)
//                     {
//                         value = m_temp;
//                         target = ch;
//                     }
//                 }
//             }
//         }
//         else{   // 적군
//            target = DungeonManager.instance.curDungeon.GetAliveMonster();
//             if (target == null) return null;
            
//             if (eMost == EMost.Least)
//             {   // 최소
//                 value = 10000f;
//                 foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
//                 {
//                     if (ch.isDead) continue;
//                     m_temp = (ch.transform.position - owner.transform.position).sqrMagnitude;
//                     if (value > m_temp)
//                     {
//                         value = m_temp;
//                         target = ch;
//                     }
//                 }
//             }
//             else{   // 최대
//                 value = 0f;
//                 foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
//                 {
//                     if (ch.isDead) continue;
//                     m_temp = (ch.transform.position - owner.transform.position).sqrMagnitude;
//                     if (value < m_temp)
//                     {
//                         value = m_temp;
//                         target = ch;
//                     }
//                 }
//             }
//         }
//         return target;
//     }

//     Character FindTargetMoveSpeed()
//     {   // 사거리
//         if (eGroup == EGroup.Ally)
//         {   // 아군
//             target = PartyManager.instance.GetAliveHero();
//             if (target == null) return null;
            
//             if (eMost == EMost.Least)
//             {   // 최소
//                 value = Mathf.Infinity;
//                 foreach (Character ch in PartyManager.instance.heroParty)
//                 {
//                     if (ch.isDead) continue;
//                     if (value > ch.moveSpeed)
//                     {
//                         value = ch.moveSpeed;
//                         target = ch;
//                     }
//                 }
//             }
//             else{   // 최대
//                 value = 0f;
//                 foreach (Character ch in PartyManager.instance.heroParty)
//                 {
//                     if (ch.isDead) continue;
//                     if (value < ch.moveSpeed)
//                     {
//                         value = ch.moveSpeed;
//                         target = ch;
//                     }
//                 }
//             }
//         }
//         else{   // 적군
//            target = DungeonManager.instance.curDungeon.GetAliveMonster();
//             if (target == null) return null;
            
//             if (eMost == EMost.Least)
//             {   // 최소
//                 value = Mathf.Infinity;
//                 foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
//                 {
//                     if (ch.isDead) continue;
//                     if (value > ch.moveSpeed)
//                     {
//                         value = ch.moveSpeed;
//                         target = ch;
//                     }
//                 }
//             }
//             else{   // 최대
//                 value = 0f;
//                 foreach (Character ch in DungeonManager.instance.curDungeon.curMonsters)
//                 {
//                     if (ch.isDead) continue;
//                     if (value < ch.moveSpeed)
//                     {
//                         value = ch.moveSpeed;
//                         target = ch;
//                     }
//                 }
//             }
//         }
//         return target;
//     }
// }
 
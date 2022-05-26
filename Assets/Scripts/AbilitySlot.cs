using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySlot : MonoBehaviour
{
    public AbilityData data;
    public AbilitySlot[] nextSlots;
    public bool isActive;

    public int curLV = 0;
    public void Init(){
        isActive = true;
    }

    public void InitNext()
    {
        if (curLV < data.maxLV) return;

        for (int i = 0; i < nextSlots.Length; i++)
        {
            if (nextSlots[i] != null)
            nextSlots[i].Init();
        }
    }

    // 우클릭이 안 돼서 
        // 키입력으로 하던가 ray로
    public void OnMouseDown() {
        LevelUp();
    }

    public void Apply()    { data.Apply(curLV); }
    public void Release()  { data.Release(curLV); } 

    public void LevelUp()   
    { 
        curLV ++;
        if (curLV == data.maxLV)
        {
            for (int i = 0; i < nextSlots.Length; i++)
        {
            if (nextSlots[i] != null)
                nextSlots[i].Init();
        }
        }
    }

    //public void LevelDown()
    //{

    //}

}

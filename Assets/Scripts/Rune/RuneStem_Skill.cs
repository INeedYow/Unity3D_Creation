using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneStem_Skill : RuneStem
{
    [HideInInspector] public Rune selectedRune;



    public override void AddPoint(Rune rune)
    {
        base.AddPoint(rune);
        
        if (selectedRune != rune)
        {
            ResetOthers(rune);
        }

        selectedRune = rune;
    }

    void ResetOthers(Rune rune)
    {
        for (int i = 0; i < runes.Length; i++)
        {
            if (runes[i] == rune) continue;

            tree.usePoints -= runes[i].point;
            PlayerManager.instance.runePoint += runes[i].point;
            runes[i].point = 0;
        }
    }
}

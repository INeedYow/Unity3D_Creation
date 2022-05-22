using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MacroBox : MonoBehaviour
{
    [Range(0, 4)] public int ID;
    public int executeCount;

    // public void MouseEnter() {
    //     DungeonManager.instance.dungeonUI.SetMiniMacroUI(true);
    // }

    // public void MouseExit() {
    //     DungeonManager.instance.dungeonUI.SetMiniMacroUI(false);
    // }
}

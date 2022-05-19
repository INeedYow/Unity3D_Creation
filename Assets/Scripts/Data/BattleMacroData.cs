using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleMacroData", menuName = "Data/BattleMacro")]
public class BattleMacroData : ScriptableObject
{
    [Header("Description")] [TextArea]
    public string desc;
    [SerializeField] public int ID;
}
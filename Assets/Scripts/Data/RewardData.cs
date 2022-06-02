using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardData", menuName = "Data/Reward")]
public class RewardData : ScriptableObject
{
    [SerializeField] int exp_perWaveMin;
    [SerializeField] int exp_perWaveMax;
    [SerializeField] int gold_perWaveMin;
    [SerializeField] int gold_perWaveMax;
    
    public int exp_clear;
    public int gold_clear;
    

    public int GetWaveExp() { return Random.Range(exp_perWaveMin, exp_perWaveMax + 1); }
    public int GetWaveGold() { return Random.Range(gold_perWaveMin, gold_perWaveMax + 1); }
}

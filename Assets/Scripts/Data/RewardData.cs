using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardData", menuName = "Data/Reward")]
public class RewardData : ScriptableObject
{
    public int exp_perWave;
    public int gold_perWave;
    public int exp_clear;
    public int gold_clear;
}

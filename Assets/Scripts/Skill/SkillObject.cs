using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillObject : MonoBehaviour
{
    public SkillObjectData data;
    public Skill skill;

    private void Awake() { gameObject.SetActive(false); }
    private void OnEnable() { Works(); }


    public abstract void Works();
    protected void FinishWorks() { gameObject.SetActive(false); }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroMacroUnit : MonoBehaviour
{
    public int ID;
    public Dropdown dropdown; 
    public Dropdown.OptionData option;
    
    // private void Awake() {   // 드래그 드랍 했음
    //     dropdown = GetComponent<Dropdown>();
    // }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.C))
        {
            option.text = " test ";
            dropdown.options[0] = option;
            dropdown.value = 0;
        }
    }
    // 써보니까 
    // 각 매크로 ID값 0부터 정수값으로 부여하고
    // value change때 value로 그 매크로 생성해서 넣어주게
    // public void RenewUI(int id){
    //     dropdown.value = id;
    // }

    public void SetText(List<Dropdown.OptionData> listOptionData){
        dropdown.AddOptions(listOptionData);
    }
}

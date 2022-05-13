using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroListUnit : MonoBehaviour
{
    public Hero hero;
    public Image icon;
    public Text nameText;
    public Button button;

    public void SetHero(Hero hero){
        this.hero = hero;
        icon.sprite = hero.icon;
        nameText.text = hero.name;
    }

    // public void Select(){ Debug.Log("Select()");
    //     GameManager.instance.selectedHero = hero;
    // }

    // public void ShowInfoUI() 
    // {   // Info UI 활성화   // event trigger로?
    //     Debug.Log("Unit ShowInfoUI()");
    // }

    // public void DragDummy() 
    // {   // 드래그 중일 때는 또 새로운 오브젝트로 처리?
    //     Debug.Log("Unit DragDummy");
    //     //hero.dummy.gameObject.SetActive(true);
    //     //hero.dummy.isSelected = true;
    // }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroSetUI : MonoBehaviour
{
    public HeroInvenUI invenUI;
    public HeroMacroUI macroUI;

    public Button invenBtn;
    public Button macroBtn;

    public void MenuToggle(){
        invenUI.gameObject.SetActive(!invenUI.isActiveAndEnabled);
        macroUI.gameObject.SetActive(!macroUI.isActiveAndEnabled);
        invenBtn.interactable = !invenBtn.IsInteractable();
        macroBtn.interactable = !macroBtn.IsInteractable();
    }
}

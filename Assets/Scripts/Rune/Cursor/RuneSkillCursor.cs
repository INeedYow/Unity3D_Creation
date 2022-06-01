using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public abstract class RuneSkillCursor : MonoBehaviour
{
    public UnityAction onWorks;

    //public Texture2D texture;
    //public Sprite mouseIcon;
    public RuneSkillObject skillObj;

    protected Ray ray;
    protected RaycastHit hit;

    private void OnEnable() {   Debug.Log("Cursor OnEnable");
        //Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
        // todo MGR.Plane.SetActive(true);
        //PlayerManager.instance.runeSkillUI.cursorImage.sprite = mouseIcon;
    }

    private void OnDisable() {
        //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void Update() 
    {
        CursorPosition();
        CheckInput();
    }

    protected abstract void CursorPosition();

    // 스킬 사용한 경우 onWorks?.Invoke(); 해주기
    // 사용 후 마우스 돌려놓기
    protected abstract void CheckInput();
}

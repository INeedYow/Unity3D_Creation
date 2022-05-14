using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public int gold;
    public int level = 1;

    [Header("Hero Prf")]
    public Hero prfKngiht;


    private void Awake() { instance = this; }

    public void TempDGEnter(int index)
    {   //
        PartyManager.instance.board.gameObject.SetActive(false);
        DungeonManager.instance.Enter(index);
    }
}

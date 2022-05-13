using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public int gold;
    public int level = 1;
    public List<Hero> heroList = new List<Hero>();

    private void Awake() {
        instance = this;
    }

    public void TempDGEnter(int index)
    {
        DungeonManager.instance.Enter(index);
    }
}

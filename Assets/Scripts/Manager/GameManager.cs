using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//public enum EMenu { Map, Dungeon, Board, Setting, Store, };
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    //public EMenu eMenu = EMenu.Map;
    public int gold;
    public int playerLevel = 1;

    public CameraMove cam;
    public CubePlanet cube;

    private void Awake() { 
        instance = this; 
        Application.targetFrameRate = 30;   // 프레임
    }
}
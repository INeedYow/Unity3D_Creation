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
        Application.targetFrameRate = 40;   // 프레임 제한
    }

    private void Update() {
        // if (Input.GetKeyDown(KeyCode.S) && eMenu == EMenu.Map)
        // {   // 임시로 보드, 배치 메뉴로 가게
        //     SetMap2Board();
        // }
        // else if (Input.GetKeyDown(KeyCode.W) && eMenu == EMenu.Board)
        // {
        //     SetBoard2Map();
        // }
        // else if (Input.GetKeyDown(KeyCode.D) && eMenu == EMenu.Board)
        // {
        //     SetBoard2Setting();
        // }
        // else if (Input.GetKeyDown(KeyCode.A) && eMenu == EMenu.Setting)
        // {
        //     SetSetting2Board();
        // }
    }

    // void SetMap2Board(){
    //     eMenu = EMenu.Board;
    //     HeroManager.instance.heroInfoUI.gameObject.SetActive(true);
    //     HeroManager.instance.heroListUI.gameObject.SetActive(true);
    //     PartyManager.instance.board.gameObject.SetActive(true);
    //     CameraManager.instance.SetCam(CameraManager.instance.setTf);
    // }

    // void SetBoard2Map(){
    //     eMenu = EMenu.Map;
    //     HeroManager.instance.heroInfoUI.gameObject.SetActive(false);
    //     HeroManager.instance.heroListUI.gameObject.SetActive(false);
    //     PartyManager.instance.board.gameObject.SetActive(false);
    //     CameraManager.instance.SetCam(CameraManager.instance.defaultTf);
    // }

    // void SetBoard2Setting(){
    //     eMenu = EMenu.Setting;
    //     HeroManager.instance.selectedHero = null;
    //     PartyManager.instance.board.gameObject.SetActive(false);
    //     HeroManager.instance.heroSetUI.gameObject.SetActive(true);
    // }

    // void SetSetting2Board(){
    //     eMenu = EMenu.Board;
    //     HeroManager.instance.selectedHero = null;
    //     PartyManager.instance.board.gameObject.SetActive(true);
    //     HeroManager.instance.heroSetUI.gameObject.SetActive(false);
    // }

    // public void SetMap2Dungeon(){
    //     eMenu = EMenu.Dungeon;
    // }
}
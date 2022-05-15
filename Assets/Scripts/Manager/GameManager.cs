using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EMenu { Map, Dungeon, Board, Setting, Store, };
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public EMenu eMenu = EMenu.Map;
    public int gold;
    public int level = 1;

    // temp test
    public GameObject ground;
    public GameObject enterance;
    public GameObject player;
    [Header("Prefabs")]
    public Projectile_Ally prfArcherArrow;  // TODO 투사체등 조건문 달거면 그냥 Hero를 직업군별로 상속하는 게 나을 것 같음

    private void Awake() { 
        instance = this; 
        Application.targetFrameRate = 40;   // 프레임 제한
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.S) && eMenu == EMenu.Map)
        {   // 임시로 보드, 배치 메뉴로 가게
            SetMap2Board();
        }
        else if (Input.GetKeyDown(KeyCode.W) && eMenu == EMenu.Board)
        {
            SetBoard2Map();
        }
        else if (Input.GetKeyDown(KeyCode.D) && eMenu == EMenu.Board)
        {
            SetBoard2Setting();
        }
        else if (Input.GetKeyDown(KeyCode.A) && eMenu == EMenu.Setting)
        {
            SetSetting2Board();
        }
    }

    void SetMap2Board(){
        eMenu = EMenu.Board;
        HeroManager.instance.heroInfoUI.gameObject.SetActive(true);
        HeroManager.instance.heroListUI.gameObject.SetActive(true);
        PartyManager.instance.board.gameObject.SetActive(true);
        CameraManager.instance.SetCam(CameraManager.instance.setTf);
        ground.gameObject.SetActive(false);
        enterance.gameObject.SetActive(false);
        player.gameObject.SetActive(false);
    }

    void SetBoard2Map(){
        eMenu = EMenu.Map;
        HeroManager.instance.heroInfoUI.gameObject.SetActive(false);
        HeroManager.instance.heroListUI.gameObject.SetActive(false);
        PartyManager.instance.board.gameObject.SetActive(false);
        CameraManager.instance.SetCam(CameraManager.instance.defaultTf);
        ground.gameObject.SetActive(true);
        enterance.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
    }

    void SetBoard2Setting(){
        eMenu = EMenu.Setting;
        HeroManager.instance.selectedHero = HeroManager.instance.heroList[0];
        PartyManager.instance.board.gameObject.SetActive(false);
        HeroManager.instance.heroSetUI.gameObject.SetActive(true);
    }

    void SetSetting2Board(){
        eMenu = EMenu.Board;
        HeroManager.instance.selectedHero = null;
        PartyManager.instance.board.gameObject.SetActive(true);
        HeroManager.instance.heroSetUI.gameObject.SetActive(false);
    }

    public void SetMap2Dungeon(){
        eMenu = EMenu.Dungeon;
        CameraManager.instance.SetCam(CameraManager.instance.DungeonTf);
        ground.gameObject.SetActive(false);
        enterance.gameObject.SetActive(false);
        player.gameObject.SetActive(false);
    }
}
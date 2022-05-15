using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public BoardBlock prfBlockA;        // 처음에 보드 생성할 프리팹
    public BoardBlock prfBlockB;        // block_typeB (단순 체크 무늬용)
    public GameObject prfEdgeBlock;     // 가장자리 블록prf
    public int hSize = 13;
    public int vSize = 7;
    public BoardBlock[,] blocks;
    public List<Dummy> dummies = new List<Dummy>();

    private void Awake() {
        blocks = new BoardBlock[vSize, hSize];
        CreateBlocks();
        gameObject.SetActive(false);
    }

    private void OnEnable() {
        Init();
    }

    private void OnDisable() {
        if (PartyManager.instance != null) HideDummy();
    }

    void ShowDummy() { PartyManager.instance.ShowDummy(); }
    void HideDummy() { PartyManager.instance.HideDummy(); }

    void CreateBlocks()
    {
        int halfH = (int)(hSize / 2);
        int halfV = (int)(vSize / 2);
        GameObject edge;
        BoardBlock block;

        for (int i = 0; i < hSize + 2; i++){
            edge = Instantiate(prfEdgeBlock);
            edge.transform.SetParent(transform);
            edge.transform.position = new Vector3(i - halfH - 1, transform.position.y, -halfV - 1);
        }

        for (int j = 0; j < vSize; j++)
        {
            edge = Instantiate(prfEdgeBlock);
            edge.transform.SetParent(transform);
            edge.transform.position = new Vector3(-halfH - 1, transform.position.y, -halfV + j);
            
            for (int i = 0; i < hSize; i++)
            {
                if (i % 2 == 0) block = Instantiate(prfBlockA);
                else block = Instantiate(prfBlockB);

                blocks[j, i] = block;
                block.transform.SetParent(transform);
                block.transform.position = new Vector3(i - halfH, transform.position.y, -halfV + j);
                block.Init();
            }

            edge = Instantiate(prfEdgeBlock);
            edge.transform.SetParent(transform);
            edge.transform.position = new Vector3(+halfH + 1, transform.position.y, -halfV + j);
        }

        for (int i = 0; i < hSize + 2; i++){
            edge = Instantiate(prfEdgeBlock);
            edge.transform.SetParent(transform);
            edge.transform.position = new Vector3(i - halfH - 1, transform.position.y, +halfV + 1);
        }
    }

    void Init(){
        // transform.rotation = Quaternion.Euler(0f,0f,0f);
        // transform.position = new Vector3(0f,0f,00f);
        StartCoroutine("SetRot");
        StartCoroutine("SetPos");
    }

    IEnumerator SetRot(){

        float rot = 0f;
        while (rot < 75f){
            transform.rotation = Quaternion.Euler(-rot, 0f, 0f);
            rot += Time.deltaTime * 60f;
            yield return null;
        }
        transform.rotation = Quaternion.Euler(-75f, 0f, 0f);
        yield return null;
    }

    IEnumerator SetPos(){

        float rot = 0f;
        while (rot < 5f){
            transform.position = new Vector3(rot, 0f, 3f * rot);
            rot += Time.deltaTime * 6f;
            yield return null;
        }
        transform.position = new Vector3(5f, 0f, 15f);
        yield return null;

        rot = 0f;
        while (rot < 1f){
            transform.position = new Vector3(5f, rot, 15f);
            rot += Time.deltaTime * 1f;
            yield return null;
        }
        transform.position = new Vector3(5f, 1f, 15f);
        ShowDummy();
        yield return null;
    }
}

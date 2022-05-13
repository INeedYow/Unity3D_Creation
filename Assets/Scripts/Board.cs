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

    private void Start() {
        blocks = new BoardBlock[vSize, hSize];
        CreateBlocks();
    }

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

}

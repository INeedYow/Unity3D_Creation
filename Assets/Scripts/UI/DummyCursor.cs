using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyCursor : MonoBehaviour
{
    public Dummy dummy;
    RaycastHit hit;
    Ray ray;
    Collider[] colls;
    Vector3 half = new Vector3 (0.3f, 1f, 0.3f);
    BoardBlock block;

    private void OnEnable() {
        dummy = HeroManager.instance.selectedHero.dummy;
    }

    void Update()
    {
        if (dummy == null) return;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("DummyPlane")))
        {
            transform.position = hit.point;
            if (!dummy.isOnBlock)
            {
                dummy.transform.position = hit.point;
            }
        }
    }

    private void FixedUpdate() {
        colls = Physics.OverlapBox(transform.position, half, Quaternion.identity, LayerMask.GetMask("BoardBlock"));
        if (colls.Length > 0){  //Debug.Log(colls.Length + "length");
            block = colls[0].transform.GetComponent<BoardBlock>();
            if (block != null){
                dummy.OnBlock(block);
            }
        }
        else{
            dummy.OnBlock(null);
        }
    }
}
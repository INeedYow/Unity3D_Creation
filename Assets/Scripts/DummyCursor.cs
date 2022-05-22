using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyCursor : MonoBehaviour
{
    public Dummy dummy;
    private void OnEnable() {
        dummy = HeroManager.instance.selectedHero.dummy;
    }
    void Update()
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("DummyPlane")))
        {
            if (dummy != null) dummy.transform.position = hit.point;
        }
    }

    public void DropDummy(){
        gameObject.SetActive(false);
    }
}

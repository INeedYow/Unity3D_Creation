using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    CharacterController controller;
    Vector3 m_moveVec;

    private void Start() {
        controller = GetComponent<CharacterController>();
    }

    private void Update() {
        
        // m_inputH = Input.GetAxis("Horizontal");
        // m_inputV = Input.GetAxis("Vertical");

        // m_moveVec = new Vector3(m_inputH, 0f, m_inputV).normalized;

        // controller.Move(m_moveVec * moveSpeed * Time.deltaTime);

        if (Input.GetMouseButton(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                m_moveVec = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                transform.LookAt(m_moveVec, Vector3.up);
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            }
        }
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    CharacterController controller;
    Vector3 m_moveVec;
    float m_inputH;
    float m_inputV;

    private void Start() {
        controller = GetComponent<CharacterController>();
    }

    private void Update() {
        
        m_inputH = Input.GetAxis("Horizontal");
        m_inputV = Input.GetAxis("Vertical");

        m_moveVec = new Vector3(m_inputH, 0f, m_inputV).normalized;

        controller.Move(m_moveVec * moveSpeed * Time.deltaTime);
    }
}

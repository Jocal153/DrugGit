using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //�����ƶ�����Ծ�ٶȴ�С
    public float moveSpeed, jumpSpeed;
    float horizon, vercital;
    Vector3 move,velocity;
    //����Ƿ�Ӵ�����
    public Transform groundCheck;
    bool isGround;
    public float checkRadius;
    public LayerMask groundLayer;
    CharacterController cc;
    public float gravity;
    

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        
    }

    

    void MoveAndJump()
    {
        //��ȡ�ƶ���������ʹ����ƶ�
        horizon = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        vercital = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        move = new Vector3(horizon, 0, vercital);
        move = transform.TransformDirection(move);
        cc.Move(move);
        //������Ծ
        isGround = Physics.CheckSphere(groundCheck.position, checkRadius, groundLayer);
        velocity.y -= gravity * Time.deltaTime;
        if(isGround)velocity.y = 0;
        if(Input.GetButtonDown("Jump") && isGround)
        {
            velocity.y += jumpSpeed;            
        }
        cc.Move(velocity * Time.deltaTime);
        
    }

    private void Update()
    {
        MoveAndJump();
        
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    //�ӽ����������Ʒʰȡ
    public Camera Cam;
    Transform pickObj;
    Transform handObj;
    bool handEmpty;
    public GameObject pickUI;
    //TextMeshPro picktmp;

    private void Awake()
    {
        pickUI.SetActive(true);
        pickUI.SetActive(false);
    }

    private void Start()
    {
        pickObj = null;
        handObj = null;
        handEmpty = true;
        //picktmp = pickUI.GetComponent<TextMeshPro>();
    }

    //�����ǰ�Ƿ������壬���򵯳�ʰȡ��ʾ
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickObj"))
        {
            pickObj = other.GetComponent<Transform>();
            //picktmp.enabled = true;
            pickUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PickObj"))
        {
            pickObj = null;
            //picktmp.enabled = false;
            pickUI.SetActive(false);
        }
    }

    private void Update()
    {
        //��fʰȡ��ǰ���嵽����
        if (Input.GetKeyDown(KeyCode.F) && pickObj != null)
        {
            if (!handEmpty) handObj.gameObject.SetActive(false);
            handObj = pickObj;
            handObj.gameObject.layer = LayerMask.NameToLayer("Player");
            //����transform����ȷ��������Ʒ�Ƕ���ȷ������Ұ
            handObj.SetParent(Cam.transform);
            handObj.localEulerAngles = Vector3.zero;
            handObj.localPosition = new Vector3(0.9f, -0.7f, 1);
            //picktmp.enabled = false;
            pickUI.SetActive(false);
            pickObj = null;
            handEmpty = false;
        }
    }
}

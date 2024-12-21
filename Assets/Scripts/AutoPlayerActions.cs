using System.Collections;
using UnityEngine;

public class AutoPlayerActions : MonoBehaviour
{
    public float moveSpeed = 10f;           // ��ɫ���ƶ��ٶ�
    public float shakeIntensity = 0.1f;      // �ӽ�ҡ�ε�ǿ��
    public float shakeDuration = 0.5f;       // �ӽ�ҡ�γ���ʱ��
    public float fallSpeed = 1f;             // ���µ��ٶ�
    public float getUpSpeed = 1f;            // �������ٶ�
    public float shakeFrequency = 0.5f;

    private Rigidbody rb;
    private Camera playerCamera;
    private Vector3 originalCameraPosition;
    private Vector3 moveDirection;
    private bool isFallen = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = Camera.main;
        originalCameraPosition = playerCamera.transform.localPosition;

        // �Զ�ִ�����ж���
        StartCoroutine(AutoSequence());
    }

    IEnumerator AutoSequence()
    {
        // 1. �����ƶ�
        StartCoroutine(MovePlayer());
        yield return new WaitForSeconds(0.5f); // �ȴ� 2 ��

        // 2. �ӽ�ҡ��
        StartCoroutine(ShakeCamera());
        yield return new WaitForSeconds(shakeDuration); // �ȴ�ҡ�γ���ʱ��

        // 3. ����
        StartCoroutine(FallDown());
        yield return new WaitForSeconds(2.5f); // �ȴ� 2 �룬�õ��¶������

        // 4. ����
        StartCoroutine(GetUp());
        yield return new WaitForSeconds(5f);
    }

    // ��������ƶ�
    IEnumerator MovePlayer()
    {
        // �ƶ��ķ���
        moveDirection = new Vector3(1, 0, 0).normalized; // �����ƶ�

        // ִ���ƶ�
        while (true)
        {
            if (isFallen) break; // ���������ֹͣ�ƶ�
            rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
            yield return null;
        }
    }

    // �ӽ�ҡ��
    IEnumerator ShakeCamera()
    {
        Vector3 originalPos = playerCamera.transform.localPosition;      // ��ȡ��ʼλ��
        float remainingShakeDuration = shakeDuration;                     // ʣ���ҡ��ʱ��
        float remainingShakeIntensity = shakeIntensity;                   // ��ʼǿ��

        // Ƶ�ʿ��ƣ���Ȼ��ָ��Ƶ�ʽ��и���
        float shakeInterval = 1f / shakeFrequency;
        float timeSinceLastShake = 0f;

        // ����ƽ����ֵ�ı���
        Vector3 currentShakePosition = originalPos;                      // ��ǰҡ��λ��
        Vector3 targetShakePosition = originalPos;                       // Ŀ��ҡ��λ��

        // ʹҡ�θ�ƽ��
        while (remainingShakeDuration > 0)
        {
            timeSinceLastShake += Time.deltaTime;

            // ����Ѿ��������Ƶ�ʵ�ʱ��
            if (timeSinceLastShake >= shakeInterval)
            {
                // ����ʣ��ʱ�䶯̬����ҡ��ǿ��
                float currentShakeAmount = remainingShakeIntensity * (remainingShakeDuration / shakeDuration);

                // ʹ��Random.insideUnitSphere����һ���µ�Ŀ��ҡ��λ��
                targetShakePosition = originalPos + Random.insideUnitSphere * currentShakeAmount;

                // ʹ�� Lerp �� SmoothDamp ��ƽ�����ɵ�Ŀ��λ��
                currentShakePosition = Vector3.Lerp(currentShakePosition, targetShakePosition, 0.2f);

                // ����ʱ��
                timeSinceLastShake = 0f;
            }

            // ���������λ��Ϊƽ�����ɺ�Ľ��
            playerCamera.transform.localPosition = currentShakePosition;

            // ����ʣ��ʱ��
            remainingShakeDuration -= Time.deltaTime;

            yield return null;
        }

        // ���ָ���ԭʼλ��
        playerCamera.transform.localPosition = originalPos;
    }
    // ���¶���
    IEnumerator FallDown()
    {
        isFallen = true;
        Vector3 fallPosition = playerCamera.transform.position + new Vector3(0, -2, 0);

        while (playerCamera.transform.position.y > fallPosition.y)
        {
            playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, fallPosition, fallSpeed * Time.deltaTime);
            yield return null;
        }

        playerCamera.transform.position = new Vector3(17, 2.6f, 21);
    }

    // ��������
    IEnumerator GetUp()
    {
        isFallen = false;

        // ���ûָ���Ŀ��λ��
        Vector3 standPosition = new Vector3(2, 0, 0);
        // Vector3 currentPosition = playerCamera.transform.position;

        // ��ֹ��ͻȻ�½������������ƽ������ y ���Ŀ��
        //while (currentPosition.y < standPosition.y)
        //{
        //    currentPosition.y = Mathf.MoveTowards(currentPosition.y, standPosition.y, getUpSpeed * Time.deltaTime);
        //   playerCamera.transform.position = currentPosition;
        //yield return null;
        //  }

        // ���ȷ��y��λ���Ѿ���ȫ�ָ�
        //currentPosition.y = standPosition.y;
        playerCamera.transform.position = standPosition;
        Debug.Log("����������");
        yield return null;
    }
}
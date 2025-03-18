using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform rocket;   // ��ҧ�ԧ价���Ǵ
    public Vector3 offset = new Vector3(0, 2, -5); // ���˹觡��ͧ (��ҹ��ѧ��Ǵ)
    public float smoothSpeed = 10f; // ��������㹡�û�Ѻ���˹觡��ͧ

    void LateUpdate()
    {
        if (rocket == null) return;

        // �ӹǳ���˹觡��ͧ�������ѧ�ҹ
        Vector3 targetPosition = rocket.position + rocket.TransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);

        // ��ع���ͧ����ѹ价ҧ���ǡѺ�ҹ
        transform.rotation = Quaternion.Lerp(transform.rotation, rocket.rotation, Time.deltaTime * smoothSpeed);
    }
}
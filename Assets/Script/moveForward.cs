using UnityEngine;

public class moveForward : MonoBehaviour
{

    public float speed = 5;
    

    // Update is called once per frame
    void FixedUpdate()
    {

        transform.Translate(speed * Time.deltaTime * Vector3.forward);

       

    }
}

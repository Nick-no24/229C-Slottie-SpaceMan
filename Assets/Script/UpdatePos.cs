using UnityEngine;

public class UpdatePos : MonoBehaviour
{
    public  GameObject pl1;
    public  GameObject pl2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pl1.transform.position = pl2.transform.position;    
        pl2.transform.rotation = pl1.transform.rotation;
    }
}

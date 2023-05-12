using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightEdge : MonoBehaviour
{
    // Start is called before the first frame update
    private float x;  
    private float y;
    private float z; 
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3 (gameObject.transform.position.x, 
            gameObject.transform.position.y, gameObject.transform.position.z+50);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightEdge_Hw : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float sightValue; 
    private float x;  
    private float y;
    private float z; 
    // Update is called once per frame
    void Update()
    {
        
    }
    private void Start()
    {
        transform.position = new Vector3(gameObject.transform.position.x,
            gameObject.transform.position.y, gameObject.transform.position.z + sightValue);
    }
    private void Awake()
    {
        gameObject.name = "Sight Pointer";
    }
}

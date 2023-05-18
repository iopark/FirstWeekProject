using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityBasic : MonoBehaviour
{
    public GameObject thisGameObject;
    public GameObject targetObject;
    public GameObject[] targetObjects;
    public AudioSource audioSource;
    public GameObject testingObject;

    [Header("Try Adding or Destroying Components")]
    public GameObject tryObject; 
    // Start is called before the first frame update
    void Start()
    {
        GameObjectBasic();
        //ComponentBasic(); 
    }

    void GameObjectBasic()
    {
        thisGameObject = gameObject;
        thisGameObject.name = "Nein Cube";
        targetObject = GameObject.Find("Player");
        targetObjects = GameObject.FindGameObjectsWithTag("Player");

        // <게임 오브젝트 생성> 
        GameObject newObject = new GameObject("New Object"); 

        // <게임 오브젝트 삭제> 

        Destroy( newObject );
        Destroy(newObject, 3f); // Delayed Destroy in interval of 3s 
        targetObjects = GameObject.FindGameObjectsWithTag("Player");
        // Game Object Search 

       //GameObject.Find("name"); // find object in the entire scene corresponding to the searching name 
       //GameObject.FindWithTag("tag name"); // Generally, this is quicker search
       //GameObject.FindGameObjectsWithTag("tag name returning list");
    }

    // Update is called once per frame
    void Update()
    {
    }

    //public void ComponentBasic()
    //{
    //    // Self Search 
    //    //audioSource = GetComponent<AudioSource>();
    //    gameObject.GetComponent<AudioSource>();
    //    GetComponent<AudioSource>(); 

    //    //Approaching Children gameObj 
    //    GetComponentInChildren<AudioSource>();
    //    GetComponentsInChildren<AudioSource>(); // Used frequently, for simplistic reason > drag and drop from inspector field 

    //    //Approaching Parent gameObj Component 
    //    GetComponentInParent<AudioSource>();
    //    GetComponentsInParent<AudioSource>(); 

    //    // Searching for Component 
    //    FindObjectOfType<AudioSource>(); 
    //    FindObjectsOfType<AudioSource>(); 

    //    // Adding Components 
    //    audioSource = gameObject.AddComponent<AudioSource>(); // 
    //    tryObject = GameObject.FindGameObjectWithTag("Player"); 
    //    AudioSource newaudio = gameObject.AddComponent<AudioSource>();
    //    Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
    //    Button button = tryObject.AddComponent<Button>();
    //    Rigidbody rigidbody1 = tryObject.AddComponent<Rigidbody>();
    //    // Removing Components 

    //    Rigidbody rigidtesting = tryObject.GetComponent<Rigidbody>();
    //    if (rigidtesting != null)
    //    {
    //        Debug.Log("rigidtesting component found");
    //    }
    //    else
    //    {
    //        Debug.Log("rigidtesting component not found");
    //    }
    //}
}

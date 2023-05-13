using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerQueue : MonoBehaviour
{

    [Header("Controlling Now ")]
    public GameObject playing;

    //특정 Scene 에서 Playable 한 gameObj 들을 관장하는 컴포넌트 
    private GameObject[] playableList;
    private Queue<GameObject> playable;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject go in playableList)
        {
            playable.Enqueue(go);
        }
    }

    // Update is called once per frame
    public GameObject Switch(GameObject current)
    {
        playable.Enqueue(current); 

        return playable.Dequeue();
    }
    private void FindPlayable()
    {
        playableList = GameObject.FindGameObjectsWithTag("Playable");
    }

    private void Awake()
    {
        FindPlayable();
    }
}

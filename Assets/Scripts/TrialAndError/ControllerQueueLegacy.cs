using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerQueueLegacy : MonoBehaviour
{

    [Header("Controlling Now ")]
    public GameObject playing;
    public PlayerInput controlling; 
    //Ư�� Scene ���� Playable �� gameObj ���� �����ϴ� ������Ʈ 
    public GameObject[] playableList;
    public Queue<GameObject> playable;
    private GameObject current; 
    private Camera cameraNow; 
    // Start is called before the first frame update
    void Start()
    {
        playable = new Queue<GameObject>();
        FindPlayable();
        foreach (GameObject go in playableList)
        {
            playable.Enqueue(go);
        }
        current = playable.Dequeue();
        Debug.Log(playable.Count);
    }

    // Update is called once per frame
    private void OnSwitchUnit(InputValue value)
    {
        if (playableList.Length <= 1)
            return;
        SetCurrentController(current);
        GameObject newControllable = playable.Dequeue();
        playable.Enqueue(current);
        SetNextController(newControllable); 

    }
    private void SetCurrentController(GameObject current)
    {
        controlling = current.GetComponent<PlayerInput>();
        controlling.enabled = false;
        playing = null;
    }
    private void SetNextController(GameObject next)
    {
        current = next;
        controlling = next.GetComponent<PlayerInput>();
        controlling.enabled = true;
        playing = next;
        cameraNow = controlling.camera;
        int nextDisplay = cameraNow.targetDisplay;
        Debug.Log(nextDisplay);
        Display.displays[nextDisplay].Activate();
    }
    private void FindPlayable()
    {
        playableList = GameObject.FindGameObjectsWithTag("Playable");
    }
}

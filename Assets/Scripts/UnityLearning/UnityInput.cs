using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnityInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputByDevice(); 
    }

    private void InputByDevice()
    {
        //Device 
        // 
        // 키보드 입력 
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Debug.Log("Key Up"); 
        }
        if (Input.GetKeyUp(KeyCode.Space))
            Debug.Log("Key Down");
        if (Input.GetKey(KeyCode.Space))
            Debug.Log("Key Pressing");

        if (Input.GetMouseButton(0))
            Debug.Log("Mouse Left Pressed");
        if (Input.GetMouseButton(1))
            Debug.Log("Mouse right Pressed"); 
    }

    private void InputbyInputManager()
    {
        if (Input.GetButton("Fire1")) // Utilizing Unity's Input Manager, which sets any Mouse 0, or JoyStick Input 0 as Fire1, 
            // IF user was to press any key which corresponds to GetButton("Fire1"), we can make game respond to that specific Key Map 
            Debug.Log("Fire1 is up");
        if (Input.GetButton("Jump"))
            Debug.Log("Jump is done");


        // Axes Input 
        // Horizontal : Where in Keyboard, a, d or arrow.left, arrow.right 
        float x = Input.GetAxis("Horizontal");
        // Vertical, Where in Keyboard, w, s, arrow.up, arrow.down
        float y = Input.GetAxis("Vertical");
        Debug.Log($"{x}, {y}"); 


    }

    // because Input System's interaction is based on the Messaging, 

    //private void OnMove(InputValue value)
    //{
    //    Vector2 dir = value.Get<Vector2>();
    //}


    //private void OnJump(InputValue value)
    //{
    //    bool isPressed = value.isPressed; 
    //}

}

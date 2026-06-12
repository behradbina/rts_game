using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private float keyboardPanSpeed = 5f;
    [SerializeField] private Transform cameraTarget;
    
    void Update()
    {
        Vector2 moveAmount = Vector2.zero;

        if(Keyboard.current.upArrowKey.isPressed)
        {
            moveAmount.y += keyboardPanSpeed;
        }

        if(Keyboard.current.downArrowKey.isPressed)
        {
            moveAmount.y -= keyboardPanSpeed;
        }
        if(Keyboard.current.leftArrowKey.isPressed)
        {
            moveAmount.x -= keyboardPanSpeed;
        }
        if(Keyboard.current.rightArrowKey.isPressed){
            moveAmount.x += keyboardPanSpeed;       

        }


        moveAmount *= Time.deltaTime;

        cameraTarget.position += new Vector3(moveAmount.x, 0, moveAmount.y);;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(CallbackContext context)
    {
        Vector2 inputMovement = context.ReadValue<Vector2>();
        Vector3 rawInput = new Vector3(inputMovement.x, 0, inputMovement.y);
        gameObject.transform.position += rawInput * Time.deltaTime;
    }
}

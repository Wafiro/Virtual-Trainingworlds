using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCube : MonoBehaviour
{
    public void OnInput(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.canceled)
        {
            var d = callbackContext.ReadValue<Vector2>();
            transform.position += Vector3.up * d.y + Vector3.right * d.x;
        }
    }
}

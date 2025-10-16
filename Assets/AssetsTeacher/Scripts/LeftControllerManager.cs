using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class LeftControllerManager : MonoBehaviour
{
    public GameObject baseController;
    public GameObject RayCastController;

    public InputActionReference ActivateRayCast;
    public InputActionReference CancelRayCast;
    [Space]

    public UnityEvent onRayCastActivate;
    public UnityEvent onRayCastCancel;

    // Start is called before the first frame update
    void Start()
    {
        ActivateRayCast.action.performed += RayCastModeActivate;
        CancelRayCast.action.performed += RayCastModeCancel;
    }

    public void RayCastModeActivate(InputAction.CallbackContext obj)
    {
        onRayCastActivate.Invoke();
    }

    public void RayCastModeCancel(InputAction.CallbackContext obj)
    {
        Invoke("DeactivateRayCast", 0.1f);
    }

    void DeactivateRayCast()
    {
        onRayCastCancel.Invoke();
    }
}

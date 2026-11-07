using UnityEngine;
using UnityEngine.InputSystem;

public class DimensionManager : MonoBehaviour
{
    [SerializeField] GameObject overworld;
    [SerializeField] GameObject otherworld;

    InputAction shiftAction;

    private void Start()
    {
        shiftAction = InputSystem.actions.FindAction("Shift");

        overworld.SetActive(true);
        otherworld.SetActive(false);
    }

    private void Update()
    {
        if (shiftAction.WasPressedThisFrame())
        {
            ShiftDimension();
        }
    }

    void ShiftDimension()
    {
        overworld.SetActive(!overworld.activeInHierarchy);
        otherworld.SetActive(!overworld.activeInHierarchy);
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class SlotMachine : MonoBehaviour
{
    int chosenNumber;
    bool canInteract;

    InputAction randomizeAction;

    private void Start()
    {
        randomizeAction = InputSystem.actions.FindAction("Interact");
        Debug.Log(randomizeAction);
    }

    private void Update()
    {
        Interact();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            canInteract = false;
        }
    }

    void Interact()
    {
        if (randomizeAction.WasPressedThisFrame() && canInteract)
        {
            chosenNumber = RandomNumber();
            Debug.Log(chosenNumber);

            Collider2D col = GetComponent<Collider2D>();
            col.enabled = false;
            canInteract = false;

            CheckResult();
        }
    }

    int RandomNumber()
    {
        int num = Random.Range(1, 11);
        return num;
    }

    void CheckResult()
    {
        if (chosenNumber == GameManager.instance.luckyNumber)
        {
            Debug.Log("Win");
        }
        else
        {
            Debug.Log("Loss");
        }
    }
}

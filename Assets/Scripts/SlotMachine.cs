using UnityEngine;
using UnityEngine.InputSystem;

public class SlotMachine : MonoBehaviour
{
    [SerializeField] GameObject interactText;

    int chosenNumber;
    bool canInteract;

    InputAction randomizeAction;
    SpriteRenderer spriteRenderer;
    Sprite inactiveSprite;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        randomizeAction = InputSystem.actions.FindAction("Interact");

        inactiveSprite = spriteRenderer.sprite;
        interactText.SetActive(false);
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
        interactText.SetActive(canInteract);

        if (randomizeAction.WasPressedThisFrame() && canInteract)
        {
            chosenNumber = RandomNumber();
            Debug.Log(chosenNumber);

            Collider2D col = GetComponent<Collider2D>();
            col.enabled = false;
            canInteract = false;

            Animator animator = GetComponent<Animator>();
            animator.enabled = false;

            SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.sprite = inactiveSprite;

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

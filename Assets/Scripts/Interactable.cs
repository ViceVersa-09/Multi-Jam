using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    enum What
    {
        Altar,
        Door,
        Item,
    }

    [Header("General")]
    [SerializeField] What type;
    [SerializeField] TextMeshProUGUI interactText;

    [Header("Altar")]
    [SerializeField] int number;

    bool canInteract;

    InputAction interactAction;
    SpriteRenderer spriteRenderer;
    Sprite inactiveSprite;
    Animator animator;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        interactAction = InputSystem.actions.FindAction("Interact");
        animator = GetComponent<Animator>();

        interactText.enabled = false;

        if (type == What.Altar)
        {
            inactiveSprite = spriteRenderer.sprite;
        }
    }

    private void Update()
    {
        switch (type)
        {
            case What.Altar:
                AltarInteract();
                break;

            case What.Door:
                DoorInteract();
                break;

            case What.Item:
                ItemInteract();
                break;
        }
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

    void AltarInteract()
    {
        interactText.text = "Press [E] to interact\n\n" + number.ToString(); 
        interactText.enabled = canInteract;
        animator.enabled = GameManager.instance.luckyNumber == number;

        if (interactAction.WasPressedThisFrame() && canInteract)
        {
            GameManager.instance.luckyNumber = number;
        }

        if (GameManager.instance.luckyNumber == number)
        {
            interactText.text = "\n\n" + number.ToString();
            interactText.enabled = true;
        }
        else
        {
            spriteRenderer.sprite = inactiveSprite;
        }
    }

    void DoorInteract()
    {
        if (interactAction.WasPressedThisFrame() && canInteract && GameManager.instance.luckyNumber != 0)
        {
            StartCoroutine(CameraController.instance.Transition(SceneManager.GetActiveScene().buildIndex + 1));
            interactText.enabled = true;
        }
        else if (canInteract && GameManager.instance.luckyNumber != 0)
        {
            interactText.enabled = true;
        }
        else
        {
            interactText.enabled = false;
        }
    }

    void ItemInteract()
    {

    }
}

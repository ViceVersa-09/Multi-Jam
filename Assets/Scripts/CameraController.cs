using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [Header("Camera Settings")]
    [SerializeField] public GameObject target;
    [SerializeField] float smoothing;
    [SerializeField] public Vector3 offset;

    [Header("Tracking")]
    [SerializeField] bool trackX;
    [SerializeField] bool trackY;

    float timeElapsed;
    bool shakeActive;

    Vector3 targetPosition;
    Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Update()
    {
        GetTargetPosition();
    }
    private void LateUpdate()
    {
        if (!shakeActive)
        {
            MoveCamera();
        }
    }

    void MoveCamera()
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothing);
    }

    public void PlayCameraShakeRoutine(float shakeDuration, float shakeMagnitude)
    {
        StartCoroutine(ShakeCameraRoutine(shakeDuration, shakeMagnitude));
    }

    void GetTargetPosition()
    {
        if (trackX && trackY) // Track both axis
        {
            if (target != null)
            {
                targetPosition = new Vector3
                (
                target.transform.position.x,
                target.transform.position.y,
                transform.position.z
                ) + new Vector3(offset.x * target.transform.localScale.x, offset.y, offset.z);
            }
        }
        else if (trackX && !trackY) // Track X But not Y Axis
        {
            if (target != null)
            {
                targetPosition = new Vector3
                (
                target.transform.position.x,
                0,
                transform.position.z
                ) + new Vector3(offset.x * target.transform.localScale.x, offset.y, offset.z);
            }
        }
        else if (!trackX && trackY) // Track Y But not X Axis
        {
            if (target != null)
            {
                targetPosition = new Vector3
                (
                0,
                target.transform.position.y,
                transform.position.z
                ) + new Vector3(offset.x * target.transform.localScale.x, offset.y, offset.z);
            }
        }
    }

    IEnumerator ShakeCameraRoutine(float shakeDuration, float shakeMagnitude)
    {
        shakeActive = true;
        timeElapsed = 0;
        while (timeElapsed < shakeDuration)
        {
            Vector3 initialPosition = transform.position;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothing) + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            transform.position = initialPosition;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothing);
        }
        shakeActive = false;
    }

    public IEnumerator Transition(int level)
    {
        Animator animator = GetComponentInChildren<Animator>();
        animator.SetTrigger("Out");
        //AudioManager.instance.PlaySFX(AudioManager.instance.transitionClip);

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(level);
    }
}

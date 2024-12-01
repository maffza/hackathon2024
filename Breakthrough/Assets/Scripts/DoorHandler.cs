using System.Collections; // Ważne dla IEnumerator
using UnityEngine; // Ważne dla MonoBehaviour i innych komponentów Unity


public class DoorHandler : MonoBehaviour
{
    private Animator animator;
    private GameObject gameManager;

    [SerializeField]
    private bool isOpen = false;

    private GameObject camera;
    private ScreenBlinkEffect screenBlinkEffect;
    private bool doOnceBlink = true;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = GameObject.Find("Main Camera");
        if (camera == null)
        {
            Debug.LogError("Camera is missing.");
        }
        
        screenBlinkEffect = camera.GetComponent<ScreenBlinkEffect>();
        if (screenBlinkEffect == null)
        {
            Debug.LogError("ScreenBlinkEffect is missing.");
        }


        gameManager = GameObject.Find("GameManager");
        if (gameManager == null)
        {
            Debug.LogError("GameManager is missing.");
        }
        
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator is missing on this object.");
        }

        if (isOpen)
        {
            animator.SetTrigger("OpenDoor");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isOpen)
            {
                if (doOnceBlink)
                {
                    screenBlinkEffect.CloseEye();
                    doOnceBlink = false;
                }
                gameManager.GetComponent<AudioPlayer>().PlaySound(2);
                StartCoroutine(ExecuteAfterDelay(2f)); // dwie sekundy opoznienia
            }
        }
    }

    IEnumerator ExecuteAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        gameManager.GetComponent<GameManager>().LoadNextLevel();
    }

    public void OpenDoor()
    {
        // Animation handler
        animator.SetTrigger("OpenDoor");

        // Setting state
        isOpen = true;
    }

    public void CloseDoor()
    {
        // Animation handler
        animator.SetTrigger("CloseDoor");

        // Setting state
        isOpen = false;
    }
}

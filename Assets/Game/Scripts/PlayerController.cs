using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ParticleSystem dirtSplatterFX;
    [SerializeField] private ParticleSystem blowFX;
    [SerializeField] private float jumpForce;

    private AudioManager playerAudio;
    private Rigidbody playerRb;
    private Animator animator;
    private const float SpeedDivider = 12.5f;
    private bool isGround;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerAudio = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        gameManager.NewStageEvent += OnStageChanged;
    }

    private void Update()
    {
        if (!gameManager.GameOver)
            PlayerJump();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            if (!gameManager.GameOver)
                dirtSplatterFX.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameManager.GameOver = true;
            animator.SetBool("Death_b", true);
            dirtSplatterFX.Stop();
            blowFX.Play();
            playerAudio.PlayBoomSFX();
        }
    }

    private void PlayerJump()
    {
        if (Input.GetKey(KeyCode.W) && isGround)
        {
            isGround = false;
            dirtSplatterFX.Stop();
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("Jump_trig");
            playerAudio.PlayJumpSFX();
        }
    }

    private void OnStageChanged()
    {
        animator.SetFloat("Speed_f", gameManager.gameSpeed / SpeedDivider);
    }
}

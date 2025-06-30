using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIMaker : MonoBehaviour
{
    [SerializeField] private GameObject preview;
    [SerializeField] private TextMeshProUGUI logoText;
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private TextMeshProUGUI playerRecordText;

    private GameManager gameManager;
    private AudioManager playerAudio;

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerAudio = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        gameManager.GameOverEvent += OnGameOver;
        gameManager.PlayerPointsEvent += OnPlayerPointsChanged;
        playerAudio.PlayRandomGameMusic();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnGameOver() // ����� ���� ��������
    {
        gameManager.SaveTheRecord();
        playerScoreText.gameObject.SetActive(false);
        preview.SetActive(true);
        playerRecordText.text = $"Your record: {AmountFormatting(gameManager.PlayerRecord)}";
    }

    private void OnPlayerPointsChanged(int amount) // ����� ����� ������ ��� ����� �� ��������
    {
        playerScoreText.text = $"Speed points: {AmountFormatting(amount)}";
    }

    private string AmountFormatting(int amount) => amount.ToString("#,#").Replace(',', ' '); // ������� �������������� ����� ������
}

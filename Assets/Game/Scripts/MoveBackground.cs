using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void Update()
    {
        transform.Translate(-Vector3.left * gameManager.gameSpeed * Time.deltaTime);
    }
}

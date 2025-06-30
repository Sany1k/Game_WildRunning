using UnityEngine;

public class GlobalMovement : MonoBehaviour
{
    private GameManager gameManager;
    private float zBound = -15f;

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * gameManager.gameSpeed * Time.deltaTime);

        if (transform.position.z < zBound)
        {
            gameObject.SetActive(false);
        }
    }
}

using UnityEngine;

public class Repeater : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth;

    private void Awake()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    private void Update()
    {
        if (transform.position.z < startPos.z - repeatWidth)
            transform.position = startPos;
    }
}

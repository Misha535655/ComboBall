using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float damping = 1.5f;
    public float yOffset = 1f;
    public bool faceLeft;
    private Transform player;
    private int lastY;

    void Start()
    {
        FindPlayer(faceLeft);
    }

    public void FindPlayer(bool playerFaceLeft)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastY = Mathf.RoundToInt(player.position.y);
        if (playerFaceLeft)
        {
            transform.position = new Vector3(transform.position.x, player.position.y + yOffset, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, player.position.y + yOffset, transform.position.z);
        }
    }

    void Update()
    {
        if (player)
        {
            int currentY = Mathf.RoundToInt(player.position.y);
            if (currentY > lastY) faceLeft = false;
            else if (currentY < lastY) faceLeft = true;
            lastY = Mathf.RoundToInt(player.position.y);

            Vector3 target;
            target = new Vector3(transform.position.x, player.position.y + yOffset, transform.position.z);

            Vector3 currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);
            transform.position = currentPosition;
        }
    }
}
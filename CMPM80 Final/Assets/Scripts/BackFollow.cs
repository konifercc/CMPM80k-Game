using UnityEngine;

public class BackFollow : MonoBehaviour
{
    public Transform player;
    public float parallaxEffect;
    private Vector3 initialPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 parallaxPos = new Vector2((initialPos.x + (player.position.x-initialPos.x) * parallaxEffect), player.position.y);
        transform.position = parallaxPos;
    }
}

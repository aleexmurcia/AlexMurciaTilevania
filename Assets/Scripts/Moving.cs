using UnityEngine;

public class Moving : MonoBehaviour
{
    public enum MoveDirection { Horizontal, Vertical }

    [Header("Movement")]
    [SerializeField] MoveDirection direction = MoveDirection.Horizontal;
    [SerializeField] float distance = 3f;
    [SerializeField] float speed = 2f;

    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float movement = Mathf.PingPong(Time.time * speed, distance);

        if (direction == MoveDirection.Horizontal)
        {
            transform.position = startPosition + Vector3.right * movement;
        }
        else
        {
            transform.position = startPosition + Vector3.up * movement;
        }
    }
}

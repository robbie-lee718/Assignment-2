using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveDistance = 3f;

    Vector3 startingPosition;
    public int facing = 0;

    int direction = -1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (facing == 0){
            transform.position += Vector3.right * direction * moveSpeed * Time.deltaTime;
            float distanceFromStart = Mathf.Abs(transform.position.x - startingPosition.x);
            if (distanceFromStart >= moveDistance){
                direction *= -1;
            }
        }
        else {
            transform.position += Vector3.up * direction * moveSpeed * Time.deltaTime;
            float distanceFromStart = Mathf.Abs(transform.position.y - startingPosition.y);
            if (distanceFromStart >= moveDistance){
                direction *= -1;
            }
        }
        
    }
}

using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public Transform posA, posB;
    public int speed;
    Vector2 targetPos;

    private void Start()
    {
        targetPos = posB.position;
    }

    private void Update()
    {
        if(Vector2.Distance(transform.position,posA.position) < .1f) targetPos = posB.position;
        if(Vector2.Distance(transform.position, posB.position) < .1f) targetPos = posA.position;

        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Player entered the platform trigger");
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player exit the platform trigger");
            collision.transform.SetParent(null);
        }
    }
}

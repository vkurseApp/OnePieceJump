using UnityEngine;

public class BrokenPlatform : MonoBehaviour

{

    private void OnCollisionEnter2D(Collision2D collider)
    {
        Rigidbody2D platformRB2d = collider.gameObject.GetComponent<Rigidbody2D>();

        if (platformRB2d.velocity.y <= 0)
        {
            Destroy(gameObject);
        }
    }

}

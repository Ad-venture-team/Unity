using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{
    public SpriteRenderer renderer;

    private int damage;
    private float speed;
    private Vector3 direction;
    private Vector3 origine;
    private float maxDistance;

    public void Init( float _speed, float _distance, int _damage)
    {
        speed = _speed;
        maxDistance = _distance;
        damage = _damage;
       // transform.LookAt(direction);
    }

    public void InitVector(Vector2 _direction, Vector2 _origine)
    {
        direction = _direction;
        transform.position = _origine;
        transform.up = direction - transform.position;
        origine = _origine;
    } 

    public void SetIcon(Sprite _icon)
    {
        renderer.sprite = _icon;
    }

    public void FixedUpdate()
    {
        //transform.position += (-origine + direction).normalized * speed * Time.deltaTime;
        transform.position += transform.up.normalized * speed * Time.deltaTime;

        float dist = Vector2.Distance(transform.position, origine);
        if (dist >= maxDistance)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("mob"))
            return;

        PlayerController player;
        if (collision.TryGetComponent(out player))
            player.TakeDamage(damage);

        Destroy(gameObject);
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

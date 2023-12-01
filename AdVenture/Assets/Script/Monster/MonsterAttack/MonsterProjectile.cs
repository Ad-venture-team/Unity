using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{
    public SpriteRenderer renderer;

    private float speed;
    private Vector3 direction;
    private Vector3 origine;
    private float maxDistance;

    public void Init( float _speed, float _distance)
    {
        speed = _speed;
        maxDistance = _distance;
       // transform.LookAt(direction);
    }

    public void InitVector(Vector2 _direction, Vector2 _origine)
    {
        direction = _direction;
        transform.position = _origine;
        origine = _origine;
    } 

    public void SetIcon(Sprite _icon)
    {
        renderer.sprite = _icon;
    }

    public void FixedUpdate()
    {
        transform.position += (-origine + direction).normalized * speed * Time.deltaTime;

        float dist = Vector2.Distance(transform.position, origine);
        if (dist >= maxDistance)
            Destroy(this.gameObject);
    }

    public void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}

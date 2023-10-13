using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{
    public SpriteRenderer renderer;

    private float speed;
    private Vector3 direction;
    private Vector3 origine;

    public void Init( float _speed)
    {
        speed = _speed;
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
    }

    public void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}

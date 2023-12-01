using UnityEngine;
using System.Linq;
public class attacks : MonoBehaviour {
    [SerializeField] GameObject bullet;
    public void attack() {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}

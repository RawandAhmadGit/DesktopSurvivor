using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class WorldText : MonoBehaviour {
    private Vector3 _speed = Vector3.zero;
    private Vector3 _accel = Vector3.zero;
    private float _age;

    public float MaximumHorizontalSpeed;
    public float LaunchSpeed;
    public float grav;
    public float lifetime;
    // Start is called before the first frame update
    void Start() {
        _speed.x = Random.Range(1, MaximumHorizontalSpeed);
        if ((int)(Random.value * 100) % 2 == 0) {
            _speed.x *= -1;
        }
        _accel.y = -grav;
        _speed.y = LaunchSpeed;
    }

    // Update is called once per frame
    void Update() {
        _speed += _accel * Time.deltaTime;
        transform.Translate(_speed * Time.deltaTime);
        _age += Time.deltaTime;
        if (_age > lifetime) {
            TextMesh tm = gameObject.gameObject.GetComponent<TextMesh>();
            Color tmc = tm.color;
            tmc.a -= Time.deltaTime;
            tm.color = tmc;
            if (tm.color.a <= 0) {
                Destroy(gameObject);
            }
        }
    }
}

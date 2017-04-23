using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject _player;

    private Vector3 _offset;

    // Use this for initialization
    void Start()
    {
        _player = GameObject.Find("Player");
        transform.SetPositionAndRotation(
            new Vector3(_player.transform.position.x, _player.transform.position.y, transform.position.z),
            new Quaternion()
        );
        _offset = transform.position - _player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = _player.transform.position + _offset;
    }
}
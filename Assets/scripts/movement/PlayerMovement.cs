using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speedFactor = 1f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var vector3 = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.Translate(vector3.normalized * Time.deltaTime * speedFactor);
    }
}
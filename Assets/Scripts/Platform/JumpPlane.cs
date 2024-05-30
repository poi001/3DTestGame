using UnityEngine;

public class JumpPlane : MonoBehaviour
{
    public float jumpPower;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb;

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision");
            rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }
}

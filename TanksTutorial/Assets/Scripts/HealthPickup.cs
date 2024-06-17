using UnityEngine;

// Jackson White's Addition - Health Pickup
public class HealthPickup : MonoBehaviour
{
    public float healthbonus = 15;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<TankHealth>().Heal(healthbonus);
            Debug.Log("Works!");
        }
        else
        {
            Debug.Log("Not work!");
        }
    }
}

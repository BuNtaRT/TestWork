using System.Collections;
using UnityEngine;

public class HeroInteract : MonoBehaviour
{
    public Spawn spawnSc;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy") 
        {
            Hero.MunisHP();
            Destroy(other.gameObject);
            spawnSc.minusEnemy(other.transform);
        }
    }
}

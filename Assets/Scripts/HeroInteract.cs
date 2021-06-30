using System.Collections;
using UnityEngine;

public class HeroInteract : MonoBehaviour
{
    public Spawn spawnSc;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")) 
        {
            Hero.MunisHP();
            other.gameObject.SetActive(false); 
            spawnSc.minusEnemy(other.transform);
        }
    }
}

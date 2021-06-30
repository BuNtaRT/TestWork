using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{


    float minTimeSpawn = 2f, maxTimeSpawn = 5.5f;

    public List<Transform> spawnEnemyZone;

    float callSpawnTimer = 2;

    int bonusCount = 0;
    int enemyCount = 0;
    int bonusMaxCount = 0;
    int enemyMaxCount = 0;


    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Scripts").GetComponent<TouchControll>().OnMinusBonus += munisBonus;
    }

    void Start()
    {
        BonusInteraction.sing.spawn = this;

        bonusMaxCount = ObjPool.Instance.GetMaxObjFromType(TypeObj.Bonus);
        enemyMaxCount = ObjPool.Instance.GetMaxObjFromType(TypeObj.Enemy);

        InvokeRepeating(nameof(MaybeSpawn), 1, callSpawnTimer);
    }


    void MaybeSpawn() 
    {
        Debug.Log("Bonus = "+ bonusCount + "    Enemy = " + enemyCount);
        callSpawnTimer = Random.Range(minTimeSpawn, maxTimeSpawn);
        if (bonusCount < bonusMaxCount)
        {
            bonusCount++;
            InGameUI.sing.ShowInfocurrencity(bonusCount);
            GameObject tempBonus = ObjPool.Instance.SpawnFromPool(TypeObj.Bonus, GetRandomPoint.Get());
            tempBonus.GetComponent<BoxCollider>().enabled = true;
            BonusInteraction.sing.AddBonus(tempBonus.transform);
        }
        if (enemyCount < enemyMaxCount)
        {
            enemyCount++;
            GameObject enemyTemp = spawnEnemy(spawnEnemyZone[Random.Range(0, spawnEnemyZone.Count)]);
            enemyTemp.GetComponent<EnemyAI>().speed = Random.Range(1.5f, 3f);
            enemyTemp.GetComponent<EnemyAI>().priority = enemyCount;
            BonusInteraction.sing.AddEnemy(enemyTemp.transform);
        }
    }

    GameObject spawnEnemy(Transform zone) 
    {
        float posX = Random.Range(zone.position.x - 3, zone.position.x + 3);

        float posZ = Random.Range(zone.position.z - 3, zone.position.z + 3);

        Vector3 tempPos = new Vector3(posX,0,posZ);

        return ObjPool.Instance.SpawnFromPool(TypeObj.Enemy, tempPos);
    }

    void munisBonus(Transform destroyedObj) 
    {
        Debug.Log("munisBonus " + bonusCount);
        InGameUI.sing.ShowInfocurrencity(bonusCount);
        bonusCount = Mathf.Clamp(bonusCount-1,0,bonusMaxCount);
        BonusInteraction.sing.RemoveBonus(destroyedObj);
    }

    public void munisBonusInteract()
    {
        Debug.Log("bonusInteract " + bonusCount);
        InGameUI.sing.ShowInfocurrencity(bonusCount);
        bonusCount = Mathf.Clamp(bonusCount - 1,0, bonusMaxCount);
    }

    public void minusEnemy(Transform destroyedObj) 
    {

        enemyCount = Mathf.Clamp(enemyCount - 1, 0, enemyMaxCount);
        BonusInteraction.sing.RemoveEnemy(destroyedObj);
    }

}

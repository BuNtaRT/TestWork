using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    int bonusMaxCount = 10;
    int enemyMaxCount = 25;
    float minTimeSpawn = 2f, maxTimeSpawn = 5.5f;

    public List<Transform> spawnEnemyZone;

    float callTimerBonus = 2;
    float callTimerEnemy = 2;

    int BonusCount = 0;
    int EnemyCount = 0;

    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Scripts").GetComponent<TouchControll>().OnMinusBonus += munisBonus;
    }

    void Start()
    {
        BonusInteraction.sing.spawn = this;

        InvokeRepeating("BonusSpawn", 1, callTimerBonus);
        InvokeRepeating("EnemySpawn", 1, callTimerEnemy);
    }


    void BonusSpawn() 
    {
        if (BonusCount < bonusMaxCount)
        {
            callTimerBonus = Random.Range(minTimeSpawn, maxTimeSpawn);
            BonusCount++;
            InGameUI.sing.ShowInfocurrencity(BonusCount);
            GameObject temp = Resources.Load<GameObject>("Prefab/Bonus");
            temp = Instantiate(temp, GetRandomPoint.Get(), new Quaternion());
            BonusInteraction.sing.AddBonus(temp.transform);
        }
    }
    void EnemySpawn()
    {
        if (EnemyCount < enemyMaxCount)
        {
            EnemyCount++;
            callTimerEnemy = Random.Range(minTimeSpawn + 2, maxTimeSpawn + 2);


            GameObject enemyTemp = spawnEnemy("Prefab/enemy",spawnEnemyZone[Random.Range(0,spawnEnemyZone.Count)]);
            enemyTemp.GetComponent<EnemyAI>().speed = Random.Range(1.5f,3f);
            enemyTemp.GetComponent<EnemyAI>().priority = EnemyCount;
            BonusInteraction.sing.AddEnemy(enemyTemp.transform);
        }
    }

    GameObject spawnEnemy(string path,Transform zone) 
    {


        GameObject temp = Resources.Load<GameObject>(path);

        float posX = Random.Range(zone.position.x-3, zone.position.x + 3);

        float posZ = Random.Range(zone.position.z - 3, zone.position.z + 3);

        Vector3 tempPos = new Vector3(posX,0,posZ);


        temp = Instantiate<GameObject>(temp,tempPos,new Quaternion());
        return temp;
    }

    void munisBonus(Transform destroyedObj) 
    {
        InGameUI.sing.ShowInfocurrencity(BonusCount);
        BonusCount = Mathf.Clamp(BonusCount - 1, 0, bonusMaxCount);
        BonusInteraction.sing.RemoveBonus(destroyedObj);
    }

    public void munisBonusInteract()
    {
        InGameUI.sing.ShowInfocurrencity(BonusCount);
        BonusCount = Mathf.Clamp(BonusCount - 1, 0, bonusMaxCount);
    }

    public void minusEnemy(Transform destroyedObj) 
    {

        EnemyCount = Mathf.Clamp(EnemyCount - 1, 0, enemyMaxCount);
        BonusInteraction.sing.RemoveEnemy(destroyedObj);
    }

}

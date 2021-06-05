using System;
using System.Collections.Generic;
using UnityEngine;

public class BonusInteraction : MonoBehaviour
{
    public Spawn spawn;
    public Transform player;
    

    public static BonusInteraction sing { get; private set; }
    void Awake() 
    {
        sing = this;
    }


    private void FixedUpdate()
    {
        if (BonusPos.Count != 0 && EnemyPos.Count != 0) 
        {
            for(int i = 0; i <= BonusPos.Count-1;i++) // берем каждый бонус 
            {
                Transform tempBonus = BonusPos[i];
                foreach (Transform tempEnemy in EnemyPos)   // и каждого противника 
                {
                    if ((tempEnemy.position - tempBonus.position).sqrMagnitude <= 0.5f) // если расстояние между ними достаточно маленькое 
                    {
                        i--;        // смещаем итерацию т.к элемент
                        RemoveBonus(tempBonus);             // удаляем 
                        spawn.munisBonusInteract();
                        Destroy(tempBonus.gameObject);
                        break;
                    }
                }
            }
        }

        // растояние игрока до разных обьектов
        Vector3 plyerPosition = player.position;
        float distBonus = 100;
        float distEnemy = 100;
        // если есть бонусы
        if (BonusPos.Count != 0)
            PlayerDistanceToSomth(BonusPos, plyerPosition, ref distBonus);
        else 
            distBonus = 0;
        // тоже самое и с врагами
        if(EnemyPos.Count != 0 )
            PlayerDistanceToSomth(EnemyPos, plyerPosition, ref distEnemy);
        else
            distBonus = 0;

        //output info
        OutputDistace(distEnemy, distBonus);
    }

    void OutputDistace(float enemy, float bonus) 
    {
        Tuple<string,string> tuple = new Tuple<string, string>(((int)enemy).ToString(), ((int)bonus).ToString());
        InGameUI.sing.UpdateDistance(tuple);
    }

    void PlayerDistanceToSomth(List<Transform> curList,Vector3 playerPos, ref float distance) 
    {
        // обьекты и расстояние до них 
        for (int i = 0; i <= curList.Count-1; i++)
        {
            float sqrMagn = (curList[i].position - playerPos).sqrMagnitude;
            // и они ближе чем предыдущий 
            if (sqrMagn <= distance)
            {
                distance = sqrMagn;
            }
        }
    }




    #region lists And Operation

    List<Transform> EnemyPos = new List<Transform>();
    List<Transform> BonusPos = new List<Transform>();
    public void AddBonus(Transform val) 
    { BonusPos.Add(val); }
    public void RemoveBonus(Transform val) 
    { BonusPos.Remove(val); }
    public void AddEnemy(Transform val)
    { EnemyPos.Add(val); }
    public void RemoveEnemy(Transform val)
    { EnemyPos.Remove(val); }

    #endregion
}

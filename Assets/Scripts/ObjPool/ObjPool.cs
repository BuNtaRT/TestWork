using System;
using System.Collections.Generic;
using UnityEngine;

public enum TypeObj
{
    Bonus,
    Enemy,
}


public class ObjPool : MonoBehaviour
{

    float callSpawnTimer = 2;
    float minTimeSpawn = 2f, maxTimeSpawn = 5.5f;

    #region singlton
    public static ObjPool Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    #region objData


    [Serializable]    
    public struct ObjectsInfo
    {

        public TypeObj Type;
        public GameObject Prefab;
        public int MaxObjects;
    }
    #endregion

    [SerializeField]
    private List<ObjectsInfo> objectsInfo;

    Dictionary<TypeObj, Queue<GameObject>> poolDictionary;


    private void Start()
    {
        poolDictionary = new Dictionary<TypeObj, Queue<GameObject>>();

        foreach (ObjectsInfo temp in objectsInfo) 
        {
            Queue<GameObject> tempQueue = new Queue<GameObject>();
            for (int i = 0; i < temp.MaxObjects; i++) 
            {
                GameObject obj = Instantiate(temp.Prefab);
                obj.SetActive(false);
                tempQueue.Enqueue(obj);
            }
            poolDictionary.Add(temp.Type,tempQueue);
        }

        InvokeRepeating(nameof(Spawn), 1, callSpawnTimer);
    }

    void Spawn() 
    {
        callSpawnTimer = UnityEngine.Random.Range(minTimeSpawn, maxTimeSpawn);
        
    }

    public GameObject SpawnFromPool(TypeObj type, Vector3 position) 
    {
        GameObject temp = poolDictionary[type].Dequeue();
        temp.SetActive(true);
        temp.transform.position = position;
        poolDictionary[type].Enqueue(temp);
        return temp;
    }

    public int GetMaxObjFromType(TypeObj type) 
    {
        int max_obj = 0;
        foreach (ObjectsInfo temp in objectsInfo) 
        {
            if (type == temp.Type)
                max_obj = temp.MaxObjects;

        }
        return max_obj;
    }


}

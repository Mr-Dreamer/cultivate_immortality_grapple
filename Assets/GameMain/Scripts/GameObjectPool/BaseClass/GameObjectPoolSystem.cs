// ---------------------------------------------------------------
// 文件名称：GameObjectPoolSystem.cs
// 创 建 者：赵志伟
// 创建时间：2023/08/18
// 功能描述：
// ---------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Grapple;

public class GameObjectPoolSystem : Singleton<GameObjectPoolSystem>
{
    [SerializeField, Header("预制体")] private List<GameObjectAssets> m_AssetList = new List<GameObjectAssets>();
    [SerializeField] private Transform m_PoolObjectParent;
    private Dictionary<string, Queue<GameObject>> m_DictPools = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        InitPool();
    }

    private void Start()
    {

    }

    private void InitPool()
    {
        if (m_AssetList.Count == 0) return;


        //首先遍历外面配置的资源
        for (int i = 0; i < m_AssetList.Count; i++)
        {
            //检查列表第一个元素的内容是否已经在池子里面了，没有的话就创建一个
            if (!m_DictPools.ContainsKey(m_AssetList[i].assetsName))
            {
                m_DictPools.Add(m_AssetList[i].assetsName, new Queue<GameObject>());

                if (m_AssetList[i].prefab.Length == 0) return;

                //创建完毕后，遍历这个对象的总数，比如总算5，那么就创建5个，然后存进字典
                for (int j = 0; j < m_AssetList[i].count; j++)
                {
                    GameObject temp_Gameobject = Instantiate(m_AssetList[i].prefab[Random.Range(0, m_AssetList[i].prefab.Length)]);
                    temp_Gameobject.transform.SetParent(m_PoolObjectParent);
                    temp_Gameobject.transform.position = Vector3.zero;
                    temp_Gameobject.transform.rotation = Quaternion.identity;
                    m_DictPools[m_AssetList[i].assetsName].Enqueue(temp_Gameobject);
                    temp_Gameobject.SetActive(false);
                }
            }
        }
    }

    public GameObject TakeGameObject(string objectName)
    {
        if (!m_DictPools.ContainsKey(objectName)) return null;
        GameObject dequeueObject = m_DictPools[objectName].Dequeue();
        m_DictPools[objectName].Enqueue(dequeueObject);
        dequeueObject.SetActive(true);
        return dequeueObject;
    }

    public void TakeGameobject(string objectName, Vector3 position, Quaternion rotation)
    {
        if (!m_DictPools.ContainsKey(objectName)) return;

        GameObject dequeueObject = m_DictPools[objectName].Dequeue();
        m_DictPools[objectName].Enqueue(dequeueObject);
        dequeueObject.SetActive(true);
        dequeueObject.transform.position = position;
        dequeueObject.transform.rotation = rotation;
        dequeueObject.GetComponent<IPool>().SpawnObject();
    }

    public void TakeGameobject(string objectName, Vector3 position, Quaternion rotation, Transform user)
    {
        if (!m_DictPools.ContainsKey(objectName)) return;

        GameObject dequeueObject = m_DictPools[objectName].Dequeue();
        m_DictPools[objectName].Enqueue(dequeueObject);
        dequeueObject.SetActive(true);
        dequeueObject.transform.position = position;
        dequeueObject.transform.rotation = rotation;
        dequeueObject.GetComponent<IPool>().SpawnObject(user);
    }

    public void RecyleGameObject(GameObject gameObject)
    {
        gameObject.transform.position = Vector3.zero;
        gameObject.transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);
    }

    [System.Serializable]
    private class GameObjectAssets
    {
        public string assetsName;
        public int count;
        public GameObject[] prefab;
    }
}
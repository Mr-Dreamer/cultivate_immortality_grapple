// ---------------------------------------------------------------
// 文件名称：Singleton.cs
// 创 建 者：赵志伟
// 创建时间：2023/07/02
// 功能描述：单例基类
// ---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grapple
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    T[] managers = Object.FindObjectsOfType(typeof(T)) as T[];//尝试获取场景中当前所有的此对象
                    if (managers.Length != 0)
                    {
                        if (managers.Length == 1)//如果只有一个，那么直接return，找到合适的了，下边不执行了
                        {
                            instance = managers[0];
                            instance.gameObject.name = typeof(T).Name;
                            return instance;
                        }
                        else//若存在多个，则全部删除，之后重新建立符合条件的单例
                        {
                            Debug.LogError("Class" + typeof(T).Name + "exists multiple times in volation of singleton pattern. destroying all copies");
                            foreach (var item in managers)
                            {
                                Destroy(item.gameObject);
                            }
                        }
                    }
                    var go = new GameObject(typeof(T).Name, typeof(T));//创建此对象的单例
                    instance = go.GetComponent<T>();
                    DontDestroyOnLoad(go);
                }
                return instance;
            }
            set { instance = value as T; }
        }
    }
}
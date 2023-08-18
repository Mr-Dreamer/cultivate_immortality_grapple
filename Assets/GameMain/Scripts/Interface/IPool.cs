// ---------------------------------------------------------------
// 文件名称：IPool.cs
// 创 建 者：赵志伟
// 创建时间：2023/08/17
// 功能描述：
// ---------------------------------------------------------------
using UnityEngine;
public interface IPool
{
    void SpawnObject();
    void SpawnObject(Transform user);
    void RecycleObject();
}

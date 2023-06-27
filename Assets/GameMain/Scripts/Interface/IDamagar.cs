// ---------------------------------------------------------------
// 文件名称：IDamagar.cs
// 创 建 者：
// 创建时间：2023/06/26
// 功能描述：攻击伤害接口
// ---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagar
{
    void TakeDamager(float damager);
    void TakeDamager(string hitAnimationName);
    void TakeDamager(float damager, string hitAnimationName);
    void TakeDamager(float damagar, string hitAnimationName, Transform attacker);
}

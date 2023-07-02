// ---------------------------------------------------------------
// 文件名称：CharacterWeaponAnimationEvent.cs
// 创 建 者：赵志伟
// 创建时间：2023/07/02
// 功能描述：切换武器动画事件
// ---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeaponAnimationEvent : MonoBehaviour
{
    [SerializeField] private Transform m_HipGS;
    [SerializeField] private Transform m_HandGS;
    [SerializeField] private Transform m_HandKatana;

    public void ShowGS()
    {
        if (!m_HandGS.gameObject.activeSelf)
        {
            m_HipGS.gameObject.SetActive(false);
            m_HandGS.gameObject.SetActive(true);
            m_HandKatana.gameObject.SetActive(false);
        }
    }

    public void HideGS()
    {
        if (m_HandGS.gameObject.activeSelf)
        {
            m_HipGS.gameObject.SetActive(true);
            m_HandGS.gameObject.SetActive(false);
            m_HandKatana.gameObject.SetActive(true);
        }
    }
}

// ---------------------------------------------------------------
// 文件名称：GameAssetsManager.cs
// 创 建 者：赵志伟
// 创建时间：2023/07/02
// 功能描述：
// ---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grapple
{
    public class GameAssetsManager : Singleton<GameAssetsManager>
    {
        [SerializeField, Header("资源")]
        private SoundAssetsSetting m_SoundAssetsSettings;

        private void Awake()
        {
            m_SoundAssetsSettings.InitAsstes();
        }

        public void PlaySound(AudioSource audioSource, SoundAssetsType soundAssetsType)
        {
            audioSource.clip = m_SoundAssetsSettings.GetAudioClipByType(soundAssetsType);
            audioSource.Play();
        }
    }
}
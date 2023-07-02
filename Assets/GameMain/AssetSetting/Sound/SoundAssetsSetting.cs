// ---------------------------------------------------------------
// 文件名称：SoundAsset.cs
// 创 建 者：赵志伟
// 创建时间：2023/07/02
// 功能描述：声音资源配置Asset文件
// ---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundAssetsType
{
    Hit,
    SwordWave,
    GSwordWave
}

[CreateAssetMenu(fileName = "SoundAssetsSetting", menuName = "CustomAsset/Sound")]
public class SoundAssetsSetting : ScriptableObject
{
    public List<SoundAssets> SoundAssetsList = new List<SoundAssets>();
    private Dictionary<string, AudioClip[]> m_SoundAssetsDic = new Dictionary<string, AudioClip[]>();

    public void InitAsstes()
    {
        for (int i = 0; i < SoundAssetsList.Count; i++)
        {
            if (!m_SoundAssetsDic.ContainsKey(SoundAssetsList[i].AssetsName))
            {
                m_SoundAssetsDic.Add(SoundAssetsList[i].AssetsName, SoundAssetsList[i].AudioClipArr);
            }
        }
    }

    public AudioClip GetAudioClipByType(SoundAssetsType soundAssetsType)
    {
        switch (soundAssetsType)
        {
            case SoundAssetsType.Hit:
                    return m_SoundAssetsDic["Hit"][Random.Range(0, m_SoundAssetsDic["Hit"].Length)];
            case SoundAssetsType.SwordWave:
                return m_SoundAssetsDic["SwordWave"][Random.Range(0, m_SoundAssetsDic["SwordWave"].Length)];
            case SoundAssetsType.GSwordWave:
                return m_SoundAssetsDic["GSwordWave"][Random.Range(0, m_SoundAssetsDic["GSwordWave"].Length)];
            default:
                return null;
        }
    }
}

[System.Serializable]
public class SoundAssets
{
    public string AssetsName;
    public AudioClip[] AudioClipArr;
}

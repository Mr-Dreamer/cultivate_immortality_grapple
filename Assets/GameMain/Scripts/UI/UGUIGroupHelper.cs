// ---------------------------------------------------------------
// �ļ����ƣ�UGUIGroupHelper.cs
// �� �� �ߣ���־ΰ
// ����ʱ�䣺2022/11/30
// ����������UGUI�����鸨����
// ---------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

public class UGUIGroupHelper : UIGroupHelperBase
{
    public const int DepthFactor = 1000;

    private int m_Depth = 0;
    private Canvas m_CachedCanvas = null;

    /// <summary>
    /// ���ý�������ȡ�
    /// </summary>
    /// <param name="depth">��������ȡ�</param>
    public override void SetDepth(int depth)
    {
        m_Depth = depth;
        m_CachedCanvas.overrideSorting = true;
        m_CachedCanvas.sortingOrder = DepthFactor * depth;
    }

    private void Awake()
    {
        m_CachedCanvas = gameObject.GetOrAddComponent<Canvas>();
        gameObject.GetOrAddComponent<GraphicRaycaster>();
    }

    private void Start()
    {
        m_CachedCanvas.overrideSorting = true;
        m_CachedCanvas.sortingOrder = DepthFactor * m_Depth;

        RectTransform transform = GetComponent<RectTransform>();
        transform.anchorMin = Vector2.zero;
        transform.anchorMax = Vector2.one;
        transform.anchoredPosition = Vector2.zero;
        transform.sizeDelta = Vector2.zero;
    }
}

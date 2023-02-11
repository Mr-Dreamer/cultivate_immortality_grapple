using System.IO;

/// <summary>
/// �����ű�ʱ�Զ����ͷ��ע��
/// </summary>
public class AddFileHeadComment : UnityEditor.AssetModificationProcessor
{
    /// <summary>
    /// �˺�����asset�������꣬�ļ��Ѿ����ɵ������ϣ�����û������.meta�ļ���import֮ǰ������
    /// </summary>
    /// <param name="path">���ɴ����ļ���path����.meta��ɵ�</param>
    public static void OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta", "");
        if (!path.EndsWith(".cs")) return;
        string allText = "// ---------------------------------------------------------------\r\n"
                         + "// �ļ����ƣ�#SCRIPTFULLNAME#\r\n"
                         + "// �� �� �ߣ�\r\n"
                         + "// ����ʱ�䣺#CreateTime#\r\n"
                         + "// ����������\r\n"
                         + "// ---------------------------------------------------------------\r\n";
        allText += File.ReadAllText(path);
        allText = allText.Replace("#SCRIPTFULLNAME#", Path.GetFileName(path));
        allText = allText.Replace("#CreateTime#", System.DateTime.Now.ToString("yyyy/MM/dd"));
        File.WriteAllText(path, allText);
    }
}
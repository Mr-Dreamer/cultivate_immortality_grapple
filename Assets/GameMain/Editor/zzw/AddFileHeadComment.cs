using System.IO;

/// <summary>
/// 创建脚本时自动添加头部注释
/// </summary>
public class AddFileHeadComment : UnityEditor.AssetModificationProcessor
{
    /// <summary>
    /// 此函数在asset被创建完，文件已经生成到磁盘上，但是没有生成.meta文件和import之前被调用
    /// </summary>
    /// <param name="path">是由创建文件的path加上.meta组成的</param>
    public static void OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta", "");
        if (!path.EndsWith(".cs")) return;
        string allText = "// ---------------------------------------------------------------\r\n"
                         + "// 文件名称：#SCRIPTFULLNAME#\r\n"
                         + "// 创 建 者：\r\n"
                         + "// 创建时间：#CreateTime#\r\n"
                         + "// 功能描述：\r\n"
                         + "// ---------------------------------------------------------------\r\n";
        allText += File.ReadAllText(path);
        allText = allText.Replace("#SCRIPTFULLNAME#", Path.GetFileName(path));
        allText = allText.Replace("#CreateTime#", System.DateTime.Now.ToString("yyyy/MM/dd"));
        File.WriteAllText(path, allText);
    }
}
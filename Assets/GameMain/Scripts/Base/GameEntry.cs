// ---------------------------------------------------------------
// 文件名称：GameEntry.cs
// 创 建 者：赵志伟
// 创建时间：2022/12/1
// 功能描述：游戏入口-各种组件(所有)的初始化
// ---------------------------------------------------------------
using UnityEngine;

namespace Grapple
{
    /// <summary>
    /// 游戏入口
    /// </summary>
    public partial class GameEntry : MonoBehaviour
    {
        private void Start()
        {
            //初始化内置组件
            InitBuiltinComponents();

            //初始化自定义组件
            InitCustomComponents();

            //初始化自定义调试器
            InitCustomDebuggers();
        }
    }
}
using System.Windows.Forms;

namespace WinFormsControlLibrary
{
    /// <summary>
    /// 透明パネル
    /// </summary>
    internal class TransparentPanel : Panel
    {
        /// <summary>
        /// パラメタ生成
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                // 透明追加
                var cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
                return cp;
            }
        }

        /// <summary>
        /// 背景描画
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // 描画しない
            //base.OnPaintBackground(e);
        }
    }
}

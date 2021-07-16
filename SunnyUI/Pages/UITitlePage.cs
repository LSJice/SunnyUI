﻿/******************************************************************************
 * SunnyUI 开源控件库、工具类库、扩展类库、多页面开发框架。
 * CopyRight (C) 2012-2021 ShenYongHua(沈永华).
 * QQ群：56829229 QQ：17612584 EMail：SunnyUI@QQ.Com
 *
 * Blog:   https://www.cnblogs.com/yhuse
 * Gitee:  https://gitee.com/yhuse/SunnyUI
 * GitHub: https://github.com/yhuse/SunnyUI
 *
 * SunnyUI.dll can be used for free under the GPL-3.0 license.
 * If you use this code, please keep this note.
 * 如果您使用此代码，请保留此说明。
 ******************************************************************************
 * 文件名称: UITitlePage.cs
 * 文件说明: 标题栏页面
 * 当前版本: V3.0
 * 创建日期: 2020-01-01
 *
 * 2020-01-01: V2.2.0 增加文件说明
******************************************************************************/

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Sunny.UI
{
    public partial class UITitlePage : UIPage, ISymbol
    {
        public UITitlePage()
        {
            InitializeComponent();
            base.BackColor = UIColor.LightBlue;
            TopLevel = false;
            if (this.Register()) SetStyle(UIStyles.Style);

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            UpdateStyles();

            if (!IsDesignMode) base.Dock = DockStyle.Fill;

            Version = UIGlobal.Version;
        }

        public new string Version
        {
            get;
        }

        private string text;

        public override string Text
        {
            get => text;
            set
            {
                text = value;
                if (PageTitle != null)
                {
                    PageTitle.Text = value;
                }
            }
        }

        private bool showTitle = true;

        [Description("显示标题"), Category("SunnyUI")]
        [DefaultValue(true)]
        public new bool ShowTitle
        {
            get => showTitle;
            set => showTitle = value;
        }

        protected override void SymbolChange()
        {
            base.SymbolChange();
            int left = Symbol > 0 ? (6 * 2 + SymbolSize) : 6;
            PageTitle.Padding = new Padding(left, 0, 0, 0);
            PageTitle.Symbol = Symbol;
            PageTitle.SymbolSize = SymbolSize;
            PageTitle.Invalidate();
        }

        /// <summary>
        /// 填充颜色，当值为背景色或透明色或空值则不填充
        /// </summary>
        [Description("标题颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "76, 76, 76")]
        public new Color TitleFillColor
        {
            get => PageTitle.FillColor;
            set
            {
                if (PageTitle != null)
                    PageTitle.FillColor = value;
            }
        }

        /// <summary>
        /// 字体颜色
        /// </summary>
        [Description("字体颜色"), Category("SunnyUI")]
        [DefaultValue(typeof(Color), "White")]
        public new Color TitleForeColor
        {
            get => PageTitle.ForeColor;
            set
            {
                if (PageTitle != null)
                    PageTitle.ForeColor = value;
            }
        }

        [ToolboxItem(true)]
        private class UITitle : UIControl
        {
            public UITitle()
            {
                SetStyleFlags(true, false);
                fillColor = Color.FromArgb(76, 76, 76);
                foreColor = Color.White;
            }

            /// <summary>
            /// 填充颜色，当值为背景色或透明色或空值则不填充
            /// </summary>
            [Description("填充颜色"), Category("SunnyUI")]
            [DefaultValue(typeof(Color), "76, 76, 76")]
            public Color FillColor
            {
                get => fillColor;
                set => SetFillColor(value);
            }

            /// <summary>
            /// 字体颜色
            /// </summary>
            [Description("字体颜色"), Category("SunnyUI")]
            [DefaultValue(typeof(Color), "White")]
            public override Color ForeColor
            {
                get => foreColor;
                set => SetForeColor(value);
            }

            public override void SetStyleColor(UIBaseStyle uiColor)
            {
                base.SetStyleColor(uiColor);
                FillColor = uiColor.PageTitleFillColor;
                ForeColor = uiColor.PageTitleForeColor;
            }

            private int symbol;
            public int Symbol
            {
                get => symbol;
                set
                {
                    symbol = value;
                    Invalidate();
                }
            }

            private int symbolSize = 24;
            public int SymbolSize
            {
                get => symbolSize;
                set
                {
                    symbolSize = value;
                    Invalidate();
                }
            }

            private Point symbolOffset = new Point(0, 0);

            [DefaultValue(typeof(Point), "0, 0")]
            [Description("字体图标的偏移位置"), Category("SunnyUI")]
            public Point SymbolOffset
            {
                get => symbolOffset;
                set
                {
                    symbolOffset = value;
                    Invalidate();
                }
            }

            protected override void OnPaintFore(Graphics g, GraphicsPath path)
            {
                base.OnPaintFore(g, path);
                if (Symbol > 0)
                {
                    g.DrawFontImage(Symbol, SymbolSize, ForeColor, new Rectangle(6, 0, SymbolSize, Height), SymbolOffset.X, SymbolOffset.Y);
                }
            }
        }

        private void UITitlePage_Shown(object sender, System.EventArgs e)
        {
            if (!IsDesignMode)
            {
                PageTitle.Visible = showTitle;
            }
        }
    }
}
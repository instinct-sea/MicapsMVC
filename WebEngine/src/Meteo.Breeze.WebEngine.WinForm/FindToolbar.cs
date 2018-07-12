//==================================================================== 
//**   MICAPS MVC框架混合开发项目-[Meteo.Breeze.WebEngine]
//====================================================================
//**   Copyright © 中国气象局 2018 -- Support 北京绘云天科技有限公司
//====================================================================
// 文件名称：FindToolbar.cs
// 项目名称：Meteo.Breeze.WebEngine.WinForm
// 创建时间：2018-07-10 09:52:33
// 创建人员：宋杰军
// 负 责 人：北京绘云天科技有限公司
// 参与人员：Breeze 开发组
// ===================================================================
// 修改日期：
// 修改人员：
// 修改内容：
// ===================================================================
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Meteo.Breeze.WebEngine.WinForm
{
	/// <summary>
	/// 浏览器框架内容查找工具栏
	/// </summary>
	public class FindToolbar : Control
	{
		/// <summary>
		/// 浏览器框架内容查找工具栏
		/// </summary>
		/// <param name="window">浏览器窗体对象</param>
		internal FindToolbar(WebWindow window)
		{
			_currentWindow = window;
			Initialize();
		}

		#region 初始化

		/// <summary>
		/// 工具栏初始化
		/// </summary>
		private void Initialize()
		{
			InitializeByControl();

			Font = new Font("Microsoft Sans Serif", 10);

			SetStyle(ControlStyles.FixedWidth
				| ControlStyles.FixedHeight
				| ControlStyles.SupportsTransparentBackColor
				| ControlStyles.EnableNotifyMessage
				| ControlStyles.DoubleBuffer
				| ControlStyles.OptimizedDoubleBuffer
				| ControlStyles.UseTextForAccessibility
				| ControlStyles.Selectable
				, false);

			SetStyle(ControlStyles.UserPaint
				| ControlStyles.AllPaintingInWmPaint
				| ControlStyles.Opaque
				| ControlStyles.ResizeRedraw
				| ControlStyles.StandardClick
				| ControlStyles.StandardDoubleClick
				| ControlStyles.UserMouse
				| ControlStyles.CacheText
				| ControlStyles.ContainerControl
				, true);

			Visible = false;
			Height = _txtContent.Height + 12;
			BackColor = Color.WhiteSmoke;

			SendMessage(_txtContent.Handle, 0x1501, 1, "在页面中查找");

			_currentWindow.BrowserWindow.FindHandler += (s, e) =>
			{
				if (e.FinalUpdate && e.Identifier == _lastFindId)
				{
					var count = e.Count;
					BeginInvoke(() => UpdateMatchInfo(count));
				}
			};
		}
		/// <summary>
		/// 基本控件初始化
		/// </summary>
		private void InitializeByControl()
		{
			_btnNext.Parent = this;
			_btnPrev.Parent = this;
			_btnMatchCase.Parent = this;
			_btnClose.Parent = this;
			_txtContent.Parent = this;
			_lblResult.Parent = this;


			_txtContent.Font = new Font(Font, FontStyle.Italic);
			_txtContent.Left = 10;
			_txtContent.Top = 8;
			_txtContent.Width = 200;
			_txtContent.ForeColor = Color.DimGray;
			_txtContent.TextChanged += new EventHandler(txtContent_TextChanged);
			_txtContent.KeyDown += new KeyEventHandler(txtContent_KeyDown);


			_btnPrev.Font = new Font(Font.FontFamily, 9);
			_btnPrev.SetBounds(_txtContent.Left + _txtContent.Width - 1,
				_txtContent.Top,
				_txtContent.Height,
				_txtContent.Height);
			SetButtonStyle(_btnPrev);
			_btnPrev.Image = ImageProvider.ArrowUp.Create();
			_btnPrev.TextAlign = ContentAlignment.MiddleCenter;
			_btnPrev.Click += (s, e) =>
			{
				Find(false);
			};
			_btnPrev.GotFocus += (s, e) =>
			{
				_txtContent.Focus();
			};
			ConfigToolTip("转到上一个匹配项", _btnPrev);


			_btnNext.Font = new Font(Font.FontFamily, 9);
			_btnNext.SetBounds(_btnPrev.Left + _btnPrev.Width - 1, _txtContent.Top, _txtContent.Height, _txtContent.Height);
			SetButtonStyle(_btnNext);
			_btnNext.Image = ImageProvider.ArrowDown.Create();
			_btnNext.TextAlign = ContentAlignment.MiddleCenter;
			_btnNext.Click += (s, e) =>
			{
				Find(true);
			};
			_btnNext.GotFocus += (s, e) =>
			{
				_txtContent.Focus();
			};
			ConfigToolTip("转到下一个匹配项", _btnNext);


			_btnMatchCase.Text = "区分大小写";
			_btnMatchCase.ForeColor = Color.DimGray;
			_btnMatchCase.Left = _btnNext.Left + _btnNext.Width + 10;
			_btnMatchCase.Top = _btnNext.Top;
			_btnMatchCase.Width = 110;
			_btnMatchCase.Height = _btnNext.Height;
			_btnMatchCase.FlatStyle = FlatStyle.Flat;
			_btnMatchCase.FlatAppearance.BorderSize = 0;
			_btnMatchCase.BackColor = Color.Transparent;
			_btnMatchCase.TextAlign = ContentAlignment.MiddleCenter;
			_btnMatchCase.Click += (s, e) =>
			{
				ChangeMatchCase();
			};
			_btnMatchCase.GotFocus += (s, e) =>
			{
				_txtContent.Focus();
			};


			_lblResult.ForeColor = Color.DimGray;
			_lblResult.Left = _btnMatchCase.Left + _btnMatchCase.Width + 10;
			_lblResult.Top = _btnMatchCase.Top + 3;
			_lblResult.AutoSize = true;


			_btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			_btnClose.Height = _btnPrev.Height;
			_btnClose.Width = _btnClose.Height;
			_btnClose.Left = Width - _btnClose.Width - 10;
			_btnClose.Top = _btnPrev.Top;
			_btnClose.BackColor = Color.Transparent;
			_btnClose.Image = ImageProvider.Cross.Create();
			_btnClose.FlatStyle = FlatStyle.Flat;
			_btnClose.FlatAppearance.BorderSize = 0;
			_btnClose.Click += (s, e) =>
			{
				Visible = false;
			};
			_btnClose.GotFocus += (s, e) =>
			{
				_txtContent.Focus();
			};
			ConfigToolTip("关闭浏览器框架内容查找工具栏。", _btnClose);
		}

		#endregion

		#region 成员窗体事件

		/// <summary>
		/// 窗体重绘事件
		/// </summary>
		/// <param name="e">传递事件</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.FillRectangle(Brushes.WhiteSmoke, e.ClipRectangle);
			e.Graphics.DrawLine(Pens.LightGray, 0, 0, Width, 0);
		}
		/// <summary>
		/// 是否显示浏览器框架内容查找工具栏
		/// </summary>
		public new bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				if (base.Visible == value) return;
				base.Visible = value;
				if (value)
				{
					Parent = _currentWindow;
				}
				else
				{
					_txtContent.Text = null;
					Parent = null;
				}
				_currentWindow.ResizeWebWindow();
			}
		}
		/// <summary>
		/// 焦点获得时
		/// </summary>
		/// <param name="e">传递事件</param>
		protected override void OnGotFocus(EventArgs e)
		{
			_txtContent.Focus();
		}

		#endregion

		#region 成员功能事件

		/// <summary>
		/// 内容查找文本框变更事件
		/// </summary>
		/// <param name="sender">传递对象</param>
		/// <param name="e">传递事件</param>
		private void txtContent_TextChanged(object sender, EventArgs e)
		{
			if (_txtContent.Text.Length == 0)
			{
				if (_txtContent.Font.Style != FontStyle.Italic)
				{
					_txtContent.Font = new Font(_txtContent.Font, FontStyle.Italic);
				}
				_currentWindow.BrowserWindow.StopFinding(true);
				UpdateMatchInfo(0);
			}
			else
			{
				if (_txtContent.Font.Style == FontStyle.Italic)
				{
					_txtContent.Font = new Font(_txtContent.Font, FontStyle.Regular);
				}
				if (!_autoSearchSuspended) Find(true);
			}
		}
		/// <summary>
		/// 内容查找文本框键盘操作事件
		/// </summary>
		/// <param name="sender">传递对象</param>
		/// <param name="e">传递事件</param>
		private void txtContent_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				Find(e.Shift ? false : true);
				e.Handled = true;
			}
		}

		#endregion

		#region 成员变量

		/// <summary>
		/// 当前的浏览器窗体对象
		/// </summary>
		private WebWindow _currentWindow;
		/// <summary>
		/// 下一个按钮
		/// </summary>
		private Button _btnNext = new Button();
		/// <summary>
		/// 上一个按钮
		/// </summary>
		private Button _btnPrev = new Button();
		/// <summary>
		/// 是否区分大小写按钮
		/// </summary>
		private Button _btnMatchCase = new Button();
		/// <summary>
		/// 工具栏关闭按钮
		/// </summary>
		private Button _btnClose = new Button();
		/// <summary>
		/// 内容查找文本框
		/// </summary>
		private TextBox _txtContent = new TextBox();
		/// <summary>
		/// 查找结果显示标签
		/// </summary>
		private Label _lblResult = new Label();
		/// <summary>
		/// 工具栏提示信息对象
		/// </summary>
		private ToolTip _tipFindbar = new ToolTip();
		/// <summary>
		/// 末次查找到的 ID
		/// </summary>
		private int _lastFindId;
		/// <summary>
		/// 是否区分大小写
		/// </summary>
		private bool _matchCase;
		/// <summary>
		/// 是否暂停自动搜索
		/// </summary>
		private bool _autoSearchSuspended;

		#endregion

		#region 成员属性

		/// <summary>
		/// 是否显示关闭按钮
		/// </summary>
		public bool CloseButtonVisible
		{
			get
			{
				return _btnClose.Visible;
			}
			set
			{
				_btnClose.Visible = value;
			}
		}
		/// <summary>
		/// 是否区分大小写
		/// </summary>
		public bool MatchCase
		{
			get
			{
				return _matchCase;
			}
			set
			{
				if (_matchCase != value) ChangeMatchCase();
			}
		}
		/// <summary>
		/// 文本框中的查找内容
		/// </summary>
		public string FindText
		{
			get
			{
				return _txtContent.Text;
			}
			set
			{
				_lastFindId = _currentWindow.Find("", false, false);
				_autoSearchSuspended = true;
				_txtContent.Text = value;
				_autoSearchSuspended = false;
			}
		}
		/// <summary>
		/// 查找文本框中选择的文本的起始点
		/// </summary>
		public int FindTextSelectionStart
		{
			get
			{
				return _txtContent.SelectionStart;
			}
			set
			{
				_txtContent.SelectionStart = value;
			}
		}
		/// <summary>
		/// 查找文本框中选择的字符数
		/// </summary>
		public int FindTextSelectionLength
		{
			get
			{
				return _txtContent.SelectionLength;
			}
			set
			{
				_txtContent.SelectionLength = value;
			}
		}

		#endregion

		#region 成员私有方法

		/// <summary>
		/// 开始调用
		/// </summary>
		/// <param name="method">需要调用的方法</param>
		private void BeginInvoke(MethodInvoker method)
		{
			_currentWindow.BeginInvoke(method);
		}
		/// <summary>
		/// 内容查找
		/// </summary>
		/// <param name="forward">是否向前查找</param>
		private void Find(bool forward)
		{
			_lastFindId = _currentWindow.Find(_txtContent.Text, forward, _matchCase);
		}
		/// <summary>
		/// 设置工具栏按钮提示信息
		/// </summary>
		/// <param name="text">提示信息</param>
		/// <param name="btn">工具栏按钮</param>
		private void ConfigToolTip(string text, Button btn)
		{
			btn.MouseHover += (s, e) =>
			{
				var pos = btn.PointToClient(MousePosition);
				pos.X += 12;
				pos.Y += 14;
				_tipFindbar.Show(text, btn, pos);
			};
			btn.MouseLeave += (s, e) =>
			{
				_tipFindbar.Hide(btn);
			};
		}
		/// <summary>
		/// 更新匹配到的数量信息
		/// </summary>
		/// <param name="count">匹配数量</param>
		private void UpdateMatchInfo(int count)
		{
			if (0 == count && _txtContent.Text.Length > 0)
			{
				_txtContent.BackColor = Color.LightCoral;
				_txtContent.ForeColor = Color.White;
				_lblResult.Text = "对不起！未能找到您要查找的内容。";
			}
			else
			{
				_txtContent.BackColor = DefaultBackColor;
				_txtContent.ForeColor = Color.DimGray;
				if (_txtContent.Text.Length > 0)
				{
					_lblResult.Text = $"共找到 {count} 个匹配项。";
				}
				else
				{
					_lblResult.Text = String.Empty;
				}
			}
		}
		/// <summary>
		/// 区分大小写状态变更事件
		/// </summary>
		private void ChangeMatchCase()
		{
			_matchCase = !_matchCase;
			if (_matchCase)
			{
				_btnMatchCase.BackColor = Color.LightGray;
				_btnMatchCase.FlatAppearance.BorderSize = 1;
			}
			else
			{
				_btnMatchCase.BackColor = Color.Transparent;
				_btnMatchCase.FlatAppearance.BorderSize = 0;
			}
			Find(true);
			Find(false);
		}
		/// <summary>
		/// 设置按钮样式
		/// </summary>
		/// <param name="btn">工具栏按钮</param>
		private void SetButtonStyle(Button btn)
		{
			btn.FlatStyle = FlatStyle.Flat;
			btn.BackColor = _txtContent.BackColor;
			btn.ForeColor = _txtContent.ForeColor;
			btn.FlatAppearance.BorderColor = Color.Gray;
			btn.FlatAppearance.MouseOverBackColor = btn.BackColor;
		}
		/// <summary>
		/// 将指定的消息发送到一个或多个窗口
		/// </summary>
		/// <param name="hWnd">接收消息的窗口的句柄号</param>
		/// <param name="Msg">被发送的消息</param>
		/// <param name="wParam">附加的消息特定信息</param>
		/// <param name="lParam">附加的消息特定信息</param>
		/// <returns>消息处理的结果</returns>
		[DllImport("user32.dll")]
		private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

		#endregion

		#region 成员公共方法

		/// <summary>
		/// 设置查找文本框中的文本并执行搜索
		/// </summary>
		/// <param name="text">需要查找的内容</param>
		public void Search(string text)
		{
			Visible = true;
			_txtContent.Text = text;
		}
		/// <summary>
		/// 设置查找文本框中的文本并执行搜索
		/// </summary>
		/// <param name="text">需要查找的内容</param>
		[Obsolete("本方法已过时！不推荐使用 SetSearchText 方法，请改用 Search 方法。")]
		public void SetSearchText(string text)
		{
			Search(text);
		}
		/// <summary>
		/// 将输入焦点设置为查找文件框
		/// </summary>
		/// <returns>焦点设置结果</returns>
		public bool FocusFindText()
		{
			return _txtContent.Focus();
		}

		#endregion

	}
}

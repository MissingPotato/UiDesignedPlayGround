using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UiDesignedPlayGround
{
	public partial class Form1 : Form
	{

		#region Rounding

		[System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
		private static extern IntPtr CreateRoundRectRgn
		  (
		   int nLeftRect, // x-coordinate of upper-left corner
		   int nTopRect, // y-coordinate of upper-left corner
		   int nRightRect, // x-coordinate of lower-right corner
		   int nBottomRect, // y-coordinate of lower-right corner
		   int nWidthEllipse, // height of ellipse
		   int nHeightEllipse // width of ellipse
		  );

		[System.Runtime.InteropServices.DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
		private static extern bool DeleteObject(System.IntPtr hObject);

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			System.IntPtr ptr = CreateRoundRectRgn(0, 0, this.Width, this.Height, 20, 20);
			this.Region = System.Drawing.Region.FromHrgn(ptr);
			DeleteObject(ptr);
		}

		private void button_Close_Paint(object sender, PaintEventArgs e)
		{
			IntPtr ptr = CreateRoundRectRgn(0, 0, button_Close.Width, button_Close.Height, 10, 10);
			button_Close.Region = Region.FromHrgn(ptr);
			DeleteObject(ptr);
		}

		private void button_Minimize_Paint(object sender, PaintEventArgs e)
		{
			IntPtr ptr = CreateRoundRectRgn(0, 0, button_Minimize.Width, button_Minimize.Height, 10, 10);
			button_Minimize.Region = Region.FromHrgn(ptr);
			DeleteObject(ptr);
		}

		#endregion

		public List<CheckBox> checkBoxList = new List<CheckBox>();
		public Point lastPoint;

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			checkBoxList.Add(checkBox_Home);
			checkBoxList.Add(checkBox_ImagePlay);
		}

		#region WindowDragging
		private void panel2_Paint(object sender, PaintEventArgs e)
		{

		}
		private void panel2_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.Left += e.X - lastPoint.X;
				this.Top += e.Y - lastPoint.Y;
			}
		}
		private void panel2_MouseDown(object sender, MouseEventArgs e)
		{
			lastPoint = new Point(e.X, e.Y);
		}
		#endregion

		#region Controller Buttons
		private void button_Close_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
		private void button_Minimize_Click(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
		}
		#endregion

		#region CheckBoxes

		private void checkBox_Home_CheckedChanged(object sender, EventArgs e)
		{
			ResetCheckBoxes((CheckBox)sender);
		}
		private void checkBox_ImagePlay_CheckedChanged(object sender, EventArgs e)
		{
			ResetCheckBoxes((CheckBox)sender);
		}
		
		#endregion
		
		#region DynamicScripts

		public void ResetCheckBoxes(CheckBox sender)
		{
			foreach (CheckBox box in checkBoxList)
			{
				if (!box.Equals (sender) )
					box.Checked = false;
				else
					box.CheckState = box.CheckState;

				

			}
		}


		#endregion

		
	}
}

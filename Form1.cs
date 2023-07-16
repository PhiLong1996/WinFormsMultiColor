using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WinFormsMultiColor
{
    public partial class Form1 : Form
    {

        private Button CurrentButton;
        private Random random;
        private int tempIndex;
        private Form activateForm;
        public Form1()
        {
            InitializeComponent();
            random = new Random();
            btnCloseChildForm.Visible = false;
            // For hide minimu, maximum, close bar of form
            this.Text = string.Empty;
            this.ControlBox = false;

            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

        }

        // Function to drag form when we hide control box (minimu, maximum, close bar)
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private Color SelectThemeColor()
        {
            int index = random.Next(ThemeColor.ColorList.Count);
            while (tempIndex == index)
            {
                index = random.Next(ThemeColor.ColorList.Count);
            }
            tempIndex = index;
            string color = ThemeColor.ColorList[tempIndex];
            return ColorTranslator.FromHtml(color);
        }

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (CurrentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = SelectThemeColor();
                    CurrentButton = (Button)btnSender;
                    CurrentButton.BackColor = color;
                    CurrentButton.ForeColor = Color.White;
                    CurrentButton.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                    panelTitleBar.BackColor = color;
                    panelLogo.BackColor = ThemeColor.ChangeColorBrightNess(color, -0.3);
                    ThemeColor.primaryColor = color;
                    ThemeColor.secondaryColor = ThemeColor.ChangeColorBrightNess(color, -0.3);
                }
            }
        }

        private void DisableButton()
        {
            foreach (Control previousBtn in panelMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(51, 51, 76);
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                }
            }
        }

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activateForm != null)
            {
                activateForm.Close();
            }
            ActivateButton(btnSender);
            activateForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktopPanel.Controls.Add(childForm);
            this.panelDesktopPanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

            lbTitleBar.Text = childForm.Text;
            btnCloseChildForm.Visible = true;

        }

        private void BtnProducts_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ProductForm(), sender);
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            OpenChildForm(new OrderForms(), sender);
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            OpenChildForm(new CustomerForm(), sender);
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ReportingForm(), sender);
        }

        private void btnNotifications_Click(object sender, EventArgs e)
        {
            OpenChildForm(new NotificationForm(), sender);
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            OpenChildForm(new SettingForm(), sender);
        }

        private void btnCloseChildForm_Click(object sender, EventArgs e)
        {
            if (activateForm != null)
            {
                activateForm.Close();
            }
            Reset();
        }
        private void Reset()
        {
            DisableButton();
            lbTitleBar.Text = "HOME";
            panelTitleBar.BackColor = Color.FromArgb(0, 150, 136);
            panelLogo.BackColor = Color.FromArgb(39, 39, 58);
            CurrentButton = null;
            btnCloseChildForm.Visible = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if(WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
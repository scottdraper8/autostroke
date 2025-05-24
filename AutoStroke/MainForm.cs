using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;

namespace AutoStroke
{
    public partial class MainForm : Form
    {
        private NotifyIcon notifyIcon = null!;
        private bool isRunning = false;
        private System.Windows.Forms.Timer keyPressTimer = null!;
        private DateTime endTime;
        private Keys selectedKey = Keys.F;
        private decimal intervalSeconds = 1.0M;
        private int durationMinutes = 60;
        private bool minimizeToTray = false;
        private bool quitAfterCompletion = false;
        private bool isDragging = false;
        private Point dragStartPoint;

        // Modern Dracula theme colors with transparency
        private readonly Color draculaBackground = Color.FromArgb(40, 42, 54);
        private readonly Color draculaForeground = Color.FromArgb(248, 248, 242);
        private readonly Color draculaSelection = Color.FromArgb(68, 71, 90);
        private readonly Color draculaComment = Color.FromArgb(98, 114, 164);
        private readonly Color draculaCyan = Color.FromArgb(139, 233, 253);
        private readonly Color draculaGreen = Color.FromArgb(80, 250, 123);
        private readonly Color draculaOrange = Color.FromArgb(255, 184, 108);
        private readonly Color draculaPink = Color.FromArgb(255, 121, 198);
        private readonly Color draculaPurple = Color.FromArgb(189, 147, 249);
        private readonly Color draculaRed = Color.FromArgb(255, 85, 85);
        private readonly Color draculaYellow = Color.FromArgb(241, 250, 140);
        
        // Modern gradient and transparency colors
        private readonly Color cardBackground = Color.FromArgb(240, 68, 71, 90);
        private readonly Color cardBackgroundHover = Color.FromArgb(250, 98, 114, 164);
        private readonly Color shadowColor = Color.FromArgb(50, 0, 0, 0);

        // Import the required Windows API functions
        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        
        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        private static extern short VkKeyScan(char ch);

        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetKeyboardLayout(uint idThread);
        
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse);

        // Constants for keyboard input
        private const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
        private const uint KEYEVENTF_KEYUP = 0x0002;
        private const uint KEYEVENTF_UNICODE = 0x0004;
        private const uint KEYEVENTF_SCANCODE = 0x0008;
        private const uint MAPVK_VK_TO_VSC = 0;

        // Structs for SendInput API
        [StructLayout(LayoutKind.Sequential)]
        public struct INPUT
        {
            public uint Type;
            public InputUnion U;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct InputUnion
        {
            [FieldOffset(0)]
            public MOUSEINPUT mi;
            [FieldOffset(0)]
            public KEYBDINPUT ki;
            [FieldOffset(0)]
            public HARDWAREINPUT hi;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HARDWAREINPUT
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }

        public MainForm()
        {
            InitializeComponent();
            InitializeNotifyIcon();
            InitializeTimer();
            
            // Set up borderless form with modern styling
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            
            // Set application icon from resources
            try
            {
                string iconPath = Path.Combine(Application.StartupPath, "Resources", "app_icon.ico");
                if (File.Exists(iconPath))
                {
                    this.Icon = new Icon(iconPath);
                    notifyIcon.Icon = this.Icon;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Icon loading error: {ex.Message}");
            }
            
            // Handle mouse events for form dragging
            this.MouseDown += MainForm_MouseDown;
            this.MouseMove += MainForm_MouseMove;
            this.MouseUp += MainForm_MouseUp;
            
            // Apply dark mode to the title bar
            this.HandleCreated += (s, e) => DarkTitleBar.ApplyDarkTitle(this);
            
            // Apply modern theme with rounded corners and gradients
            ApplyModernTheme();
            
            // Set rounded window shape
            this.Load += (s, e) => SetRoundedWindowShape();
            this.SizeChanged += (s, e) => SetRoundedWindowShape();
        }
        
        private void SetRoundedWindowShape()
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 20, 20));
            }
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            // Create modern gradient background without double layer effect
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            
            // Create single gradient brush for background
            using (LinearGradientBrush gradientBrush = new LinearGradientBrush(
                rect, 
                Color.FromArgb(40, 42, 54), // Use the exact draculaBackground color
                Color.FromArgb(35, 37, 49), 
                LinearGradientMode.Vertical))
            {
                // Create rounded rectangle path
                using (GraphicsPath path = CreateRoundedRectangle(rect, 20))
                {
                    e.Graphics.FillPath(gradientBrush, path);
                }
            }
        }
        
        private void FixControlTransparency(Control control)
        {
            // Use panel background color instead of transparency for controls
            // Some controls don't support transparent backgrounds
            Color panelColor = Color.FromArgb(68, 71, 90); // draculaSelection
            
            if (control is Label || control is Panel)
            {
                // These generally support transparency
                control.BackColor = Color.Transparent;
            }
            else
            {
                // For other controls, use the panel color
                control.BackColor = panelColor;
            }
            
            // For buttons, set all appearance properties
            if (control is Button button)
            {
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.FlatAppearance.MouseOverBackColor = panelColor;
                button.FlatAppearance.MouseDownBackColor = panelColor;
                button.FlatAppearance.CheckedBackColor = panelColor;
                button.UseVisualStyleBackColor = false;
            }
            
            // Apply to child controls
            foreach (Control child in control.Controls)
            {
                if (!(child is NumericUpDown)) // Skip numeric controls which we handle separately
                {
                    FixControlTransparency(child);
                }
            }
        }

        private void ApplyModernTheme()
        {
            // Remove tab stops and focus rectangles
            intervalNumeric.TabStop = false;
            durationNumeric.TabStop = false;
            minimizeCheckBox.TabStop = false;
            quitCheckBox.TabStop = false;
            startButton.TabStop = false;
            closeButton.TabStop = false;
            minimizeButton.TabStop = false;
            
            // Set panel colors to our standard panel background
            Color panelColor = Color.FromArgb(68, 71, 90); // draculaSelection
            
            keyboardPanel.BackColor = draculaBackground;
            intervalPanel.BackColor = draculaBackground;
            durationPanel.BackColor = draculaBackground;
            optionsPanel.BackColor = draculaBackground;
            controlsPanel.BackColor = draculaBackground;
            
            // Fix control colors
            virtualKeyboard.BackColor = panelColor;
            minimizeCheckBox.BackColor = panelColor;
            quitCheckBox.BackColor = panelColor;
            
            // Make all panel labels transparent
            foreach (Control control in this.Controls)
            {
                if (control is Panel panel)
                {
                    foreach (Control panelChild in panel.Controls)
                    {
                        if (panelChild is Label)
                        {
                            panelChild.BackColor = Color.Transparent;
                        }
                    }
                }
            }
            
            // Apply styling to panels
            ApplyModernPanelStyling(keyboardPanel);
            ApplyModernPanelStyling(intervalPanel);
            ApplyModernPanelStyling(durationPanel);
            ApplyModernPanelStyling(optionsPanel);
            ApplyModernPanelStyling(controlsPanel);
            
            // Apply modern button styling to start button
            ApplyModernButtonStyling(startButton);
            
            // Set the background color of the title bar buttons to match the form's background
            Color formBackColor = Color.FromArgb(40, 42, 54); // draculaBackground
            closeButton.BackColor = formBackColor;
            minimizeButton.BackColor = formBackColor;
            closeButton.FlatAppearance.MouseOverBackColor = formBackColor;
            minimizeButton.FlatAppearance.MouseOverBackColor = formBackColor;
            closeButton.FlatAppearance.MouseDownBackColor = formBackColor;
            minimizeButton.FlatAppearance.MouseDownBackColor = formBackColor;
            
            // Style the numeric inputs with only bottom border
            StyleNumericInput(intervalNumeric);
            StyleNumericInput(durationNumeric);
            
            // Apply custom renderers
            UpDownButtonRenderer.AttachToNumericUpDown(intervalNumeric);
            UpDownButtonRenderer.AttachToNumericUpDown(durationNumeric);
        }
        
        private void StyleNumericInput(NumericUpDown numeric)
        {
            numeric.BorderStyle = BorderStyle.None;
            numeric.BackColor = draculaSelection;
            
            // Create a panel to hold the numeric and draw the bottom border
            Panel borderPanel = new Panel();
            borderPanel.Size = new Size(numeric.Width, numeric.Height + 2);
            borderPanel.Location = numeric.Location;
            borderPanel.BackColor = Color.Transparent; // Use transparent background
            
            // Move numeric inside the border panel
            numeric.Parent.Controls.Add(borderPanel);
            numeric.Parent = borderPanel;
            numeric.Location = new Point(0, 0);
            
            // Paint the bottom border
            borderPanel.Paint += (sender, e) =>
            {
                // Fill the background to match panel
                using (SolidBrush brush = new SolidBrush(draculaSelection))
                {
                    e.Graphics.FillRectangle(brush, new Rectangle(0, 0, borderPanel.Width, borderPanel.Height - 2));
                }
                
                // Draw the bottom border
                using (Pen pen = new Pen(draculaComment, 2))
                {
                    e.Graphics.DrawLine(pen, 0, borderPanel.Height - 2, borderPanel.Width, borderPanel.Height - 2);
                }
            };
        }
        
        private void ApplyModernPanelStyling(Panel panel)
        {
            // Set consistent panel background color
            Color panelColor = Color.FromArgb(68, 71, 90); // draculaSelection
            panel.BackColor = draculaBackground; // Match window background
            
            // Add rounded corners to panels
            panel.Paint += (sender, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                
                // Create rounded rectangle path
                using (GraphicsPath path = CreateRoundedRectangle(new Rectangle(0, 0, panel.Width, panel.Height), 12))
                {
                    using (SolidBrush brush = new SolidBrush(panelColor))
                    {
                        e.Graphics.FillPath(brush, path);
                    }
                }
            };
            
            // Ensure all child labels are transparent
            foreach (Control control in panel.Controls)
            {
                if (control is Label)
                {
                    control.BackColor = Color.Transparent;
                }
            }
        }
        
        private void ApplyModernButtonStyling(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            
            // Only apply custom painting to the start button
            if (button == startButton)
            {
                // The base color of the button
                Color buttonColor = draculaPurple;
                
                // Remove all Windows standard button styling
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.FlatAppearance.MouseOverBackColor = Color.Transparent;
                button.FlatAppearance.MouseDownBackColor = Color.Transparent;
                button.BackColor = Color.Transparent;
                button.UseVisualStyleBackColor = false;
                button.ForeColor = draculaBackground;
                
                // Override the Paint event completely
                button.Paint += (sender, e) =>
                {
                    if (sender is Button btn)
                    {
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        
                        // Get the button's dimensions
                        Rectangle rect = new Rectangle(0, 0, btn.Width, btn.Height);
                        bool isHovered = btn.ClientRectangle.Contains(btn.PointToClient(Control.MousePosition));
                        
                        // Determine button colors based on state
                        Color startColor, endColor;
                        if (isRunning)
                        {
                            startColor = Color.FromArgb(255, 121, 198); // draculaPink
                            endColor = Color.FromArgb(235, 101, 178);   // Darker pink
                        }
                        else
                        {
                            startColor = isHovered ? Color.FromArgb(209, 167, 255) : draculaPurple;
                            endColor = isHovered ? Color.FromArgb(169, 127, 229) : Color.FromArgb(169, 127, 229);
                        }
                        
                        // Draw the button with rounded corners
                        using (LinearGradientBrush gradientBrush = new LinearGradientBrush(
                            rect, startColor, endColor, LinearGradientMode.Vertical))
                        {
                            using (GraphicsPath path = CreateRoundedRectangle(rect, 12))
                            {
                                e.Graphics.FillPath(gradientBrush, path);
                            }
                        }
                        
                        // Add subtle border
                        using (GraphicsPath borderPath = CreateRoundedRectangle(new Rectangle(1, 1, rect.Width - 2, rect.Height - 2), 11))
                        using (Pen borderPen = new Pen(Color.FromArgb(60, 255, 255, 255), 1))
                        {
                            e.Graphics.DrawPath(borderPen, borderPath);
                        }
                        
                        // Draw the text
                        using (SolidBrush textBrush = new SolidBrush(btn.ForeColor))
                        {
                            StringFormat sf = new StringFormat
                            {
                                Alignment = StringAlignment.Center,
                                LineAlignment = StringAlignment.Center
                            };
                            e.Graphics.DrawString(btn.Text, btn.Font, textBrush, rect, sf);
                        }
                    }
                };
                
                // Add hover effects for start button
                button.MouseEnter += (s, e) => button.Invalidate();
                button.MouseLeave += (s, e) => button.Invalidate();
            }
        }

        // Custom window movement logic
        private void MainForm_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Y < 100)
            {
                isDragging = true;
                dragStartPoint = new Point(e.X, e.Y);
            }
        }

        private void MainForm_MouseMove(object? sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentScreenPos = PointToScreen(e.Location);
                Location = new Point(
                    currentScreenPos.X - dragStartPoint.X,
                    currentScreenPos.Y - dragStartPoint.Y);
            }
        }

        private void MainForm_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }
        
        private void VirtualKeyboard_KeySelected(object? sender, string selectedKey)
        {
            if (Enum.TryParse(selectedKey, out Keys key))
            {
                this.selectedKey = key;
            }
        }
        
        // Helper method for creating rounded rectangle
        private GraphicsPath CreateRoundedRectangle(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();
            
            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }
            
            // Top left arc
            path.AddArc(arc, 180, 90);
            
            // Top right arc
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            
            // Bottom right arc
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            
            // Bottom left arc
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);
            
            path.CloseFigure();
            return path;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Initialize virtual keyboard with default selection
            virtualKeyboard.SelectedKey = "F";
        }

        private void InitializeNotifyIcon()
        {
            notifyIcon = new NotifyIcon
            {
                Icon = this.Icon,
                Visible = false,
                Text = "AutoStroke"
            };

            notifyIcon.DoubleClick += NotifyIcon_DoubleClick;

            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.BackColor = draculaBackground;
            contextMenu.ForeColor = draculaForeground;
            contextMenu.RenderMode = ToolStripRenderMode.System;

            ToolStripMenuItem openMenuItem = new ToolStripMenuItem("Open");
            openMenuItem.BackColor = draculaBackground;
            openMenuItem.ForeColor = draculaForeground;
            openMenuItem.Click += NotifyIcon_DoubleClick;

            ToolStripMenuItem exitMenuItem = new ToolStripMenuItem("Exit");
            exitMenuItem.BackColor = draculaBackground;
            exitMenuItem.ForeColor = draculaForeground;
            exitMenuItem.Click += ExitMenuItem_Click;

            contextMenu.Items.Add(openMenuItem);
            contextMenu.Items.Add(exitMenuItem);

            notifyIcon.ContextMenuStrip = contextMenu;
        }

        private void InitializeTimer()
        {
            keyPressTimer = new System.Windows.Forms.Timer();
            keyPressTimer.Tick += KeyPressTimer_Tick;
        }

        private void NotifyIcon_DoubleClick(object? sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
            Activate();
        }

        private void ExitMenuItem_Click(object? sender, EventArgs e)
        {
            StopKeyPresses();
            Application.Exit();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                StartKeyPresses();
            }
            else
            {
                StopKeyPresses();
            }
        }

        private void StartKeyPresses()
        {
            isRunning = true;
            startButton.Text = "Stop";
            startButton.Invalidate();
            statusLabel.Text = "Running";
            statusLabel.ForeColor = draculaGreen;
            statusLabel.Font = new Font(statusLabel.Font, FontStyle.Bold);

            endTime = DateTime.Now.AddMinutes(durationMinutes);
            keyPressTimer.Interval = (int)(intervalSeconds * 1000M);
            keyPressTimer.Start();

            virtualKeyboard.Enabled = false;
            intervalNumeric.Enabled = false;
            durationNumeric.Enabled = false;
            minimizeCheckBox.Enabled = false;
            quitCheckBox.Enabled = false;

            if (minimizeToTray)
            {
                WindowState = FormWindowState.Minimized;
                Hide();
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(3000, "AutoStroke Running", 
                    $"Pressing {selectedKey} key every {intervalSeconds} seconds for {durationMinutes} minutes", 
                    ToolTipIcon.Info);
            }
        }

        private void StopKeyPresses()
        {
            isRunning = false;
            keyPressTimer.Stop();
            startButton.Text = "Start";
            startButton.Invalidate();
            statusLabel.Text = "Stopped";
            statusLabel.ForeColor = draculaRed;
            statusLabel.Font = new Font(statusLabel.Font, FontStyle.Bold);

            virtualKeyboard.Enabled = true;
            intervalNumeric.Enabled = true;
            durationNumeric.Enabled = true;
            minimizeCheckBox.Enabled = true;
            quitCheckBox.Enabled = true;
        }

        private void KeyPressTimer_Tick(object? sender, EventArgs e)
        {
            SimulateKeyPress(selectedKey);

            if (DateTime.Now >= endTime)
            {
                StopKeyPresses();
                
                if (quitAfterCompletion)
                {
                    Application.Exit();
                }
                else
                {
                    if (minimizeToTray && notifyIcon.Visible)
                    {
                        notifyIcon.ShowBalloonTip(3000, "AutoStroke", 
                            "Key pressing sequence completed.", 
                            ToolTipIcon.Info);
                    }
                    else
                    {
                        MessageBox.Show("Key pressing sequence completed.", "AutoStroke", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void SimulateKeyPress(Keys key)
        {
            // Get scan code for more authentic simulation
            uint scanCode = MapVirtualKey((uint)key, MAPVK_VK_TO_VSC);

            // Prepare input structure array
            INPUT[] inputs = new INPUT[2];
            
            // KeyDown event
            inputs[0].Type = 1; // INPUT_KEYBOARD
            inputs[0].U.ki.wVk = (ushort)key;
            inputs[0].U.ki.wScan = (ushort)scanCode;
            inputs[0].U.ki.dwFlags = 0;
            inputs[0].U.ki.time = 0;
            inputs[0].U.ki.dwExtraInfo = IntPtr.Zero;
            
            // KeyUp event
            inputs[1].Type = 1; // INPUT_KEYBOARD
            inputs[1].U.ki.wVk = (ushort)key;
            inputs[1].U.ki.wScan = (ushort)scanCode;
            inputs[1].U.ki.dwFlags = KEYEVENTF_KEYUP;
            inputs[1].U.ki.time = 0;
            inputs[1].U.ki.dwExtraInfo = IntPtr.Zero;
            
            // Send input
            uint result = SendInput(2, inputs, Marshal.SizeOf(typeof(INPUT)));
            
            // Fallback to old method if SendInput fails
            if (result == 0)
            {
                keybd_event((byte)key, 0, KEYEVENTF_EXTENDEDKEY, UIntPtr.Zero);
                keybd_event((byte)key, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, UIntPtr.Zero);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isRunning && minimizeToTray)
            {
                e.Cancel = true;
                WindowState = FormWindowState.Minimized;
                Hide();
                notifyIcon.Visible = true;
            }
            else
            {
                StopKeyPresses();
                notifyIcon.Dispose();
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (minimizeToTray && WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
            }
        }

        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            closeButton.ForeColor = draculaRed;
        }
        
        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            closeButton.ForeColor = draculaComment;
        }
        
        private void MinimizeButton_MouseEnter(object sender, EventArgs e)
        {
            minimizeButton.ForeColor = draculaForeground;
        }
        
        private void MinimizeButton_MouseLeave(object sender, EventArgs e)
        {
            minimizeButton.ForeColor = draculaComment;
        }

        private void IntervalNumeric_ValueChanged(object sender, EventArgs e)
        {
            intervalSeconds = intervalNumeric.Value;
        }

        private void DurationNumeric_ValueChanged(object sender, EventArgs e)
        {
            durationMinutes = (int)durationNumeric.Value;
        }

        private void MinimizeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            minimizeToTray = minimizeCheckBox.Checked;
        }

        private void QuitCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            quitAfterCompletion = quitCheckBox.Checked;
        }
        
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
} 
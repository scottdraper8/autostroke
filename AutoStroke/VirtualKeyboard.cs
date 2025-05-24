using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace AutoStroke
{
    public class VirtualKeyboard : UserControl
    {
        private Dictionary<string, Button> keyButtons = new Dictionary<string, Button>();
        private string selectedKey = "F";
        
        // Modern Dracula theme colors
        private readonly Color draculaBackground = Color.FromArgb(40, 42, 54);
        private readonly Color draculaForeground = Color.FromArgb(248, 248, 242);
        private readonly Color draculaSelection = Color.FromArgb(68, 71, 90);
        private readonly Color draculaComment = Color.FromArgb(98, 114, 164);
        private readonly Color draculaPurple = Color.FromArgb(189, 147, 249);
        private readonly Color shadowColor = Color.FromArgb(30, 0, 0, 0);
        
        public event EventHandler<string>? KeySelected;
        
        public string SelectedKey
        {
            get => selectedKey;
            set
            {
                if (selectedKey != value)
                {
                    // Deselect previous key
                    if (keyButtons.ContainsKey(selectedKey))
                    {
                        keyButtons[selectedKey].BackColor = draculaSelection;
                        keyButtons[selectedKey].Invalidate();
                    }
                    
                    selectedKey = value;
                    
                    // Select new key
                    if (keyButtons.ContainsKey(selectedKey))
                    {
                        keyButtons[selectedKey].BackColor = draculaPurple;
                        keyButtons[selectedKey].Invalidate();
                    }
                    
                    KeySelected?.Invoke(this, selectedKey);
                }
            }
        }
        
        public VirtualKeyboard()
        {
            InitializeComponent();
            
            // Ensure proper rendering
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            
            CreateKeyboard();
        }
        
        private void InitializeComponent()
        {
            // Use panel background color
            this.BackColor = Color.FromArgb(68, 71, 90); // Panel background color
            this.Size = new Size(620, 200);
            this.DoubleBuffered = true;
        }
        
        private void CreateKeyboard()
        {
            // Make sure the parent control uses the same background color
            this.BackColor = Color.FromArgb(68, 71, 90); // Panel background color
            
            // Function keys row - adjusted width to fit all 12 keys properly
            var functionKeys = new[] { "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12" };
            CreateKeyRow(functionKeys, 0, 0, 48, 30);
            
            // Numbers row with ESC at the beginning
            CreateKeyButton("Escape", 0, 35, 45, 30);
            var numberKeys = new[] { "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "D0" };
            CreateKeyRow(numberKeys, 1, 35, 47, 30, 50);
            
            // First letter row (QWERTY) - adjusted Enter key position to prevent overlap with P key
            CreateKeyButton("Tab", 0, 70, 45, 30);
            var firstRow = new[] { "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P" };
            CreateKeyRow(firstRow, 1, 70, 47, 30, 50);
            // Moved Enter key to top right corner aligned with Tab and Escape
            CreateKeyButton("Enter", 550, 35, 55, 65);
            
            // Second letter row (ASDF)
            var secondRow = new[] { "A", "S", "D", "F", "G", "H", "J", "K", "L" };
            CreateKeyRow(secondRow, 1, 105, 47, 30, 75);
            
            // Third letter row (ZXCV)
            var thirdRow = new[] { "Z", "X", "C", "V", "B", "N", "M" };
            CreateKeyRow(thirdRow, 1, 140, 47, 30, 100);
            
            // Special keys row
            CreateSpecialKeys();
            
            // Set initial selection
            SelectedKey = "F";
        }
        
        private void CreateKeyRow(string[] keys, int row, int y, int keyWidth, int keyHeight, int startX = 0)
        {
            int x = startX;
            foreach (string key in keys)
            {
                CreateKeyButton(key, x, y, keyWidth, keyHeight);
                x += keyWidth + 3;
            }
        }
        
        private void CreateSpecialKeys()
        {
            CreateKeyButton("Space", 200, 175, 150, 25);
            CreateKeyButton("Up", 545, 120, 35, 25);
            CreateKeyButton("Left", 505, 150, 35, 25);
            CreateKeyButton("Down", 545, 150, 35, 25);
            CreateKeyButton("Right", 585, 150, 35, 25);
        }
        
        private void CreateKeyButton(string keyName, int x, int y, int width, int height)
        {
            // Create a panel for the button background first
            Panel buttonPanel = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                BackColor = Color.FromArgb(68, 71, 90), // Match panel background
                Margin = new Padding(0),
                Padding = new Padding(0)
            };
            
            Button keyButton = new Button
            {
                Text = GetDisplayText(keyName),
                Location = new Point(0, 0),
                Size = new Size(width, height),
                BackColor = Color.FromArgb(68, 71, 90), // Match panel background
                ForeColor = draculaForeground,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                Tag = keyName,
                Cursor = Cursors.Hand,
                Parent = buttonPanel,
                Margin = new Padding(0),
                Dock = DockStyle.Fill
            };
            
            // Ensure flat appearance has no borders and consistent colors
            keyButton.FlatAppearance.BorderSize = 0;
            keyButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(68, 71, 90);
            keyButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(68, 71, 90);
            keyButton.FlatAppearance.CheckedBackColor = Color.FromArgb(68, 71, 90);
            keyButton.UseVisualStyleBackColor = false;
            
            keyButton.Click += KeyButton_Click;
            
            // Custom painting for the key
            keyButton.Paint += KeyButton_Paint;
            keyButton.MouseEnter += (s, e) => keyButton.Invalidate();
            keyButton.MouseLeave += (s, e) => keyButton.Invalidate();
            
            keyButtons[keyName] = keyButton;
            buttonPanel.Controls.Add(keyButton);
            this.Controls.Add(buttonPanel);
        }
        
        private void KeyButton_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is Button btn)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                
                // First, clear the entire button area with the parent's background color
                e.Graphics.Clear(Color.FromArgb(68, 71, 90)); // Panel background color
                
                // Reduce the rectangle size slightly to prevent border cutoff
                Rectangle rect = new Rectangle(1, 1, btn.Width - 2, btn.Height - 2);
                bool isHovered = btn.ClientRectangle.Contains(btn.PointToClient(Control.MousePosition));
                bool isSelected = btn.Tag?.ToString() == selectedKey;
                
                // Draw the main background for the key
                using (GraphicsPath path = CreateRoundedRectangle(rect, 6))
                {
                    // Determine colors
                    Color startColor, endColor, textColor;
                    if (isSelected)
                    {
                        startColor = Color.FromArgb(209, 167, 255);
                        endColor = Color.FromArgb(169, 127, 229);
                        textColor = draculaBackground;
                    }
                    else if (isHovered)
                    {
                        startColor = Color.FromArgb(88, 91, 110);
                        endColor = Color.FromArgb(78, 81, 100);
                        textColor = draculaForeground;
                    }
                    else
                    {
                        startColor = Color.FromArgb(78, 81, 100);
                        endColor = Color.FromArgb(68, 71, 90);
                        textColor = draculaForeground;
                    }
                    
                    // Create gradient background
                    using (LinearGradientBrush gradientBrush = new LinearGradientBrush(
                        rect, startColor, endColor, LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillPath(gradientBrush, path);
                    }
                    
                    // Add border
                    using (Pen borderPen = new Pen(isSelected ? Color.FromArgb(100, 255, 255, 255) : Color.FromArgb(50, 139, 233, 253), 1))
                    {
                        e.Graphics.DrawPath(borderPen, path);
                    }
                    
                    // Draw text
                    using (SolidBrush textBrush = new SolidBrush(textColor))
                    {
                        StringFormat sf = new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };
                        e.Graphics.DrawString(btn.Text, btn.Font, textBrush, rect, sf);
                    }
                }
            }
        }
        
        private void KeyButton_Click(object? sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is string keyName)
            {
                SelectedKey = keyName;
            }
        }
        
        private string GetDisplayText(string keyName)
        {
            return keyName switch
            {
                "Space" => "Space",
                "Enter" => "↵",
                "Tab" => "Tab",
                "Escape" => "Esc",
                "Up" => "↑",
                "Down" => "↓",
                "Left" => "←",
                "Right" => "→",
                string s when s.StartsWith("D") && s.Length == 2 => s.Substring(1), // D1 -> 1
                _ => keyName
            };
        }
        
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
    }
} 
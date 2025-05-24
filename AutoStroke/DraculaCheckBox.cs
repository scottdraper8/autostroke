using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AutoStroke
{
    public class DraculaCheckBox : CheckBox
    {
        // Modern Dracula theme colors
        private readonly Color draculaBackground = Color.FromArgb(40, 42, 54);
        private readonly Color draculaForeground = Color.FromArgb(248, 248, 242);
        private readonly Color draculaSelection = Color.FromArgb(68, 71, 90);
        private readonly Color draculaPurple = Color.FromArgb(189, 147, 249);
        private readonly Color draculaPink = Color.FromArgb(255, 121, 198);
        private readonly Color draculaCyan = Color.FromArgb(139, 233, 253);
        private readonly Color draculaGreen = Color.FromArgb(80, 250, 123);
        
        // Modern design colors
        private readonly Color shadowColor = Color.FromArgb(40, 0, 0, 0);
        private readonly Color highlightColor = Color.FromArgb(60, 255, 255, 255);
        
        // Size of the checkbox
        private const int CheckBoxSize = 20;
        private bool isHovered = false;
        
        public DraculaCheckBox()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.FlatAppearance.CheckedBackColor = Color.FromArgb(68, 71, 90);
            this.FlatAppearance.MouseOverBackColor = Color.FromArgb(78, 81, 100);
            this.FlatAppearance.MouseDownBackColor = Color.FromArgb(78, 81, 100);
            this.BackColor = Color.FromArgb(68, 71, 90); // Panel background color
            this.Cursor = Cursors.Hand;
            this.TabStop = false;
            
            // Set the control to be transparent
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            
            // Add hover events
            this.MouseEnter += (s, e) => { isHovered = true; this.Invalidate(); };
            this.MouseLeave += (s, e) => { isHovered = false; this.Invalidate(); };
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            // Completely skip the base rendering which can cause the black background box
            // base.OnPaint(e);
            
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            
            // Fill with background color first to ensure no transparency issues
            e.Graphics.Clear(Color.FromArgb(68, 71, 90)); // Panel background color
            
            // Calculate checkbox rectangle
            var checkBoxRect = new Rectangle(2, (Height - CheckBoxSize) / 2, CheckBoxSize, CheckBoxSize);
            
            // Create rounded rectangle path for checkbox
            using (GraphicsPath checkboxPath = CreateRoundedRectangle(checkBoxRect, 6))
            {
                // Create gradient background
                Color startColor, endColor;
                if (Checked)
                {
                    startColor = isHovered ? 
                        Color.FromArgb(209, 167, 255) : 
                        draculaPurple;
                    endColor = isHovered ? 
                        Color.FromArgb(169, 127, 229) : 
                        Color.FromArgb(169, 127, 229);
                }
                else
                {
                    startColor = isHovered ? 
                        Color.FromArgb(88, 91, 110) : 
                        Color.FromArgb(78, 81, 100);
                    endColor = isHovered ? 
                        Color.FromArgb(68, 71, 90) : 
                        Color.FromArgb(58, 61, 80);
                }
                
                using (LinearGradientBrush gradientBrush = new LinearGradientBrush(
                    checkBoxRect, startColor, endColor, LinearGradientMode.Vertical))
                {
                    e.Graphics.FillPath(gradientBrush, checkboxPath);
                }
                
                // Add subtle border
                Color borderColor = Checked ? 
                    Color.FromArgb(120, 255, 255, 255) : 
                    Color.FromArgb(80, draculaCyan);
                    
                using (Pen borderPen = new Pen(borderColor, 1))
                {
                    e.Graphics.DrawPath(borderPen, checkboxPath);
                }
            }
            
            // Draw checkmark if checked
            if (Checked)
            {
                DrawModernCheckmark(e.Graphics, checkBoxRect);
            }
            
            // Draw the text with modern styling
            if (!string.IsNullOrEmpty(Text))
            {
                var textRect = new Rectangle(CheckBoxSize + 16, 0, Width - CheckBoxSize - 16, Height);
                
                // Draw main text
                Color textColor = isHovered ? draculaCyan : draculaForeground;
                TextRenderer.DrawText(e.Graphics, Text, Font, textRect, textColor, 
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
            }
        }
        
        private void DrawModernCheckmark(Graphics g, Rectangle checkBoxRect)
        {
            const int padding = 5;
            var checkPoints = new Point[]
            {
                new Point(checkBoxRect.X + padding, checkBoxRect.Y + CheckBoxSize / 2),
                new Point(checkBoxRect.X + CheckBoxSize / 2 - 1, checkBoxRect.Y + CheckBoxSize - padding - 1),
                new Point(checkBoxRect.X + CheckBoxSize - padding, checkBoxRect.Y + padding + 1)
            };
            
            // Draw checkmark with modern styling
            using (Pen checkPen = new Pen(draculaBackground, 2.5f))
            {
                checkPen.StartCap = LineCap.Round;
                checkPen.EndCap = LineCap.Round;
                checkPen.LineJoin = LineJoin.Round;
                
                g.DrawLines(checkPen, checkPoints);
            }
            
            // Add subtle glow
            using (Pen glowPen = new Pen(Color.FromArgb(50, 255, 255, 255), 4))
            {
                glowPen.StartCap = LineCap.Round;
                glowPen.EndCap = LineCap.Round;
                glowPen.LineJoin = LineJoin.Round;
                
                g.DrawLines(glowPen, checkPoints);
            }
        }
        
        // Helper method for creating rounded rectangle
        private GraphicsPath CreateRoundedRectangle(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();
            
            if (radius == 0 || bounds.Width <= diameter || bounds.Height <= diameter)
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
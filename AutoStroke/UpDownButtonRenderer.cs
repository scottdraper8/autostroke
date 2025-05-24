using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AutoStroke
{
    public class UpDownButtonRenderer
    {
        // Modern Dracula theme colors
        private static readonly Color draculaBackground = Color.FromArgb(40, 42, 54);
        private static readonly Color draculaForeground = Color.FromArgb(248, 248, 242);
        private static readonly Color draculaSelection = Color.FromArgb(68, 71, 90);
        private static readonly Color draculaPurple = Color.FromArgb(189, 147, 249);
        private static readonly Color draculaCyan = Color.FromArgb(139, 233, 253);

        public static void AttachToNumericUpDown(NumericUpDown numericUpDown)
        {
            // Apply modern styling to the numeric up/down control
            numericUpDown.BackColor = draculaSelection;
            numericUpDown.ForeColor = draculaForeground;
            numericUpDown.BorderStyle = BorderStyle.None;
            
            // Find and style the up/down buttons container
            foreach (Control control in numericUpDown.Controls)
            {
                // This targets the up-down button control
                if (control is Control upDownButtons)
                {
                    // Keep the background color matching the panel instead of making it transparent
                    upDownButtons.BackColor = draculaSelection;
                    
                    // Override the paint method to draw custom chevrons
                    upDownButtons.Paint += (sender, e) => 
                    {
                        if (sender is Control ctrl)
                        {
                            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                            
                            // Clear with the selection color to ensure a clean slate
                            e.Graphics.Clear(draculaSelection);
                            
                            var buttonWidth = ctrl.Width;
                            var buttonHeight = ctrl.Height / 2;
                            
                            var upButtonRect = new Rectangle(0, 0, buttonWidth, buttonHeight);
                            var downButtonRect = new Rectangle(0, buttonHeight, buttonWidth, buttonHeight);
                            
                            // Check which part is being hovered
                            Point mousePos = ctrl.PointToClient(Control.MousePosition);
                            bool isUpHovered = upButtonRect.Contains(mousePos) && ctrl.ClientRectangle.Contains(mousePos);
                            bool isDownHovered = downButtonRect.Contains(mousePos) && ctrl.ClientRectangle.Contains(mousePos);
                            
                            // Draw only the chevron arrows, no button backgrounds
                            DrawModernArrow(e.Graphics, upButtonRect, true, isUpHovered);
                            DrawModernArrow(e.Graphics, downButtonRect, false, isDownHovered);
                        }
                    };
                    
                    // Add hover effects
                    upDownButtons.MouseEnter += (s, e) => upDownButtons.Invalidate();
                    upDownButtons.MouseLeave += (s, e) => upDownButtons.Invalidate();
                    upDownButtons.MouseMove += (s, e) => upDownButtons.Invalidate();
                }
            }
        }
        
        private static void DrawModernArrow(Graphics g, Rectangle rect, bool isUpButton, bool isHovered)
        {
            if (rect.Width <= 0 || rect.Height <= 0) return;
            
            var arrowSize = Math.Min(rect.Width, rect.Height) / 2.5f; // Smaller arrow size
            var arrowX = rect.Left + (rect.Width - arrowSize) / 2;
            var arrowY = rect.Top + (rect.Height - arrowSize / 2) / 2;
            
            Point[] points = new Point[3];
            
            if (isUpButton)
            {
                // Up chevron
                points[0] = new Point((int)arrowX, (int)(arrowY + arrowSize / 2));
                points[1] = new Point((int)(arrowX + arrowSize / 2), (int)arrowY);
                points[2] = new Point((int)(arrowX + arrowSize), (int)(arrowY + arrowSize / 2));
            }
            else
            {
                // Down chevron
                points[0] = new Point((int)arrowX, (int)arrowY);
                points[1] = new Point((int)(arrowX + arrowSize / 2), (int)(arrowY + arrowSize / 2));
                points[2] = new Point((int)(arrowX + arrowSize), (int)arrowY);
            }
            
            // Draw clean arrow lines with thicker, more visible stroke
            Color arrowColor = isHovered ? draculaCyan : draculaForeground;
            using (Pen arrowPen = new Pen(arrowColor, 2.0f))
            {
                arrowPen.StartCap = LineCap.Round;
                arrowPen.EndCap = LineCap.Round;
                arrowPen.LineJoin = LineJoin.Round;
                
                // Draw two lines to form a chevron
                g.DrawLine(arrowPen, points[0], points[1]);
                g.DrawLine(arrowPen, points[1], points[2]);
            }
        }
        
        // Helper method for creating rounded rectangle
        private static GraphicsPath CreateRoundedRectangle(Rectangle bounds, int radius)
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
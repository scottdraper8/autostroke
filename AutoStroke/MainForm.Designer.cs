namespace AutoStroke
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.startButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.virtualKeyboard = new AutoStroke.VirtualKeyboard();
            this.intervalNumeric = new System.Windows.Forms.NumericUpDown();
            this.durationNumeric = new System.Windows.Forms.NumericUpDown();
            this.intervalLabel = new System.Windows.Forms.Label();
            this.durationLabel = new System.Windows.Forms.Label();
            this.secondsLabel = new System.Windows.Forms.Label();
            this.minutesLabel = new System.Windows.Forms.Label();
            this.optionsPanel = new System.Windows.Forms.Panel();
            this.quitCheckBox = new AutoStroke.DraculaCheckBox();
            this.minimizeCheckBox = new AutoStroke.DraculaCheckBox();
            this.optionsLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.minimizeButton = new System.Windows.Forms.Button();
            this.intervalPanel = new System.Windows.Forms.Panel();
            this.durationPanel = new System.Windows.Forms.Panel();
            this.controlsPanel = new System.Windows.Forms.Panel();
            this.subtitleLabel = new System.Windows.Forms.Label();
            this.keyboardPanel = new System.Windows.Forms.Panel();
            this.keyboardLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.intervalNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.durationNumeric)).BeginInit();
            this.optionsPanel.SuspendLayout();
            this.intervalPanel.SuspendLayout();
            this.durationPanel.SuspendLayout();
            this.controlsPanel.SuspendLayout();
            this.keyboardPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.Transparent;
            this.startButton.FlatAppearance.BorderSize = 0;
            this.startButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startButton.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.startButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.startButton.Location = new System.Drawing.Point(90, 20);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(150, 45);
            this.startButton.TabIndex = 6;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = false;
            this.statusLabel.BackColor = System.Drawing.Color.Transparent;
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.statusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.statusLabel.Location = new System.Drawing.Point(0, 75);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(330, 35);
            this.statusLabel.TabIndex = 7;
            this.statusLabel.Text = "Stopped";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // virtualKeyboard
            // 
            this.virtualKeyboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(71)))), ((int)(((byte)(90)))));
            this.virtualKeyboard.Location = new System.Drawing.Point(25, 55);
            this.virtualKeyboard.Name = "virtualKeyboard";
            this.virtualKeyboard.SelectedKey = "F";
            this.virtualKeyboard.Size = new System.Drawing.Size(620, 200);
            this.virtualKeyboard.TabIndex = 0;
            this.virtualKeyboard.KeySelected += new System.EventHandler<string>(this.VirtualKeyboard_KeySelected);
            // 
            // intervalNumeric
            // 
            this.intervalNumeric.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(71)))), ((int)(((byte)(90)))));
            this.intervalNumeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.intervalNumeric.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.intervalNumeric.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(242)))));
            this.intervalNumeric.Location = new System.Drawing.Point(25, 55);
            this.intervalNumeric.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.intervalNumeric.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.intervalNumeric.Name = "intervalNumeric";
            this.intervalNumeric.Size = new System.Drawing.Size(120, 43);
            this.intervalNumeric.TabIndex = 1;
            this.intervalNumeric.TabStop = false;
            this.intervalNumeric.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.intervalNumeric.DecimalPlaces = 1;
            this.intervalNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.intervalNumeric.ValueChanged += new System.EventHandler(this.IntervalNumeric_ValueChanged);
            // 
            // durationNumeric
            // 
            this.durationNumeric.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(71)))), ((int)(((byte)(90)))));
            this.durationNumeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.durationNumeric.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.durationNumeric.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(242)))));
            this.durationNumeric.Location = new System.Drawing.Point(25, 55);
            this.durationNumeric.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.durationNumeric.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.durationNumeric.Name = "durationNumeric";
            this.durationNumeric.Size = new System.Drawing.Size(120, 43);
            this.durationNumeric.TabIndex = 2;
            this.durationNumeric.TabStop = false;
            this.durationNumeric.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.durationNumeric.ValueChanged += new System.EventHandler(this.DurationNumeric_ValueChanged);
            // 
            // intervalLabel
            // 
            this.intervalLabel.AutoSize = true;
            this.intervalLabel.BackColor = System.Drawing.Color.Transparent;
            this.intervalLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.intervalLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(121)))), ((int)(((byte)(198)))));
            this.intervalLabel.Location = new System.Drawing.Point(25, 20);
            this.intervalLabel.Name = "intervalLabel";
            this.intervalLabel.Size = new System.Drawing.Size(81, 28);
            this.intervalLabel.TabIndex = 9;
            this.intervalLabel.Text = "Interval";
            // 
            // durationLabel
            // 
            this.durationLabel.AutoSize = true;
            this.durationLabel.BackColor = System.Drawing.Color.Transparent;
            this.durationLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.durationLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(233)))), ((int)(((byte)(253)))));
            this.durationLabel.Location = new System.Drawing.Point(25, 20);
            this.durationLabel.Name = "durationLabel";
            this.durationLabel.Size = new System.Drawing.Size(91, 28);
            this.durationLabel.TabIndex = 10;
            this.durationLabel.Text = "Duration";
            // 
            // secondsLabel
            // 
            this.secondsLabel.AutoSize = true;
            this.secondsLabel.BackColor = System.Drawing.Color.Transparent;
            this.secondsLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.secondsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(114)))), ((int)(((byte)(164)))));
            this.secondsLabel.Location = new System.Drawing.Point(155, 60);
            this.secondsLabel.Name = "secondsLabel";
            this.secondsLabel.Size = new System.Drawing.Size(78, 28);
            this.secondsLabel.TabIndex = 11;
            this.secondsLabel.Text = "seconds";
            this.secondsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // minutesLabel
            // 
            this.minutesLabel.AutoSize = true;
            this.minutesLabel.BackColor = System.Drawing.Color.Transparent;
            this.minutesLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.minutesLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(114)))), ((int)(((byte)(164)))));
            this.minutesLabel.Location = new System.Drawing.Point(155, 60);
            this.minutesLabel.Name = "minutesLabel";
            this.minutesLabel.Size = new System.Drawing.Size(77, 28);
            this.minutesLabel.TabIndex = 12;
            this.minutesLabel.Text = "minutes";
            this.minutesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // optionsPanel
            // 
            this.optionsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(71)))), ((int)(((byte)(90)))));
            this.optionsPanel.Controls.Add(this.quitCheckBox);
            this.optionsPanel.Controls.Add(this.minimizeCheckBox);
            this.optionsPanel.Controls.Add(this.optionsLabel);
            this.optionsPanel.Location = new System.Drawing.Point(420, 460);
            this.optionsPanel.Name = "optionsPanel";
            this.optionsPanel.Size = new System.Drawing.Size(330, 130);
            this.optionsPanel.TabIndex = 13;
            // 
            // quitCheckBox
            // 
            this.quitCheckBox.Checked = false;
            this.quitCheckBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.quitCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(242)))));
            this.quitCheckBox.Location = new System.Drawing.Point(25, 80);
            this.quitCheckBox.Name = "quitCheckBox";
            this.quitCheckBox.Size = new System.Drawing.Size(280, 30);
            this.quitCheckBox.TabIndex = 5;
            this.quitCheckBox.Text = "Quit application when completed";
            this.quitCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.quitCheckBox.CheckedChanged += new System.EventHandler(this.QuitCheckBox_CheckedChanged);
            // 
            // minimizeCheckBox
            // 
            this.minimizeCheckBox.Checked = false;
            this.minimizeCheckBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.minimizeCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(242)))));
            this.minimizeCheckBox.Location = new System.Drawing.Point(25, 50);
            this.minimizeCheckBox.Name = "minimizeCheckBox";
            this.minimizeCheckBox.Size = new System.Drawing.Size(280, 30);
            this.minimizeCheckBox.TabIndex = 4;
            this.minimizeCheckBox.Text = "Minimize to system tray when running";
            this.minimizeCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.minimizeCheckBox.CheckedChanged += new System.EventHandler(this.MinimizeCheckBox_CheckedChanged);
            // 
            // optionsLabel
            // 
            this.optionsLabel.AutoSize = true;
            this.optionsLabel.BackColor = System.Drawing.Color.Transparent;
            this.optionsLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.optionsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(250)))), ((int)(((byte)(123)))));
            this.optionsLabel.Location = new System.Drawing.Point(25, 20);
            this.optionsLabel.Name = "optionsLabel";
            this.optionsLabel.Size = new System.Drawing.Size(82, 28);
            this.optionsLabel.TabIndex = 0;
            this.optionsLabel.Text = "Options";
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.BackColor = System.Drawing.Color.Transparent;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(242)))));
            this.titleLabel.Location = new System.Drawing.Point(60, 30);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(295, 72);
            this.titleLabel.TabIndex = 14;
            this.titleLabel.Text = "AutoStroke";
            // 
            // closeButton
            // 
            this.closeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.closeButton.FlatAppearance.BorderSize = 0;
            this.closeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.closeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.closeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(114)))), ((int)(((byte)(164)))));
            this.closeButton.Location = new System.Drawing.Point(745, 10);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(40, 40);
            this.closeButton.TabIndex = 15;
            this.closeButton.TabStop = false;
            this.closeButton.Text = "✕";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.CloseButton_Click);
            this.closeButton.MouseEnter += new System.EventHandler(this.CloseButton_MouseEnter);
            this.closeButton.MouseLeave += new System.EventHandler(this.CloseButton_MouseLeave);
            // 
            // minimizeButton
            // 
            this.minimizeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.minimizeButton.FlatAppearance.BorderSize = 0;
            this.minimizeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.minimizeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.minimizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimizeButton.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.minimizeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(114)))), ((int)(((byte)(164)))));
            this.minimizeButton.Location = new System.Drawing.Point(700, 10);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.Size = new System.Drawing.Size(40, 40);
            this.minimizeButton.TabIndex = 16;
            this.minimizeButton.TabStop = false;
            this.minimizeButton.Text = "−";
            this.minimizeButton.UseVisualStyleBackColor = false;
            this.minimizeButton.Click += new System.EventHandler(this.MinimizeButton_Click);
            this.minimizeButton.MouseEnter += new System.EventHandler(this.MinimizeButton_MouseEnter);
            this.minimizeButton.MouseLeave += new System.EventHandler(this.MinimizeButton_MouseLeave);
            // 
            // intervalPanel
            // 
            this.intervalPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(71)))), ((int)(((byte)(90)))));
            this.intervalPanel.Controls.Add(this.intervalNumeric);
            this.intervalPanel.Controls.Add(this.intervalLabel);
            this.intervalPanel.Controls.Add(this.secondsLabel);
            this.intervalPanel.Location = new System.Drawing.Point(60, 460);
            this.intervalPanel.Name = "intervalPanel";
            this.intervalPanel.Size = new System.Drawing.Size(330, 130);
            this.intervalPanel.TabIndex = 18;
            // 
            // durationPanel
            // 
            this.durationPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(71)))), ((int)(((byte)(90)))));
            this.durationPanel.Controls.Add(this.durationNumeric);
            this.durationPanel.Controls.Add(this.durationLabel);
            this.durationPanel.Controls.Add(this.minutesLabel);
            this.durationPanel.Location = new System.Drawing.Point(60, 600);
            this.durationPanel.Name = "durationPanel";
            this.durationPanel.Size = new System.Drawing.Size(330, 130);
            this.durationPanel.TabIndex = 19;
            // 
            // controlsPanel
            // 
            this.controlsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(71)))), ((int)(((byte)(90)))));
            this.controlsPanel.Controls.Add(this.startButton);
            this.controlsPanel.Controls.Add(this.statusLabel);
            this.controlsPanel.Location = new System.Drawing.Point(420, 600);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(330, 130);
            this.controlsPanel.TabIndex = 20;
            // 
            // subtitleLabel
            // 
            this.subtitleLabel.AutoSize = true;
            this.subtitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(114)))), ((int)(((byte)(164)))));
            this.subtitleLabel.Location = new System.Drawing.Point(60, 100);
            this.subtitleLabel.Name = "subtitleLabel";
            this.subtitleLabel.Size = new System.Drawing.Size(400, 28);
            this.subtitleLabel.TabIndex = 21;
            this.subtitleLabel.Text = "Automate key presses with customizable timing";
            // 
            // keyboardPanel
            // 
            this.keyboardPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(71)))), ((int)(((byte)(90)))));
            this.keyboardPanel.Controls.Add(this.virtualKeyboard);
            this.keyboardPanel.Controls.Add(this.keyboardLabel);
            this.keyboardPanel.Location = new System.Drawing.Point(60, 150);
            this.keyboardPanel.Name = "keyboardPanel";
            this.keyboardPanel.Size = new System.Drawing.Size(690, 290);
            this.keyboardPanel.TabIndex = 22;
            // 
            // keyboardLabel
            // 
            this.keyboardLabel.AutoSize = true;
            this.keyboardLabel.BackColor = System.Drawing.Color.Transparent;
            this.keyboardLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.keyboardLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(147)))), ((int)(((byte)(249)))));
            this.keyboardLabel.Location = new System.Drawing.Point(25, 20);
            this.keyboardLabel.Name = "keyboardLabel";
            this.keyboardLabel.Size = new System.Drawing.Size(154, 28);
            this.keyboardLabel.TabIndex = 23;
            this.keyboardLabel.Text = "Key to Autopress";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(800, 750);
            this.Controls.Add(this.keyboardPanel);
            this.Controls.Add(this.subtitleLabel);
            this.Controls.Add(this.controlsPanel);
            this.Controls.Add(this.durationPanel);
            this.Controls.Add(this.intervalPanel);
            this.Controls.Add(this.minimizeButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.optionsPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AutoStroke";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.intervalNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.durationNumeric)).EndInit();
            this.optionsPanel.ResumeLayout(false);
            this.optionsPanel.PerformLayout();
            this.intervalPanel.ResumeLayout(false);
            this.intervalPanel.PerformLayout();
            this.durationPanel.ResumeLayout(false);
            this.durationPanel.PerformLayout();
            this.controlsPanel.ResumeLayout(false);
            this.controlsPanel.PerformLayout();
            this.keyboardPanel.ResumeLayout(false);
            this.keyboardPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label statusLabel;
        private AutoStroke.VirtualKeyboard virtualKeyboard;
        private System.Windows.Forms.NumericUpDown intervalNumeric;
        private System.Windows.Forms.NumericUpDown durationNumeric;
        private System.Windows.Forms.Label intervalLabel;
        private System.Windows.Forms.Label durationLabel;
        private System.Windows.Forms.Label secondsLabel;
        private System.Windows.Forms.Label minutesLabel;
        private System.Windows.Forms.Panel optionsPanel;
        private DraculaCheckBox quitCheckBox;
        private DraculaCheckBox minimizeCheckBox;
        private System.Windows.Forms.Label optionsLabel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button minimizeButton;
        private System.Windows.Forms.Panel intervalPanel;
        private System.Windows.Forms.Panel durationPanel;
        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.Label subtitleLabel;
        private System.Windows.Forms.Panel keyboardPanel;
        private System.Windows.Forms.Label keyboardLabel;
    }
} 
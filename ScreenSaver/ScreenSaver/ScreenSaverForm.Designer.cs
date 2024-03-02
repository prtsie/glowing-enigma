namespace ScreenSaver
{
    partial class ScreenSaverForm
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
            components = new System.ComponentModel.Container();
            redrawer = new System.Windows.Forms.Timer(components);
            snowflakeGenerator = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // redrawer
            // 
            redrawer.Interval = 30;
            redrawer.Tick += RedrawerTick;
            // 
            // snowflakeGenerator
            // 
            snowflakeGenerator.Interval = 200;
            snowflakeGenerator.Tick += SnowflakeGeneratorTick;
            // 
            // ScreenSaverForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(800, 450);
            ControlBox = false;
            Cursor = Cursors.No;
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ScreenSaverForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "Form1";
            TopMost = true;
            WindowState = FormWindowState.Maximized;
            Shown += ScreenSaverForm_Shown;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer redrawer;
        private System.Windows.Forms.Timer snowflakeGenerator;
    }
}

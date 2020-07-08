namespace MemoryGame_UI
{
    partial class GameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            this.labelCurrentPlayer = new System.Windows.Forms.Label();
            this.labelFirstPlayerPairs = new System.Windows.Forms.Label();
            this.labelSecondPlayerPairs = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelCurrentPlayer
            // 
            this.labelCurrentPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelCurrentPlayer.AutoSize = true;
            this.labelCurrentPlayer.Location = new System.Drawing.Point(12, 421);
            this.labelCurrentPlayer.Name = "labelCurrentPlayer";
            this.labelCurrentPlayer.Size = new System.Drawing.Size(107, 17);
            this.labelCurrentPlayer.TabIndex = 0;
            this.labelCurrentPlayer.Text = "Current Player: ";
            // 
            // labelFirstPlayerPairs
            // 
            this.labelFirstPlayerPairs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelFirstPlayerPairs.AutoSize = true;
            this.labelFirstPlayerPairs.BackColor = System.Drawing.Color.Pink;
            this.labelFirstPlayerPairs.Location = new System.Drawing.Point(12, 448);
            this.labelFirstPlayerPairs.Name = "labelFirstPlayerPairs";
            this.labelFirstPlayerPairs.Size = new System.Drawing.Size(107, 17);
            this.labelFirstPlayerPairs.TabIndex = 1;
            this.labelFirstPlayerPairs.Text = "FirstPlayerPairs";
            // 
            // labelSecondPlayerPairs
            // 
            this.labelSecondPlayerPairs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSecondPlayerPairs.AutoSize = true;
            this.labelSecondPlayerPairs.BackColor = System.Drawing.Color.SkyBlue;
            this.labelSecondPlayerPairs.Location = new System.Drawing.Point(12, 475);
            this.labelSecondPlayerPairs.Name = "labelSecondPlayerPairs";
            this.labelSecondPlayerPairs.Size = new System.Drawing.Size(128, 17);
            this.labelSecondPlayerPairs.TabIndex = 2;
            this.labelSecondPlayerPairs.Text = "SecondPlayerPairs";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 501);
            this.Controls.Add(this.labelSecondPlayerPairs);
            this.Controls.Add(this.labelFirstPlayerPairs);
            this.Controls.Add(this.labelCurrentPlayer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Memory Game";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCurrentPlayer;
        private System.Windows.Forms.Label labelFirstPlayerPairs;
        private System.Windows.Forms.Label labelSecondPlayerPairs;
    }
}
namespace FiniteStateMachine
{
    partial class Form1
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
            this.welcomeLabel = new System.Windows.Forms.Label();
            this.stateLabel = new System.Windows.Forms.Label();
            this.gumButton = new System.Windows.Forms.RadioButton();
            this.granolaButton = new System.Windows.Forms.RadioButton();
            this.selectButton = new System.Windows.Forms.Button();
            this.quarterButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.moneyLabel = new System.Windows.Forms.Label();
            this.outputLabel = new System.Windows.Forms.Label();
            this.vendingBox = new System.Windows.Forms.GroupBox();
            this.vendingBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // welcomeLabel
            // 
            this.welcomeLabel.AutoSize = true;
            this.welcomeLabel.Location = new System.Drawing.Point(245, 9);
            this.welcomeLabel.Name = "welcomeLabel";
            this.welcomeLabel.Size = new System.Drawing.Size(279, 34);
            this.welcomeLabel.TabIndex = 1;
            this.welcomeLabel.Text = "Welcome to the vending machine!\r\nInsert a quarter and then make a selection.";
            this.welcomeLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // stateLabel
            // 
            this.stateLabel.AutoSize = true;
            this.stateLabel.Location = new System.Drawing.Point(245, 193);
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Size = new System.Drawing.Size(49, 17);
            this.stateLabel.TabIndex = 2;
            this.stateLabel.Text = "*state*";
            // 
            // gumButton
            // 
            this.gumButton.AutoSize = true;
            this.gumButton.Checked = true;
            this.gumButton.Location = new System.Drawing.Point(6, 19);
            this.gumButton.Name = "gumButton";
            this.gumButton.Size = new System.Drawing.Size(108, 21);
            this.gumButton.TabIndex = 3;
            this.gumButton.TabStop = true;
            this.gumButton.Text = "$0.50 - Gum";
            this.gumButton.UseVisualStyleBackColor = true;
            // 
            // granolaButton
            // 
            this.granolaButton.AutoSize = true;
            this.granolaButton.Location = new System.Drawing.Point(6, 41);
            this.granolaButton.Name = "granolaButton";
            this.granolaButton.Size = new System.Drawing.Size(129, 21);
            this.granolaButton.TabIndex = 4;
            this.granolaButton.TabStop = true;
            this.granolaButton.Text = "$0.75 - Granola";
            this.granolaButton.UseVisualStyleBackColor = true;
            // 
            // selectButton
            // 
            this.selectButton.Location = new System.Drawing.Point(424, 85);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(132, 37);
            this.selectButton.TabIndex = 5;
            this.selectButton.Text = "Confirm Selection";
            this.selectButton.UseVisualStyleBackColor = true;
            // 
            // quarterButton
            // 
            this.quarterButton.Location = new System.Drawing.Point(424, 46);
            this.quarterButton.Name = "quarterButton";
            this.quarterButton.Size = new System.Drawing.Size(132, 33);
            this.quarterButton.TabIndex = 6;
            this.quarterButton.Text = "Insert Quarter";
            this.quarterButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(424, 141);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 33);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // moneyLabel
            // 
            this.moneyLabel.AutoSize = true;
            this.moneyLabel.Location = new System.Drawing.Point(562, 54);
            this.moneyLabel.Name = "moneyLabel";
            this.moneyLabel.Size = new System.Drawing.Size(84, 17);
            this.moneyLabel.TabIndex = 8;
            this.moneyLabel.Text = "Total: $0.00";
            // 
            // outputLabel
            // 
            this.outputLabel.AutoSize = true;
            this.outputLabel.Location = new System.Drawing.Point(245, 225);
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Size = new System.Drawing.Size(58, 17);
            this.outputLabel.TabIndex = 9;
            this.outputLabel.Text = "*output*";
            // 
            // vendingBox
            // 
            this.vendingBox.Controls.Add(this.gumButton);
            this.vendingBox.Controls.Add(this.granolaButton);
            this.vendingBox.Location = new System.Drawing.Point(273, 63);
            this.vendingBox.Name = "vendingBox";
            this.vendingBox.Size = new System.Drawing.Size(145, 68);
            this.vendingBox.TabIndex = 10;
            this.vendingBox.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.vendingBox);
            this.Controls.Add(this.outputLabel);
            this.Controls.Add(this.moneyLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.quarterButton);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.stateLabel);
            this.Controls.Add(this.welcomeLabel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.vendingBox.ResumeLayout(false);
            this.vendingBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label welcomeLabel;
        private System.Windows.Forms.Label stateLabel;
        private System.Windows.Forms.RadioButton gumButton;
        private System.Windows.Forms.RadioButton granolaButton;
        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.Button quarterButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label moneyLabel;
        private System.Windows.Forms.Label outputLabel;
        private System.Windows.Forms.GroupBox vendingBox;
    }
}


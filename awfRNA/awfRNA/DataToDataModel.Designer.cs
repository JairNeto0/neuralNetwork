﻿namespace awfRNA
{
    partial class DataToDataModel
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
            label1 = new Label();
            label2 = new Label();
            tbDataPath = new TextBox();
            tbDataModelPath = new TextBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(66, 19);
            label1.Name = "label1";
            label1.Size = new Size(88, 25);
            label1.TabIndex = 0;
            label1.Text = "Data Path";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(66, 100);
            label2.Name = "label2";
            label2.Size = new Size(144, 25);
            label2.TabIndex = 1;
            label2.Text = "Data Model Path";
            // 
            // tbDataPath
            // 
            tbDataPath.Location = new Point(160, 19);
            tbDataPath.Name = "tbDataPath";
            tbDataPath.Size = new Size(150, 31);
            tbDataPath.TabIndex = 2;
            // 
            // tbDataModelPath
            // 
            tbDataModelPath.Location = new Point(216, 94);
            tbDataModelPath.Name = "tbDataModelPath";
            tbDataModelPath.Size = new Size(150, 31);
            tbDataModelPath.TabIndex = 3;
            // 
            // button1
            // 
            button1.Location = new Point(443, 91);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 4;
            button1.Text = "Converter";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // DataToDataModel
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(572, 158);
            Controls.Add(button1);
            Controls.Add(tbDataModelPath);
            Controls.Add(tbDataPath);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "DataToDataModel";
            Text = "DataToDataModel";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox tbDataPath;
        private TextBox tbDataModelPath;
        private Button button1;
    }
}
﻿namespace MEAClosedLoop
{
  partial class FormCreateExpOpt
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
      this.Create = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.ExpTarget = new System.Windows.Forms.TextBox();
      this.AuthorName = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.ExpName = new System.Windows.Forms.TextBox();
      this.button1 = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // Create
      // 
      this.Create.Location = new System.Drawing.Point(73, 229);
      this.Create.Name = "Create";
      this.Create.Size = new System.Drawing.Size(75, 23);
      this.Create.TabIndex = 0;
      this.Create.Text = "Create";
      this.Create.UseVisualStyleBackColor = true;
      this.Create.Click += new System.EventHandler(this.Create_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(10, 84);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(55, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Exp target";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(10, 7);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(38, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Author";
      // 
      // ExpTarget
      // 
      this.ExpTarget.Location = new System.Drawing.Point(72, 81);
      this.ExpTarget.Multiline = true;
      this.ExpTarget.Name = "ExpTarget";
      this.ExpTarget.Size = new System.Drawing.Size(287, 142);
      this.ExpTarget.TabIndex = 2;
      // 
      // AuthorName
      // 
      this.AuthorName.AllowDrop = true;
      this.AuthorName.Location = new System.Drawing.Point(72, 4);
      this.AuthorName.Name = "AuthorName";
      this.AuthorName.Size = new System.Drawing.Size(287, 20);
      this.AuthorName.TabIndex = 3;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(10, 33);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(56, 13);
      this.label3.TabIndex = 1;
      this.label3.Text = "Exp Name";
      // 
      // ExpName
      // 
      this.ExpName.Location = new System.Drawing.Point(72, 30);
      this.ExpName.Multiline = true;
      this.ExpName.Name = "ExpName";
      this.ExpName.Size = new System.Drawing.Size(287, 45);
      this.ExpName.TabIndex = 2;
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(154, 229);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 23);
      this.button1.TabIndex = 0;
      this.button1.Text = "Cancel";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // FormCreateExpOpt
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(366, 253);
      this.Controls.Add(this.AuthorName);
      this.Controls.Add(this.ExpName);
      this.Controls.Add(this.ExpTarget);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.Create);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "FormCreateExpOpt";
      this.Text = "CreateExpOpt";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button Create;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    public System.Windows.Forms.TextBox ExpTarget;
    public System.Windows.Forms.TextBox AuthorName;
    private System.Windows.Forms.Label label3;
    public System.Windows.Forms.TextBox ExpName;
    private System.Windows.Forms.Button button1;
  }
}
namespace SqlFormatter_Demo
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
            this.TextBoxOriginalSQL = new System.Windows.Forms.RichTextBox();
            this.TextBoxFormattedSQL = new System.Windows.Forms.RichTextBox();
            this.ButtonFormat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TextBoxOriginalSQL
            // 
            this.TextBoxOriginalSQL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxOriginalSQL.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxOriginalSQL.Location = new System.Drawing.Point(12, 37);
            this.TextBoxOriginalSQL.Name = "TextBoxOriginalSQL";
            this.TextBoxOriginalSQL.Size = new System.Drawing.Size(934, 222);
            this.TextBoxOriginalSQL.TabIndex = 0;
            this.TextBoxOriginalSQL.Text = "SELECT * FROM TEST INNER JOIN SUBTEST ON TEST.ID_TEST = SUBTEST.ID_TEST";
            // 
            // TextBoxFormattedSQL
            // 
            this.TextBoxFormattedSQL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxFormattedSQL.BackColor = System.Drawing.SystemColors.Window;
            this.TextBoxFormattedSQL.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.TextBoxFormattedSQL.Location = new System.Drawing.Point(12, 272);
            this.TextBoxFormattedSQL.Name = "TextBoxFormattedSQL";
            this.TextBoxFormattedSQL.ReadOnly = true;
            this.TextBoxFormattedSQL.Size = new System.Drawing.Size(934, 217);
            this.TextBoxFormattedSQL.TabIndex = 1;
            this.TextBoxFormattedSQL.Text = "";
            // 
            // ButtonFormat
            // 
            this.ButtonFormat.Location = new System.Drawing.Point(12, 8);
            this.ButtonFormat.Name = "ButtonFormat";
            this.ButtonFormat.Size = new System.Drawing.Size(75, 23);
            this.ButtonFormat.TabIndex = 2;
            this.ButtonFormat.Text = "Format";
            this.ButtonFormat.UseVisualStyleBackColor = true;
            this.ButtonFormat.Click += new System.EventHandler(this.ButtonFormat_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 501);
            this.Controls.Add(this.ButtonFormat);
            this.Controls.Add(this.TextBoxFormattedSQL);
            this.Controls.Add(this.TextBoxOriginalSQL);
            this.Name = "Form1";
            this.Text = "SQL Formatter";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox TextBoxOriginalSQL;
        private System.Windows.Forms.RichTextBox TextBoxFormattedSQL;
        private System.Windows.Forms.Button ButtonFormat;
    }
}


namespace XML2JSManager
{
    partial class frmMain
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
            panel1 = new Panel();
            panel2 = new Panel();
            btnEnvVariables = new Button();
            btnLoadTreeView = new Button();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            txtXML = new TextBox();
            tabPage2 = new TabPage();
            treeView = new TreeView();
            tabPage3 = new TabPage();
            txtJavaScript = new TextBox();
            btnContent = new Button();
            panel2.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1133, 27);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnContent);
            panel2.Controls.Add(btnEnvVariables);
            panel2.Controls.Add(btnLoadTreeView);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(1022, 27);
            panel2.Name = "panel2";
            panel2.Size = new Size(111, 820);
            panel2.TabIndex = 1;
            // 
            // btnEnvVariables
            // 
            btnEnvVariables.Location = new Point(15, 215);
            btnEnvVariables.Name = "btnEnvVariables";
            btnEnvVariables.Size = new Size(75, 82);
            btnEnvVariables.TabIndex = 1;
            btnEnvVariables.Text = "Environment Variables";
            btnEnvVariables.UseVisualStyleBackColor = true;
            btnEnvVariables.Click += btnEnvVariables_Click;
            // 
            // btnLoadTreeView
            // 
            btnLoadTreeView.Location = new Point(15, 86);
            btnLoadTreeView.Name = "btnLoadTreeView";
            btnLoadTreeView.Size = new Size(75, 82);
            btnLoadTreeView.TabIndex = 0;
            btnLoadTreeView.Text = "Load TreeView";
            btnLoadTreeView.UseVisualStyleBackColor = true;
            btnLoadTreeView.Click += btnLoadTreeView_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            tabControl1.Location = new Point(0, 27);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1022, 820);
            tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(txtXML);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1014, 790);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "XML";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtXML
            // 
            txtXML.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            txtXML.Location = new Point(8, 6);
            txtXML.Multiline = true;
            txtXML.Name = "txtXML";
            txtXML.Size = new Size(1000, 776);
            txtXML.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(treeView);
            tabPage2.Location = new Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1014, 790);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Tree";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // treeView
            // 
            treeView.Location = new Point(8, 6);
            treeView.Name = "treeView";
            treeView.Size = new Size(1000, 776);
            treeView.TabIndex = 0;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(txtJavaScript);
            tabPage3.Location = new Point(4, 26);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(1014, 790);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "JScript";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtJavaScript
            // 
            txtJavaScript.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            txtJavaScript.Location = new Point(8, 13);
            txtJavaScript.Multiline = true;
            txtJavaScript.Name = "txtJavaScript";
            txtJavaScript.Size = new Size(1003, 698);
            txtJavaScript.TabIndex = 0;
            // 
            // btnContent
            // 
            btnContent.Location = new Point(15, 391);
            btnContent.Name = "btnContent";
            btnContent.Size = new Size(75, 82);
            btnContent.TabIndex = 2;
            btnContent.Text = "Variable Content";
            btnContent.UseVisualStyleBackColor = true;
            btnContent.Click += btnContent_Click;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1133, 847);
            Controls.Add(tabControl1);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "frmMain";
            Text = "XML2JavaScript";
            panel2.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TextBox txtXML;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TextBox txtJavaScript;
        private Button btnLoadTreeView;
        private TreeView treeView;
        private Button btnEnvVariables;
        private Button btnContent;
    }
}
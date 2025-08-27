namespace ArduWare
{
    partial class MainUI
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainUI));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.connectionStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusRX = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusTX = new System.Windows.Forms.ToolStripStatusLabel();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.txtConsole = new System.Windows.Forms.RichTextBox();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.buttonConnect = new System.Windows.Forms.ToolStripButton();
            this.buttonHelp = new System.Windows.Forms.ToolStripButton();
            this.tab_addOrRemove = new System.Windows.Forms.TabControl();
            this.AddOrRemove = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.progressBarDataLoad = new System.Windows.Forms.ProgressBar();
            this.statusStrip1.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.tab_addOrRemove.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionStatus,
            this.statusRX,
            this.statusTX});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // connectionStatus
            // 
            this.connectionStatus.BackColor = System.Drawing.Color.Red;
            this.connectionStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.connectionStatus, "connectionStatus");
            this.connectionStatus.ForeColor = System.Drawing.Color.White;
            this.connectionStatus.Name = "connectionStatus";
            this.connectionStatus.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            // 
            // statusRX
            // 
            this.statusRX.ActiveLinkColor = System.Drawing.Color.Lime;
            this.statusRX.BackColor = System.Drawing.Color.Transparent;
            this.statusRX.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.statusRX.Margin = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.statusRX.Name = "statusRX";
            resources.ApplyResources(this.statusRX, "statusRX");
            // 
            // statusTX
            // 
            this.statusTX.ActiveLinkColor = System.Drawing.Color.Red;
            this.statusTX.BackColor = System.Drawing.Color.Transparent;
            this.statusTX.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.statusTX.Margin = new System.Windows.Forms.Padding(0);
            this.statusTX.Name = "statusTX";
            resources.ApplyResources(this.statusTX, "statusTX");
            // 
            // serialPort1
            // 
            this.serialPort1.PortName = "COM3";
            // 
            // txtConsole
            // 
            resources.ApplyResources(this.txtConsole, "txtConsole");
            this.txtConsole.BackColor = System.Drawing.Color.Black;
            this.txtConsole.ForeColor = System.Drawing.Color.White;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ReadOnly = true;
            // 
            // toolStrip3
            // 
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonConnect,
            this.buttonHelp});
            resources.ApplyResources(this.toolStrip3, "toolStrip3");
            this.toolStrip3.Name = "toolStrip3";
            // 
            // buttonConnect
            // 
            this.buttonConnect.BackColor = System.Drawing.Color.Transparent;
            this.buttonConnect.Image = global::ArduWare.Properties.Resources.connect_icon;
            resources.ApplyResources(this.buttonConnect, "buttonConnect");
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonHelp
            // 
            resources.ApplyResources(this.buttonHelp, "buttonHelp");
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
            // 
            // tab_addOrRemove
            // 
            resources.ApplyResources(this.tab_addOrRemove, "tab_addOrRemove");
            this.tab_addOrRemove.Controls.Add(this.AddOrRemove);
            this.tab_addOrRemove.Multiline = true;
            this.tab_addOrRemove.Name = "tab_addOrRemove";
            this.tab_addOrRemove.SelectedIndex = 0;
            // 
            // AddOrRemove
            // 
            this.AddOrRemove.BackColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.AddOrRemove, "AddOrRemove");
            this.AddOrRemove.Name = "AddOrRemove";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // progressBarDataLoad
            // 
            this.progressBarDataLoad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.progressBarDataLoad, "progressBarDataLoad");
            this.progressBarDataLoad.Name = "progressBarDataLoad";
            // 
            // MainUI
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.progressBarDataLoad);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.tab_addOrRemove);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.toolStrip3);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "MainUI";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.tab_addOrRemove.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel connectionStatus;
        private System.Windows.Forms.ToolStripStatusLabel statusRX;
        private System.Windows.Forms.ToolStripStatusLabel statusTX;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.RichTextBox txtConsole;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton buttonConnect;
        private System.Windows.Forms.TabControl tab_addOrRemove;
        private System.Windows.Forms.TabPage AddOrRemove;
        private System.Windows.Forms.ToolStripButton buttonHelp;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ProgressBar progressBarDataLoad;
    }
}


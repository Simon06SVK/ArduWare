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
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.txtConsole = new System.Windows.Forms.RichTextBox();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.progressBarDataLoad = new System.Windows.Forms.ProgressBar();
            this.AddOrRemove = new System.Windows.Forms.TabPage();
            this.buttonAddFP = new System.Windows.Forms.Button();
            this.buttonRemoveFP = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tab_Settings = new System.Windows.Forms.TabControl();
            this.buttonConnect = new System.Windows.Forms.ToolStripButton();
            this.buttonCheckUpdates = new System.Windows.Forms.ToolStripButton();
            this.buttonSettings = new System.Windows.Forms.ToolStripButton();
            this.buttonHelp = new System.Windows.Forms.ToolStripButton();
            this.buttonReloadData = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.AddOrRemove.SuspendLayout();
            this.tab_Settings.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionStatus,
            this.statusRX});
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
            this.buttonCheckUpdates,
            this.buttonSettings,
            this.buttonHelp});
            resources.ApplyResources(this.toolStrip3, "toolStrip3");
            this.toolStrip3.Name = "toolStrip3";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // progressBarDataLoad
            // 
            this.progressBarDataLoad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.progressBarDataLoad, "progressBarDataLoad");
            this.progressBarDataLoad.Name = "progressBarDataLoad";
            // 
            // AddOrRemove
            // 
            this.AddOrRemove.BackColor = System.Drawing.Color.WhiteSmoke;
            this.AddOrRemove.Controls.Add(this.buttonReloadData);
            this.AddOrRemove.Controls.Add(this.label1);
            this.AddOrRemove.Controls.Add(this.buttonRemoveFP);
            this.AddOrRemove.Controls.Add(this.buttonAddFP);
            resources.ApplyResources(this.AddOrRemove, "AddOrRemove");
            this.AddOrRemove.Name = "AddOrRemove";
            // 
            // buttonAddFP
            // 
            this.buttonAddFP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            resources.ApplyResources(this.buttonAddFP, "buttonAddFP");
            this.buttonAddFP.Name = "buttonAddFP";
            this.buttonAddFP.UseVisualStyleBackColor = true;
            this.buttonAddFP.Click += new System.EventHandler(this.buttonAddFP_Click);
            // 
            // buttonRemoveFP
            // 
            this.buttonRemoveFP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            resources.ApplyResources(this.buttonRemoveFP, "buttonRemoveFP");
            this.buttonRemoveFP.Name = "buttonRemoveFP";
            this.buttonRemoveFP.UseVisualStyleBackColor = false;
            this.buttonRemoveFP.Click += new System.EventHandler(this.buttonRemoveFP_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tab_Settings
            // 
            resources.ApplyResources(this.tab_Settings, "tab_Settings");
            this.tab_Settings.Controls.Add(this.AddOrRemove);
            this.tab_Settings.Multiline = true;
            this.tab_Settings.Name = "tab_Settings";
            this.tab_Settings.SelectedIndex = 0;
            // 
            // buttonConnect
            // 
            this.buttonConnect.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.buttonConnect, "buttonConnect");
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(2);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonCheckUpdates
            // 
            this.buttonCheckUpdates.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            resources.ApplyResources(this.buttonCheckUpdates, "buttonCheckUpdates");
            this.buttonCheckUpdates.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCheckUpdates.Name = "buttonCheckUpdates";
            this.buttonCheckUpdates.Click += new System.EventHandler(this.buttonCheckUpdates_Click);
            // 
            // buttonSettings
            // 
            resources.ApplyResources(this.buttonSettings, "buttonSettings");
            this.buttonSettings.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // buttonHelp
            // 
            resources.ApplyResources(this.buttonHelp, "buttonHelp");
            this.buttonHelp.Margin = new System.Windows.Forms.Padding(2);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
            // 
            // buttonReloadData
            // 
            this.buttonReloadData.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.buttonReloadData, "buttonReloadData");
            this.buttonReloadData.Name = "buttonReloadData";
            this.buttonReloadData.UseVisualStyleBackColor = false;
            this.buttonReloadData.Click += new System.EventHandler(this.buttonReloadData_Click);
            // 
            // MainUI
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.progressBarDataLoad);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.tab_Settings);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.toolStrip3);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "MainUI";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.AddOrRemove.ResumeLayout(false);
            this.AddOrRemove.PerformLayout();
            this.tab_Settings.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel connectionStatus;
        private System.Windows.Forms.ToolStripStatusLabel statusRX;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.RichTextBox txtConsole;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton buttonConnect;
        private System.Windows.Forms.ToolStripButton buttonHelp;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ProgressBar progressBarDataLoad;
        private System.Windows.Forms.ToolStripButton buttonCheckUpdates;
        private System.Windows.Forms.ToolStripButton buttonSettings;
        private System.Windows.Forms.TabPage AddOrRemove;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonRemoveFP;
        private System.Windows.Forms.Button buttonAddFP;
        private System.Windows.Forms.TabControl tab_Settings;
        private System.Windows.Forms.Button buttonReloadData;
    }
}


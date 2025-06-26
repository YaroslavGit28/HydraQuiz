namespace QuizApp.WinForms
{
    partial class HistoryForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this._historyDataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this._historyDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // _historyDataGridView
            // 
            this._historyDataGridView.AllowUserToAddRows = false;
            this._historyDataGridView.AllowUserToDeleteRows = false;
            this._historyDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._historyDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._historyDataGridView.ReadOnly = true;
            this._historyDataGridView.Size = new System.Drawing.Size(684, 461);
            // 
            // HistoryForm
            // 
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this._historyDataGridView);
            this.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.Name = "HistoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "История игр";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.Load += new System.EventHandler(this.HistoryForm_Load);
            this.Load += (s, e) => { if (this.WindowState == System.Windows.Forms.FormWindowState.Normal) this.CenterToScreen(); };
            ((System.ComponentModel.ISupportInitialize)(this._historyDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView _historyDataGridView;
    }
} 
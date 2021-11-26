
namespace AIS
{
    partial class UserControlStudent
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.labelFullName = new System.Windows.Forms.Label();
            this.labelId = new System.Windows.Forms.Label();
            this.labelFaculty = new System.Windows.Forms.Label();
            this.labelProgram = new System.Windows.Forms.Label();
            this.labelGroupName = new System.Windows.Forms.Label();
            this.dataGridViewSemester = new System.Windows.Forms.DataGridView();
            this.dataGridViewGrades = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSemester)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGrades)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Studento ID:";
            // 
            // labelFullName
            // 
            this.labelFullName.AutoSize = true;
            this.labelFullName.Location = new System.Drawing.Point(13, 28);
            this.labelFullName.Name = "labelFullName";
            this.labelFullName.Size = new System.Drawing.Size(115, 17);
            this.labelFullName.TabIndex = 1;
            this.labelFullName.Text = "[vardas pavarde]";
            // 
            // labelId
            // 
            this.labelId.AutoSize = true;
            this.labelId.Location = new System.Drawing.Point(101, 11);
            this.labelId.Name = "labelId";
            this.labelId.Size = new System.Drawing.Size(27, 17);
            this.labelId.TabIndex = 2;
            this.labelId.Text = "[id]";
            // 
            // labelFaculty
            // 
            this.labelFaculty.AutoSize = true;
            this.labelFaculty.Location = new System.Drawing.Point(13, 45);
            this.labelFaculty.Name = "labelFaculty";
            this.labelFaculty.Size = new System.Drawing.Size(77, 17);
            this.labelFaculty.TabIndex = 3;
            this.labelFaculty.Text = "[fakultetas]";
            // 
            // labelProgram
            // 
            this.labelProgram.AutoSize = true;
            this.labelProgram.Location = new System.Drawing.Point(13, 62);
            this.labelProgram.Name = "labelProgram";
            this.labelProgram.Size = new System.Drawing.Size(122, 17);
            this.labelProgram.TabIndex = 4;
            this.labelProgram.Text = "[studiju programa]";
            // 
            // labelGroupName
            // 
            this.labelGroupName.AutoSize = true;
            this.labelGroupName.Location = new System.Drawing.Point(13, 79);
            this.labelGroupName.Name = "labelGroupName";
            this.labelGroupName.Size = new System.Drawing.Size(143, 17);
            this.labelGroupName.TabIndex = 5;
            this.labelGroupName.Text = "[grupes pavadinimas]";
            // 
            // dataGridViewSemester
            // 
            this.dataGridViewSemester.AllowUserToAddRows = false;
            this.dataGridViewSemester.AllowUserToDeleteRows = false;
            this.dataGridViewSemester.AllowUserToResizeColumns = false;
            this.dataGridViewSemester.AllowUserToResizeRows = false;
            this.dataGridViewSemester.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSemester.ColumnHeadersVisible = false;
            this.dataGridViewSemester.Location = new System.Drawing.Point(3, 111);
            this.dataGridViewSemester.MultiSelect = false;
            this.dataGridViewSemester.Name = "dataGridViewSemester";
            this.dataGridViewSemester.ReadOnly = true;
            this.dataGridViewSemester.RowHeadersVisible = false;
            this.dataGridViewSemester.RowHeadersWidth = 51;
            this.dataGridViewSemester.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewSemester.RowTemplate.Height = 24;
            this.dataGridViewSemester.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSemester.Size = new System.Drawing.Size(237, 336);
            this.dataGridViewSemester.TabIndex = 6;
            this.dataGridViewSemester.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSemester_CellClick);
            // 
            // dataGridViewGrades
            // 
            this.dataGridViewGrades.AllowUserToAddRows = false;
            this.dataGridViewGrades.AllowUserToDeleteRows = false;
            this.dataGridViewGrades.AllowUserToResizeColumns = false;
            this.dataGridViewGrades.AllowUserToResizeRows = false;
            this.dataGridViewGrades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewGrades.Location = new System.Drawing.Point(246, 111);
            this.dataGridViewGrades.MultiSelect = false;
            this.dataGridViewGrades.Name = "dataGridViewGrades";
            this.dataGridViewGrades.ReadOnly = true;
            this.dataGridViewGrades.RowHeadersVisible = false;
            this.dataGridViewGrades.RowHeadersWidth = 51;
            this.dataGridViewGrades.RowTemplate.Height = 24;
            this.dataGridViewGrades.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewGrades.Size = new System.Drawing.Size(688, 336);
            this.dataGridViewGrades.TabIndex = 7;
            // 
            // UserControlStudent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridViewGrades);
            this.Controls.Add(this.dataGridViewSemester);
            this.Controls.Add(this.labelGroupName);
            this.Controls.Add(this.labelProgram);
            this.Controls.Add(this.labelFaculty);
            this.Controls.Add(this.labelId);
            this.Controls.Add(this.labelFullName);
            this.Controls.Add(this.label1);
            this.Name = "UserControlStudent";
            this.Size = new System.Drawing.Size(937, 450);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSemester)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGrades)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelFullName;
        private System.Windows.Forms.Label labelId;
        private System.Windows.Forms.Label labelFaculty;
        private System.Windows.Forms.Label labelProgram;
        private System.Windows.Forms.Label labelGroupName;
        private System.Windows.Forms.DataGridView dataGridViewSemester;
        private System.Windows.Forms.DataGridView dataGridViewGrades;
    }
}

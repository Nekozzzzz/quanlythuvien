namespace BookManagement._1.Forms
{
    partial class ReaderListPopupForm
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
            listReader = new DataGridView();
            groupBox1 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)listReader).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // listReader
            // 
            listReader.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            listReader.Location = new Point(6, 22);
            listReader.Name = "listReader";
            listReader.Size = new Size(572, 283);
            listReader.TabIndex = 0;
            listReader.CellMouseDoubleClick += readerBook_CellMouseDoubleClick;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(listReader);
            groupBox1.Location = new Point(-1, -1);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(584, 311);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Danh sách Độc giả";
            // 
            // ReaderListPopupForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 311);
            Controls.Add(groupBox1);
            Name = "ReaderListPopupForm";
            Text = "BookListPopupForm";
            ((System.ComponentModel.ISupportInitialize)listReader).EndInit();
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView listReader;
        private GroupBox groupBox1;
    }
}
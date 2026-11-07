namespace BookManagement._1.Forms
{
    partial class BookListPopupForm
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
            listBook = new DataGridView();
            groupBox1 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)listBook).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // listBook
            // 
            listBook.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            listBook.Location = new Point(6, 22);
            listBook.Name = "listBook";
            listBook.Size = new Size(572, 283);
            listBook.TabIndex = 0;
            listBook.CellMouseDoubleClick += listBook_CellMouseDoubleClick;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(listBook);
            groupBox1.Location = new Point(-1, -1);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(584, 311);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Danh sách Sách";
            // 
            // BookListPopupForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 311);
            Controls.Add(groupBox1);
            Name = "BookListPopupForm";
            Text = "BookListPopupForm";
            ((System.ComponentModel.ISupportInitialize)listBook).EndInit();
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView listBook;
        private GroupBox groupBox1;
    }
}
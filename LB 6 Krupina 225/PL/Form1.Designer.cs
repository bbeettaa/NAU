
namespace PL
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.viewList_contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddObjectStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox3 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.WorckWithGroups_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AllObjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TfansferToGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.створитиГруппуToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.створитиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.переToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RenameGroupToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.RenameThisGroup_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.операціїЗіСтудентамиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CountPercentOfFirstCourseArrivalsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поселенняВГуртожитокПриToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.viewList_contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // viewList_contextMenuStrip
            // 
            this.viewList_contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddObjectStripMenuItem,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.WorckWithGroups_ToolStripMenuItem,
            this.TfansferToGroupToolStripMenuItem,
            this.створитиГруппуToolStripMenuItem1,
            this.переToolStripMenuItem,
            this.toolStripMenuItem4,
            this.операціїЗіСтудентамиToolStripMenuItem});
            this.viewList_contextMenuStrip.Name = "viewList_contextMenuStrip";
            this.viewList_contextMenuStrip.Size = new System.Drawing.Size(222, 170);
            this.viewList_contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.viewList_contextMenuStrip_Opening);
            // 
            // AddObjectStripMenuItem
            // 
            this.AddObjectStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox3});
            this.AddObjectStripMenuItem.Name = "AddObjectStripMenuItem";
            this.AddObjectStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.AddObjectStripMenuItem.Text = "Добавити об\'єкт:";
            // 
            // toolStripComboBox3
            // 
            this.toolStripComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.toolStripComboBox3.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.toolStripComboBox3.Name = "toolStripComboBox3";
            this.toolStripComboBox3.Size = new System.Drawing.Size(121, 150);
            this.toolStripComboBox3.Click += new System.EventHandler(this.toolStripComboBox3_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(221, 22);
            this.toolStripMenuItem2.Text = "Видалити об\'єкт";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.dellObject_toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(218, 6);
            // 
            // WorckWithGroups_ToolStripMenuItem
            // 
            this.WorckWithGroups_ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AllObjectsToolStripMenuItem});
            this.WorckWithGroups_ToolStripMenuItem.Name = "WorckWithGroups_ToolStripMenuItem";
            this.WorckWithGroups_ToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.WorckWithGroups_ToolStripMenuItem.Text = "Відображення груп";
            // 
            // AllObjectsToolStripMenuItem
            // 
            this.AllObjectsToolStripMenuItem.Name = "AllObjectsToolStripMenuItem";
            this.AllObjectsToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.AllObjectsToolStripMenuItem.Text = "Всі об\'єкті";
            this.AllObjectsToolStripMenuItem.Click += new System.EventHandler(this.ShowAllObjects_HideAllGroup_Click);
            // 
            // TfansferToGroupToolStripMenuItem
            // 
            this.TfansferToGroupToolStripMenuItem.Name = "TfansferToGroupToolStripMenuItem";
            this.TfansferToGroupToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.TfansferToGroupToolStripMenuItem.Text = "Перенести об\'єкт до групи";
            // 
            // створитиГруппуToolStripMenuItem1
            // 
            this.створитиГруппуToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1,
            this.створитиToolStripMenuItem});
            this.створитиГруппуToolStripMenuItem1.Name = "створитиГруппуToolStripMenuItem1";
            this.створитиГруппуToolStripMenuItem1.Size = new System.Drawing.Size(221, 22);
            this.створитиГруппуToolStripMenuItem1.Text = "Створити группу";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 23);
            // 
            // створитиToolStripMenuItem
            // 
            this.створитиToolStripMenuItem.Name = "створитиToolStripMenuItem";
            this.створитиToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.створитиToolStripMenuItem.Text = "Створити";
            this.створитиToolStripMenuItem.Click += new System.EventHandler(this.CreateGroup_ToolStripMenuItem_Click);
            // 
            // переToolStripMenuItem
            // 
            this.переToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RenameGroupToolStripTextBox,
            this.RenameThisGroup_ToolStripMenuItem});
            this.переToolStripMenuItem.Name = "переToolStripMenuItem";
            this.переToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.переToolStripMenuItem.Text = "Перейменувати групу";
            // 
            // RenameGroupToolStripTextBox
            // 
            this.RenameGroupToolStripTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RenameGroupToolStripTextBox.Name = "RenameGroupToolStripTextBox";
            this.RenameGroupToolStripTextBox.Size = new System.Drawing.Size(100, 23);
            // 
            // RenameThisGroup_ToolStripMenuItem
            // 
            this.RenameThisGroup_ToolStripMenuItem.Name = "RenameThisGroup_ToolStripMenuItem";
            this.RenameThisGroup_ToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.RenameThisGroup_ToolStripMenuItem.Text = "Перейменувати цю групу";
            this.RenameThisGroup_ToolStripMenuItem.Click += new System.EventHandler(this.RenameThisGroup_ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(218, 6);
            // 
            // операціїЗіСтудентамиToolStripMenuItem
            // 
            this.операціїЗіСтудентамиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CountPercentOfFirstCourseArrivalsToolStripMenuItem,
            this.поселенняВГуртожитокПриToolStripMenuItem});
            this.операціїЗіСтудентамиToolStripMenuItem.Name = "операціїЗіСтудентамиToolStripMenuItem";
            this.операціїЗіСтудентамиToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.операціїЗіСтудентамиToolStripMenuItem.Text = "Операції зі студентами";
            // 
            // CountPercentOfFirstCourseArrivalsToolStripMenuItem
            // 
            this.CountPercentOfFirstCourseArrivalsToolStripMenuItem.Name = "CountPercentOfFirstCourseArrivalsToolStripMenuItem";
            this.CountPercentOfFirstCourseArrivalsToolStripMenuItem.Size = new System.Drawing.Size(380, 22);
            this.CountPercentOfFirstCourseArrivalsToolStripMenuItem.Text = "Відсоток студентів 1-го курсу, приїхавших з інших міст.";
            this.CountPercentOfFirstCourseArrivalsToolStripMenuItem.Click += new System.EventHandler(this.CountPercentOfFirstCourseArrialStudentsToolStripMenuItem_Click);
            // 
            // поселенняВГуртожитокПриToolStripMenuItem
            // 
            this.поселенняВГуртожитокПриToolStripMenuItem.Name = "поселенняВГуртожитокПриToolStripMenuItem";
            this.поселенняВГуртожитокПриToolStripMenuItem.Size = new System.Drawing.Size(380, 22);
            this.поселенняВГуртожитокПриToolStripMenuItem.Text = "Поселення в гуртожиток приїхавших з іншого міста";
            this.поселенняВГуртожитокПриToolStripMenuItem.Click += new System.EventHandler(this.HostelArrivalStudToolStripMenuItem_Click);
            // 
            // listView
            // 
            this.listView.AutoArrange = false;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView.ContextMenuStrip = this.viewList_contextMenuStrip;
            this.listView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listView.GridLines = true;
            this.listView.HideSelection = false;
            this.listView.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.listView.LabelWrap = false;
            this.listView.Location = new System.Drawing.Point(12, 38);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listView.Size = new System.Drawing.Size(325, 353);
            this.listView.TabIndex = 11;
            this.listView.TileSize = new System.Drawing.Size(228, 18);
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Tile;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView.Click += new System.EventHandler(this.listView_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 106;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(358, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Дії об\'єкту";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.comboBox1.Location = new System.Drawing.Point(361, 180);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.comboBox1.Size = new System.Drawing.Size(90, 24);
            this.comboBox1.TabIndex = 6;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(461, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Тип об\'єкту";
            // 
            // listBox2
            // 
            this.listBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 16;
            this.listBox2.Location = new System.Drawing.Point(464, 38);
            this.listBox2.Name = "listBox2";
            this.listBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listBox2.Size = new System.Drawing.Size(325, 324);
            this.listBox2.TabIndex = 4;
            this.listBox2.Click += new System.EventHandler(this.listBox2_Click);
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox2.Location = new System.Drawing.Point(464, 369);
            this.textBox2.Name = "textBox2";
            this.textBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox2.Size = new System.Drawing.Size(325, 22);
            this.textBox2.TabIndex = 2;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBox1.Size = new System.Drawing.Size(325, 22);
            this.textBox1.TabIndex = 1;
            this.textBox1.Tag = "Пошук об\'єкту";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(801, 410);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.comboBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.viewList_contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip viewList_contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem AddObjectStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox3;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem WorckWithGroups_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem створитиГруппуToolStripMenuItem1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripMenuItem створитиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TfansferToGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem операціїЗіСтудентамиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CountPercentOfFirstCourseArrivalsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поселенняВГуртожитокПриToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem переToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox RenameGroupToolStripTextBox;
        private System.Windows.Forms.ToolStripMenuItem RenameThisGroup_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AllObjectsToolStripMenuItem;
    }
}


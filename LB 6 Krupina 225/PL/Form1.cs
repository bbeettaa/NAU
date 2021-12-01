using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;

using static System.Windows.Forms.ListView;

namespace PL
{

    public partial class Form1 : Form
    {
        EntityService service = new EntityService();
        private string find = "";
        private int selectedProperty = 0;
        private int textBox2_caretka = 0;
        private bool isShowAllItemsAndIgrnoreGroups = false;

        public Form1()
        {
            InitializeComponent();

            AddObjectStripMenuItem.ShowDropDown();
            AddObjectStripMenuItem.HideDropDown();

            foreach (var obj in EntityService.GetAssemblyTypes())//!1
                toolStripComboBox3.Items.Add(obj.Name.ToString());


            InitializeListView();
        }


        private void InitializeListView()//rename me please 
        {
            foreach(var obj in listView1.Groups)
            service.groups.Add(obj.ToString());

            listView1.Items.Clear();
            listView1.Groups.Clear();

            service.FindObjects(find);

            List<String> names = service.GetObjNames();
            int  groupNumber = 0;

            foreach (var obj in service.GetGroupsOfObj())
                service.groups.Add(obj);

            Hashtable groupsAndNames = service.GetTableOfObjectAndGroup();

            List<String> groups = service.groups;
            service.groups = service.groups.GroupBy(x => x).Where(g => g.Count() > 0 && g != null).Select(g => g.Key).ToList();
            groups = service.groups.GroupBy(x => x).Where(g => g.Count() > 0).Select(g => g.Key).ToList();
            groups.ForEach(x => listView1.Groups.Add(x, x));

            CheckGroupButtonsInit(ref groups);
            TransferToGroupButtonInit(ref groups);

            int indexOfGroup = 0;
            for (int i = 0; i < names.Count; i++)
            {
                 groupNumber = groups.IndexOf(groupsAndNames[service.objList[i]].ToString());

                if ( groupNumber == -1 && isShowAllItemsAndIgrnoreGroups== false)
                {
                    if(i< service.objList.Count)
                    service.objList.RemoveAt(i);
                    names.RemoveAt(i);
                    i -=1;
                    continue;
                }
                if (i< names.Count)
                {
                    listView1.Items.Add($"{names[i]}");

                    if (isShowAllItemsAndIgrnoreGroups)
                        listView1.Items[indexOfGroup++].Group = null;
                    else
                        listView1.Items[indexOfGroup++].Group = listView1.Groups[groupNumber];
                }
            }
        }


        private void CheckGroupButtonsInit(ref List<String> groups)
        {
            InitializeToolStrips(ref groups, WorckWithGroups_ToolStripMenuItem, true, new EventHandler(this.CheckGroup_Click));
            
            try
            {
                for (int i = 1; i < WorckWithGroups_ToolStripMenuItem.DropDownItems.Count; i++)
                {
                    if (!(WorckWithGroups_ToolStripMenuItem.DropDownItems[i] as ToolStripMenuItem).Checked)
                       groups[i-1] = null;
                }
            }
            catch (Exception e) { }
        }

        
        private void TransferToGroupButtonInit(ref List<String> groups)
        {
            InitializeToolStrips(ref groups, TfansferToGroupToolStripMenuItem,false,new EventHandler(this.TransferToGroup_Click));
        }

        private void InitializeToolStrips(ref List<String> groups, ToolStripMenuItem menuItem, bool isChecked, EventHandler eventHandler)
        {
            bool isExistedToolStrop = false;

            foreach (var group in groups)
            {
                foreach (var toolStrip in menuItem.DropDownItems)
                    if (toolStrip.ToString() == group)
                        isExistedToolStrop = true;

                if (!isExistedToolStrop && group != null)
                {
                    menuItem.DropDownItems.Add(group, null, eventHandler);
                    (menuItem.DropDownItems[menuItem.DropDownItems.Count - 1] as ToolStripMenuItem).Checked = isChecked;
                }
                isExistedToolStrop = false;
            }
        }

        private void initializeFieldOfProperty() 
        {
            textBox2.Text = service.GetObjValueProp(selectedProperty);
            listBox2.Items.Clear();


            List<String> objNameProp = service.GetObjNameProps();
            List<String> objValueProp = service.GetObjValueProp();

            label1.Text = $"Тип об'єкту       {objNameProp[0]}";
            objNameProp.RemoveAt(0);

            for (int i = 0; i < objNameProp.Count; i++)
            {
                listBox2.Items.Add($"{objNameProp[i].PadRight(15)}\t{objValueProp[i].PadLeft(0)}");
            }

            if (selectedProperty >= listBox2.Items.Count)
                selectedProperty = listBox2.Items.Count-1;
            listBox2.SelectedIndex = selectedProperty;

            comboBox1.Items.Clear();
            foreach (string str in service.GetMethodsInfo())
                comboBox1.Items.Add(str);
        }

        private void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedProperty = listBox2.SelectedIndex;
            service.PropertyNum = selectedProperty;
            textBox2.Text = service.GetObjValueProp(selectedProperty);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
       {
            service.PropertyNum = selectedProperty;
            if (textBox2.SelectionStart!=0)
            textBox2_caretka = textBox2.SelectionStart + textBox2.SelectionLength;

            if (!service.InputInfoAndSaveObj(textBox2.Text))
                textBox2.Text = service.GetObjValueProp(selectedProperty);
            
            initializeFieldOfProperty();
            textBox2.SelectionStart = textBox2_caretka;

            InitializeListView();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            service.WorckWithMethods(comboBox1.SelectedIndex, true);
            initializeFieldOfProperty();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            find = textBox1.Text;
            InitializeListView();
        }

        private void toolStripComboBox3_Click(object sender, EventArgs e)
        {
            if(toolStripComboBox3.SelectedIndex <0) return;
            service.AppendObjectInDatabase(toolStripComboBox3.SelectedIndex);
            InitializeListView();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
                service.IndexOfChosenObj= listView1.SelectedIndices[0];          

            initializeFieldOfProperty();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            service.DeleteObj();
            InitializeListView();
        }

        private void viewList_contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            InitializeListView();

            ListViewGroupCollection groups = listView1.Groups;
        }

        private void CreateGroup_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String name = toolStripTextBox1.Text;
            listView1.Groups.Add(name, name);
            listView1.Items.Add("");
            listView1.Items[listView1.Items.Count-1].Group = listView1.Groups[listView1.Groups.Count - 1];
            InitializeListView();
        }

        private void CheckGroup_Click(object sender, EventArgs e)
        {
            if ((sender as ToolStripMenuItem).Checked)
                (sender as ToolStripMenuItem).Checked = false;
            else
                (sender as ToolStripMenuItem).Checked = true;

            isShowAllItemsAndIgrnoreGroups = false;

            InitializeListView();
        }

        private void ShowAllObjects_HideAllGroup_Click(object sender, EventArgs e)
        {
            foreach (var obj in WorckWithGroups_ToolStripMenuItem.DropDownItems)
                (obj as ToolStripMenuItem).Checked = false;

            isShowAllItemsAndIgrnoreGroups = true;

                    InitializeListView();
        }
        private void TransferToGroup_Click(object sender, EventArgs e)
        {
            int indexGroup = service.groups.IndexOf((sender as ToolStripMenuItem).Text);
            service.SetGroupToCurrentObject_andSave(service.groups[indexGroup]);
            InitializeListView();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            InitializeListView();
        }

        private void RenameThisGroup_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            service.RenameGroupOfCurrentObject(RenameGroupToolStripTextBox.Text);
            service.SaveObjList();
            InitializeListView();
        }





        private void CountPercentOfFirstCourseArrialStudentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = service.CountPercentOfFirstCourseArrivalsStudent();
            string caption = CountPercentOfFirstCourseArrivalsToolStripMenuItem.Text;
            MessageBoxButtons buttons = MessageBoxButtons.OK;

            MessageBox.Show(message, caption, buttons);
        }

        private void HostelArrivalStudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            service.HostelArrivalStud();
            service.SaveObjList();
            InitializeListView();
        }


    }
}

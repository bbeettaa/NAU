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
        private EntityService service = new EntityService();
        private List<Object> categories;

        private string find = "";
        private int selectedProperty = 0;
        private int textBox2_caretka = 0;
        private bool isShowAllItemsAndIgrnoreGroups = false;

        public Form1()
        {
            InitializeComponent();

            AddObjectStripMenuItem.ShowDropDown();
            AddObjectStripMenuItem.HideDropDown();

            foreach (var obj in EntityService.GetAssemblyTypes())
                toolStripComboBox3.Items.Add(obj.Name.ToString());

            InitializeListView();
        }
        
        private void InitializeListView()
        {
            //foreach(var obj in listView.Groups)
            //service.groups.Add(obj.ToString());

            //listView.Items.Clear();
            //listView.Groups.Clear();

            //service.FindObjects(find);
            int  groupNumber = 0;

/*            foreach (var obj in service.GetGroupsOfObj())
                service.groups.Add(obj);*/

            Hashtable groupsAndNames = service.GetTableOfObjectAndGroup();

            List<Object> objList = service.GetFindObjects(find);
            List<String> names = service.GetObjNames(objList);

            categories = service.GetObjsCategories();
            //service.groups = service.groups.GroupBy(x => x).Where(g => g.Count() > 0 && g != null).Select(g => g.Key).ToList();
            //groups = service.groups.GroupBy(x => x).Where(g => g.Count() > 0).Select(g => g.Key).ToList();
            //groups.ForEach(x => listView.Groups.Add(x, x));

            /*for (int i =0;i <categories.Count;i+=2)
                listView.Groups.Add(categories[i].ToString(), categories[i].ToString());*/



            initGroupListView(categories);
            CheckGroupButtonsInit(ref categories);
            TransferToGroupButtonInit(ref categories);

            //clearListView();

            int indexOfGroup = 0;
            for (int i = 0; i < names.Count; i++)
            {
                groupNumber = categories.IndexOf(groupsAndNames[objList[i]].ToString());

                if (groupNumber == -1 && isShowAllItemsAndIgrnoreGroups == false)
                {
                    names.RemoveAt(i);
                    i -= 1;
                    continue;
                }
                if (i < names.Count)
                {
                    if ((bool)categories[groupNumber + 1] == false)
                    {
                        //names.RemoveAt(i);
                        //i--;
                       // continue;

                    }

                    if (i >= listView.Items.Count)
                        listView.Items.Add($"{names[i]}");
                    else
                        listView.Items[i].Text = $"{names[i]}";

                    if (isShowAllItemsAndIgrnoreGroups)
                        listView.Items[indexOfGroup++].Group = null;
                    else
                        listView.Items[indexOfGroup++].Group = listView.Groups[groupNumber / 2];                    
                }
            }

            for (int i = names.Count; i < listView.Items.Count; i++)
            {
                listView.Items.RemoveAt(i);
                i--;
            }
            //listView.Items[service.IndexOfChosenObj].Selected = true;
            //listView.Items[service.IndexOfChosenObj].Checked = true;
        }



        public void initGroupListView(List<Object> categories) {
            for (int i = 0; i < categories.Count; i += 2)
                if ((bool)categories[i + 1] == true)
                {
                    if (i / 2 >= listView.Groups.Count)
                        listView.Groups.Add(categories[i].ToString(), categories[i].ToString());
                    else
                        listView.Groups[i / 2].Header = categories[i].ToString();

                }
                else
                {
                    listView.Groups[i/2] = null;
                }
        }


        private void CheckGroupButtonsInit(ref List<Object> categories)
        {
            InitializeToolStrips(ref categories, WorckWithGroups_ToolStripMenuItem, true, new EventHandler(this.CheckGroup_Click));
            
            try
            {
                for (int i = 1; i < WorckWithGroups_ToolStripMenuItem.DropDownItems.Count; i++)
                {
                    //if (!(WorckWithGroups_ToolStripMenuItem.DropDownItems[i] as ToolStripMenuItem).Checked)
                  //     groups[i-1] = null;
                }
            }
            catch (Exception e) { }
        }

        
        private void TransferToGroupButtonInit(ref List<Object> categories)
        {
            InitializeToolStrips(ref categories, TfansferToGroupToolStripMenuItem,false,new EventHandler(this.TransferToGroup_Click));
        }

        private void InitializeToolStrips(ref List<Object> categories, ToolStripMenuItem menuItem, bool isChecked, EventHandler eventHandler)
        {
            bool isExistedToolStrop = false;

            for (int i =0;i< categories.Count;i+=2)
            {
                foreach (var toolStrip in menuItem.DropDownItems)
                    if (toolStrip.ToString() == categories[i].ToString())
                        isExistedToolStrop = true;

                if (!isExistedToolStrop)
                {
                    menuItem.DropDownItems.Add(categories[i].ToString(), null, eventHandler);
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
            List<String> objValueProp = service.GetAllObjValueProp();

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
           
        }

        private void dellObject_toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            service.DeleteObj();
            listView.Items[service.IndexOfChosenObj].Remove();
            InitializeListView();
        }

        private void viewList_contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            InitializeListView();

            ListViewGroupCollection groups = listView.Groups;
        }

        private void CreateGroup_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String name = toolStripTextBox1.Text;
            if (name == null || name == "") return;

            service.AddCategory(name);

            InitializeListView();
        }

        private void CheckGroup_Click(object sender, EventArgs e)
        {
            if ((sender as ToolStripMenuItem).Checked)
            {
                (sender as ToolStripMenuItem).Checked = false;
                int index = categories.IndexOf((sender as ToolStripMenuItem).Text);
                categories[index + 1] = false;
            }
            else
            {
                (sender as ToolStripMenuItem).Checked = true;
                int index = categories.IndexOf((sender as ToolStripMenuItem).Text);
                categories[index + 1] = true;
            }

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
            List<Object> categories = service.GetObjsCategories();
            int indexGroup = categories.IndexOf((sender as ToolStripMenuItem).Text);
            service.SetGroupToCurrentObject_andSave(categories[indexGroup].ToString());
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

        private void listView_Click(object sender, EventArgs e)
        {
            if (listView.SelectedIndices.Count > 0)
                service.IndexOfChosenObj = listView.SelectedIndices[0];

            initializeFieldOfProperty();
        }

        private void listBox2_Click(object sender, EventArgs e)
        {
            selectedProperty = listBox2.SelectedIndex;
            service.PropertyNum = selectedProperty;
            textBox2.Text = service.GetObjValueProp(selectedProperty);

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            service.PropertyNum = selectedProperty;
            if (textBox2.SelectionStart != 0)
                textBox2_caretka = textBox2.SelectionStart + textBox2.SelectionLength;

            if (!service.InputInfoAndSaveObj(textBox2.Text))
                textBox2.Text = service.GetObjValueProp(selectedProperty);

            initializeFieldOfProperty();
            textBox2.SelectionStart = textBox2_caretka;

            InitializeListView();

        }
    }
}

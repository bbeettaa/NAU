using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        readonly private EntityService service = new EntityService();
        private List<Object> categories;
        List<Object> objProductList;
        List<Object> objSuppliertList;

        private string productFind = "";
        private string suppliersFind = "";
        private int selectedProperty = 0;
        private int textBox2_caretka = 0;
        private bool isShowAllItemsAndIgrnoreGroups = false;

        delegate List<Object> SortAlgorithm(List<Object> objList);
        SortAlgorithm sortAlgorithm;



        public Form1()
        {
            InitializeComponent();
            sortAlgorithm = service.sorting.SortName;

            AddObjectStripMenuItem.ShowDropDown();
            AddObjectStripMenuItem.HideDropDown();

            objProductList = service.FindWorkableObject(productFind);
            objSuppliertList = service.FindSupplierObject(suppliersFind);

            InitializeListView();
            InitSupplyerListView();
            InitSort();
            InitButtons();
        }





        //
        // First Page
        //

        private void InitializeListView()
        {
            Hashtable groupsAndNames = service.GetTableOfObjectAndGroup();
            categories = service.category.GetObjsCategories();

            InitGroupListView();
            InitCheckGroupButtons();
            InitTransferToGroupButton();
            InitDellCategory();

            List<String> names = service.GetObjNames(objProductList);

            for (int i = 0; i < names.Count; i++)
            {
                int groupNumber = categories.IndexOf(groupsAndNames[objProductList[i]].ToString());

                if (groupNumber == -1 && isShowAllItemsAndIgrnoreGroups == false)
                {
                    names.RemoveAt(i);
                    i -= 1;
                    continue;
                }
                if (i < names.Count)
                {
                    if (!isShowAllItemsAndIgrnoreGroups)
                        if ((bool)categories[groupNumber + 1] == false)
                        {
                            objProductList.RemoveAt(i);
                            names.RemoveAt(i);
                            i--;
                            continue;
                        }

                    if (i >= listViewProduct.Items.Count)
                        listViewProduct.Items.Add($"{names[i]}");
                    else
                        listViewProduct.Items[i].Text = $"{names[i]}";

                    if (isShowAllItemsAndIgrnoreGroups)
                        listViewProduct.Items[i].Group = null;
                    else
                        listViewProduct.Items[i].Group = listViewProduct.Groups[groupNumber / 2];
                }
            }

            for (int i = names.Count; i < listViewProduct.Items.Count; i++)
            {
                listViewProduct.Items.RemoveAt(i);
                i--;
            }
        }

        private void InitDellCategory()
        {
            InitToolStrips(DellCAtegory_ToolStripMenuItem, false, new EventHandler(this.DellGroup_Click));
        }

        public void InitGroupListView()
        {
            for (int i = 0; i < categories.Count; i += 2)
                if ((bool)categories[i + 1] == true)
                {
                    if (i / 2 >= listViewProduct.Groups.Count)
                        listViewProduct.Groups.Add(categories[i].ToString(), categories[i].ToString());
                    else
                    {
                        listViewProduct.Groups[i / 2].Header = categories[i].ToString();
                    }
                }
                else
                {
                    listViewProduct.Groups.RemoveAt(i / 2);
                    listViewProduct.Groups.Add(categories[i].ToString(), categories[i].ToString());
                }
        }

        private void InitCheckGroupButtons()
        { InitToolStrips(WorckWithGroups_ToolStripMenuItem, true, new EventHandler(this.CheckGroup_Click)); }

        private void InitTransferToGroupButton()
        { InitToolStrips(TfansferToGroupToolStripMenuItem, false, new EventHandler(this.TransferToGroup_Click)); }

        private void InitToolStrips(ToolStripMenuItem menuItem, bool isChecked, EventHandler eventHandler)
        {
            bool isExistedToolStrip = false;

            for (int i = 0; i < categories.Count; i += 2)
            {
                foreach (ToolStripMenuItem toolStrip in menuItem.DropDownItems)
                    if (toolStrip.ToString() == categories[i].ToString())
                        isExistedToolStrip = true;

                if (!isExistedToolStrip)
                {
                    menuItem.DropDownItems.Add(categories[i].ToString(), null, eventHandler);
                    (menuItem.DropDownItems[menuItem.DropDownItems.Count - 1] as ToolStripMenuItem).Checked = isChecked;
                }

                isExistedToolStrip = false;
            }


            foreach (ToolStripMenuItem toolStrip in menuItem.DropDownItems)

            {
                var obj = toolStrip;
                if (categories.IndexOf(obj.ToString()) == -1 && obj.Tag != "System")
                {
                    menuItem.DropDownItems.Remove(obj);
                    break;
                }
            }



        }

        private void InitFieldOfProperty()
        {
            ChangeProperty_textBox.Text = service.GetObjValueProp(selectedProperty);
            Product_listBox.Items.Clear();

            List<String> objNameProp = service.GetObjNamePropsOfCurrentObj();
            List<String> objValueProp = service.GetAllObjValuePropProduct();

            ProductLabel.Text = $"Тип об'єкту       {objNameProp[0]}";
            objNameProp.RemoveAt(0);

            for (int i = 0; i < objNameProp.Count; i++)
            {
                Product_listBox.Items.Add($"{objNameProp[i],-25}\t{objValueProp[i]}");
            }

            if (selectedProperty >= Product_listBox.Items.Count)
                selectedProperty = Product_listBox.Items.Count - 1;
            Product_listBox.SelectedIndex = selectedProperty;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            productFind = SearchProduct.Text;
            objProductList = service.FindWorkableObject(productFind);
            InitializeListView();
        }

        private void DellObject_toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            service.DeleteProduct();
            if(service.GetIndexOfObjOfProduct() < listViewProduct.Items.Count)
            listViewProduct.Items[service.GetIndexOfObjOfProduct()].Remove();
            InitializeListView();
        }

        private void CreateGroup_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String name = toolStripTextBox1.Text;
            if (name == null || name == "") return;

            service.category.AddCategory(name);

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
                objProductList = service.FindWorkableObject(productFind);
            }

            isShowAllItemsAndIgrnoreGroups = false;


            InitSort();
            InitializeListView();
        }

        private void ShowAllObjects_HideAllGroup_Click(object sender, EventArgs e)
        {
            objProductList = service.FindWorkableObject(productFind);

            foreach (var obj in WorckWithGroups_ToolStripMenuItem.DropDownItems)
            {
                for (int i = 0; i < categories.Count; i += 2)
                    categories[i + 1] = false;
                (obj as ToolStripMenuItem).Checked = false;
            }

            isShowAllItemsAndIgrnoreGroups = true;

            InitializeListView();
        }

        private void ShowAllGroup_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            objProductList = service.FindWorkableObject(productFind);
            for (int i = 0; i < categories.Count; i += 2)
            {
                categories[i + 1] = true;
                foreach (var obj in WorckWithGroups_ToolStripMenuItem.DropDownItems)
                {
                    if ((obj as ToolStripMenuItem).Text == categories[i].ToString())
                        (obj as ToolStripMenuItem).Checked = true;
                }
            }

            isShowAllItemsAndIgrnoreGroups = false;

            InitializeListView();
        }

        private void TransferToGroup_Click(object sender, EventArgs e)
        {
            List<Object> categories = service.category.GetObjsCategories();
            int indexGroup = categories.IndexOf((sender as ToolStripMenuItem).Text);
            service.SetGroupToCurrentObject_andSave(categories[indexGroup].ToString());
            InitializeListView();
        }

        private void RenameThisGroup_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String newCat = RenameGroupToolStripTextBox.Text;
            service.RenameGroupOfCurrentObjectAndDellOldGroup(newCat);

            
            InitializeListView();
        }

        private void DellGroup_Click(object sender, EventArgs e)
        {
            categories = service.category.GetObjsCategories();
            int indexGroup = categories.IndexOf((sender as ToolStripMenuItem).Text);

            service.category.DeleteCategory(categories[indexGroup].ToString(), objProductList);

            InitializeListView();
        }

        private void AddProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            service.AppendProductInDatabase("Product");
            objProductList = service.FindWorkableObject(productFind);
            InitializeListView();
        }

        private void ListBox2_Click(object sender, EventArgs e)
        {
            selectedProperty = Product_listBox.SelectedIndex;
            service.SetIndexOfPropOfProduct(selectedProperty);
            ChangeProperty_textBox.Text = service.GetObjValueProp(selectedProperty);

        }

        private void SortName_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sortAlgorithm = service.sorting.SortName;
            InitSort();
        }

        private void SortBrand_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sortAlgorithm = service.sorting.SortBrand;
            InitSort();
        }

        private void SortPrice_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sortAlgorithm = service.sorting.SortPrice;
            InitSort();
        }

        private void ListView_Click(object sender, EventArgs e)
        {
            if (listViewProduct.SelectedIndices.Count > 0)
                service.SetIndexOfObjOfProduct((int)listViewProduct.SelectedIndices[0]);

            InitFieldOfProperty();
        }

        private void InitSort()
        {
            listViewProduct.ListViewItemSorter = new ListViewItemComparer();

            objProductList = service.GetFindObjects();
            sortAlgorithm(objProductList);

            InitializeListView();

            listViewProduct.Sort();
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            service.SetIndexOfPropOfProduct(selectedProperty);
            if (ChangeProperty_textBox.SelectionStart != 0)
                textBox2_caretka = ChangeProperty_textBox.SelectionStart + ChangeProperty_textBox.SelectionLength;

            if (!service.InputInfoAndSaveObj(ChangeProperty_textBox.Text))
                ChangeProperty_textBox.Text = service.GetObjValueProp(selectedProperty);

            InitFieldOfProperty();
            ChangeProperty_textBox.SelectionStart = textBox2_caretka;

            InitializeListView();

        }

        private void ASCSort_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Product_contextMenuStrip.Show();
            Sort_ToolStripMenuItem.ShowDropDown();

            ASCSort_ToolStripMenuItem.Checked = true;
            DESCSort_ToolStripMenuItem.Checked = false;


            service.sorting.sortingOreder = Sorting.SortingOreder.ASC;
            InitializeListView();
        }

        private void DESCSort_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Product_contextMenuStrip.Show();
            Sort_ToolStripMenuItem.ShowDropDown();

            DESCSort_ToolStripMenuItem.Checked = true;
            ASCSort_ToolStripMenuItem.Checked = false;

            service.sorting.sortingOreder = Sorting.SortingOreder.DESC;
            InitializeListView();
        }




        //
        // Second Page
        //
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            // InitializeListView();
        }

        private void InitSupplyerListView()
        {
            List<String> names = service.GetObjNames(objSuppliertList);
            for (int i = 0; i < names.Count; i++)
            {
                if (i >= Supplier_listView.Items.Count)
                    Supplier_listView.Items.Add($"{names[i]}");
                else
                    Supplier_listView.Items[i].Text = $"{names[i]}";
            }
            for (int i = names.Count; i < Supplier_listView.Items.Count; i++)
            {
                Supplier_listView.Items.RemoveAt(i);
            }
        }

        private void InitFieldOfPropertySupplies()
        {
            ProviderChangeProps_textBox.Text = service.GetSuppValueProp(selectedProperty);
            ProviderProps_listBox.Items.Clear();

            List<String> objNameProp = service.GetNamePropsOfCurrentSupp();
            List<String> objValueProp = service.GetAllSuppValueProp();

            Provider_label.Text = $"Тип об'єкту       {objNameProp[0]}";
            objNameProp.RemoveAt(0);

            for (int i = 0; i < objNameProp.Count; i++)
            {
                ProviderProps_listBox.Items.Add($"{objNameProp[i],-25}\t{objValueProp[i]}");
            }

            if (selectedProperty >= ProviderProps_listBox.Items.Count)
                selectedProperty = ProviderProps_listBox.Items.Count - 1;
            ProviderProps_listBox.SelectedIndex = selectedProperty;
        }

        private void Supplier_listView_Click(object sender, EventArgs e)
        {
            if (Supplier_listView.SelectedIndices.Count > 0)
                service.SetIndexOfObjOfSupp( Supplier_listView.SelectedIndices[0]);

            InitFieldOfPropertySupplies();
        }

        private void ListViewSupplier_Click(object sender, EventArgs e)
        {

        }

        private void TabPage1_Click(object sender, EventArgs e)
        {
            InitSort();
        }

        private void AddSupplier_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            service.AppendSuppInDatabase("Supplier");
            objSuppliertList = service.FindSupplierObject(productFind);
            InitSupplyerListView();
        }

        private void ProviderChangeProps_textBox_TextChanged(object sender, EventArgs e)
        {
            service.SetIndexOfPropOfSupp(selectedProperty);
            if (ChangeProperty_textBox.SelectionStart != 0)
                textBox2_caretka = ChangeProperty_textBox.SelectionStart + ChangeProperty_textBox.SelectionLength;

            if (!service.InputInfoAndSaveSupp(ProviderChangeProps_textBox.Text))
                ChangeProperty_textBox.Text = service.GetObjValueProp(selectedProperty);

            InitFieldOfPropertySupplies();
            ChangeProperty_textBox.SelectionStart = textBox2_caretka;

            InitSupplyerListView();
        }

        private void ProviderProps_listBox_Click(object sender, EventArgs e)
        {
            selectedProperty = ProviderProps_listBox.SelectedIndex;
            service.SetIndexOfPropOfSupp(selectedProperty);
            ProviderChangeProps_textBox.Text = service.GetSuppValueProp(selectedProperty);
       
        }

        private void SarchProvider_textBox_TextChanged(object sender, EventArgs e)
        {
            suppliersFind = SarchProvider_textBox.Text;
            objSuppliertList = service.FindSupplierObject(suppliersFind);
            InitSupplyerListView();
            InitSupplyerListView();
        }

        private void ASCSortІSuppliers_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Supplier_contextMenuStrip.Show();
            SortSuppliers_ToolStripMenuItem.ShowDropDown();

            ASCSortSuppToolStripMenuItem.Checked = true;
            DescSortSuppToolStripMenuItem.Checked = false;

            service.sorting.sortingOreder = Sorting.SortingOreder.ASC;
            InitSupplyerListView();
        }

        private void DESCSortSuppliers_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Supplier_contextMenuStrip.Show();
            SortSuppliers_ToolStripMenuItem.ShowDropDown();

            DescSortSuppToolStripMenuItem.Checked = true;
            ASCSortSuppToolStripMenuItem.Checked = false;

            service.sorting.sortingOreder = Sorting.SortingOreder.DESC;
            InitSupplyerListView();
        }

        private void AscSortSupp_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sortAlgorithm = service.sorting.SortName;
            InitSortSupp();
        }

        private void DescSortSupp_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sortAlgorithm = service.sorting.SortLastNameSupp;
            InitSortSupp();
        }

        private void InitSortSupp()
        {
            listViewProduct.ListViewItemSorter = new ListViewItemComparer();

            objSuppliertList = service.GetFindSupp();
            sortAlgorithm(objSuppliertList);

            InitSupplyerListView();

            Supplier_listView.Sort();
        }

        private void DellObject_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            service.DeleteSupp();
            if (service.GetIndexOfObjOfSupp() < Supplier_listView.Items.Count)
                Supplier_listView.Items[service.GetIndexOfObjOfSupp()].Remove();
            InitSupplyerListView();
        }







        //
        // Third Page
        //
        public void InitButtons()
        {
            int fileNum = service.GetSerializeNum();
            switch (fileNum) {
                case (0):
                    JsonRadioButton.Checked=true;
                    break;
                case (1):
                    XmlRadioButton.Checked = true;
                    break;
                case (2):
                    BinaryRadioButton.Checked = true;
                    break;

            }

            List<String> namesSett =  service.GetNameSettings();

            JsonLabel.Text = namesSett[1];
            XmlLabel.Text =  namesSett[2];
            BinaryLabel.Text = namesSett[3];

        }

        private void JsonRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            service.SetNumCurrentFileName(0);
        }

        private void XmlRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            service.SetNumCurrentFileName(1);
        }

        private void BinaryRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            service.SetNumCurrentFileName(2);
        }

        private void CustomRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            service.SetNumCurrentFileName(3);
        }

        private void TfansferToGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }

    class ListViewItemComparer : IComparer
    {
        public ListViewItemComparer()
        {
        }
        public int Compare(object x, object y)
        {
            return 0;
        }
    }
}

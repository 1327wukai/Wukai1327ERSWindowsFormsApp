using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wukai1327ERSWindowsFormsApp
{
    public partial class EmployeeRecordsForm : Form
    {
        private TreeNode tvRootNode;

        public EmployeeRecordsForm()
        {
            InitializeComponent();
            PopulateTreeView();
            InitalizeListControl();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void EmployeeRecordsForm_Load(object sender, EventArgs e)
        {

        }

        private void PopulateTreeView()
        {
            statusBarPanel1.Tag = "Refreshing Employee Code.Please Wait...";
            this.Cursor = Cursors.WaitCursor;
            treeView1.Nodes.Clear();
            tvRootNode = new TreeNode("Employee Records");
            this.Cursor=Cursors.Default;
            treeView1.Nodes.Add(tvRootNode);

            TreeNodeCollection nodeCollection=tvRootNode.Nodes;
            XmlTextReader reader
                = new XmlTextReader("E:\\visual studio2022-CASE-workspace\\Wukai1327ERSWindowsFormsApp\\Wukai1327ERSWindowsFormsApp\\EmpRec.xml");
            reader.MoveToElement();
            try
            {
                while (reader.Read())
                {
                    if(reader.HasAttributes && reader.NodeType == XmlNodeType.Element)
                    {
                        reader.MoveToElement();
                        reader.MoveToElement();

                        reader.MoveToAttribute("Id");
                        String strVal = reader.Value;

                        reader.Read();
                        reader.Read();
                        if (reader.Name == "Dept")
                        {
                            reader.Read();

                        }
                        //create a child node
                        TreeNode EcodeNode = new TreeNode(strVal); 
                        //add the node
                        nodeCollection.Add(EcodeNode);

                    }
                }
                statusBarPanel1.Text = "Click on an emplyoee code to see their record.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected void InitalizeListControl()
        {
            listView1.Clear();
            listView1.Columns.Add("Employee Name", 150, HorizontalAlignment.Left);
            listView1.Columns.Add("Date of Join",70,HorizontalAlignment.Right);
            listView1.Columns.Add("Gread",105,HorizontalAlignment.Left);
            listView1.Columns.Add("Salary",105,HorizontalAlignment.Left);
        }

        protected void PopulateListview(TreeNode crrNode)
        {
            InitalizeListControl();
            XmlTextReader listRead = new XmlTextReader("E:\\visual studio2022-CASE-workspace\\Wukai1327ERSWindowsFormsApp\\Wukai1327ERSWindowsFormsApp\\EmpRec.xml");
            listRead.MoveToElement();
            while (listRead.Read())
            {
                String strNodeName;
                String strNodePath;
                String name; 
                String gread; 
                String doj; 
                String sal;
                String[] strItemsArr = new string[4]; 
                listRead.MoveToFirstAttribute(); 
                strNodeName = listRead.Value;
                strNodePath = crrNode.FullPath.Remove(0, 17);
                if(strNodePath == strNodeName)
                {
                    ListViewItem lvi;

                    listRead.MoveToNextAttribute(); 
                    name = listRead.Value;
                    lvi = listView1.Items.Add(listRead.Value);

                    listRead.Read();
                    listRead.Read();

                    listRead.MoveToFirstAttribute(); 
                    doj = listRead.Value;
                    lvi.SubItems.Add(doj);

                    listRead.MoveToNextAttribute(); 
                    gread = listRead.Value;
                    lvi.SubItems.Add(gread);

                    listRead.MoveToNextAttribute (); 
                    sal = listRead.Value;
                    lvi.SubItems.Add(sal);

                    listRead.MoveToNextAttribute(); 
                    listRead.MoveToElement();
                    listRead.ReadString();

                }
            }


        }//end PopulateListView,


        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode currNode = e.Node;
            if (tvRootNode == currNode) 
            {
                InitalizeListControl();
                statusBarPanel1.Text = "Double click the Employee Records";
                return;
            }
            else 
            {
                statusBarPanel1.Text = "Click an Employee code to view individual record";
            }
            PopulateListview(currNode);
        }
    }
}

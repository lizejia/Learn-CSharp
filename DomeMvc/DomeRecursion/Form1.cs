using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DomeRecursion
{
    public partial class Form1 : Form
    {

        string path = @"E:\Code\16\TL";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadTree(path);
        }

        private void LoadTree(string path, TreeNode node = null)
        {
            //string[] dirs = Directory.GetDirectories(path);
            //foreach (string dir in dirs)
            //{
            //    TreeNode tn = new TreeNode(Path.GetFileName(dir));
            //    //如果node为null,就把获得的文件夹名称加到TreeView上
            //    if (node == null)
            //    {
            //        treeViewData.Nodes.Add(tn);
            //    }
            //    else
            //    {
            //        node.Nodes.Add(tn);
            //    }
            //    if (Directory.GetDirectories(dir).Length > 0)
            //    {
            //        LoadTree(dir, tn);
            //    }
            //}
            if (node == null)
            {
                string[] dirs = Directory.GetDirectories(path);
                foreach (string dir in dirs)
                {
                    TreeNode tn = new TreeNode(Path.GetFileName(dir));
                    treeViewData.Nodes.Add(tn);
                    if (Directory.GetDirectories(dir).Length > 0)
                    {
                        LoadTree(dir, tn);
                    }
                }
            }
            else
            {
                string[] dirs = Directory.GetDirectories(path);
                foreach (string dir in dirs)
                {
                    TreeNode tn = new TreeNode(Path.GetFileName(dir));
                    node.Nodes.Add(tn);
                    if (Directory.GetDirectories(dir).Length > 0)
                    {
                        LoadTree(dir, tn);
                    }
                }
            }
        }
    }
}

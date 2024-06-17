using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace activiser.Library.Forms
{
    public partial class PickList : Form
    {
        Dictionary<string, string> listData = new Dictionary<string, string>();

        public PickList()
        {
            InitializeComponent();
        }

        private void loadList()
        {
            ListBox1.Items.Clear();
            ListBox1.DataSource = null;
            ListBox1.DataSource = listData;
            ListBox1.ValueMember = "Key";
            ListBox1.DisplayMember = "Value";
        }

        public int AddItem(string key, string description)
        {
            listData.Add(key, description);
            return listData.Count;
        }

        public int RemoveItem(string key)
        {
            if (listData.ContainsKey(key)) listData.Remove(key);
            return listData.Count;
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PickList_Load(object sender, EventArgs e)
        {

        }
    }
}

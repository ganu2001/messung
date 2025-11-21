using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.Base;
using static System.Net.Mime.MediaTypeNames;

namespace LadderDrawing
{
    public partial class AutoComplete : UserControl
    {
        ListItems listItems = new ListItems();
        public event EventHandler OnSelect;
        public event EventHandler TextValidation;
        public string EnteredText { set; get; }
        public bool ValidText { set; get; }
        public string SelectedText { set; get; }

        public AutoComplete()
        {
            InitializeComponent();
        }

        public void ClearList()
        {
            listItems.Clear();
        }

        public void AddListItem(string key, string text)
        {
            listItems.Add(key, text);
        }

        public void SetText(string text)
        {
            textBox1.Text = text == "???" ? "" : text;
            textBox1.Focus();
            if (textBox1.Text == null || textBox1.Text == "")
            {
                listBox1.Items.Clear();
                SelectedText = "";
                for (int i = 0; i < listItems.Count; i++)
                {
                    if (listItems[i].Text.ToLower().Trim().IndexOf(textBox1.Text.ToLower().Trim()) >= 0 || listItems[i].Text.ToLower().Trim().IndexOf(textBox1.Text.ToLower().Trim()) == -1)
                    {
                        listBox1.Items.Add(listItems[i].Text);
                    }
                }
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            SelectedText = "";
            for (int i = 0; i < listItems.Count; i++)
            {
                if (listItems[i].Text.ToLower().Trim().IndexOf(textBox1.Text.ToLower().Trim()) >= 0 || listItems[i].Text.ToLower().Trim().IndexOf(textBox1.Text.ToLower().Trim()) == -1)
                {
                    listBox1.Items.Add(listItems[i].Text);
                }
            }
            if((textBox1.Text != null || textBox1.Text !="") && (!textBox1.Text.StartsWith("c") && !textBox1.Text.StartsWith("???")))
            {
                List<string> filteredItems = listBox1.Items.Cast<string>().Where(item => item.ToLower().Contains(textBox1.Text.ToLower())).ToList();
                listBox1.Items.Clear();
                foreach (string tag in filteredItems)
                    listBox1.Items.Add(tag);
            }
            
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                SelectedText = listBox1.SelectedItem.ToString();
                if (OnSelect != null)
                    OnSelect(this, new EventArgs());
                CloseForm(SelectedText);
            }
        }

        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (listBox1.SelectedIndex != -1 && e.KeyCode == Keys.Enter)
            {
                SelectedText = listBox1.SelectedItem.ToString();
                if (OnSelect != null)
                    OnSelect(this, new EventArgs());
                CloseForm(SelectedText);

            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectedText = textBox1.Text;
                if (OnSelect != null)
                    OnSelect(this, new EventArgs());
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && (e.KeyChar) != 8 && (e.KeyChar) != 95 && (e.KeyChar) != 3 && (e.KeyChar) != 22)
            {
                e.Handled = true;
            }

        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            ValidText = true;
            EnteredText = textBox1.Text.ToString();
            e.Cancel = !ValidText;
            if (this.TextValidation != null)
            {
                TextValidation(sender, e);
            }
        }

        private void CloseForm(string sendText)
        {
            EnteredText = sendText;
            this.ParentForm.Close();
            this.ParentForm.DialogResult = DialogResult.OK;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //If Element is present in Tag List And while Renaming Tag it Should not be able to click add tag.
            if (!CheckElementPresent(textBox1.Text))
            {
                CloseForm(textBox1.Text);
            }
        }

        private bool CheckElementPresent(string check)
        {
            string text = check;
            for (int i = 0; i < listItems.Count; i++)
            {
                if (listItems[i].Text == text)
                {
                    return true;
                }
            }
            return false;
        }

        private void listBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 )
            {
                if (listBox1.SelectedItem == null) return;
                SelectedText = listBox1.SelectedItem.ToString();
                if (OnSelect != null)
                    OnSelect(this, new EventArgs());
                CloseForm(SelectedText);
            }
        }
    }
}

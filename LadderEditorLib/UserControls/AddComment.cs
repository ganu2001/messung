using System;
using System.Windows.Forms;

namespace LadderDrawing
{
    public partial class AddComment : UserControl
    {
        ListItems listItems = new ListItems();
        public event EventHandler OnSelect;
        public string EnteredText { set; get; }
        public bool ValidText { set; get; }
        public string SelectedText { set; get; }

        public AddComment()
        {
            InitializeComponent();
        }

        public void ClearList()
        {
            listItems.Clear();
        }

        public void SetText(string text)
        {
            textBox1.Text = text;
            textBox1.Focus();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectedText = textBox1.Text;
                if (OnSelect != null)
                    OnSelect(this, new EventArgs());
                EnteredText = textBox1.Text;
                this.ParentForm.Close();
                this.ParentForm.DialogResult = DialogResult.OK;
            }
        }

        private void AddComment_Load(object sender, EventArgs e)
        {

        }
    }
}

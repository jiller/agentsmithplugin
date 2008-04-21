using System;
using System.Windows.Forms;

namespace AgentSmith.Options
{
    public partial class StringListItemEditDialog : Form
    {
        private StringListItemEditDialog()
        {
            InitializeComponent();

            CustomInitializeComponent();
        }


        private void CustomInitializeComponent()
        {
            ActiveControl = _itemTextBox;
            _itemTextBox.TextChanged += _itemTextBox_TextChanged;
        }

        void _itemTextBox_TextChanged(object sender, EventArgs e)
        {
            InvalidateState();
        }


        private void InvalidateState()
        {
            _okButton.Enabled = !string.IsNullOrEmpty(Value);
        }


        public static StringListItemEditDialog CreateNewItemEditDialog()
        {
            StringListItemEditDialog dialog = new StringListItemEditDialog();
            dialog.Text = "Add New Item";
            dialog.InvalidateState();
            return dialog;
        }


        public static StringListItemEditDialog CreateExistingItemEditDialog(string value)
        {
            StringListItemEditDialog dialog = new StringListItemEditDialog();
            dialog.Text = "Edit Item";
            dialog._itemTextBox.Text = value;
            dialog.InvalidateState();
            return dialog;
        }


        public string Value
        {
            get { return _itemTextBox.Text; }
        }

        private void _okButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
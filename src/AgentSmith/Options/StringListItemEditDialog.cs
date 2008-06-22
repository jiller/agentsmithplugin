using System;
using System.Windows.Forms;

namespace AgentSmith.Options
{
    public partial class StringListItemEditDialog : Form
    {
        private ValidateHandler _itemValidate;

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

        private void _itemTextBox_TextChanged(object sender, EventArgs e)
        {
            InvalidateState();
        }


        private void InvalidateState()
        {
            _okButton.Enabled = !string.IsNullOrEmpty(Value);
        }


        public static StringListItemEditDialog CreateNewItemEditDialog(ValidateHandler validateHandler)
        {
            StringListItemEditDialog dialog = new StringListItemEditDialog();
            dialog.Text = "Add New Item";
            dialog.InvalidateState();
            dialog._itemValidate = validateHandler;
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

        private void okButton_Click(object sender, EventArgs e)
        {
            if (_itemValidate == null || _itemValidate(Value))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AgentSmith.Options
{
    public partial class StringListEdit : UserControl
    {
        public StringListEdit()
        {
            InitializeComponent();

            CustomInitializeComponent();

            InvalidateState();
        }


        private void CustomInitializeComponent()
        {
            _addButton.Click += _addButton_Click;
            _editButton.Click += _editButton_Click;
            _removeButton.Click += _removeButton_Click;
            _itemsListBox.SelectedIndexChanged += _itemsListBox_SelectedIndexChanged;
            _itemsListBox.DoubleClick += _itemsListBox_DoubleClick;
        }

        void _itemsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            InvalidateState();
        }

        void _removeButton_Click(object sender, EventArgs e)
        {
            if (!IsRemoveEnabled())
                return;

            _itemsListBox.BeginUpdate();
            
            List<int> selectedIndexes = new List<int>();
            foreach (int selectedIndex in _itemsListBox.SelectedIndices)
                selectedIndexes.Add(selectedIndex);
            selectedIndexes.Sort();

            int deletedCount = 0;
            foreach (int selectedIndex in selectedIndexes)
            {
                _itemsListBox.Items.RemoveAt(selectedIndex - deletedCount);
                deletedCount++;
            }                

            _itemsListBox.EndUpdate();            
        }

        void _itemsListBox_DoubleClick(object sender, EventArgs e)
        {
            EditSelectedItem();
        }


        void _editButton_Click(object sender, EventArgs e)
        {
            EditSelectedItem();
        }

        void _addButton_Click(object sender, EventArgs e)
        {
            AddNewItem();
        }


        private void AddNewItem()
        {
            using (StringListItemEditDialog dialog = StringListItemEditDialog.CreateNewItemEditDialog())
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;

                DoAddNewItem(dialog.Value);
            }
        }


        private void DoAddNewItem(string value)
        {
            _itemsListBox.Items.Add(value);
            _itemsListBox.ClearSelected();
            _itemsListBox.SelectedIndex = _itemsListBox.Items.Count - 1;
        }


        void InvalidateState()
        {
            _editButton.Enabled = IsEditEnabled();
            _removeButton.Enabled = IsRemoveEnabled();
        }


        private bool IsRemoveEnabled()
        {
            return _itemsListBox.SelectedIndices.Count > 0;
        }


        bool IsEditEnabled()
        {
            return _itemsListBox.SelectedIndices.Count == 1;
        }

        private void EditSelectedItem()
        {
            if (!IsEditEnabled())
                return;

            int index = _itemsListBox.SelectedIndices[0];
            string itemToChange = (string)_itemsListBox.SelectedItem;
            if (itemToChange == null)
                return;

            using (StringListItemEditDialog dialog = StringListItemEditDialog.CreateExistingItemEditDialog(itemToChange))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;

                _itemsListBox.BeginUpdate();
                _itemsListBox.Items.Insert(index + 1, dialog.Value);
                _itemsListBox.Items.RemoveAt(index);
                _itemsListBox.ClearSelected();
                _itemsListBox.SelectedIndex = index;

                _itemsListBox.EndUpdate();            

            }
        }

        public string Caption
        {
            get { return _captionLabel.Text;  }
            set { _captionLabel.Text = value; }
        }

        public string [] Items
        {
            get
            {
                List<string> result = new List<string>(_itemsListBox.Items.Count);
                foreach (string str in _itemsListBox.Items)
                    result.Add(str);

                return result.ToArray();
            }
            set
            {
                _itemsListBox.Items.Clear();
                if (value == null)
                    return;

                foreach (string str in value)
                    _itemsListBox.Items.Add(str);
            }
        }
    }
}

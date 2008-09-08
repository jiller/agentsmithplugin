using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AgentSmith.Options
{
    public delegate bool ValidateHandler(string value);
    
    public partial class StringListEdit : UserControl
    {
        public StringListEdit()
        {
            InitializeComponent();

            customInitializeComponent();

            invalidateState();
        }


        private void customInitializeComponent()
        {
            _addButton.Click += addButton_Click;
            _editButton.Click += editButton_Click;
            _removeButton.Click += removeButton_Click;
            _itemsListBox.SelectedIndexChanged += itemsListBox_SelectedIndexChanged;
            _itemsListBox.DoubleClick += itemsListBox_DoubleClick;
        }

        private void itemsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            invalidateState();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (!isRemoveEnabled())
            {
                return;
            }

            _itemsListBox.BeginUpdate();

            List<int> selectedIndexes = new List<int>();
            foreach (int selectedIndex in _itemsListBox.SelectedIndices)
            {
                selectedIndexes.Add(selectedIndex);
            }
            selectedIndexes.Sort();

            int deletedCount = 0;
            foreach (int selectedIndex in selectedIndexes)
            {
                _itemsListBox.Items.RemoveAt(selectedIndex - deletedCount);
                deletedCount++;
            }

            _itemsListBox.EndUpdate();
        }

        private void itemsListBox_DoubleClick(object sender, EventArgs e)
        {
            editSelectedItem();
        }


        private void editButton_Click(object sender, EventArgs e)
        {
            editSelectedItem();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            addNewItem();
        }


        private void addNewItem()
        {
            using (StringListItemEditDialog dialog = StringListItemEditDialog.CreateNewItemEditDialog(OnValidate))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                doAddNewItem(dialog.Value);
            }
        }


        private void doAddNewItem(string value)
        {
            _itemsListBox.Items.Add(value);
            _itemsListBox.ClearSelected();
            _itemsListBox.SelectedIndex = _itemsListBox.Items.Count - 1;
        }


        private void invalidateState()
        {
            _editButton.Enabled = isEditEnabled();
            _removeButton.Enabled = isRemoveEnabled();
        }


        private bool isRemoveEnabled()
        {
            return _itemsListBox.SelectedIndices.Count > 0;
        }

        private bool isEditEnabled()
        {
            return _itemsListBox.SelectedIndices.Count == 1;
        }

        private void editSelectedItem()
        {
            if (!isEditEnabled())
            {
                return;
            }

            int index = _itemsListBox.SelectedIndices[0];
            string itemToChange = (string) _itemsListBox.SelectedItem;
            if (itemToChange == null)
            {
                return;
            }

            using (StringListItemEditDialog dialog = StringListItemEditDialog.CreateExistingItemEditDialog(itemToChange)
                )
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

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
            get { return _captionLabel.Text; }
            set { _captionLabel.Text = value; }
        }

        public event ValidateHandler OnValidate;

        public string[] Items
        {
            get
            {
                List<string> result = new List<string>(_itemsListBox.Items.Count);
                foreach (string str in _itemsListBox.Items)
                {
                    result.Add(str);
                }

                return result.ToArray();
            }
            set
            {
                _itemsListBox.Items.Clear();
                if (value == null)
                {
                    return;
                }

                foreach (string str in value)
                {
                    _itemsListBox.Items.Add(str);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using LogicLayer;
using DataObjects;
using System.Text.RegularExpressions;

namespace wpf_Presentation_Layer
{
    /// <summary>
    /// Interaction logic for FlexDetailWindow.xaml
    /// </summary>
    public partial class FlexDetailWindow : Window
    {
        // Reference existing Equipment Manager
        EquipmentManager _equipmentManager = null;
        // Initialize the item to show details of
        ItemVM _itemVM = null;
        int _original_ID;
        string _original_name;

        private string _windowType = null; // Allowed values: "newItem" "editItem"

        private string _selected_Name = null;
        private string _selected_newName = null;
        private int _selected_ID = -1;
        private int _selected_newID = -1;

        // Constructor for new ItemVM
        public FlexDetailWindow(EquipmentManager equipmentManager, string windowType)
        {
            _windowType = windowType;
            _equipmentManager = equipmentManager;

            ItemVM defaultItem = new ItemVM { ItemID = 0, ItemName = "Default Item", ItemStats = null };
            defaultItem.ItemStats.Add("Placeholder_Stat", 0);
            _itemVM = defaultItem;
            InitializeComponent();
        }
        // Constructor when using ItemVM input
        public FlexDetailWindow(EquipmentManager equipmentManager, string windowType, ItemVM itemVM)
        {
            // Set this window's private variables to the arguments being passed to it
            _windowType = windowType;
            _itemVM = itemVM;
            _original_ID = itemVM.ItemID;  // Store this so that item ID can be changed
            _original_name = itemVM.ItemName;
            _equipmentManager = equipmentManager;

            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _selected_newName = txtBoxChangeName.Text;   // Has default value

            if (_windowType.Equals("editItem") || _windowType.Equals("newItem")) // Item version of window
            {
                _selected_Name = _itemVM.ItemName;
                _selected_ID = _itemVM.ItemID;

                lblFlexDetailWindow_SelectedObject_Name.Content = _selected_Name;
                lblFlexDetailWindow_SelectedObject_ID.Content = "ID: " + _selected_ID;
                datFlexDetails.ItemsSource = _itemVM.ItemStats;
                datFlexDetails.Columns[0].Header = "Stat Name";
                datFlexDetails.Columns[1].Header = "Stat Value";
            }
        }

        private void btnChangeName_Click(object sender, RoutedEventArgs e)
        {
            if (txtBoxChangeName.IsReadOnly)    // Show disabled text box
            {
                txtBoxChangeName.Text = "";
                txtBoxChangeName.Visibility = Visibility.Visible;
                btnCancelNameChange.Visibility = Visibility.Visible;
                txtBoxChangeName.IsReadOnly = false;
                txtBoxChangeName.Focus();
                btnChangeName.IsDefault = true;
            }
            else    // Apply the change
            {
                if (txtBoxChangeName.Text != "")    // Verify input
                {
                    _selected_newName = txtBoxChangeName.Text;
                    _selected_Name = _selected_newName;
                    if (_itemVM != null) // If window is for ItemVM
                    {
                        _itemVM.ItemName = _selected_Name;
                    }
                }
                txtBoxChangeName.IsReadOnly = true;
                txtBoxChangeName.Visibility = Visibility.Hidden;
                btnCancelNameChange.Visibility = Visibility.Hidden;
                btnChangeName.IsDefault = false;
                refreshView();
            }
        }
        private void btnCancelNameChange_Click(object sender, RoutedEventArgs e)
        {
            txtBoxChangeName.Text = "";
            txtBoxChangeName.IsReadOnly = true;
            txtBoxChangeName.Visibility = Visibility.Hidden;
            btnCancelNameChange.Visibility = Visibility.Hidden;
            btnChangeName.IsDefault = false;
        }
        private void btnChangeID_Click(object sender, RoutedEventArgs e)
        {
            if (txtBoxChangeID.IsReadOnly)    // Show disabled text box
            {
                txtBoxChangeID.Text = "";
                txtBoxChangeID.Visibility = Visibility.Visible;
                btnCancelIDChange.Visibility = Visibility.Visible;
                txtBoxChangeID.IsReadOnly = false;
                txtBoxChangeID.Focus();
                btnChangeID.IsDefault = true;
            }
            else    // Apply the change
            {
                if (txtBoxChangeID.Text != "")    // Verify input
                {
                    _selected_newID = Convert.ToInt32(txtBoxChangeID.Text);
                    _selected_ID = _selected_newID;
                    if (_itemVM != null)
                    {
                        _itemVM.ItemID = _selected_ID;
                    }
                }
                txtBoxChangeID.IsReadOnly = true;
                txtBoxChangeID.Visibility = Visibility.Hidden;
                btnCancelIDChange.Visibility = Visibility.Hidden;
                btnChangeName.IsDefault = false;
                refreshView();
            }
        }
        private void btnCancelIDChange_Click(object sender, RoutedEventArgs e)
        {
            txtBoxChangeID.Text = "";
            txtBoxChangeID.IsReadOnly = true;
            txtBoxChangeID.Visibility = Visibility.Hidden;
            btnCancelIDChange.Visibility = Visibility.Hidden;
            btnChangeID.IsDefault = false;
        }
        private void txtBoxChangeID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !isTextAllowed(e.Text);
        }
        private void refreshView()  // Refresh view of current item without pulling from DB
        {
            lblFlexDetailWindow_SelectedObject_Name.Content = _selected_Name;
            lblFlexDetailWindow_SelectedObject_ID.Content = "ID: " + _selected_ID;
            datFlexDetails.Items.Refresh();
        }
        // Logic to restrict text box input to integers (From internet)
        private static readonly Regex _integerOnlyRegex = new Regex("[0-9]");
        private static bool isTextAllowed(string input)
        {
            return (_integerOnlyRegex.IsMatch(input));
        }
        // Add to selected Object
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (_windowType.Equals("editItem") || _windowType.Equals("newItem"))
            {
                var addStatToItemWindow = new AddToSelectedObjectWindow(_equipmentManager, _itemVM);
                addStatToItemWindow.ShowDialog();
                refreshView();
            }
        }
        // Edit selected object
        private void datFlexDetails_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (datFlexDetails.SelectedItem != null)
            {
                // Get the stat to be edited (There has to be a better way to do this)
                var selectedStatAsString = datFlexDetails.SelectedItem.ToString();  // Not sure about proper conversion here
                string[] selectionAsArray = selectedStatAsString.Split(',');
                char[] charactersToTrim = { '[', ']' };
                string key = selectionAsArray[0].Trim(charactersToTrim);
                int value = Convert.ToInt32(selectionAsArray[1].Trim(charactersToTrim).Trim());

                if (_windowType.Equals("editItem") || _windowType.Equals("newItem"))
                {
                    var editStatOnItemWindow = new AddToSelectedObjectWindow(_equipmentManager, _itemVM, key, value);
                    editStatOnItemWindow.ShowDialog();
                    refreshView();
                }
            }

        }
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (datFlexDetails.SelectedItem != null)
            {
                var selectedStatAsString = datFlexDetails.SelectedItem.ToString();  // Not sure about proper conversion here
                string[] selectionAsArray = selectedStatAsString.Split(',');
                char[] charactersToTrim = { '[', ']' };
                string key = selectionAsArray[0].Trim(charactersToTrim);
                int value = Convert.ToInt32(selectionAsArray[1].Trim(charactersToTrim).Trim());

                if (_windowType.Equals("editItem") || _windowType.Equals("newItem"))
                {
                    _itemVM.ItemStats.Remove(key);
                    refreshView();
                }
            }
        }

        private void btnCommit_Click(object sender, RoutedEventArgs e)
        {
            // First, confirm window type
            if (_windowType.Equals("editItem") || _windowType.Equals("newItem"))
            {
                Item validationItem = _equipmentManager.RetrieveItemVMByItemID(_itemVM.ItemID);
                if (_windowType.Equals("newItem"))
                {
                    if (validationItem.ItemID == _itemVM.ItemID)
                    {
                        MessageBox.Show("There is already an item with that ID");
                        return;
                    }
                    else if (_itemVM.ItemName.Equals("Default Item"))
                    {
                        MessageBox.Show("Please specify a name for this item.");
                        return;
                    }
                    else
                    {
                        _original_ID = _itemVM.ItemID;
                        _original_name = _itemVM.ItemName;
                        // Actually add the item to the DB
                        _equipmentManager.SendNewItemToDB(_itemVM);
                    }
                }
                if (_itemVM.ItemStats.Count > 0)
                {
                    var commitChoice = MessageBox.Show("Commit changes to database?", "Cancel commit?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (commitChoice == MessageBoxResult.Yes)
                    {
                        bool stats_added = _equipmentManager.SendItemUpdateToDB(_itemVM, _original_ID, _original_name);
                        if (stats_added)
                        {
                            MessageBox.Show("Item update successful!");
                            refreshView();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Item update failed!");
                    }
                }
                else
                {
                    MessageBox.Show("No stats to add!");
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

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
    /// Interaction logic for AddToSelectedObjectWindow.xaml
    /// </summary>
    public partial class AddToSelectedObjectWindow : Window
    {
        EquipmentManager _equipmentManager = null;
        ItemVM _itemVM = null;  // Passing an ItemVM is for adding stats to said ItemVM
        // For updating a stat
        string _statToEdit = null;  // Passing a string here is for updating the values on an existing stat
        int _valueOfStatBeingEdited = 0;
        List<string> _masterStatList = null;    // For restricting choices to existing stats
        // For returning finished stats
        string _selectedStatName = null;
        int _selectedStatValue = 0;
        // Constructor for adding a stat to an item
        public AddToSelectedObjectWindow(EquipmentManager equipmentManager, ItemVM itemVM)
        {
            _equipmentManager = equipmentManager;
            _itemVM = itemVM;
            InitializeComponent();
        }
        // Constructor for editing a stat on an item
        public AddToSelectedObjectWindow(EquipmentManager equipmentManager, ItemVM itemVM, string dictionaryKey, int dictionaryValue)
        {
            _equipmentManager = equipmentManager;
            _itemVM = itemVM;
            _statToEdit = dictionaryKey;
            _valueOfStatBeingEdited = dictionaryValue;
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _masterStatList = _equipmentManager.RetrieveMasterStatList();
            if (_itemVM != null && _statToEdit == null)  // Loading window as adding a stat
            {
                // Confirm visibility
                lblHiddenStatNameLabel.Visibility = Visibility.Hidden;
                comboStatName.Visibility = Visibility.Visible;
                comboStatName.IsReadOnly = false;

                comboStatName.ItemsSource = _masterStatList;
            }
            else if (_itemVM != null && _statToEdit != null)   // Loading window as editing a stat
            {
                // Hide the combobox, show the label
                comboStatName.Visibility = Visibility.Hidden;
                comboStatName.IsReadOnly = true;
                lblHiddenStatNameLabel.Content = _statToEdit;
                lblHiddenStatNameLabel.Visibility = Visibility.Visible;
                txtStatValue.Text = _valueOfStatBeingEdited.ToString(); // Start by showing current stat value
            }
        }

        private void comboStatName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedStatName = comboStatName.SelectedItem.ToString();
        }
        private void txtStatValue_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !isTextAllowed(e.Text);
        }


        // Logic to restrict text box input to integers
        private static readonly Regex _integerOnlyRegex = new Regex(@"-|\d");
        private static bool isTextAllowed(string input)
        {
            return (_integerOnlyRegex.IsMatch(input));
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            bool validResult = false;
            // Add stat conditions
            if (_selectedStatName != null && _masterStatList.Contains(_selectedStatName) && !txtStatValue.Text.Equals("") && txtStatValue.Text != null)
            {
                int statValueReturn = 0;
                try
                {
                    statValueReturn = Convert.ToInt32(txtStatValue.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Stat value must be an integer!");
                    return;
                }
                _selectedStatValue = statValueReturn;
                if (!_itemVM.ItemStats.ContainsKey(_selectedStatName))
                {
                    if (_selectedStatValue != 0)
                    {
                        // Validation FINALLY checks out, add the stat
                        _itemVM.ItemStats.Add(_selectedStatName, _selectedStatValue);
                        validResult = true;
                    }
                    else
                    {
                        MessageBox.Show("Please make sure to enter a value for this stat.");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("This item already has a stat of type: " + _selectedStatName + "\nPlease select a different stat, or cancel.");
                }
            }
            // Editing a stat
            else if (_selectedStatName == null && !txtStatValue.Text.Equals("") && txtStatValue.Text != null && _statToEdit != null)
            {
                int statValueReturn = 0;
                try
                {
                    statValueReturn = Convert.ToInt32(txtStatValue.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Stat value must be an integer!");
                    return;
                }
                _selectedStatValue = statValueReturn;
                if(_selectedStatValue != 0)
                {
                    // Finally update the stat dictionary with new value
                    _itemVM.ItemStats[_statToEdit] = _selectedStatValue;
                    validResult = true;
                }
                
            }
            if(validResult == true)
            {
                this.DialogResult = false;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

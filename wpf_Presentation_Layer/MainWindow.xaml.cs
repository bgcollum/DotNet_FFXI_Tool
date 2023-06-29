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
using System.Windows.Navigation;
using System.Windows.Shapes;

using DataObjects;
using LogicLayer;
using LogicLayerInterfaces;
using System.Text.RegularExpressions;


namespace wpf_Presentation_Layer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Initialize properties
        private User _user = null;

        // TESTING VARIABLE This shows all tabs while logged out!
        private Boolean showTabsWhileLoggedOut = false;

        private EquipmentManager _equipmnentManager = new EquipmentManager();
        
        private List<string> _masterStatList = null;
        private List<Item> _masterEquipmentList = null;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void frmMain_Loaded(object sender, RoutedEventArgs e)
        {
            updateUIForLogOut();
        }
        // Logged out should be default UI state
        private void updateUIForLogOut()
        {
            hideAllUserTabs();

            // Set up the input boxes
            txtEmail.Text = "";
            txtPassword.Password = "";
            txtEmail.Visibility = Visibility.Visible;
            txtPassword.Visibility = Visibility.Visible;
            lblEmail.Visibility = Visibility.Visible;
            lblPassword.Visibility = Visibility.Visible;

            btnLogin.Content = "Log In";
            btnLogin.IsDefault = true;  // Sets login button as default, enables enter key for it
            txtEmail.Focus();
        }
        // Hide all user tabs (Anonymous user Log in will enable default tabs)
        private void hideAllUserTabs()  // This is currently disabled for testing!
        {
            if (showTabsWhileLoggedOut == false)
            {
                pnlTabs.Visibility = Visibility.Hidden;
                foreach (var tab in tabSetMain.Items)
                {
                    ((TabItem)tab).Visibility = Visibility.Collapsed;
                }
            }
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(btnLogin.Content == "Log Out")
            {
                _user = null;
                updateUIForLogOut();
                return;
            }
            UserManager userManager = new UserManager();
            string email = txtEmail.Text;
            string password = txtPassword.Password;
            // Failure cases
            if(email.Length < 6)
            {
                MessageBox.Show("Invalid Email address");
                txtEmail.Text = "";
                txtEmail.Focus();
                return;
            }
            if(password == "")
            {
                MessageBox.Show("You must enter a password");
                txtPassword.Focus();
                return;
            }
            try
            {
                _user = userManager.LoginUser(email, password);
                // Prompt for fresh PW here

                // Update the UI
                showTabsForUser();
                updateUIForUser();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }
        private void btnLogin_Loaded(object sender, RoutedEventArgs e)
        {
            txtEmail.Focus();
            hideAllUserTabs();
        }
        private void updateUIForUser()
        {
            string rolesList = "";  // This is just a human readable list of roles
            for (int i = 0; i < _user.UserRoles.Count; i++)
            {
                rolesList += " " + _user.UserRoles[i];  // Concatenate list of roles
            }
            // Not doing anything with that greeting area yet

            // Clean up Log In area
            txtEmail.Text = "";
            txtPassword.Password = "";
            txtEmail.Visibility = Visibility.Hidden;
            txtPassword.Visibility = Visibility.Hidden;
            lblEmail.Visibility = Visibility.Hidden;
            lblPassword.Visibility = Visibility.Hidden;

            btnLogin.Content = "Log Out";
            btnLogin.IsDefault = false;
        }
        private void showTabsForUser()
        {
            // Loop through all user's roles
            foreach(var role in _user.UserRoles)
            {
                switch (role)
                {
                    case "Administrator":
                        tabMasterStatList.Visibility = Visibility.Visible;  // Editing this is the most dangerous thing. Admin only
                        tabMasterStatList.IsSelected = true;
                        break;
                    case "Contributor":
                        tabMasterEquipmentList.Visibility = Visibility.Visible; // Can work with master equipment list
                        tabMasterEquipmentList.IsSelected = true;
                        break;
                    case "Registered_User": // Basic user can work with characters
                        tabCharacters.Visibility = Visibility.Visible;
                        tabEquipmentSets.Visibility = Visibility.Visible;
                        tabCharacters.IsSelected = true;
                        break;
                    case "Anonymous_User":
                        // Does not have user permissions yet. Will be view-only later.
                        break;
                }
                pnlTabs.Visibility = Visibility.Visible;
            }
        }
        private void refreshMasterEquipmentList()
        {
            _masterEquipmentList = _equipmnentManager.RetrieveMasterEquipmentList();
            datMasterEquipmentList.ItemsSource = _masterEquipmentList;
        }
        private void tabMasterStatList_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_masterStatList == null)
            {
                // Populate and display master stat list
                try
                {
                    // Pull Master Stat List from Logic Layer
                    _masterStatList = _equipmnentManager.RetrieveMasterStatList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
            // display the list of stats
            lstMasterStatList.ItemsSource = _masterStatList;
            //lstMasterStatList.

        }

        private void tabMasterEquipmentList_GotFocus(object sender, RoutedEventArgs e)
        {
            if(_masterEquipmentList == null)
            {
                // Populate and display Master Equipment List
                try
                {
                    refreshMasterEquipmentList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
        }

        private void datMasterEquipmentList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(datMasterEquipmentList.SelectedItem != null)
            {
                // When user clicks on an item, bring up item detail window
                var selectedItem = (Item)datMasterEquipmentList.SelectedItem;
                ItemVM selectedItemVM = _equipmnentManager.RetrieveItemVMByItemID(selectedItem.ItemID);
                try
                {
                    string windowType = "editItem";
                    var flexDetailWindow = new FlexDetailWindow(_equipmnentManager, windowType, selectedItemVM);
                    flexDetailWindow.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
                refreshMasterEquipmentList();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Item defaultItem = new Item { ItemID = 0, ItemName = "Default Item"};
            try
            {
                string windowType = "newItem";
                var flexDetailWindow = new FlexDetailWindow(_equipmnentManager, windowType);
                flexDetailWindow.ShowDialog();
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
            refreshMasterEquipmentList();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if(datMasterEquipmentList.SelectedItem != null)
            {
                var selectedItem = (Item)datMasterEquipmentList.SelectedItem;
                var deleteChoice = MessageBox.Show("Do you really want to delete this item?", "Cancel Delete?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (deleteChoice == MessageBoxResult.Yes)
                {
                    if (_equipmnentManager.SendItemDeleteToDBByItemID(selectedItem.ItemID))
                    {
                        MessageBox.Show("Item deleted!");
                        refreshMasterEquipmentList();
                    }
                    else
                    {
                        MessageBox.Show("Item deletion failed!");
                    }
                }
            }
        }
    }
}

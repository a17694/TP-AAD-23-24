using System.Windows.Controls;
using System.Windows.Input;
using InterfaceAAD.ViewModels;
using System.Data;

namespace InterfaceAAD.Views
{
    /// <summary>
    /// Represents a view for displaying clients.
    /// </summary>
    public partial class ClientsListView : Page
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientsListView"/> class.
        /// </summary>
        public ClientsListView()
        {
            InitializeComponent();
            DataContext = new ClientListViewModel();
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the click event for editing a client.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        public void EditClient_Click(object sender, MouseButtonEventArgs e)
        {
            // Get the DataRowView selected in the data grid
            DataRowView selectedRowView = (DataRowView)dataGridClientes.SelectedItem;

            // Check if a client is selected
            if (selectedRowView != null)
            {
                // Access data from the DataRowView
                DataRow selectedDataRow = selectedRowView.Row;

                // Check if the ClienteNIF column is not null
                if (Convert.ToInt32(selectedDataRow["ClienteNIF"]) != 0)
                {
                    // Navigate to the edit page passing the selected client's NIF
                    if (NavigationService != null)
                        NavigationService.Navigate(new ClientEditView((int)selectedDataRow["ClienteNIF"]));
                }
            }
        }

        #endregion
    }
}
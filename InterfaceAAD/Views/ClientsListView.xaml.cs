using System.Windows.Controls;
using System.Windows.Input;
using InterfaceAAD.ViewModels;
using System.Data;
using System.Windows;

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



        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            // Verifica se a coluna é para a propriedade ClienteDataNasc
            if (e.PropertyName == "ClienteDataNasc")
            {
                // Cria uma nova coluna de texto e define o formato desejado
                DataGridTextColumn textColumn = new DataGridTextColumn();
                textColumn.Header = "ClienteDataNasc";
                textColumn.Binding = new System.Windows.Data.Binding("ClienteDataNasc") { StringFormat = "dd/MM/yyyy" };

                // Substitui a coluna gerada automaticamente pela personalizada
                e.Column = textColumn;
            }


            if (e.PropertyName == "ClientContacts")
            {
                e.Cancel = true; 
            }
            
        }

        private void AdicionarCliente_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new ClientEditView(0));
        }

        #endregion
    }
}
using System.Windows;
using InterfaceAAD.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;
using InterfaceAAD.Models.Entities;

namespace InterfaceAAD.Views
{
    /// <summary>
    /// Represents the view for editing a client.
    /// </summary>
    public partial class ClientEditView : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientEditView"/> class.
        /// </summary>
        /// <param name="NIF">The unique identifier of the client.</param>
        public ClientEditView(int NIF)
        {
            InitializeComponent();

            if (NIF != 0)
            {
                textBoxNIF.IsReadOnly = true;
            }

            DataContext = new ClientEditViewModel(NIF);
        }

        /// <summary>
        /// Handles the click event of the back button.
        /// Uses the NavigationService to navigate back to the previous menu.
        /// </summary>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            new ClientListViewModel().ReloadClients();
            NavigationService?.Navigate(new ClientsListView());
        }

        private void PCodeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.SelectedItem != null)
            {
                PCode selectedPCodeClient = (PCode)comboBox.SelectedItem;

                if (DataContext is ClientEditViewModel clientEditViewModel)
                {
                    clientEditViewModel.SelectedClient.CPCP = selectedPCodeClient.CP;
                }
            }
        }

        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
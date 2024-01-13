using System.Windows;
using InterfaceAAD.ViewModels;
using System.Windows.Controls;

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
            DataContext = new ClientEditViewModel(NIF);

        }




        /// <summary>
        /// Handles the click event of the back button.
        /// Uses the NavigationService to navigate back to the previous menu.
        /// </summary>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
using System.Windows.Controls;
using InterfaceAAD.ViewModels;

namespace InterfaceAAD.Views;

/// <summary>
/// Represents a view for displaying clients.
/// </summary>
public partial class ClientsListView : Page
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ClientsListView"/> class.
    /// </summary>
    public ClientsListView()
    {
        InitializeComponent();
        DataContext = new ClientViewModel();
    }
}
using InterfaceAAD.ViewModels;
using System.Windows.Controls;

namespace InterfaceAAD.Views;

public partial class PropertyListView : Page
{
    public PropertyListView()
    {
        InitializeComponent();
        DataContext = new PropertyListViewModel();
    }
}
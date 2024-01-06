using System.ComponentModel;
using System.Data;
using InterfaceAAD.Repositories;

namespace InterfaceAAD.ViewModels;

/// <summary>
/// View model for managing client data.
/// </summary>
public class ClientViewModel : INotifyPropertyChanged
{
    private DataTable _clients = null!;
    
    /// <summary>
    /// Event triggered when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Gets or sets the DataTable containing client data.
    /// </summary>
    public DataTable Clients
    {
        get { return _clients; }
        set
        {
            if (_clients != value)
            {
                _clients = value;
                OnPropertyChanged(nameof(Clients));
            }
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ClientViewModel"/> class.
    /// </summary>
    public ClientViewModel()
    {
        LoadClients();
    }

    /// <summary>
    /// Asynchronously loads client data into the DataTable.
    /// </summary>
    private async void LoadClients()
    {
        ClientRepository clientRepository = new ClientRepository();
        Clients = await clientRepository.GetAllClientsAsDataTable();
    }

    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">The name of the property that changed.</param>
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
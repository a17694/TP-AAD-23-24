using System.ComponentModel;
using System.Data;
using InterfaceAAD.Repositories;

namespace InterfaceAAD.ViewModels;

/// <summary>
/// View model for managing client data.
/// </summary>
public class ClientListViewModel : BaseViewModel
{
    private DataTable _clients = null!;
    
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
    /// Initializes a new instance of the <see cref="ClientListViewModel"/> class.
    /// </summary>
    public ClientListViewModel()
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
}
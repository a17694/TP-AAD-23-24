using InterfaceAAD.Models.Entities;
using System.Windows;
using System.Windows.Input;
using InterfaceAAD.Repositories;
using System.ComponentModel;
using System.Windows.Data;

namespace InterfaceAAD.ViewModels
{
    /// <summary>
    /// ViewModel for editing client information.
    /// </summary>
    public class ClientEditViewModel : BaseViewModel
    {
        #region Properties

        private ClientRepository _clientRepository;
        private Client _selectedClient;
        private List<TipoContacto> _tipoContacto;
        private TipoContacto _selectedContactType;
        private string _novoContato;
        private string _novoContatoTipo;
        private List<TipoContacto> _filteredTipoContacto;
        private List<PCode> _pCodeList;
        private PCode _selectedPCodeCliente;


        /// <summary>
        /// Gets or sets the list of client contacts.
        /// </summary>
        public List<ClientContact> ClientContacts { get; set; }

        /// <summary>
        /// Gets or sets the type of the new contact.
        /// </summary>
        public string NovoContatoTipo
        {
            get { return _novoContatoTipo; }
            set
            {
                _novoContatoTipo = value;
                OnPropertyChanged(nameof(NovoContatoTipo));
            }
        }

        /// <summary>
        /// Gets or sets the new contact.
        /// </summary>
        public string NovoContato
        {
            get { return _novoContato; }
            set
            {
                _novoContato = value;
                OnPropertyChanged(nameof(NovoContato));
            }
        }

        /// <summary>
        /// Gets or sets the selected client.
        /// </summary>
        public Client SelectedClient
        {
            get { return _selectedClient; }
            set
            {
                _selectedClient = value;
                OnPropertyChanged(nameof(SelectedClient));
            }
        }

        /// <summary>
        /// Gets or sets the list of contact types.
        /// </summary>
        public List<TipoContacto> TipoContacto
        {
            get { return _tipoContacto; }
            set
            {
                _tipoContacto = value;
                OnPropertyChanged(nameof(TipoContacto));
            }
        }

        /// <summary>
        /// Gets or sets the selected contact type.
        /// </summary>
        public TipoContacto SelectedContactType
        {
            get { return _selectedContactType; }
            set
            {
                _selectedContactType = value;
                OnPropertyChanged(nameof(SelectedContactType));
            }
        }

        public List<TipoContacto> TipoContactoFiltro
        {
            get { return _filteredTipoContacto; }
            set
            {
                _filteredTipoContacto = value;
                OnPropertyChanged(nameof(TipoContactoFiltro));
            }
        }

        public List<PCode> PCodeList
        {
            get { return _pCodeList; }
            set
            {
                _pCodeList = value;
                OnPropertyChanged(nameof(PCodeList));
            }
        }

        public PCode SelectedPCodeClient
        {
            get { return _selectedPCodeCliente; }
            set
            {
                _selectedPCodeCliente = value;
                OnPropertyChanged(nameof(SelectedPCodeClient));
            }

        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientEditViewModel"/> class.
        /// </summary>
        public ClientEditViewModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientEditViewModel"/> class with NavigationService and client NIF.
        /// </summary>
        /// <param name="NIF">The NIF (Número de Identificação Fiscal) of the client.</param>
        public ClientEditViewModel(int NIF)
        {
            // Initialize the ClientRepository
            _clientRepository = new ClientRepository();

            // Get the selected client by NIF
            SelectedClient = _clientRepository.GetById(NIF);

            ContactTypeRepository contactTypeRepository = new ContactTypeRepository();
            TipoContacto = contactTypeRepository.GetAll();

            FilterClientContactTypes();

            GetClientContactName();

            PCodeRepository pCodeRepository = new PCodeRepository();
            PCodeList = pCodeRepository.GetAll();

            GetClienteCP();
        }

        public void GetClienteCP()
        {
            SelectedPCodeClient = PCodeList.FirstOrDefault(cp => cp.CP == SelectedClient.CPCP);
        }

        /// <summary>
        /// Loads client contacts and associates contact types.
        /// </summary>
        public void GetClientContactName()
        {
            // Load client contacts
            ClientContacts = SelectedClient.ClientContacts;

            // Associate contact types with client contacts
            foreach (var contact in ClientContacts)
            {
                contact.TipoContacto = TipoContacto.FirstOrDefault(t => t.TpContactoID == contact.TipoContactoTpContactoID);
            }
        }

        public void FilterClientContactTypes()
        {
            // Filter contact types for the ComboBox
            var filteredContactTypes = TipoContacto
                .Where(type => !SelectedClient.ClientContacts.Any(contact => contact.TipoContactoTpContactoID == type.TpContactoID))
                .ToList();

            // Update TipoContacto with filtered contact types
            TipoContactoFiltro = filteredContactTypes;
        }

        #endregion

        #region ICommand Implementation

        /// <summary>
        /// Gets the command to save changes.
        /// </summary>
        public ICommand SaveCommand => new RelayCommand(Save);

        /// <summary>
        /// Gets the command to cancel changes.
        /// </summary>
        public ICommand CancelCommand => new RelayCommand(Cancel);

        /// <summary>
        /// Gets the command to add a new contact.
        /// </summary>
        public ICommand AdicionarContatoCommand => new RelayCommand(AdicionarContato);

        /// <summary>
        /// Gets the command to remove a contact.
        /// </summary>
        public ICommand RemoverContatoCommand => new RelayCommand(RemoverContato);

        /// <summary>
        /// Gets the command to delete a client.
        /// </summary>
        public ICommand EliminarClienteCommand => new RelayCommand(EliminarCliente);

        #endregion

        #region Private Methods

        /// <summary>
        /// Adds a new contact.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        private void AdicionarContato(object parameter)
        {
            if (SelectedContactType == null || string.IsNullOrEmpty(NovoContato))
            {
                MessageBox.Show("Por favor, selecione um tipo de contato e insira um novo contato.");
                return;
            }

            // Create a new contact
            ClientContact novoContato = new ClientContact
            {
                TipoContactoTpContactoID = SelectedContactType.TpContactoID,
                ContactoCliente = NovoContato
            };

            // Associate the contact type with the new contact
            novoContato.TipoContacto = SelectedContactType;

            // Add the new contact to the collection and DataGrid
            SelectedClient.ClientContacts.Add(novoContato);

            OnPropertyChanged(nameof(ClientContacts));
            FilterClientContactTypes();

            // Clear the fields after adding the new contact
            NovoContatoTipo = null;
            NovoContato = null;

            ICollectionView view = CollectionViewSource.GetDefaultView(SelectedClient.ClientContacts);
            view.Refresh();
        }

        /// <summary>
        /// Removes a contact.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        private void RemoverContato(object parameter)
        {
            MessageBoxResult result = MessageBox.Show("Tem certeza de que deseja remover este contato?", "Confirmação",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                if (parameter is ClientContact contactToRemove)
                {
                    // Remove the contact from the collection and DataGrid
                    SelectedClient.ClientContacts.Remove(contactToRemove);
                    OnPropertyChanged(nameof(ClientContacts));
                    FilterClientContactTypes();


                    ICollectionView view = CollectionViewSource.GetDefaultView(SelectedClient.ClientContacts);
                    view.Refresh();
                }
            }
        }

        /// <summary>
        /// Deletes a client.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        private void EliminarCliente(object parameter)
        {
            // Display a confirmation message
            MessageBoxResult result = MessageBox.Show("Tem a certeza de que deseja eliminar este cliente?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Remove the client
                _clientRepository.Delete(SelectedClient);
                SelectedClient = new Client(0);
                // Display a success message or handle other operations after deletion
                MessageBox.Show($"{SelectedClient.ClienteNome} foi eliminado com sucesso!");
            }
        }

        /// <summary>
        /// Saves changes.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        private void Save(object parameter)
        {
            if (_clientRepository.Save(SelectedClient))
            {
                MessageBox.Show($"{SelectedClient.ClienteNome} os seus dados foram guardados com sucesso!");
            }
        }

        /// <summary>
        /// Cancels changes.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        private void Cancel(object parameter)
        {
            // Logic to cancel changes
            SelectedClient = _clientRepository.GetById(SelectedClient.ClienteNIF);
            GetClientContactName();
            FilterClientContactTypes();
        }
        
        #endregion
    }
}

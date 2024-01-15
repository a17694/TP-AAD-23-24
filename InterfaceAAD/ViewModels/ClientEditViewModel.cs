using InterfaceAAD.Models.Entities;
using System.Windows;
using System.Windows.Input;
using InterfaceAAD.Repositories;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;

namespace InterfaceAAD.ViewModels;

public class ClientEditViewModel : BaseViewModel
{
    #region Properties

    private ClientRepository _clientRepository;
    private Client _selectedClient;
    private List<TipoContacto> _tipoContacto;
    private TipoContacto _selectedContactType;
    private string _novoContato;
    private string _novoContatoTipo;

    public List<ClientContact> ClientContacts { get; set; }


    public string NovoContatoTipo
    {
        get { return _novoContatoTipo; }
        set
        {
            _novoContatoTipo = value;
            OnPropertyChanged(nameof(NovoContatoTipo));
        }
    }

    public string NovoContato
    {
        get { return _novoContato; }
        set
        {
            _novoContato = value;
            OnPropertyChanged(nameof(NovoContato));
        }
    }

    public Client SelectedClient
    {
        get { return _selectedClient; }
        set
        {
            _selectedClient = value;
            OnPropertyChanged(nameof(SelectedClient));
        }
    }

    public List<TipoContacto> TipoContacto
    {
        get { return _tipoContacto; }
        set
        {
            _tipoContacto = value;
            OnPropertyChanged(nameof(TipoContacto));
        }
    }

    public TipoContacto SelectedContactType
    {
        get { return _selectedContactType; }
        set
        {
            _selectedContactType = value;
            OnPropertyChanged(nameof(SelectedContactType));
        }
    }

    #endregion

    #region Constructor

    public ClientEditViewModel()
    {
    }

    public ClientEditViewModel(int NIF)
    {
        // Initialize the ClientRepository
        _clientRepository = new ClientRepository();

        // Get the selected client by NIF
        SelectedClient = _clientRepository.GetById(NIF);


        ContactTypeRepository contactTypeRepository = new ContactTypeRepository();
        TipoContacto = contactTypeRepository.GetAll();

        GetClientContactName();
    }


    public void GetClientContactName()
    {
        // Carregar contatos do cliente
        ClientContacts = SelectedClient.ClientContacts;

        // Associar os tipos de contato aos contatos do cliente
        foreach (var contact in ClientContacts)
        {
            contact.TipoContacto =
                TipoContacto.FirstOrDefault(t => t.TpContactoID == contact.TipoContactoTpContactoID);
        }
    }

    private void AdicionarContato(object parameter)
    {
        if (SelectedContactType == null || string.IsNullOrEmpty(NovoContato))
        {
            MessageBox.Show("Por favor, selecione um tipo de contato e insira um novo contato.");
            return;
        }

        // Criar um novo contato
        ClientContact novoContato = new ClientContact
        {
            TipoContactoTpContactoID = SelectedContactType.TpContactoID,
            ContactoCliente = NovoContato
        };

        // Associar o tipo de contato ao novo contato
        novoContato.TipoContacto = SelectedContactType;

        // Adicionar o novo contato à coleção e ao DataGrid
        SelectedClient.ClientContacts.Add(novoContato);

        OnPropertyChanged(nameof(ClientContacts));

        // Limpar os campos após adicionar o novo contato
        NovoContatoTipo = null;
        NovoContato = null;

        ICollectionView view = CollectionViewSource.GetDefaultView(SelectedClient.ClientContacts);


        view.Refresh();
    }

    private void RemoverContato(object parameter)
    {
        MessageBoxResult result = MessageBox.Show("Tem certeza de que deseja remover este contato?", "Confirmação",
            MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            if (parameter is ClientContact contactToRemove)
            {
                // Remover o contato da coleção e do DataGrid
                SelectedClient.ClientContacts.Remove(contactToRemove);
                OnPropertyChanged(nameof(ClientContacts));

                ICollectionView view = CollectionViewSource.GetDefaultView(SelectedClient.ClientContacts);
                view.Refresh();
            }
        }
    }

    #endregion

    #region ICommand Implementation

    public ICommand SaveCommand => new RelayCommand(Save);
    public ICommand CancelCommand => new RelayCommand(Cancel);

    public ICommand AdicionarContatoCommand => new RelayCommand(AdicionarContato);

    public ICommand RemoverContatoCommand => new RelayCommand(RemoverContato);

    #endregion

    #region Private Methods

    private void Save(object parameter)
    {
        if (_clientRepository.Save(SelectedClient))
        {
            MessageBox.Show($"{SelectedClient.ClienteNome} Os seus dados foram guardados com sucesso!");
        }
    }

    private void Cancel(object parameter)
    {
        // Lógica para cancelar as alterações
        SelectedClient = _clientRepository.GetById(SelectedClient.ClienteNIF);
        GetClientContactName();
    }

    #endregion
}
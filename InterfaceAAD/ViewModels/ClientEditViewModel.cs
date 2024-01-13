using InterfaceAAD.Models.Entities;
using System.Windows;
using System.Windows.Input;
using InterfaceAAD.Repositories;
using System.Windows.Controls;

namespace InterfaceAAD.ViewModels;

public class ClientEditViewModel : BaseViewModel
{
    #region Properties

    private Client _selectedClient;
    private List<TipoContacto> _tipoContacto;
    private TipoContacto _selectedContactType;

    public List<ClientContact> ClientContacts { get; set; }


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
        ClientRepository clientRepository = new ClientRepository();

        // Get the selected client by NIF
        SelectedClient = clientRepository.GetById(NIF);

        // TipoContacto = (new ContactTypeRepository()).GetAll();

        ContactTypeRepository contactTypeRepository = new ContactTypeRepository();
        List<TipoContacto> allContactTypes = contactTypeRepository.GetAll();

        // Carregar contatos do cliente
        ClientContacts = SelectedClient.ClientContacts;

        // Associar os tipos de contato aos contatos do cliente
        foreach (var contact in ClientContacts)
        {
            contact.TipoContacto = allContactTypes.FirstOrDefault(t => t.TpContactoID == contact.TipoContactoTpContactoID);
        }

        // Obter a lista de tipos de contato (se necessário)
        TipoContacto = allContactTypes;


    }



    #endregion

    #region ICommand Implementation

    public ICommand SaveCommand => new RelayCommand(Save);
    public ICommand CancelCommand => new RelayCommand(Cancel);


    #endregion

    #region Private Methods

    private void Save(object parameter)
    {
        MessageBox.Show("Save Click");
        // Lógica para salvar as alterações no cliente
        // Exemplo: clientRepository.Save(SelectedClient);
    }

    private void Cancel(object parameter)
    {
        // Lógica para cancelar as alterações
        // Exemplo: SelectedClient = clientRepository.GetClientById(SelectedClient.Id);
    }

    #endregion
}
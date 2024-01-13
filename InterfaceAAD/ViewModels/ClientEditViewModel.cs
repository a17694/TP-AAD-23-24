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
    private TipoContacto _tipoContacto;


    public Client SelectedClient
    {
        get { return _selectedClient; }
        set
        {
            _selectedClient = value;
            OnPropertyChanged(nameof(SelectedClient));
        }
    }

    public TipoContacto TipoContacto
    {
        get { return _tipoContacto; }
        set
        {
            _tipoContacto = value;
            OnPropertyChanged(nameof(TipoContacto));
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

        List<TipoContacto> typeContacts = (new ContactTypeRepository()).GetAll();

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
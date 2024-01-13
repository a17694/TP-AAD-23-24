namespace InterfaceAAD.Models.Entities;

public class ClientContact
{
    public int TipoContactoTpContactoID { get; set; }
    
    public int ClienteClienteNIF { get; set; }

    public string ContactoCliente { get; set; }

    public TipoContacto TipoContacto { get; set; }

    public ClientContact() { }

}
namespace InterfaceAAD.Models.Entities;

/// <summary>
/// Represents a client entity.
/// </summary>
public class Client
{
    /// <summary>
    /// Gets or sets the NIF (Tax Identification Number) of the client.
    /// </summary>
    public int ClienteNIF { get; set; }

    /// <summary>
    /// Gets or sets the name of the client.
    /// </summary>
    public string ClienteNome { get; set; }
    
    public List<ClientContact> ClientContacts { get; set; }
    
    public Client()
    {}

    /// <summary>
    /// Initializes a new instance of the <see cref="Client"/> class with the specified NIF.
    /// </summary>
    /// <param name="clienteNif">The NIF (Tax Identification Number) of the client.</param>
    /// <param name="clienteNome">The name of the client (optional).</param>
    public Client(int clienteNif, string clienteNome = null)
    {
        ClienteNIF = clienteNif;
        ClienteNome = clienteNome;
    }
    
    
}
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

    public DateTime ClienteDataNasc {  get; set; }

    public string ClienteMorada { get; set; }

    public string CPCP { get; set; }

    public List<ClientContact> ClientContacts { get; set; } = new List<ClientContact>();

    public Client()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Client"/> class with the specified NIF.
    /// </summary>
    /// <param name="clienteNif">The NIF (Tax Identification Number) of the client.</param>
    /// <param name="clienteNome">The name of the client (optional).</param>
    /// <param name="clienteDataNasc"></param>
    /// <param name="clienteMorada"></param>
    /// <param name="cPCP"></param>
    public Client(int clienteNif, string clienteNome = null, DateTime? clienteDataNasc = null, string clienteMorada = null, string cPCP = null )
    {
        ClienteNIF= clienteNif;
        ClienteNome = clienteNome;
        ClienteDataNasc = clienteDataNasc.HasValue ? clienteDataNasc.Value : DateTime.Now;
        ClienteMorada = clienteMorada;
        CPCP = cPCP;
    }
}
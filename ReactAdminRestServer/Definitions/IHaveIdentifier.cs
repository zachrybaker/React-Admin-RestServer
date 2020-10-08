namespace ReactAdminRestServer.Definitions
{
    public interface IHaveIdentifier<TIndentifier>
    {
        TIndentifier Id { get; set; }
    }
}

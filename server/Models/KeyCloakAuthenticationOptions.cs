namespace server.cabinet.orleu.kz.Models
{
    public class KeyCloakAuthenticationOptions
    {
        public string mode { get; set; }
        public string external_client_url { get; set; }
        public string authority { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
    }
}

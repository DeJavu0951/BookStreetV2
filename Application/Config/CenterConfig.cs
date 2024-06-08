namespace Application.Config
{
    public class ConnectionStrings
    {
        public string SystemDB { get; set; }
    }

    public class JWTConfig
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }
        public int ExpireMinutes { get; set; }
    }

    public class CenterConfig
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public JWTConfig JWTConfig { get; set; }
    }
}

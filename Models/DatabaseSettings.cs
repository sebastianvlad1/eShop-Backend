namespace eShop_backend.Models{
    public class DatabaseSettings: IDatabaseSettings
    {
        public string UsersCollectionName {get; set;}
        public string ProductsCollectionName {get; set;}
        public string CartsCollectionName { get; set;}
        public string ComenziCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    public interface IDatabaseSettings
    {

        string UsersCollectionName {get; set;}
        string ProductsCollectionName {get; set;}
        string CartsCollectionName { get; set; }
        string ComenziCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
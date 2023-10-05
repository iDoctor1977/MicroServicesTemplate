namespace CoreServicesTemplate.Shared.Core.Infrastructures;

public static class ApiUrl
{
    /// <summary>
    /// Url project references
    /// </summary>
    public static class StorageRoomApi
    {
        private static readonly string StorageRoomRoot = "http://localhost:32001/api/storageroom";

        public static string StorageRoomUrlBase() => $"{StorageRoomRoot}";
        public static string GetHealthy() => $"{StorageRoomRoot}/health";

        public static string CreateWallet() => $"{StorageRoomRoot}/createwallet";
        public static string GetWallet() => $"{StorageRoomRoot}/getwallet";
        public static string GetTradingAvailableBalance() => $"{StorageRoomRoot}/gettradingavailablebalance";
        public static string GetWalletItems() => $"{StorageRoomRoot}/getwalletitems";

        public static object CreateWalletEvent() => $"{StorageRoomRoot}/createwalletevent" ;
    }
    public static class EventBusApi
    {
        private static readonly string EventBusRoot = "http://localhost:32001/api/storageroom";

        public static string EventBusUrlBase() => $"{EventBusRoot}";
        public static string GetHealthy() => $"{EventBusRoot}/health";
    }
}
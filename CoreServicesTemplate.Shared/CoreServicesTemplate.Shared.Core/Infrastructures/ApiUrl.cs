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

        public static string CreateWallet() => $"{StorageRoomRoot}/create_wallet";
        public static string GetWallet() => $"{StorageRoomRoot}/get_wallet";
        public static string GetTradingAvailableBalance() => $"{StorageRoomRoot}/get_trading_available_balance";
        public static string GetWalletItems() => $"{StorageRoomRoot}/get_wallet_items";
    }
    public static class EventBusApi
    {
        private static readonly string EventBusRoot = "http://localhost:32001/api/event-bus";

        public static string EventBusUrlBase() => $"{EventBusRoot}";
        public static string GetHealthy() => $"{EventBusRoot}/health";
    }
}
namespace CoreServicesTemplate.Shared.Core.Infrastructures;

public static class ApiUrl
{
    /// <summary>
    /// Url project references
    /// </summary>
    public static class StorageRoom
    {
        private static readonly string StorageRoomRoot = "http://localhost:32001/api/storageroom";

        public static string StorageRoomUrlBase() => $"{StorageRoomRoot}";
        public static string GetHealthy() => $"{StorageRoomRoot}/health";

        public static string CreateWallet() => $"{StorageRoomRoot}/create_wallet";
        public static string GetTradingAvailableBalance() => $"{StorageRoomRoot}/get_trading_available_balance";
        public static string GetWalletItems() => $"{StorageRoomRoot}/get_wallet_items";
    }

    /// <summary>
    /// Url project references
    /// </summary>
    public static class Dashboard
    {
        private static readonly string DashboardRoot = "http://localhost:31753/api/dashboard";

        /// <summary>
        /// Url project controller references
        /// </summary>

        public static class Wallet
        {
            private static readonly string WalletEndPoint = "wallet";

            public static string IndexFromDashboardUser() => $"{DashboardRoot}/{WalletEndPoint}/index";
            public static string CreateWalletToDashboard() => $"{DashboardRoot}/{WalletEndPoint}/post";
            public static string DeleteWalletToDashboard() => $"{DashboardRoot}/{WalletEndPoint}/delete";
            public static string UpdateWalletToDashboard() => $"{DashboardRoot}/{WalletEndPoint}/put";
            public static string GetWalletToDashboard() => $"{DashboardRoot}/{WalletEndPoint}/get";
            public static string GetAllWalletToDashboard() => $"{DashboardRoot}/{WalletEndPoint}/get";
            public static string GetErrorToDashboard() => $"{DashboardRoot}/{WalletEndPoint}/error";
            public static string GeHealthyToDashboard() => $"{DashboardRoot}/{WalletEndPoint}/health";
        }
    }
}
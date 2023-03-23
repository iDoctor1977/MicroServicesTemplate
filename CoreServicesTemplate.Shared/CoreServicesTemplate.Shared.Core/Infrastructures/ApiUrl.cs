namespace CoreServicesTemplate.Shared.Core.Infrastructures;

public static class ApiUrl
{
    /// <summary>
    /// Url project references
    /// </summary>
    public static class StorageRoom
    {
        private static readonly string StorageRoomRoot = "http://localhost:32001/api/storageroom";
        private static readonly string WalletEndPoint = "wallet";
        private static readonly string WalletItemEndPoint = "walletitem";

        public static string StorageRoomUrlBase() => $"{StorageRoomRoot}";
        public static string GetHealthyToStorageRoom() => $"{StorageRoomRoot}/health";

        /// <summary>
        /// Url project references
        /// </summary>
        public static class Wallet 
        {
            public static string WalletUrlBase() => $"{StorageRoomRoot}/{WalletEndPoint}";
            public static string IndexFromWalletToStorageRoom() => $"{StorageRoomRoot}/{WalletEndPoint}/index";
            public static string CreateWalletToStorageRoom() => $"{StorageRoomRoot}/{WalletEndPoint}/post";
            public static string DeleteWalletToStorageRoom() => $"{StorageRoomRoot}/{WalletEndPoint}/delete";
            public static string UpdateWalletToStorageRoom() => $"{StorageRoomRoot}/{WalletEndPoint}/put";
            public static string GetWalletToStorageRoom() => $"{StorageRoomRoot}/{WalletEndPoint}/get";
            public static string GetAllWalletToStorageRoom() => $"{StorageRoomRoot}/{WalletEndPoint}/get";
            public static string GetErrorToStorageRoom() => $"{StorageRoomRoot}/{WalletEndPoint}/error";
        }

        /// <summary>
        /// Url project references
        /// </summary>
        public static class WalletItem
        {
            public static string WalletItemUrlBase() => $"{StorageRoomRoot}/{WalletItemEndPoint}";
            public static string IndexFromWalletToStorageRoom() => $"{StorageRoomRoot}/{WalletItemEndPoint}/index";
            public static string CreateUserToStorageRoom() => $"{StorageRoomRoot}/{WalletItemEndPoint}/post";
            public static string DeleteWalletToStorageRoom() => $"{StorageRoomRoot}/{WalletItemEndPoint}/delete";
            public static string UpdateWalletToStorageRoom() => $"{StorageRoomRoot}/{WalletItemEndPoint}/put";
            public static string GetWalletToStorageRoom() => $"{StorageRoomRoot}/{WalletItemEndPoint}/get";
            public static string GetAllWalletToStorageRoom() => $"{StorageRoomRoot}/{WalletItemEndPoint}/get";
            public static string GetErrorToStorageRoom() => $"{StorageRoomRoot}/{WalletItemEndPoint}/error";
        }
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
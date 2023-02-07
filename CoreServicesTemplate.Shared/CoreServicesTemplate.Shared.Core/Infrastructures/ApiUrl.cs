namespace CoreServicesTemplate.Shared.Core.Infrastructures;

public static class ApiUrl
{
    /// <summary>
    /// Url project references
    /// </summary>
    public static class StorageRoom
    {
        private static readonly string StorageRoomRoot = "http://localhost:32001/api/storageroom";

        /// <summary>
        /// Url project controller references
        /// </summary>
        public static class User 
        {
            public static string IndexFromUserToStorageRoom() => $"{StorageRoomRoot}/user/index";
            public static string AddUserToStorageRoom() => $"{StorageRoomRoot}/user/add";
            public static string DeleteUserToStorageRoom() => $"{StorageRoomRoot}/user/delete/";
            public static string UpdateUserToStorageRoom() => $"{StorageRoomRoot}/user/update/";
            public static string GetUserToStorageRoom() => $"{StorageRoomRoot}/user/get";
            public static string GetAllUserToStorageRoom() => $"{StorageRoomRoot}/user/getall";
            public static string GetErrorToStorageRoom() => $"{StorageRoomRoot}/user/error";
            public static string GeHealthyToStorageRoom() => $"{StorageRoomRoot}/user/health";
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
        public static class User
        {
            public static string IndexFromDashboardUser() => $"{DashboardRoot}/user/index";
            public static string AddUserToDashboard() => $"{DashboardRoot}/user/add";
            public static string DeleteUserToDashboard() => $"{DashboardRoot}/user/delete";
            public static string UpdateUserToDashboard() => $"{DashboardRoot}/user/update";
            public static string GetUserToDashboard() => $"{DashboardRoot}/user/get";
            public static string GetAllUserToDashboard() => $"{DashboardRoot}/user/getall";
            public static string GetErrorToDashboard() => $"{DashboardRoot}/user/error";
            public static string GeHealthyToDashboard() => $"{DashboardRoot}/user/health";
        }
    }
}
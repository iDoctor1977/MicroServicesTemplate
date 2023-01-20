namespace CoreServicesTemplate.Shared.Core.Infrastructures;

public static class API
{
    public static class StorageRoom
    {
        private static readonly string StorageRoomUrl = "http://localhost:33785/storageroom/api";

        public static class User
        {
            public static string IndexFromUserToStorageRoomUrl() => $"{StorageRoomUrl}/user/index";
            public static string AddUserToStorageRoomUrl() => $"{StorageRoomUrl}/user/add";
            public static string DeleteUserToStorageRoomUrl() => $"{StorageRoomUrl}/user/delete";
            public static string UpdateUserToStorageRoomUrl() => $"{StorageRoomUrl}/user/update";
            public static string GetUserToStorageRoomUrl() => $"{StorageRoomUrl}/user/get";
            public static string GetAllUserToStorageRoomUrl() => $"{StorageRoomUrl}/user/getall";
        }
    }

    public static class Dashboard
    {
        private static readonly string DashboardUrl = "http://localhost:31753/dashboard/api";

        public static class User
        {
            public static string IndexFromDashboardUserUrl() => $"{DashboardUrl}/user/index";
            public static string AddUserToDashboardUrl() => $"{DashboardUrl}/user/add";
            public static string DeleteUserToDashboardUrl() => $"{DashboardUrl}/user/delete";
            public static string UpdateUserToDashboardUrl() => $"{DashboardUrl}/user/update";
            public static string GetUserToDashboardUrl() => $"{DashboardUrl}/user/get";
            public static string GetAllUserToDashboardUrl() => $"{DashboardUrl}/user/getall";
        }
    }
}
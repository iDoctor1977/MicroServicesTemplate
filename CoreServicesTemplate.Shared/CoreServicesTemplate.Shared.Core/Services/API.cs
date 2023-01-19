namespace CoreServicesTemplate.Shared.Core.Services;

public static class API
{
    public static class StorageRoom
    {
        private static readonly string StorageRoomUrl = "http://localhost:33785/storageroom";

        public static string IndexFromUserToStorageRoomUrl() => $"{StorageRoomUrl}/user/index";
        public static string AddUserToStorageRoomUrl() => $"{StorageRoomUrl}/user/add";
        public static string DeleteUserToStorageRoomUrl() => $"{StorageRoomUrl}/user/delete";
        public static string UpdateUserToStorageRoomUrl() => $"{StorageRoomUrl}/user/update";
        public static string GetUserToStorageRoomUrl() => $"{StorageRoomUrl}/user/get";
        public static string GetAllUserToStorageRoomUrl() => $"{StorageRoomUrl}/user/getall";
    }
}
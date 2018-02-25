using System;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace iTracker
{
    public static partial class Help
    {
        public static class Permissions
        {

            public static async Task<bool> IsCameraPermissionGranted()
            {
                return (await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera) == PermissionStatus.Granted);
            }

            public async static Task<bool> RequestCameraPermission()
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);

                bool status = false;

                if (results.ContainsKey(Permission.Camera))
                    status = (results[Permission.Camera] == PermissionStatus.Granted);

                return status;
            }
        }
    }
}

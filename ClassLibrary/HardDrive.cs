using System;
using System.Linq;
using System.Management;

namespace ClassLibrary
{
    public class HardDrive
    {
        public string DiskModel { get; set; }
        public string DiskNo { get; set; }
        public string OsSerialNumber { get; set; }

        public void Get()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                this.DiskModel = wmi_HD["Model"].ToString();
                this.DiskNo = wmi_HD["SerialNumber"].ToString();
                break;
            }

            const string queryString = "SELECT SerialNumber FROM Win32_OperatingSystem";

            string productId = (from ManagementObject managementObject in new ManagementObjectSearcher(queryString).Get()
                                from PropertyData propertyData in managementObject.Properties
                                where propertyData.Name == "SerialNumber"
                                select (string)propertyData.Value).FirstOrDefault();

            this.OsSerialNumber = productId ?? "WMI is broken";
        }
    }
}

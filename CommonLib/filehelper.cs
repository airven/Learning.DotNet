using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CommonLib
{
    public class filehelper
    {
        public static long GetRemoteFolderFreeSpace(string ip, string disksrc)
        {
            var freesize = 0L;
            try
            {
                var localIpList = GetLocalIpv4();
                //如果是本机磁盘，走这个分支
                if (localIpList.Contains(ip))
                {
                    disksrc = disksrc + ":\\";
                    var drives = DriveInfo.GetDrives();
                    foreach (var drive in drives)
                    {
                        if (drive.Name == disksrc)
                        {
                            freesize = drive.TotalFreeSpace / (1024 * 1024 * 1024);
                        }
                    }
                    return freesize;
                }
                //如果是远程磁盘，走这个分支
                var username = "gfgfgfdgfd";
                var password = "gfdgdfgdfgdfgdfgdf";
                Console.WriteLine(username);
                disksrc += ":";
                long gb = 1024 * 1024 * 1024;
                var connectionOptions = new ConnectionOptions();
                connectionOptions.Username = username;
                connectionOptions.Password = password;
                connectionOptions.Timeout = new TimeSpan(1, 1, 1, 1); //连接时间

                //ManagementScope 的服务器和命名空间。
                var path = string.Format("\\\\{0}\\root\\cimv2", ip);
                //表示管理操作的范围（命名空间）,使用指定选项初始化ManagementScope 类的、表示指定范围路径的新实例。
                var scope = new ManagementScope(path, connectionOptions);
                scope.Connect();
                //查询字符串，某磁盘上信息
                var strQuery = string.Format("select * from Win32_LogicalDisk where deviceid='{0}'", disksrc);

                var query = new ObjectQuery(strQuery);
                //查询ManagementObjectCollection返回结果集
                var searcher = new ManagementObjectSearcher(scope, query);
                foreach (var o in searcher.Get())
                {
                    var m = (ManagementObject)o;
                    if (m["Name"].ToString() == disksrc)
                    {
                        //通过m["属性名"]
                        freesize = Convert.ToInt64(m["FreeSpace"]) / gb;
                    }
                }
                Console.WriteLine(ip + " 磁盘" + disksrc + "的可用空间为" + freesize + "GB");
                return freesize;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static List<string> GetLocalIpv4()
        {
            var localIpList = new List<string>();
            try
            {
                //事先不知道ip的个数，数组长度未知，因此用StringCollection储存  
                IPAddress[] localIPs;
                localIPs = Dns.GetHostAddresses(Dns.GetHostName());
                foreach (IPAddress ip in localIPs)
                {
                    //根据AddressFamily判断是否为ipv4,如果是InterNetWorkV6则为ipv6  
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                        localIpList.Add(ip.ToString());
                }

                return localIpList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

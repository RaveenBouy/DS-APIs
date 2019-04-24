using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.BusinessLogic
{
    public static class ReferenceList
    {
        public static string IpAddr { get; set; } = "localhost:44376/";
        public static string IpAddr2 { get; set; } = "http://localhost:44444/";
        public static string Book { get; set; } = $"{IpAddr}/api/book/";
		public static string UserRegister { get; set; } = $"{IpAddr}/api/register/";
        public static string ImgProcServer1 { get; set; } = $"{IpAddr2}image1/watermark/";
        public static string ImgProcServer2 { get; set; } = $"{IpAddr2}image2/watermark/";
    }
}

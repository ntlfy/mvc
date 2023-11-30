using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_63132244.App_Start
{
    public class Cart_63132244
    {
        public string Anh_SP { get; set; }
        public string Ma_SP { get; set; }
        public string Ten_SP { get; set; }
        public int DonGia { get; set; }
        public int KhoiLuong { get; set; }
        public int ThanhTien
        {
            get
            {
                return KhoiLuong * DonGia;
            }
        }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopGiay.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Chi_tiet_don_hang
    {
        public int ID_Donhang { get; set; }
        public int ID_Sanpham { get; set; }
        public Nullable<int> soluong { get; set; }
        public Nullable<System.DateTime> Gia { get; set; }
    
        public virtual Don_hang Don_hang { get; set; }
        public virtual San_pham San_pham { get; set; }
    }
}
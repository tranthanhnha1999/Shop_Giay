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
    
    public partial class Don_hang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Don_hang()
        {
            this.Chi_tiet_don_hang = new HashSet<Chi_tiet_don_hang>();
            this.Thanh_toan = new HashSet<Thanh_toan>();
            this.Van_chuyen = new HashSet<Van_chuyen>();
        }
    
        public int ID_Donhang { get; set; }
        public Nullable<System.DateTime> Ngay_Dat_hang { get; set; }
        public int ID_Nguoidung { get; set; }
        public string Ten_Nguoidung { get; set; }
        public string Email_Nguoidung { get; set; }
        public string Mota { get; set; }
        public Nullable<decimal> Tongtien { get; set; }
        public Nullable<int> ID_Nhanvien { get; set; }
        public Nullable<int> Trangthai { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Chi_tiet_don_hang> Chi_tiet_don_hang { get; set; }
        public virtual Nguoi_dung Nguoi_dung { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Thanh_toan> Thanh_toan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Van_chuyen> Van_chuyen { get; set; }
    }
}

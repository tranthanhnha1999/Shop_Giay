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
    
    public partial class Hinh_anh
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hinh_anh()
        {
            this.San_pham = new HashSet<San_pham>();
        }
    
        public int ID_Hinhanh { get; set; }
        public int ID_Sanpham { get; set; }
        public string duongdan { get; set; }
        public Nullable<int> Trangthai { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<San_pham> San_pham { get; set; }
    }
}

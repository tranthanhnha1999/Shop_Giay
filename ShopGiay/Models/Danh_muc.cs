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
    
    public partial class Danh_muc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Danh_muc()
        {
            this.San_pham = new HashSet<San_pham>();
        }
    
        public string ID_Danhmuc { get; set; }
        public string ID_Nhom_Danh_Muc { get; set; }
        public string Ten_Danhmuc { get; set; }
        public Nullable<int> Trangthai { get; set; }
    
        public virtual Nhom_Danh_Muc Nhom_Danh_Muc { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<San_pham> San_pham { get; set; }
    }
}

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
    
    public partial class Kich_thuoc
    {
        public int ID_Size { get; set; }
        public int ID_san_pham { get; set; }
        public Nullable<int> Size_36 { get; set; }
        public Nullable<int> Size_37 { get; set; }
        public Nullable<int> Size_38 { get; set; }
        public Nullable<int> Size_39 { get; set; }
        public Nullable<int> Size_40 { get; set; }
        public Nullable<int> Size_41 { get; set; }
        public Nullable<int> Size_42 { get; set; }
    
        public virtual San_pham San_pham { get; set; }
    }
}

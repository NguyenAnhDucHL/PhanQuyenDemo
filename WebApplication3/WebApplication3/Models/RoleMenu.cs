//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RoleMenu
    {
        public int ID { get; set; }
        public Nullable<int> Role_ID { get; set; }
        public Nullable<int> Menu_ID { get; set; }
    
        public virtual Menu Menu { get; set; }
        public virtual Role Role { get; set; }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WamApplication
{
    using System;
    using System.Collections.Generic;
    
    public partial class Application
    {
        public Application()
        {
            this.Page = new HashSet<Page>();
        }
    
        public int Id { get; set; }
        public string name { get; set; }
        public Nullable<System.DateTime> created { get; set; }
        public string category { get; set; }
        public Nullable<double> price { get; set; }
        public Nullable<int> developerId { get; set; }
    
        public virtual Developer Developer { get; set; }
        public virtual ICollection<Page> Page { get; set; }
    }
}

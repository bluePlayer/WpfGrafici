//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfGraficiVezhba
{
    using System;
    using System.Collections.Generic;
    
    public partial class DimDepartmentGroup
    {
        public DimDepartmentGroup()
        {
            this.DimDepartmentGroup1 = new HashSet<DimDepartmentGroup>();
            this.FactFinance = new HashSet<FactFinance>();
        }
    
        public int DepartmentGroupKey { get; set; }
        public Nullable<int> ParentDepartmentGroupKey { get; set; }
        public string DepartmentGroupName { get; set; }
    
        public virtual ICollection<DimDepartmentGroup> DimDepartmentGroup1 { get; set; }
        public virtual DimDepartmentGroup DimDepartmentGroup2 { get; set; }
        public virtual ICollection<FactFinance> FactFinance { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FIS_174867.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class WorkHistory
    {
        public int WorkHistoryID { get; set; }
        public Nullable<int> FacultyID { get; set; }
        [Required]
        public string Organization { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public Nullable<System.DateTime> JobBeginDate { get; set; }
        [Required]
        public Nullable<System.DateTime> JobEndDate { get; set; }
        [Required]
        public string JobResponsibilities { get; set; }
        [Required]
        public string JobType { get; set; }
    
        public virtual Faculty Faculty { get; set; }
    }
}

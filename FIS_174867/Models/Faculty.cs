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

    public partial class Faculty
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Faculty()
        {
            this.CoursesTaughts = new HashSet<CoursesTaught>();
            this.Degrees = new HashSet<Degree>();
            this.Grants = new HashSet<Grant>();
            this.Publications = new HashSet<Publication>();
            this.WorkHistories = new HashSet<WorkHistory>();
        }

        public int FacultyID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public Nullable<int> Pincode { get; set; }
        [Required]
        public string MobileNo { get; set; }
        [Required]
        public Nullable<System.DateTime> HireDate { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public Nullable<System.DateTime> DateofBirth { get; set; }
        public Nullable<int> DeptID { get; set; }
        public Nullable<int> DesignationID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CoursesTaught> CoursesTaughts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Degree> Degrees { get; set; }
        public virtual Department Department { get; set; }
        public virtual Designation Designation { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Grant> Grants { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Publication> Publications { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkHistory> WorkHistories { get; set; }
    }
}

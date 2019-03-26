//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LoansApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BOOK
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BOOK()
        {
            this.LOANs = new HashSet<LOAN>();
        }
    
        public string ISBN { get; set; }
        public string Title { get; set; }
        public Nullable<int> YearPublished { get; set; }
        public Nullable<int> AuthorId { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorSurname { get; set; }
        public string AuthorTFN { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOAN> LOANs { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StartAppModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserVisitedPlaces
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserVisitedPlaces()
        {
            this.Comments = new HashSet<Comments>();
        }
    
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PlaceId { get; set; }
        public System.DateTime RecordDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual Places Places { get; set; }
        public virtual Users Users { get; set; }
    }
}

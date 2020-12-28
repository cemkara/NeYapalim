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
    
    public partial class PlaceProducts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PlaceProducts()
        {
            this.AddedByUserProducts = new HashSet<AddedByUserProducts>();
            this.PlaceMostPreferredProducts = new HashSet<PlaceMostPreferredProducts>();
        }
    
        public int Id { get; set; }
        public int PlaceId { get; set; }
        public int AllProductId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int Price { get; set; }
        public bool IsActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AddedByUserProducts> AddedByUserProducts { get; set; }
        public virtual AllProducts AllProducts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlaceMostPreferredProducts> PlaceMostPreferredProducts { get; set; }
        public virtual Places Places { get; set; }
    }
}
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
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class Places
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Places()
        {
            this.Comments = new HashSet<Comments>();
            this.PlaceCategories = new HashSet<PlaceCategories>();
            this.PlaceProducts = new HashSet<PlaceProducts>();
            this.PlaceProperties = new HashSet<PlaceProperties>();
            this.UserFavoritePlaces = new HashSet<UserFavoritePlaces>();
            this.UserRecommendedPlaces = new HashSet<UserRecommendedPlaces>();
            this.UserVisitedPlaces = new HashSet<UserVisitedPlaces>();
            this.ContentPlaces = new HashSet<ContentPlaces>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int DistrictId { get; set; }
        public Nullable<int> AveragePrice { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int PlaceTypeID { get; set; }
        public bool IsActive { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Longitude { get; set; }
        public string Distance { get; set; }
        public System.DateTime RecordDate { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
    

        public int TotalComment
        {
            get { return Comments.Count; }
            set { }
        }

        public int AveragePoint
        {
            get {
                if(Comments.Count>0)
                    return Comments.Where(x => x.IsActive).Select(x=>x.Point).Sum() / Comments.Count;
                return 0;
            }
            set { }
        }

        public string PlaceShortLocation
        {
            get
            {
                return Districts.Name + " ," + Districts.Cities.Name;
            }
            set { }
        }

        public int TotalVisited
        {
            get
            {
                return UserVisitedPlaces.Count;
            }
            set { }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual Districts Districts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlaceCategories> PlaceCategories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlaceProducts> PlaceProducts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PlaceProperties> PlaceProperties { get; set; }
        public virtual PlaceTypes PlaceTypes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserFavoritePlaces> UserFavoritePlaces { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRecommendedPlaces> UserRecommendedPlaces { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserVisitedPlaces> UserVisitedPlaces { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContentPlaces> ContentPlaces { get; set; }
    }
}

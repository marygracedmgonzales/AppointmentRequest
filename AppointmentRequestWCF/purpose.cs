namespace AppointmentRequestWCF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("purpose")]
    public partial class purpose
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public purpose()
        {
            appointments = new HashSet<appointment>();
        }

        [Key]
        public int purpose_id { get; set; }

        [Column("purpose")]
        [StringLength(30)]
        public string purpose1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<appointment> appointments { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CinemaProject.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Seanses
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Seanses()
        {
            this.Seats = new HashSet<Seats>();
        }
    
        public System.DateTime Date { get; set; }
        public int TicketCount { get; set; }
        public int Films_FK_ID { get; set; }
        public int Id { get; set; }
    
        public virtual Films Films { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Seats> Seats { get; set; }
    }
}

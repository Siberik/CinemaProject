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
    
    public partial class AdministratoRole
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AdministratoRole()
        {
            this.Administrators = new HashSet<Administrators>();
        }
    
        public int Id { get; set; }
        public string Post { get; set; }
        public string Responsibilities { get; set; }
        public string Requirements { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Administrators> Administrators { get; set; }
    }
}
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
    
    public partial class Tickets
    {
        public int TicketId { get; set; }
        public int SeansId { get; set; }
        public int Row { get; set; }
        public int Columns { get; set; }
        public int Users_Id_FK { get; set; }
    
        public virtual Seanses Seanses { get; set; }
        public virtual Users Users { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace courseproject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Объекты
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Объекты()
        {
            this.Заказы = new HashSet<Заказы>();
            this.Сотрудники = new HashSet<Сотрудники>();
        }
    
        public int ID_объекта { get; set; }
        public string Наименование { get; set; }
        public int Площадь { get; set; }
        public int Влажность { get; set; }
        public bool Наличие_морозильных_камер { get; set; }
        public double Температура { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Заказы> Заказы { get; set; }
        public virtual Склады Склады { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Сотрудники> Сотрудники { get; set; }
        public virtual Цеха_производства Цеха_производства { get; set; }
    }
}

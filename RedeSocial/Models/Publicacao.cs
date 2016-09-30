namespace RedeSocial.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Publicacao")]
    public partial class Publicacao
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }

        [Column("Publicacao")]
        [Required]
        [StringLength(500)]
        public string Publicacao1 { get; set; }

        
        public DateTime DtPublicacao { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        //public virtual Usuario Usuario { get; set; }
    }
}

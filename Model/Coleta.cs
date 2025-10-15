using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebService.Cap7.Model
{
    [Table("Coletas")]
    public class Coleta
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A data e hora da coleta são obrigatórias.")]
        public DateTime DataHora { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        [Range(0.1, 10000, ErrorMessage = "A quantidade deve estar entre 0.1 e 10.000.")]
        public double Quantidade { get; set; }  // Em Kg ou Litros

        // Relacionamento com o ponto de descarte
        [Required]
        public int PontoDeDescarteId { get; set; }

        [ForeignKey("PontoDeDescarteId")]
        public PontoDeDescarte Ponto { get; set; }

        // Relacionamento com o tipo de resíduo
        [Required]
        public int ResiduoId { get; set; }

        [ForeignKey("ResiduoId")]
        public Residuo Residuo { get; set; }
    }
}

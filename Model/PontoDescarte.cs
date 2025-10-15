using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebService.Cap7.Model
{
    [Table("PontosDeDescarte")]
    public class PontoDeDescarte
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do ponto de descarte é obrigatório.")]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        [StringLength(200)]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        [StringLength(100)]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O estado é obrigatório.")]
        [StringLength(50)]
        public string Estado { get; set; }

        // Capacidade maxima
        public double QuantidadeAtualKg { get; set; } = 0;

        public double CapacidadeMaximaKg { get; set; } = 10000;
        
        // Relacionamento com Coletas
        public ICollection<Coleta> Coletas { get; set; }
    }
}


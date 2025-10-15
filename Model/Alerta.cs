using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebService.Cap7.Model
{
    [Table("Alertas")]
    public class Alerta
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Mensagem { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        public bool Resolvido { get; set; } = false;

        // Relacionamento com ponto de descarte
        public int? PontoDeDescarteId { get; set; }

        [ForeignKey("PontoDeDescarteId")]
        public PontoDeDescarte? Ponto { get; set; }

        // Relacionamento com coleta (opcional, caso alerta esteja vinculado a uma coleta)
        public int? ColetaId { get; set; }

        [ForeignKey("ColetaId")]
        public Coleta? Coleta { get; set; }
    }
}

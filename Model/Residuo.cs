using System.ComponentModel.DataAnnotations;

namespace WebService.Cap7.Model
{
    public class Residuo
    {
        [Key]
        public int Id { get; set; } // Identificador único do resíduo

        [Required(ErrorMessage = "O nome do resíduo é obrigatório.")]
        [StringLength(100)]
        public string Nome { get; set; } // Nome do resíduo
        [StringLength(500)]public string Descricao { get; set; } // Descrição do resíduo
        public double Peso { get; set; } // Peso do resíduo em kg
        public DateTime DataColeta { get; set; } // Data da coleta do resíduo
    }
}   

using colabAPI.Business.Models.Entities;
using colabAPI.Business.Models.Entities.Enums;

namespace colabAPI.Business.DTOs
{
    public class PesquisadorDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Cpf { get; set; }
        public List<PesquisadorTime>? Times { get; set; }
    }
}
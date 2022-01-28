using System;
using System.ComponentModel.DataAnnotations;

namespace NerdStore.Catalogo.Application.ViewModels
{
    public class CategoriaViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obirgatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obirgatório.")]
        public int Codigo { get; set; }
    }
}

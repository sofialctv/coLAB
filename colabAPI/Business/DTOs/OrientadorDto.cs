
﻿using colabAPI.Business.Models.Entities;

namespace colabAPI.Business.DTOs
{
    public class OrientadorDto : PesquisadorDto
    {
        public ICollection<Bolsista> Bolsistas { get; } = new List<Bolsista>();
    }
}

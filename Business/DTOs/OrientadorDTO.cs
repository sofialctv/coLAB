﻿
﻿using colabAPI.Business.Models.Entities;

namespace colabAPI.Business.DTOs
{
    public class OrientadorDTO : PesquisadorDTO
    {
        public Bolsa? Bolsa { get; set; }

    }
}
﻿
﻿using colabAPI.Business.Models.Entities;

namespace colabAPI.Business.DTOs
{
    public class OrientadorDTO : PesquisadorDto
    {
        public Bolsa? Bolsa { get; set; }

    }
}

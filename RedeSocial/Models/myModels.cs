using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedeSocial.Models
{
    public class myModels
    {
        public Publicacao publicacao { get; set; }

        public IEnumerable<Publicacao> pubList { get; set; }
    }
}
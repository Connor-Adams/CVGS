using System;
using System.Collections.Generic;

#nullable disable

namespace Team6CVGS.Models
{
    public partial class EsrbContentDescriptor
    {
        public EsrbContentDescriptor()
        {
            GameEsrbContentDescriptors = new HashSet<GameEsrbContentDescriptor>();
        }

        public int Id { get; set; }
        public string EnglishDescriptor { get; set; }
        public string FrenchDescriptor { get; set; }

        public virtual ICollection<GameEsrbContentDescriptor> GameEsrbContentDescriptors { get; set; }
    }
}

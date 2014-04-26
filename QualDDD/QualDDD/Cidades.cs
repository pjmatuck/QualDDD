using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace QualDDD
{
    public class Cidade
    {
        [XmlElement("ID")]
        public String Id { get; set; }

        [XmlElement("NOME")]
        public String Nome { get; set; }

        [XmlElement("IDESTADO")]
        public String IdEstado { get; set; }

        [XmlElement("IDREGIAO")]
        public String IdRegiao { get; set; }

        public String DDD { get; set; }
    }

    [XmlRoot("CIDADES")]
    public class Cidades
    {
        [XmlElement("CIDADE")]
        public List<Cidade> ListaCidades { get; set; }
    }
}

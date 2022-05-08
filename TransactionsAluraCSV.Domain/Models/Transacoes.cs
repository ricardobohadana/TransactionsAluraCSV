using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TransactionsAluraCSV.Domain.Models
{
	[XmlRoot(ElementName = "origem")]
	public class Origem
	{
		[XmlElement(ElementName = "banco")]
		public string Banco { get; set; }
		[XmlElement(ElementName = "agencia")]
		public string Agencia { get; set; }
		[XmlElement(ElementName = "conta")]
		public string Conta { get; set; }
	}

	[XmlRoot(ElementName = "destino")]
	public class Destino
	{
		[XmlElement(ElementName = "banco")]
		public string Banco { get; set; }
		[XmlElement(ElementName = "agencia")]
		public string Agencia { get; set; }
		[XmlElement(ElementName = "conta")]
		public string Conta { get; set; }
	}

	[XmlRoot(ElementName = "transacao")]
	public class Transacao
	{
		[XmlElement(ElementName = "origem")]
		public Origem Origem { get; set; }
		[XmlElement(ElementName = "destino")]
		public Destino Destino { get; set; }
		[XmlElement(ElementName = "valor")]
		public string Valor { get; set; }
		[XmlElement(ElementName = "data")]
		public string Data { get; set; }
	}

	[XmlRoot(ElementName = "transacoes")]
	public class Transacoes
	{
		[XmlElement(ElementName = "transacao")]
		public List<Transacao> Transacao { get; set; }
	}
}

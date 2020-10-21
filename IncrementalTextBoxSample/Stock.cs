using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicalNuts.IncrementalTextBoxSample
{
	public class Stock
	{
		public string Code { get; set; }
		public string Name { get; set; }

		public Stock(string code, string name)
		{
			Code = code;
			Name = name;
		}

		public override string ToString()
		{
			return string.Join(" ", new string[]{ Code, Name });
		}

		public static Stock[] GetStocks()
		{
			return new Stock[]
			{
				new Stock("AA", "Alcoa Corporation Common Stock "),
				new Stock("AAA", "Listed Funds Trust AAF First Priority CLO Bond ETF"),
				new Stock("AAAU", "Perth Mint Physical Gold ETF"),
				new Stock("AACG", "ATA Creativity Global - American Depositary Shares, each representing two common shares"),
				new Stock("AACQ", "Artius Acquisition Inc. - Class A Common Stock"),
				new Stock("AACQU", "Artius Acquisition Inc. - Unit consisting of one ordinary share and one third redeemable warrant"),
				new Stock("AACQW", "Artius Acquisition Inc. - Warrant"),
				new Stock("AADR", "AdvisorShares Dorsey Wright ADR ETF"),
				new Stock("AAL", "American Airlines Group, Inc. - Common Stock"),
				new Stock("AAME", "Atlantic American Corporation - Common Stock"),
				new Stock("AAN", "Aarons Holdings Company, Inc. Common Stock"),
				new Stock("AAOI", "Applied Optoelectronics, Inc. - Common Stock"),
				new Stock("AAON", "AAON, Inc. - Common Stock"),
				new Stock("AAP", "Advance Auto Parts Inc Advance Auto Parts Inc W/I"),
				new Stock("AAPL", "Apple Inc. - Common Stock"),
				new Stock("AAT", "American Assets Trust, Inc. Common Stock"),
				new Stock("AAWW", "Atlas Air Worldwide Holdings - Common Stock"),
				new Stock("AAXJ", "iShares MSCI All Country Asia ex Japan Index Fund"),
				new Stock("AAXN", "Axon Enterprise, Inc. - Common Stock")
			};
		}
	}
}

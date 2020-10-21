using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagicalNuts.IncrementalTextBoxSample
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			// incrementalTextBox1 : 最も基本的な使用例
			incrementalTextBox1.SetCandidates(Stock.GetStocks());
		}

		private async void Form1_Load(object sender, EventArgs e)
		{
			// 複数のIncrementalTextBoxで、大量の同じ検索対象群を使用する場合、
			// それぞれのSetCandidatesを呼ぶと重くなるため、先に検索対象の辞書を作って、それぞれに設定する。
			// また、検索高速化のための辞書キー生成と、検索ワードと検索対象の関連付けを上書きする例にもなっている。
			Dictionary<string, List<IncrementalTextBox.CandidateListViewItem>> dict
				= await IncrementalTextBox.GetCandidateListViewItemDictionaryAsync(Stock.GetStocks(), KeysForDictionary, Match);
			incrementalTextBox2.CandidateListViewItemDictionary = dict;
			incrementalTextBox3.CandidateListViewItemDictionary = dict;
			incrementalTextBox2.Enabled = true;
			incrementalTextBox3.Enabled = true;
		}

		private string[] KeysForDictionary(object candidate)
		{
			// CodeとNameの先頭１文字を辞書のキーとする
			Stock stock = candidate as Stock;
			return new string[] { stock.Code.ToUpper()[0].ToString(), stock.Name.ToUpper()[0].ToString() };
		}

		private bool Match(string key, object candidate)
		{
			// CodeとNameの両方に検索が引っ掛かるようにする
			Stock stock = candidate as Stock;
			return stock.Code.ToUpper().StartsWith(key.ToUpper()) || stock.Name.ToUpper().StartsWith(key.ToUpper());
		}

		private void incrementalTextBox1_Decided(MagicalNuts.IncrementalTextBox sender, MagicalNuts.IncrementalTextBoxEventArgs e)
		{
			textBox1.Text = e.Decided.ToString();
		}
	}
}

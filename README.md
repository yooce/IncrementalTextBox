# IncrementalTextBox

![Demo](https://github.com/yooce/IncrementalTextBox/blob/main/demo.gif)

インクリメンタルサーチ機能を備えたテキストボックスです。

# Requirement

.NET Framework 4.6

# Basic Usage

インスタンスを作って、検索対象を`SetCandidates`で渡します。内部でソートされるため、検索対象をソートしておく必要はありません。

```C#
IncrementalTextBox1 incrementalTextBox = new IncrementalTextBox();

string[] candidates = new string[] { "apple", "banana", "orange", "grape", "peach", "strawberry" };
incrementalTextBox1.SetCandidates(candidates);
```

# Advanced Usage

## 検索ワードと検索対象の関連付けルールの上書き

下記のような`Stock`クラスがあったとします。株式の証券コードと会社名をイメージしています。

```C#
public class Stock
{
    public string Code { get; set; }
    public string Name { get; set; }
    
    public override string ToString()
    {
        return Code;
    }
}
```

あるインスタンスが下記のとおりだったとします。

| Code | Name |
| --- | --- |
| AAPL | Apple Inc. |

デフォルトの挙動としては、検索ワードに対し、`ToString()`の戻り値がインクリメンタルサーチされます。`Stock`クラスの`ToString()`は`Code`であるため、この例の場合、`A`という検索ワードに対しては、`AAPL`だけが候補となります。

IncrementalTextBoxは、このような検索ワードと検索対象の関連付けルールの上書き機能を提供しています。

```C#
private bool Match(string key, object candidate)
{
    Stock stock = candidate as Stock;
    return stock.Code.ToUpper().StartsWith(key.ToUpper()) || stock.Name.ToUpper().StartsWith(key.ToUpper());
}

incrementalTextBox1.Match = Match;
```

上記のようなメソッドを用意して、`Match`デリゲートに渡します。この例では、`Code`と`Name`が検索対象となるため、`A`という検索ワードに対して、`AAPL`と`Apple Inc.`が候補となります。

## 辞書用索引生成ルールの上書き

前項の`Match`を設定した場合、辞書用索引生成ルールの上書きも同時に設定してください。

`Stock`クラスのインスタンスが下記のとおりだったとします。

| Code | Name |
| --- | --- |
| 7203 | トヨタ自動車 |

デフォルトでは`ToString()`の戻り値の先頭１文字が索引として採用されます。もし仮に、`Stock`クラスのすべてのインスタンスの`Code`が数字のみだった場合、`トヨタ自動車`の`ト`は索引として登録されないことになります。

そこで、前項の`Match`を設定した上で、下記のようなメソッドを用意して、`KeysForDictionary`デリゲートに渡します。

```C#
private string[] KeysForDictionary(object candidate)
{
    Stock stock = candidate as Stock;
    return new string[] { stock.Code.ToUpper()[0].ToString(), stock.Name.ToUpper()[0].ToString() };
}

incrementalTextBox1.KeysForDictionary = KeysForDictionary;
```

この例では、`Code`と`Name`の先頭１文字を索引としているため、`トヨタ自動車`の`ト`も登録されることになります。

## 辞書生成の高速化

`SetCandidates`は内部で辞書を生成するため、大量の検索対象を渡した場合、処理が重くなります。さらに、複数のIncrementalTextBoxの`SetCandidates`をコールすると、その数だけ処理時間が増大してしまいます。

そこで、同じ検索対象であることが条件ですが、先に辞書を作っておいて、それを各IncrementalTextBoxに設定する手段を提供しています。

```C#
string[] candidates = new string[] { "apple", "banana", "orange", "grape", "peach", "strawberry" };
Dictionary<string, List<IncrementalTextBox.CandidateListViewItem>> dict
    = await IncrementalTextBox.GetCandidateListViewItemDictionaryAsync(candidates);

incrementalTextBox2.CandidateListViewItemDictionary = dict;
incrementalTextBox3.CandidateListViewItemDictionary = dict;
```

# Author

yooce

* Blog : [https://yooce.hatenablog.com/](https://yooce.hatenablog.com/)

# License

[MIT license](https://en.wikipedia.org/wiki/MIT_License).

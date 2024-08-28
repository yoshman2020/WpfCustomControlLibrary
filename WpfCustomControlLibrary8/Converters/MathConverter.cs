using System.Globalization;
using System.Windows.Data;

namespace WpfCustomControlLibrary.Converters
{
    /// <summary>
    /// バインド値の数値計算
    /// バインド値に@VALUEを使用
    /// かっこ()の次に左から右へ（×、÷などの順位はなし）
    /// </summary>
    [ValueConversion(typeof(string), typeof(double))]
    public class MathConverter : IValueConverter
    {
        /// <summary>
        /// 記号リスト
        /// </summary>
        private static readonly char[] _allOperators = ['+', '-', '*', '/', '%', '(', ')'];

        /// <summary>
        /// グループ化文字列
        /// </summary>
        private static readonly List<string> _grouping = ["(", ")"];

        /// <summary>
        /// 計算記号
        /// </summary>
        private static readonly List<string> _operators = ["+", "-", "*", "/", "%"];

        #region IValueConverter Members

        /// <summary>
        /// コンバート
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException"></exception>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 値を式にして空白除去
            var mathEquation = parameter as string ?? "";
            mathEquation = mathEquation.Replace(" ", "");
            mathEquation = mathEquation.Replace("@VALUE", value.ToString());

            // 値を検証し式の数値をリスト化
            var numbers = new List<double>();

            foreach (string s in mathEquation.Split(_allOperators))
            {
                if (s != string.Empty)
                {
                    if (double.TryParse(s, out double tmp))
                    {
                        numbers.Add(tmp);
                    }
                    else
                    {
                        // エラー 数値、記号、かっこ以外
                        throw new InvalidCastException();
                    }
                }
            }

            // パース
            EvaluateMathString(ref mathEquation, ref numbers, 0);

            if (numbers[0] < 0)
            {
                numbers[0] = 0;
            }

            // バース後は最終的に1つの値のみ
            return numbers[0];
        }

        /// <summary>
        /// 入力はないので不要
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// 数式文字列を評価し結果を数値リストに入れる
        /// </summary>
        /// <param name="mathEquation">式</param>
        /// <param name="numbers">値リスト</param>
        /// <param name="index">インデックス</param>
        /// <exception cref="FormatException">フォーマットエラー</exception>
        private static void EvaluateMathString(ref string mathEquation, ref List<double> numbers, int index)
        {
            // 式のトークンを順にループ
            string token = GetNextToken(mathEquation);

            while (token != string.Empty)
            {
                // 式のからトークンを除去
                mathEquation = mathEquation.Remove(0, token.Length);

                // トークンがグループ文字の場合範囲設定
                if (_grouping.Contains(token))
                {
                    switch (token)
                    {
                        case "(":
                            EvaluateMathString(ref mathEquation, ref numbers, index);
                            break;

                        case ")":
                            return;
                    }
                }

                // トークンが記号の場合、その記号を実行
                if (_operators.Contains(token))
                {
                    // 記号の次のトークンがかっこの場合、再帰的に実行
                    string nextToken = GetNextToken(mathEquation);
                    if (nextToken == "(")
                    {
                        EvaluateMathString(ref mathEquation, ref numbers, index + 1);
                    }

                    // 計算が終了して数値がリストに入っているか確認し
                    // 次のトークンが数値かどうか確認（再帰的に呼ばれて数値が変更される）
                    if (numbers.Count > (index + 1) &&
                        (double.Parse(nextToken) == numbers[index + 1] || nextToken == "("))
                    {
                        switch (token)
                        {
                            case "+":
                                numbers[index] = numbers[index] + numbers[index + 1];
                                break;
                            case "-":
                                numbers[index] = numbers[index] - numbers[index + 1];
                                break;
                            case "*":
                                numbers[index] = numbers[index] * numbers[index + 1];
                                break;
                            case "/":
                                numbers[index] = numbers[index] / numbers[index + 1];
                                break;
                            case "%":
                                numbers[index] = numbers[index] % numbers[index + 1];
                                break;
                        }
                        numbers.RemoveAt(index + 1);
                    }
                    else
                    {
                        // エラー 次のトークンが数値ではない
                        throw new FormatException("数値ではありません。");
                    }
                }

                token = GetNextToken(mathEquation);
            }
        }

        /// <summary>
        /// 数式の次のトークンを取得
        /// </summary>
        /// <param name="mathEquation">式</param>
        /// <returns>次のトークン</returns>
        private static string GetNextToken(string mathEquation)
        {
            // 数式の終わりの場合、string.emptyを返す
            if (mathEquation == string.Empty)
            {
                return string.Empty;
            }

            // 式の次の記号か数値を返す
            string tmp = "";
            foreach (char c in mathEquation)
            {
                if (_allOperators.Contains(c))
                {
                    return (tmp == "" ? c.ToString() : tmp);
                }
                else
                {
                    tmp += c;
                }
            }

            return tmp;
        }
    }
}

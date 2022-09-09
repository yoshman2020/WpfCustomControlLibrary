using System.Globalization;
using System.Windows.Controls;

namespace WpfCustomControlLibrary.Validation
{
    /// <summary>
    /// 数値バリデーション
    /// </summary>
    public class NumericValidationRule : ValidationRule
    {
        /// <summary>
        /// 依存プロパティ用ラッパー
        /// </summary>
        public NumericValidationWrapper NumericValidationWrapper { get; set; }

        /// <summary>
        /// 検証
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns>検証結果</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!double.TryParse(value.ToString(), out double numericValue))
            {
                return new ValidationResult(false, "数値を入力してください。");
            }
            //if (!int.TryParse(value.ToString(), out int numericValue))
            //{
            //    return new ValidationResult(false, "整数を入力してください。");
            //}
            if (numericValue < NumericValidationWrapper.Minimum ||
                NumericValidationWrapper.Maximum < numericValue)
            {
                return new ValidationResult(false,
                    $"{NumericValidationWrapper.Minimum}から" +
                    $"{NumericValidationWrapper.Maximum}の" +
                    $"範囲で入力してください。");
            }
            return ValidationResult.ValidResult;
        }
    }
}

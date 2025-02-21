using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class TransactionRequest
    {
        [Required(ErrorMessage = "שם מלא בעברית הוא שדה חובה")]
        [MaxLength(20, ErrorMessage = "שם מלא בעברית יכול להכיל עד 20 תווים")]
        [RegularExpression("^[א-ת\\s'’-]+$", ErrorMessage = "עברית בלבד")]
        public string FullNameHebrew { get; set; }

        [Required(ErrorMessage = "שם מלא באנגלית הוא שדה חובה")]
        [MaxLength(15, ErrorMessage = "שם מלא באנגלית יכול להכיל עד 15 תווים")]
        [RegularExpression("^[A-Za-z\\s'’-]+$", ErrorMessage = "אנגלית בלבד")]
        public string FullNameEnglish { get; set; }

        [Required(ErrorMessage = "תאריך לידה הוא שדה חובה")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "תעודת זהות היא שדה חובה")]
        [RegularExpression("^\\d{9}$", ErrorMessage = "תעודת זהות לא תקינה")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "סוג הפעולה הוא שדה חובה")]
        [RegularExpression("^(deposit|withdraw)$", ErrorMessage = "סוג פעולה לא תקין")]
        public string TransactionType { get; set; }

        [Required(ErrorMessage = "סכום הוא שדה חובה")]
        [Range(0.01, 9999999999, ErrorMessage = "הסכום חייב להיות מספר חיובי")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "מספר חשבון הוא שדה חובה")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "מספר חשבון לא תקין")]
        public string BankAccount { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace RaceParsing.Models
{
    public class FormulaModel
    {
        [Required(ErrorMessage = "Пожалуйста, введите ссылку")]
        [Display(Name = "Ссылка для парсинга")]
        public string Link { get; set; } = "https://www.formula1.com/en/results.html/2017/races/970/belgium/race-result.html";

        [Display(Name = "Таблица для парсинга")]
        public string Description { get; set; } = "<html></html>";
    }
}

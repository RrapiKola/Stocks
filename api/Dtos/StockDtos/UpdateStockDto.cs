using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.StockDtos
{
    public class UpdateStockDto
    {
        [Required(ErrorMessage = "Symbol is required")]
    public string Symbol { get; set; } = string.Empty;

    [Required(ErrorMessage = "CompanyName is required")]
    public string CompanyName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Purchase is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Purchase must be greater than 0")]
    public decimal Purchase { get; set; }

    [Required(ErrorMessage = "LastDiv is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "LastDiv must be greater than 0")]
    public decimal LastDiv { get; set; }

    [Required(ErrorMessage = "Industry is required")]
    public string Industry { get; set; } = string.Empty;

    [Required(ErrorMessage = "MarketCap is required")]
    [Range(1, long.MaxValue, ErrorMessage = "MarketCap must be greater than 0")]
    public long MarketCap { get; set; }
    }
}
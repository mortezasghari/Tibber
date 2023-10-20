using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tibber.TechnicalCase.Infrastructure.Entities;

public class ResultEntity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public DateTimeOffset TimeStamp { get; set; }
    public int Commands { get; set; }
    public int Result { get; set; }

    public double Duration { get; set; }
}
﻿using BusinessLogic.IModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.Models;

[Table("target_audience")]
public class TargetAudience : ITargetAudience
{
    [Column("id")]
    public int Id { get; set; }

    [Column("from")]
    public int From { get; set; }

    [Column("to")]
    public int To { get; set; }

    [Column("label")]
    public string? Label { get; set; }

    public int bullshit {  get; set; }
}

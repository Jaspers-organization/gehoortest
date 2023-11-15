﻿using System.ComponentModel.DataAnnotations.Schema;

namespace gehoortest.application_Repository.Models.TestData_Management;

[Table("test_result")]
public class TestResult
{
    [Column("id")]
    public int Id { get; set; }

    [Column("branch_id")]
    internal int BranchId { get; set; }

    [Column("client_id")]
    internal int? ClientId { get; set; }

    [Column("start_date_time")]
    public DateTime StartDate { get; set; }

    [Column("duration")]
    public int TestDuration { get; set; }

    [Column("test_answers")]
    public string? TestAnswers { get; set; }

    //public virtual IClient? Client { get; set; }

    //public virtual IBranch Branch { get; set; }
}

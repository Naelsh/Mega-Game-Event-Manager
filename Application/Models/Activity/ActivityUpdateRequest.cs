﻿namespace Application.Models.Activity;

public class ActivityUpdateRequest
{
    public string? Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
}

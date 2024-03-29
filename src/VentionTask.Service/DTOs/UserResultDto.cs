﻿using System.ComponentModel.DataAnnotations;
using Ventiontask.Domain.Entities;

namespace Ventiontask.Service.DTOs;

public class UserResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public long GroupId { get; set; }
}

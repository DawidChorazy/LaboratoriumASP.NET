﻿using System;
using System.Collections.Generic;

namespace WebApp.Models.Olympics;

public partial class NocRegion
{
    public int Id { get; set; }

    public string? Noc { get; set; }

    public string? RegionName { get; set; }
}

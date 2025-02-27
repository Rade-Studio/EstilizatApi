﻿using System;

namespace Models.DbEntities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateUTC { get; set; }
        public DateTime LastUpdateUTC { get; set; }
    }
}
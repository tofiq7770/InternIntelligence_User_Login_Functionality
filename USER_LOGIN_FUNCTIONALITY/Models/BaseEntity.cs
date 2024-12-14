﻿namespace USER_LOGIN_FUNCTIONALITY.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public bool SoftDelete { get; set; } = false;
    }
}

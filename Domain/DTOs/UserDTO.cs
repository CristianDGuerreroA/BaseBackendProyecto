﻿using Domain.Entities;

namespace Domain.DTOs
{
    public class UserDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public long Phone { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}

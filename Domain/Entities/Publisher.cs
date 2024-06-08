﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Lớp này chứa thông tin về nhà xuất bản
    /// </summary>
    public class Publisher
    {
        [Key] 
        public int PublisherId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Year { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}

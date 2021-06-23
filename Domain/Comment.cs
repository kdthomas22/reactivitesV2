using System;
using Domain;

namespace reactivitiesV2.Domain
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body  { get; set; }
        public User Author { get; set; }
        public Activity Activity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
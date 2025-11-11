using System;

namespace ABCProperties.Domain.Entities
{
    public class Property
    {
        public int Id { get; set; }
        /// <summary>
        /// Foreign Key to the Agent entity
        /// </summary>
        public int AgentId { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public decimal Price { get; set; }
        public DateTime ListingDate { get; set; }

        public Agent Agent { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class WebSiteInformation
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date{ get; set; }
        public bool IsWebSiteUp { get; set; }
        public string Url { get; set; }
    }
}

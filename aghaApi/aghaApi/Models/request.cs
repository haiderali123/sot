//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace aghaApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class request
    {
        public int Id { get; set; }
        public int user_id { get; set; }
        public string request_status { get; set; }
        public int requested_worker_id { get; set; }
        public int sid { get; set; }
        public System.DateTime request_date { get; set; }
        public System.TimeSpan request_time { get; set; }
        public string Alloted_slots { get; set; }
    
        public virtual user user { get; set; }
        public virtual service service { get; set; }
        public virtual worker_Portfolio worker_Portfolio { get; set; }
    }
}

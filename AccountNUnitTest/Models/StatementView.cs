using System;
using System.Collections.Generic;
using System.Text;

namespace AccountNUnitTest.Models
{
    public class StatementView
    {
        public int Id { get; set; }
        public DateTime from_date { get; set; }
        public DateTime to_date { get; set; }
    }
}

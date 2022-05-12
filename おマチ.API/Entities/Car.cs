using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace おマチ.API.Entities
{
    public class Car
    {
        #region Attributes
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public String Name { get; set; }

        public String Number { get; set; }

        public String Color { get; set; }
        #endregion Attributes
    }
}

using ReactAdminRestServer.Definitions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactAdminRestServer.DemoAPI.Data.Entities
{
    public partial class TestItem : IHaveIdentifier<string>
    {
        public TestItem()
        {
            #region Generated Constructor
            #endregion
        }
        
        [NotMapped]
        public string Id { get { return Key; } set { Key = value;} }

        #region Generated Properties
        public string Key { get; set; }

        public string Value { get; set; }

        #endregion

        #region Generated Relationships
        #endregion

    }
}

using System;
using DevExpress.ExpressApp.DC;

namespace DomainComponents {
    [DomainComponent]
    public interface IPerson {
        String Name { get; set; }        
    }
    [DomainComponent]
    public interface IProduct {
        Decimal Price { get; set; }
        String Name { get; set; }        
    }
    [DomainComponent]
    public interface ISale {
        IProduct Product { get; set; }
        IPerson Person { get; set; }
        Decimal Count { get; set; }
    }
}

 


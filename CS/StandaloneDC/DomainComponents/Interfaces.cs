using System;

namespace DomainComponents {
    public interface IPerson {
        String Name { get; set; }        
    }
    public interface IProduct {
        Decimal Price { get; set; }
        String Name { get; set; }        
    }
    public interface ISale {
        IProduct Product { get; set; }
        IPerson Person { get; set; }
        Decimal Count { get; set; }
    }
}

 


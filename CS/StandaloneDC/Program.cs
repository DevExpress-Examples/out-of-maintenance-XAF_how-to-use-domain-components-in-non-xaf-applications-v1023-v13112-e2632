using System;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using DomainComponents;

namespace StandaloneDC {
    class Program {
        static void Main(string[] args) {
            XafTypesInfo.Instance.AddEntityToGenerate("Person", typeof(IPerson));
            XafTypesInfo.Instance.AddEntityToGenerate("Product", typeof(IProduct));
            XafTypesInfo.Instance.AddEntityToGenerate("Sale", typeof(ISale));
            XafTypesInfo.Instance.GenerateEntities();

            String connectionString = "Integrated Security=SSPI;Pooling=false;Data Source=(local);Initial Catalog=MyBase";            
            DatabaseUpdater databaseUpdater = new DatabaseUpdater("", connectionString, null);
            databaseUpdater.Update();

            ConnectionStringDataStoreProvider dataStoreProvider = new ConnectionStringDataStoreProvider(connectionString);
            using(ObjectSpaceProvider objectSpaceProvider = new ObjectSpaceProvider(dataStoreProvider)) {                
                IObjectSpace objectSpace = objectSpaceProvider.CreateObjectSpace();
                
                IPerson johnSmith = objectSpace.FindObject<IPerson>(CriteriaOperator.Parse("Name == 'John Smith'"));
                if(johnSmith == null) {
                    johnSmith = objectSpace.CreateObject<IPerson>();
                    johnSmith.Name = "John Smith";
                }
                IProduct geitost = objectSpace.FindObject<IProduct>(CriteriaOperator.Parse("Name == 'Geitost'"));
                if(geitost == null) {
                    geitost = objectSpace.CreateObject<IProduct>();
                    geitost.Name = "Geitost";
                    geitost.Price = 11.95M;
                }
                ISale sale = objectSpace.FindObject<ISale>(CriteriaOperator.Parse("Person.Name == 'John Smith' and Product.Name == 'Geitost'"));
                if(sale == null) {
                    sale = objectSpace.CreateObject<ISale>();
                    sale.Person = johnSmith;
                    sale.Product = geitost;
                }
                objectSpace.CommitChanges();
            }
        }
    }
}

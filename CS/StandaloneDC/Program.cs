using System;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.DC.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Updating;
using DomainComponents;

namespace StandaloneDC {
    class Program {
        static void Main(string[] args) {
            XpoTypesInfoHelper.ForceInitialize();
            ITypesInfo typesInfo = XpoTypesInfoHelper.GetTypesInfo();
            XpoTypeInfoSource xpoTypeInfoSource = XpoTypesInfoHelper.GetXpoTypeInfoSource();
            typesInfo.RegisterEntity("Person", typeof(IPerson));
            typesInfo.RegisterEntity("Product", typeof(IProduct));
            typesInfo.RegisterEntity("Sale", typeof(ISale));
            typesInfo.GenerateEntities();
            String connectionString = 
                "Integrated Security=SSPI;Pooling=false;Data Source=(local);Initial Catalog=MyBase";
            ConnectionStringDataStoreProvider dataStoreProvider =
                new ConnectionStringDataStoreProvider(connectionString);
            using(XPObjectSpaceProvider objectSpaceProvider = 
                new XPObjectSpaceProvider(dataStoreProvider, typesInfo, xpoTypeInfoSource)) {
                typesInfo.RegisterEntity(objectSpaceProvider.ModuleInfoType);
                DatabaseUpdater databaseUpdater = new DatabaseUpdater(
                    objectSpaceProvider, new ModuleBase[0], "", objectSpaceProvider.ModuleInfoType);
                databaseUpdater.Update();
                IObjectSpace objectSpace = objectSpaceProvider.CreateObjectSpace();
                IPerson johnSmith = objectSpace.FindObject<IPerson>(
                    CriteriaOperator.Parse("Name == 'John Smith'"));
                if(johnSmith == null) {
                    johnSmith = objectSpace.CreateObject<IPerson>();
                    johnSmith.Name = "John Smith";
                }
                IProduct geitost = objectSpace.FindObject<IProduct>(
                    CriteriaOperator.Parse("Name == 'Geitost'"));
                if(geitost == null) {
                    geitost = objectSpace.CreateObject<IProduct>();
                    geitost.Name = "Geitost";
                    geitost.Price = 11.95M;
                }
                ISale sale = objectSpace.FindObject<ISale>(CriteriaOperator.Parse(
                    "Person.Name == 'John Smith' and Product.Name == 'Geitost'"));
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

Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.DC
Imports DevExpress.ExpressApp.DC.Xpo
Imports DevExpress.ExpressApp.Xpo
Imports DevExpress.ExpressApp.Updating
Imports DomainComponents

Namespace StandaloneDC
    Friend Class Program
        Shared Sub Main(ByVal args() As String)
            XpoTypesInfoHelper.ForceInitialize()
            Dim typesInfo As ITypesInfo = XpoTypesInfoHelper.GetTypesInfo()
            Dim xpoTypeInfoSource As XpoTypeInfoSource = XpoTypesInfoHelper.GetXpoTypeInfoSource()
            typesInfo.RegisterEntity("Person", GetType(IPerson))
            typesInfo.RegisterEntity("Product", GetType(IProduct))
            typesInfo.RegisterEntity("Sale", GetType(ISale))
            typesInfo.GenerateEntities()
            Dim connectionString As String = "Integrated Security=SSPI;Pooling=false;Data Source=(local);Initial Catalog=MyBase"
            Dim dataStoreProvider As New ConnectionStringDataStoreProvider(connectionString)
            Using objectSpaceProvider As New XPObjectSpaceProvider(dataStoreProvider, typesInfo, xpoTypeInfoSource)
                typesInfo.RegisterEntity(objectSpaceProvider.ModuleInfoType)
                Dim databaseUpdater As New DatabaseUpdater(objectSpaceProvider, New ModuleBase(){}, "", objectSpaceProvider.ModuleInfoType)
                databaseUpdater.Update()
                Dim objectSpace As IObjectSpace = objectSpaceProvider.CreateObjectSpace()
                Dim johnSmith As IPerson = objectSpace.FindObject(Of IPerson)(CriteriaOperator.Parse("Name == 'John Smith'"))
                If johnSmith Is Nothing Then
                    johnSmith = objectSpace.CreateObject(Of IPerson)()
                    johnSmith.Name = "John Smith"
                End If
                Dim geitost As IProduct = objectSpace.FindObject(Of IProduct)(CriteriaOperator.Parse("Name == 'Geitost'"))
                If geitost Is Nothing Then
                    geitost = objectSpace.CreateObject(Of IProduct)()
                    geitost.Name = "Geitost"
                    geitost.Price = 11.95D
                End If
                Dim sale As ISale = objectSpace.FindObject(Of ISale)(CriteriaOperator.Parse("Person.Name == 'John Smith' and Product.Name == 'Geitost'"))
                If sale Is Nothing Then
                    sale = objectSpace.CreateObject(Of ISale)()
                    sale.Person = johnSmith
                    sale.Product = geitost
                End If
                objectSpace.CommitChanges()
            End Using
        End Sub
    End Class
End Namespace

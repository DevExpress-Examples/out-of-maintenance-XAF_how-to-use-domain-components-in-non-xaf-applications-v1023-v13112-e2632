Imports DevExpress.ExpressApp.DC

Namespace DomainComponents
    <DomainComponent>
    Public Interface IPerson
        Property Name() As String
    End Interface
    <DomainComponent>
    Public Interface IProduct
        Property Price() As Decimal
        Property Name() As String
    End Interface
    <DomainComponent>
    Public Interface ISale
        Property Product() As IProduct
        Property Person() As IPerson
        Property Count() As Decimal
    End Interface
End Namespace




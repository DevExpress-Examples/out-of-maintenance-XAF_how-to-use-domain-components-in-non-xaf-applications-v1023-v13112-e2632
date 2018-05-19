Imports DevExpress.ExpressApp.DC

Namespace DomainComponents
    <DomainComponent> _
    Public Interface IPerson
        Property Name() As String
    End Interface
    <DomainComponent> _
    Public Interface IProduct
        Property Price() As Decimal
        Property Name() As String
    End Interface
    <DomainComponent> _
    Public Interface ISale
        Property Product() As IProduct
        Property Person() As IPerson
        Property Count() As Decimal
    End Interface
End Namespace




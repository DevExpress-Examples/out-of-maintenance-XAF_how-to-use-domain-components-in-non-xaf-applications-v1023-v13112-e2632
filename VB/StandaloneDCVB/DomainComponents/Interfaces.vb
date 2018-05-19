Imports Microsoft.VisualBasic
Imports System

Namespace DomainComponents
	Public Interface IPerson
		Property Name() As String
	End Interface
	Public Interface IProduct
		Property Price() As Decimal
		Property Name() As String
	End Interface
	Public Interface ISale
		Property Product() As IProduct
		Property Person() As IPerson
		Property Count() As Decimal
	End Interface
End Namespace




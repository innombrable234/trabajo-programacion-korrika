Public Class KilometroFinanciado : Inherits Kilometro
    Public Property Organizacion As String
    Public Property Euros As Decimal

    Public Sub New()
        MyBase.New()
        Organizacion = ""
        Euros = 0
    End Sub
    Public Sub New(numKm As Integer)
        MyBase.New(numKm)
        Organizacion = ""
        Euros = 0
    End Sub
    Public Sub New(numKm As Integer, direccion As String, localidad As String, provincia As String)
        MyBase.New(numKm, direccion, localidad, provincia)
        Organizacion = ""
        Euros = 0
    End Sub
    Public Sub New(numKm As Integer, direccion As String, localidad As String, provincia As String, organizacion As String, euros As Decimal)
        MyBase.New(numKm, direccion, localidad, provincia)
        Me.Organizacion = organizacion
        Me.Euros = euros
    End Sub
End Class

Public Class Kilometro
    Implements IEquatable(Of Kilometro)

    Public Property NumKm As Integer
    Public Property Direccion As String
    Public Property Localidad As String
    Public Property Provincia As String

    Public Sub New()
        NumKm = 0
        Direccion = ""
        Localidad = ""
        Provincia = ""
    End Sub
    Public Sub New(numKm As Integer)
        Me.New()
        Me.NumKm = numKm
    End Sub

    Public Sub New(numKm As Integer, direccion As String, localidad As String, provincia As String)
        Me.NumKm = numKm
        Me.Direccion = direccion
        Me.Localidad = localidad
        Me.Provincia = provincia
    End Sub

    Public Overrides Function Equals(obj As Object) As Boolean
        Return Equals(TryCast(obj, Kilometro))
    End Function

    Public Overloads Function Equals(other As Kilometro) As Boolean Implements IEquatable(Of Kilometro).Equals
        Return other IsNot Nothing AndAlso
               NumKm = other.NumKm
    End Function

    Public Shared Operator =(left As Kilometro, right As Kilometro) As Boolean
        Return EqualityComparer(Of Kilometro).Default.Equals(left, right)
    End Operator

    Public Shared Operator <>(left As Kilometro, right As Kilometro) As Boolean
        Return Not left = right
    End Operator

    Public Overrides Function ToString() As String
        Return $"{NumKm} '{Localidad}' en {Provincia}"
    End Function
End Class

Imports System.Collections.ObjectModel
Imports System.IO
Imports Entidades

Public Class Korrika
    Private Const NOMBREFICHERO As String = "./Ficheros/Korrika23.txt"
    Public ReadOnly Property Cambios As Boolean
        Get
            Dim estandar As String() = File.ReadAllLines(NOMBREFICHERO)

            If String.Equals(estandar, File.ReadAllLines(NOMBREFICHERO).ToString) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property
    Public Property DatosKorrika As DatosGeneralesKorrika
    Private _Provincias As New List(Of String) From {"araba", "gipuzkoa", "nafarroa", "bizkaia", "zuberoa", "nafarra behera", "lapurdi"}
    Public ReadOnly Property Provincias As ReadOnlyCollection(Of String)
        Get
            Return _Provincias.AsReadOnly
        End Get
    End Property


    Private _Kilometros As New List(Of Kilometro) From {}
    Public ReadOnly Property Kilometros As ReadOnlyCollection(Of Kilometro)
        Get
            Return _Kilometros.AsReadOnly
        End Get
    End Property
    Public ReadOnly Property TotalRecaudado As Decimal
        Get
            Dim totalEuros As Decimal = 0
            For Each km In _Kilometros
                If TypeOf km Is KilometroFinanciado Then
                    totalEuros += TryCast(km, KilometroFinanciado).Euros
                End If
            Next
            Return totalEuros
        End Get
    End Property
    Public Sub New(datosKorrika As DatosGeneralesKorrika, ByRef mensajeError As String)
        Me.DatosKorrika = datosKorrika
        If File.Exists(NombreFichero) Then
            mensajeError = $"la korrika {datosKorrika.NKorrika} ya existe"
        Else
            Dim arraydatos As New List(Of String)
            arraydatos.AddRange(datosKorrika)
            For i = 1 To datosKorrika.CantKms

                _Kilometros.Add(New Kilometro(i))
            Next
            For i = 0 To arraydatos.Count
                File.WriteAllLines(NombreFichero, arraydatos.ToArray)
            Next
        End If

    End Sub
    Public Sub New(ByRef mensajeError As String)
        Me.DatosKorrika = DatosKorrika

        If File.Exists(NombreFichero) Then
            mensajeError = $"ya existe una korrika"
        Else
            Dim arraydatos As New List(Of String)
            arraydatos.AddRange(DatosKorrika)

            For i = 0 To arraydatos.Count
                If DatosKorrika.NKorrika = File.ReadAllLines(NombreFichero)(0) Then
                    For j = 0 To arraydatos.Count
                        File.WriteAllLines(NombreFichero, arraydatos.ToArray)

                    Next
                Else
                    mensajeError = $"el archivo {NombreFichero} no corresponde a la korrika actual"
                End If
            Next
        End If


    End Sub
    Public Sub New(datosKorrika As DatosGeneralesKorrika)
        Me.DatosKorrika = datosKorrika
        For i = 1 To datosKorrika.CantKms
            _Kilometros.Add(New Kilometro(i))
        Next
    End Sub
    Public Sub New(nKorrika As Byte, anyo As Integer, eslogan As String, fechaInicio As Date, fechaFin As Date, cantKms As Integer)
        Me.New(New DatosGeneralesKorrika(nKorrika, anyo, eslogan, fechaInicio, fechaFin, cantKms))
    End Sub

    Public Overrides Function ToString() As String
        Return DatosKorrika.ToString
    End Function

    Public Overloads Function toString(strCorto As Boolean) As String
        If strCorto Then
            Return $"Korrika {DatosKorrika.NKorrika} {DatosKorrika.Eslogan}"
        End If
        Return ToString()
    End Function
    Public Function DefinirKm(km As Kilometro) As String
        If km Is Nothing Then
            Return "No puede ser Nothing"
        End If
        If String.IsNullOrWhiteSpace(km.Direccion) Then
            Return "La dirección no puede estar vacía"
        End If
        If String.IsNullOrWhiteSpace(km.Localidad) Then
            Return "La localidad no puede estar vacía"
        End If
        If String.IsNullOrWhiteSpace(km.Provincia) Then
            Return "La provincia no puede estar vacía"
        End If
        If Not _Kilometros.Contains(km) Then
            Return $"No existe el kilómetro {km.NumKm}"
        End If
        If Not Provincias.Contains(km.Provincia.ToLower) Then
            Return $"No existe la provincia {km.Provincia}"
        End If
        For Each klmtro In _Kilometros
            If km.Direccion = klmtro.Direccion AndAlso km.Localidad = klmtro.Localidad Then
                Return $"El kilómetro número {klmtro.NumKm} ya comienza en la dirección {klmtro.Direccion} de {klmtro.Localidad}"
            End If
        Next
        _Kilometros(_Kilometros.IndexOf(km)) = km
        Return "Se definió correctamente"
    End Function
    Public Function DefinirKm(numKm As Integer, direccion As String, localidad As String, provincia As String) As String
        Return DefinirKm(New Kilometro(numKm, direccion, localidad, provincia))
    End Function
    Public Function PatrocinarKm(numKm As Integer, organizacion As String, euros As Decimal) As String
        Dim km As New Kilometro(numKm)
        Dim pos As Integer = _Kilometros.IndexOf(km)
        If pos = -1 Then
            Return $"Mo existe el kilómetro {numKm}"
        End If
        km = _Kilometros(pos)
        If TypeOf km Is KilometroFinanciado Then
            Return $"El kilómetro número {numKm} ya está financiado por {TryCast(km, KilometroFinanciado).Organizacion}"
        End If
        Dim cantKmDeOrganizacion As Integer = 0
        For Each klmtro In _Kilometros
            If Not TryCast(klmtro, KilometroFinanciado) Is Nothing AndAlso TryCast(klmtro, KilometroFinanciado).Organizacion.ToLower = organizacion.ToLower Then
                cantKmDeOrganizacion += 1
            End If
        Next
        _Kilometros(pos) = New KilometroFinanciado(km.NumKm, km.Direccion, km.Localidad, km.Provincia, organizacion, euros)
        If cantKmDeOrganizacion > 0 Then
            Return $"La organización {organizacion} ya había financiado ´{cantKmDeOrganizacion} kilómetros(se ha financiado este nuevo kilómetro exitosamente)"
        End If
        Return $"La organización {organizacion} financia el kilómetro {numKm} exitosamente"
    End Function
    Public Function KmLibresDeProvincia(prov As String) As List(Of Kilometro)
        If Not _Provincias.Contains(prov.ToLower) Then
            Return Nothing
        End If
        Dim kmLibres As New List(Of Kilometro)
        For Each km In _Kilometros
            If TypeOf km IsNot KilometroFinanciado AndAlso km.Provincia.ToLower = prov.ToLower Then
                kmLibres.Add(km)
            End If
        Next
        Return kmLibres
    End Function
    Public Function LeerKorrika() As String
        Dim existeFichero = File.Exists(NombreFichero)

        If existeFichero = False Then
            Return $"El fichero {NombreFichero} no existe"
        End If
        Dim lineas() As String = File.ReadAllLines(NombreFichero)
        _Kilometros = New List(Of Kilometro)
        For Each linea In lineas
            Dim verificarFecha As Date
            Dim datos As String() = linea.Split("*")
            If Date.TryParse(datos(2), verificarFecha) Then
                If datos.Length < 4 Then
                    Dim kilometroCrear As New Kilometro(datos(0), datos(1), datos(2), datos(3))
                    _Kilometros.Add(kilometroCrear)
                Else
                    Dim kilometroCrear As New KilometroFinanciado(datos(0), datos(1), datos(2), datos(3), datos(4), datos(5))
                    _Kilometros.Add(kilometroCrear)
                End If

            End If
        Next
        Return ""
    End Function
    Public Function GrabarFichero() As String
        If Not File.Exists(NombreFichero) Then
            Return $"No ha sido posible guardar porque el fichero {NombreFichero} no existe"
        End If

        If DatosKorrika Is Nothing OrElse Kilometros Is Nothing OrElse Kilometros.Count < 1 Then
            Return "Aún no has añadido ningún kilómetro en la lista"
        End If
        Dim kmStr As String = $"{Me.DatosKorrika.NKorrika}{Me.DatosKorrika.Anyo}{Me.DatosKorrika.Eslogan} {Me.DatosKorrika.Eslogan}{Me.DatosKorrika.FechaInicio}{Me.DatosKorrika.FechaFin}{Me.DatosKorrika.CantKms}"


        Dim todosKms As New List(Of String)
        For Each km As Kilometro In Kilometros
            kmStr &= $"{km.NumKm}{km.Direccion}{km.Localidad}{km.Provincia}"
            If TypeOf km Is KilometroFinanciado Then
                Dim kmfinan As KilometroFinanciado = TryCast(km, KilometroFinanciado)
                kmStr &= $"{kmfinan.Organizacion}{kmfinan.Euros}"
            End If
            todosKms.Add(kmStr)
        Next

        File.WriteAllLines(NombreFichero, todosKms.ToArray)
        Return ""

    End Function
End Class

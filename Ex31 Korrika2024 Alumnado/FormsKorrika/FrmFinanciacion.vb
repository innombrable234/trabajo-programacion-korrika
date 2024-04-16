

Imports Entidades

Public Class FrmFinanciacion
    Private Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        Close()
    End Sub

    Private Sub FrmFinanciacion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblDatosKorrika.Text = korrika.ToString
        cboProvincias.Items.AddRange(korrika.Provincias.ToArray)
    End Sub

    Private Sub cboProvincias_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProvincias.SelectedIndexChanged
        cboKmsProvincia.Items.Clear()
        cboKmsProvincia.Items.AddRange(korrika.KmLibresDeProvincia(cboProvincias.SelectedItem).ToArray)
        lblNumKm.Text = ""
        lblDireccion.Text = ""
        lblPueblo.Text = ""
    End Sub

    Private Sub cboKmsProvincia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboKmsProvincia.SelectedIndexChanged
        lblNumKm.Text = TryCast(cboKmsProvincia.SelectedItem, Kilometro).NumKm
        lblDireccion.Text = TryCast(cboKmsProvincia.SelectedItem, Kilometro).Direccion
        lblPueblo.Text = TryCast(cboKmsProvincia.SelectedItem, Kilometro).Localidad
    End Sub

    Private Sub btnFinanciarKm_Click(sender As Object, e As EventArgs) Handles btnFinanciarKm.Click
        If cboProvincias.SelectedItem Is Nothing OrElse cboKmsProvincia.SelectedItem Is Nothing OrElse String.IsNullOrWhiteSpace(txtNombreOrganización.Text) OrElse String.IsNullOrWhiteSpace(txtEuros.Text) Then
            MessageBox.Show("No pueden haber campos vacíos o sin escoger", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        MessageBox.Show(korrika.PatrocinarKm(lblNumKm.Text, txtNombreOrganización.Text, txtEuros.Text))
        cboProvincias_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub btnTotalRecaudado_Click(sender As Object, e As EventArgs) Handles btnTotalRecaudado.Click
        MessageBox.Show($"Recaudación hasta el momento: {korrika.TotalRecaudado} euros", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class

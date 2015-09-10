Public Class email
    Public Property fromName As String

    Public Property fromEmail As String

    Public Property toEmails As System.Collections.ObjectModel.Collection(Of String)

    Public Property subject As String

    Public Property body As String

    Public Sub New(fromName As String, fromEmail As String, toEmails As System.Collections.ObjectModel.Collection(Of String), subject As String, body As String)

    End Sub

    Public Sub New()

    End Sub
End Class

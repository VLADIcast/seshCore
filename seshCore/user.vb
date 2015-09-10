Public Class user
    Inherits baseItem

    Public Property name As String
    Public Property credentials As credentials

    Public Sub contact()

    End Sub

    Public Sub authenticate()

    End Sub

    Public Sub register()

    End Sub

    Public Function isUserValid() As Boolean
        Dim iuv As Boolean = False

        Return iuv
    End Function

    Public Sub New(userGUID As String)
        MyBase.GUID = userGUID
    End Sub

    Public Sub New()

    End Sub



End Class

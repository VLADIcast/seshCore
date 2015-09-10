
Public Class mediaItem
    Inherits baseItem

    Public Enum itemMediaType As Integer
        LINK = 0
        VIDEO = 1
        AUDIO = 2
    End Enum

    Public Property mediaType As itemMediaType

    Public Property URL As String

    Public Property description As String



    Public Sub Update()

    End Sub

    Public Sub Remove()

    End Sub

    Public Sub New()

    End Sub

    Public Sub New(mediaType As Integer, URL As String, description As String)
        _mediaType = mediaType
        _URL = URL
        _description = description
    End Sub

    Public Sub New(GUID As String)
        MyBase.GUID = GUID
    End Sub

End Class


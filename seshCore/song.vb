Public Class song
    Inherits baseItem



    Public Property media As System.Collections.ObjectModel.Collection(Of mediaItem)

    Public Property title As String

    Public Property artist As String

    Public Property description As String

    Public Property band As band
   
    Public Sub Add()

    End Sub

    Public Sub Remove()

    End Sub

    Public Sub addMedia(media As System.Collections.ObjectModel.Collection(Of mediaItem))

    End Sub

    Public Sub AddMediaItem(mediaItem As mediaItem)

    End Sub

    Public Sub Update()

    End Sub

    Public Sub New(title As String, artist As String, Optional ByVal description As String = "")
        _title = title
        _artist = artist
        _description = description
    End Sub

    Public Sub New(GUID As String)
        MyBase.GUID = GUID
    End Sub
    Public Sub New()

    End Sub

End Class

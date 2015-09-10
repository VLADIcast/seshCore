Public Class artist
    Inherits baseItem
    Public Property user As user
    Public Property equipment As Collection
    Public Property images As System.Collections.ObjectModel.Collection(Of seshImage)
    Public Property about As String
    Public Property location As geo

    Public Property bandsJoined As System.Collections.ObjectModel.Collection(Of bandMember)

    Public Property artistID As Integer



    Public Property media As System.Collections.ObjectModel.Collection(Of mediaItem)


    Public Sub addPhoto(artistPhoto As seshImage)

    End Sub

    Public Sub removePhoto(artistPhoto As seshImage)

    End Sub

    Public Sub updatePhoto(artistPhoto As seshImage)

    End Sub

    Public Sub addMedia(media As System.Collections.ObjectModel.Collection(Of mediaItem))

    End Sub

    Public Sub getEquipment()

    End Sub

    Public Sub getImages()

    End Sub


    Public Sub Save()

    End Sub

    Public Sub Populate()
        ' sql to populate artist data
    End Sub

    Public Sub Message(fromUser As user, subject As String)
        Dim notification As notification
        Dim toUsers As System.Collections.ObjectModel.Collection(Of user)
        toUsers = New System.Collections.ObjectModel.Collection(Of user)
        toUsers.Add(user)
        notification = New notification(seshCore.notification.notificationType.CONTACT_MESSAGE, fromUser, toUsers, Nothing, Nothing, subject)
    End Sub

    Public Sub New(userGUID As String)
        user = New user
        user.GUID = userGUID
        Populate()

    End Sub

    Public Sub New()

    End Sub


End Class

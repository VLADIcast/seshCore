Public Class seshEvent
    Inherits baseItem


    Public Property name As String

    Public Property organizers As System.Collections.ObjectModel.Collection(Of organizer)

    Public Property creatorOrganizer As user



    Public Property description As String

    Public Property location As geo

    Public Property venuePhotos As System.Collections.ObjectModel.Collection(Of seshImage)

    Public Property eventDateTime As String

    Public Property signupDeadline As String

    Public Property filledBandSlots As Integer


    Public Property availableBandSlots As Integer

    Public Property equipmentAvailable As Collection

    Public Property bands As System.Collections.ObjectModel.Collection(Of band)

    

    Public Sub getBands()

    End Sub

    Public Sub getBands(statuses As System.Collections.ObjectModel.Collection(Of band.playStatusType))

    End Sub

    Public Sub getOrganizers()

    End Sub


    Public Sub getVenuePhotos()

    End Sub

    Public Sub getFilledBandSlots()

    End Sub

    Public Sub getAvailableBandSlots()

    End Sub

    ' request organizer status
    Public Sub requestOrganizerAccess(requestingUser As user)
        ' run SQL to create organizer request

        ' notify organizer regarding the request
        Dim toUsers As System.Collections.ObjectModel.Collection(Of user)
        toUsers = New System.Collections.ObjectModel.Collection(Of user)
        toUsers.Add(creatorOrganizer)
        Dim notification As notification
        notification = New notification(seshCore.notification.notificationType.ORGANIZER_REQUEST, requestingUser, toUsers, Nothing, Me, "")
    End Sub
    Public Sub New()

    End Sub


End Class

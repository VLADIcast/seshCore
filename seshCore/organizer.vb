Public Class organizerRequest
    Public Property user As user

    Public Property seshEvent As seshEvent

    Public Sub New()

    End Sub
End Class

Public Class organizer
    Inherits baseItem

    Public Property user As user

    Public Property seshEventsOrganizing As System.Collections.ObjectModel.Collection(Of seshEvent)

    Public Property seshEvent As seshEvent

    ' ACCEPT OR KICK A BAND
    Public Sub updateBandStatus(band As band, playStatus As band.playStatusType)

        Dim organizerBandAccess As Boolean
        organizerBandAccess = False

        ' run sql to see if user has access to band

        If organizerBandAccess = True Then
            Dim toUsers As System.Collections.ObjectModel.Collection(Of user)
            Dim notification As notification

            ' run sql to update band status

            ' notify the band leader and members of the new status
            ' PLAYING
            If playStatus = band.playStatusType.PLAYING Then
                toUsers = New System.Collections.ObjectModel.Collection(Of user)
                For Each bandMember As bandMember In band.members
                    toUsers.Add(bandMember.user)
                Next
                notification = New notification(seshCore.notification.notificationType.BAND_ACCEPTED, user, toUsers, band, band.seshEvent)
            End If

            ' KICKED
            If playStatus = band.playStatusType.FORMED Then
                toUsers = New System.Collections.ObjectModel.Collection(Of user)
                For Each bandMember As bandMember In band.members
                    toUsers.Add(bandMember.user)
                Next
                notification = New notification(seshCore.notification.notificationType.BAND_KICKED_BY_ORGANIZER, user, toUsers, band, band.seshEvent)
            End If
        End If




    End Sub

    Public Sub updateSeshEvent()

    End Sub

    Public Sub addSeshVenuePhoto(venuePhoto As seshImage)

    End Sub

    Public Sub removeSeshVenuePhoto(venuePhoto As seshImage)

    End Sub

    Public Sub updateSeshVenuePhoto(venuePhoto As seshImage)

    End Sub

    ' invite event organizers
    Public Sub inviteOrganizers(toEmails As System.Collections.ObjectModel.Collection(Of String))
        Dim usr As user
        Dim toUsers As System.Collections.ObjectModel.Collection(Of user)
        toUsers = New System.Collections.ObjectModel.Collection(Of user)
        For Each email As String In toEmails
            usr = New user
            usr.credentials = New credentials
            usr.credentials.email = email
            toUsers.Add(usr)
        Next

        ' send email notification of event organizer invite to recipient
        Dim notification As notification
        notification = New notification(seshCore.notification.notificationType.ORGANIZER_INVITE, user, toUsers, Nothing, seshEvent, "")
    End Sub

    ' invite musicians to play the events
    Public Sub inviteMusicians(toEmails As System.Collections.ObjectModel.Collection(Of String))
        Dim usr As user
        Dim toUsers As System.Collections.ObjectModel.Collection(Of user)
        toUsers = New System.Collections.ObjectModel.Collection(Of user)
        For Each email As String In toEmails
            usr = New user
            usr.credentials = New credentials
            usr.credentials.email = email
            toUsers.Add(usr)
        Next

        ' send email notification of event organizer invite to recipient
        Dim notification As notification
        notification = New notification(seshCore.notification.notificationType.EVENT_MUSICIAN_INVITE, user, toUsers, Nothing, seshEvent, "")
    End Sub

    Public Function getRequestingOrganizerFromCode(requestCode As String) As organizerRequest
        Dim ru As organizerRequest
        ru = Nothing

        ' run sql to get the organizerRequest

        Return ru
    End Function

    ' approve organizer status
    Public Sub approveSeshOrganizer(requestCode As String)
        Dim organizerRequest As organizerRequest
        organizerRequest = getRequestingOrganizerFromCode(requestCode)

        If Not organizerRequest Is Nothing Then
            ' run SQL to approve session organizer

            ' notify user that they are approved as an organizer
            Dim toUsers As System.Collections.ObjectModel.Collection(Of user)
            toUsers = New System.Collections.ObjectModel.Collection(Of user)
            toUsers.Add(organizerRequest.user)
            Dim notification As notification
            notification = New notification(seshCore.notification.notificationType.ORGANIZER_APPROVAL, user, toUsers, Nothing, organizerRequest.seshEvent, "")
        End If


    End Sub

    Public Sub CancelSesh()

    End Sub

    Public Sub CreateSesh()

    End Sub




    Public Sub New(seshEvent As seshEvent, user As user)
        _seshEvent = seshEvent
        _user = user
    End Sub

    Public Sub New(user As user)
        _user = user
    End Sub

    Public Sub New()

    End Sub
End Class

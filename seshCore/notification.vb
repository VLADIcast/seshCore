Public Class notification
    Inherits email

    Public Enum notificationType As Integer
        BAND_FORMED = 0
        BAND_ACCEPTED = 1
        MUSICIAN_APPLIED = 2
        MUSICIAN_ACCEPTED = 3
        MUSICIAN_DROPPED_OUT = 4
        ORGANIZER_SUBMITTED_BAND_UNFORMED = 5
        ORGANIZER_PLAYING_BAND_UNFORMED = 6
        CONTACT_MESSAGE = 7
        BAND_CANCELED = 8
        MUSICIAN_REJECTED = 9
        BAND_KICKED_BY_ORGANIZER = 10
        BANDMEMBER_INVITATION = 11
        ORGANIZER_REQUEST = 12
        ORGANIZER_APPROVAL = 13
        ORGANIZER_INVITE = 14
        EVENT_MUSICIAN_INVITE = 15
        BAND_SUBMITTED_TO_EVENT = 16
        ORGANIZER_HAS_NEW_BAND_SUBMITTED = 17
        BAND_CREATION_INITIATED = 18
        SUBMITTED_BAND_SLOT_ADDED = 19
        PLAYING_BAND_SLOT_ADDED = 20
        ORGANIZER_SUBMITTED_BAND_SLOT_ADDED = 21
        ORGANIZER_PLAYING_BAND_SLOT_ADDED = 22
    End Enum

    Public Property emailNotificationType As notificationType



    Public Sub New(emailNotificationType As notificationType, fromUser As user, toUsers As System.Collections.ObjectModel.Collection(Of user), Optional ByVal band As band = Nothing, Optional ByVal seshEvent As seshEvent = Nothing, Optional ByVal customSubject As String = "", Optional ByVal customBody As String = "")
        MyBase.New()
        MyBase.fromName = fromUser.credentials.name
        MyBase.fromEmail = fromUser.credentials.email
        MyBase.toEmails = toEmails
        _emailNotificationType = emailNotificationType

        ' Run SQL to get subject and body based on the emailNotificationType
        MyBase.subject = ""
        MyBase.body = ""


        ' Replace out MACROs
        MyBase.body = Replace(MyBase.body, "{applyingMusicianName}", fromName)
        MyBase.body = Replace(MyBase.body, "{applyingMusicianEmail}", fromEmail)
        MyBase.body = Replace(MyBase.body, "{applyingMusicianArtistPage}", fromUser.pageURL)
        If Not IsNothing(band) Then
            MyBase.body = Replace(MyBase.body, "{bandName}", band.name)
        End If
        If Not IsNothing(seshEvent) Then
            MyBase.body = Replace(MyBase.body, "{eventName}", seshEvent.name)
            MyBase.body = Replace(MyBase.body, "{eventAddress}", seshEvent.location.address)
            MyBase.body = Replace(MyBase.body, "{eventAddress2}", seshEvent.location.address2)
            MyBase.body = Replace(MyBase.body, "{eventCity}", seshEvent.location.city)
            MyBase.body = Replace(MyBase.body, "{eventState}", seshEvent.location.state)
            MyBase.body = Replace(MyBase.body, "{eventZipcode}", seshEvent.location.zipcode)
            MyBase.body = Replace(MyBase.body, "{eventCountry}", seshEvent.location.country)
        End If



    End Sub
    Public Sub New()

    End Sub

End Class

Public Class notification
    Inherits email

    Public Enum notificationType As Integer
        BAND_FORMED = 0
        BAND_ACCEPTED = 1
        MUSICIAN_APPLIED = 2
        MUSICIAN_ACCEPTED = 3
        MUSICIAN_DROPPED_OUT = 4
        BAND_UNFORMED = 5
        CONTACT_MESSAGE = 6
        BAND_CANCELED = 7
        MUSICIAN_REJECTED = 8
        BAND_KICKED_BY_ORGANIZER = 9
        BANDMEMBER_INVITATION = 10
        ORGANIZER_REQUEST = 11
        ORGANIZER_APPROVAL = 12
        ORGANIZER_INVITE = 13
        EVENT_MUSICIAN_INVITE = 14
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

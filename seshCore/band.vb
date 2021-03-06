﻿
Public Class bandMemberActivity
    Inherits baseItem
    Public Property activityType As notification.notificationType

    Public Property activityDate As String

    Public Property bandMember As bandMember

    ' log the activity to SQL
    Public Sub Log()

    End Sub

    Public Sub New(bandMember As bandMember, activityType As notification.notificationType)
        _bandMember = bandMember
        _activityType = activityType
    End Sub

    Public Sub New(bandMember As bandMember)
        _bandMember = bandMember
    End Sub

    Public Sub New()

    End Sub

End Class

Public Class bandMember
    Inherits artist
    Public Property band As band


    Public Enum joinStatusType As Integer
        APPLIED_NOTJOINED = 0
        MEMBER_JOINED_ACCEPTED = 1
        LEADER = 2
        MEMBER_DROPPEDOUT = 3
        MEMBER_REJECTED = 4
    End Enum

    Public Enum joinErrorType As Integer
        NO_ERROR = 0
        ALREADY_JOINED = 1
    End Enum

    Public Property joinError As joinErrorType
    Public Property bandSlot As slot

    Public Property joinStatus As joinStatusType

    Public Property isLeader As Boolean
    Public Property activityStream As System.Collections.ObjectModel.Collection(Of bandMemberActivity)

    Public Property latestActivity As bandMemberActivity

    ' Join a band
    Public Sub joinBand()
        ' make sure all band slots are not already filled

        ' check if user already put in a previous join request for band and did NOT drop out. Also check if leader as leaders cannot drop out of band. They must delete the band

        ' if not then join the band. If user previously dropped out, they can put in another join request
        joinStatus = joinStatusType.APPLIED_NOTJOINED

        ' run SQL to do a join request

        If joinError = joinErrorType.NO_ERROR Then
            ' notify band leader of the join request
            Dim toUsers As System.Collections.ObjectModel.Collection(Of user)
            toUsers = New System.Collections.ObjectModel.Collection(Of user)
            toUsers.Add(band.leader.user)
            Dim notification As notification
            notification = New notification(seshCore.notification.notificationType.MUSICIAN_APPLIED, user, toUsers, band)

            ' log the activity
            Dim bandMemberActivity As bandMemberActivity
            bandMemberActivity = New bandMemberActivity(Me, seshCore.notification.notificationType.MUSICIAN_APPLIED)
            bandMemberActivity.Log()

        End If






    End Sub

    ' Update the membership status of another band member
    Public Sub updateBandMembershipStatus(anotherBandMember As bandMember)

        Dim emailNotificationType As notification.notificationType
        Dim notification As notification
        If joinStatus = seshCore.bandMember.joinStatusType.MEMBER_JOINED_ACCEPTED Then
            emailNotificationType = seshCore.notification.notificationType.MUSICIAN_ACCEPTED
        End If

        If joinStatus = seshCore.bandMember.joinStatusType.MEMBER_REJECTED Then
            emailNotificationType = seshCore.notification.notificationType.MUSICIAN_REJECTED
        End If



        ' run SQL to update status.


        ' send email notification to band member regarding their status
        Dim toUsers As System.Collections.ObjectModel.Collection(Of user)
        toUsers = New System.Collections.ObjectModel.Collection(Of user)
        toUsers.Add(anotherBandMember.user)
        notification = New notification(emailNotificationType, band.leader.user, toUsers, band, Nothing)

        ' log the activity
        Dim bandMemberActivity As bandMemberActivity
        bandMemberActivity = New bandMemberActivity(anotherBandMember, emailNotificationType)
        bandMemberActivity.Log()



        ' if band has submitted or is playing, then there status gets changed back to FORMED and organizers are notified
        If band.playStatus = seshCore.band.playStatusType.SUBMITTED Or band.playStatus = seshCore.band.playStatusType.PLAYING Then

            If band.playStatus = seshCore.band.playStatusType.SUBMITTED Then
                emailNotificationType = seshCore.notification.notificationType.ORGANIZER_SUBMITTED_BAND_UNFORMED
            End If

            If band.playStatus = seshCore.band.playStatusType.PLAYING Then
                emailNotificationType = seshCore.notification.notificationType.ORGANIZER_PLAYING_BAND_UNFORMED
            End If



            ' get the event organizers
            toUsers = New System.Collections.ObjectModel.Collection(Of user)
            For Each organizer As organizer In band.seshEvent.organizers
                toUsers.Add(organizer.user)
            Next
            notification = New notification(emailNotificationType, band.leader.user, toUsers, band, band.seshEvent)

            ' log the activity
            Dim bandActivity As bandActivity
            bandActivity = New bandActivity(band, emailNotificationType)
            bandActivity.Log()

        End If
       

    End Sub

    Public Sub submitBandToEvent()
        If band.playStatus = seshCore.band.playStatusType.FORMED Then
            ' run sql to submit band to event
            ' sql to submit bandGUID to band.seshEvent.GUID

            ' notify band members that the band has been submitted
            Dim notification As notification
            Dim toUsers As System.Collections.ObjectModel.Collection(Of user)

            ' get the band members
            toUsers = New System.Collections.ObjectModel.Collection(Of user)
            For Each bandMember As bandMember In band.members
                toUsers.Add(bandMember.user)
            Next
            notification = New notification(seshCore.notification.notificationType.BAND_SUBMITTED_TO_EVENT, band.leader.user, toUsers, band, band.seshEvent)

            ' log the activity
            Dim bandActivity As bandActivity
            bandActivity = New bandActivity(band, seshCore.notification.notificationType.BAND_SUBMITTED_TO_EVENT)
            bandActivity.Log()

            ' notify event organizers of the submission
            ' get the event organizers
            toUsers = New System.Collections.ObjectModel.Collection(Of user)
            For Each organizer As organizer In band.seshEvent.organizers
                toUsers.Add(organizer.user)
            Next
            notification = New notification(seshCore.notification.notificationType.ORGANIZER_HAS_NEW_BAND_SUBMITTED, band.leader.user, toUsers, band, band.seshEvent)


        End If
    End Sub

    ' cancel the band's performance
    Public Sub cancelBand()

        ' run sql to cancel band

        ' notify band members that the band has canceled
        Dim notification As notification
        Dim toUsers As System.Collections.ObjectModel.Collection(Of user)

        ' get the band members
        toUsers = New System.Collections.ObjectModel.Collection(Of user)
        For Each bandMember As bandMember In band.members
            toUsers.Add(bandMember.user)
        Next
        notification = New notification(seshCore.notification.notificationType.BAND_CANCELED, band.leader.user, toUsers, band, band.seshEvent)

        ' log the activity
        Dim bandActivity As bandActivity
        bandActivity = New bandActivity(band, seshCore.notification.notificationType.BAND_CANCELED)
        bandActivity.Log()


        ' notify organizers that the band is canceled if it was previously accepted to play
        If band.isAcceptedBandCanceled Then


            ' get the event organizers
            toUsers = New System.Collections.ObjectModel.Collection(Of user)
            For Each organizer As organizer In band.seshEvent.organizers
                toUsers.Add(organizer.user)
            Next
            notification = New notification(seshCore.notification.notificationType.BAND_CANCELED, band.leader.user, toUsers, band, band.seshEvent)
        End If

    End Sub

    ' drop out of a band
    Public Sub dropOutOfBand()
        ' check if user is not leader

        ' if not leader allow user to drop out
        joinStatus = joinStatusType.MEMBER_DROPPEDOUT

        ' run SQL to drop out. Get if accepted band became unformed

        ' notify band leader of the drop out
        Dim toUsers As System.Collections.ObjectModel.Collection(Of user)
        toUsers = New System.Collections.ObjectModel.Collection(Of user)
        toUsers.Add(band.leader.user)
        Dim notification As notification
        notification = New notification(seshCore.notification.notificationType.MUSICIAN_DROPPED_OUT, user, toUsers, band)

        ' log the activity
        Dim bandMemberActivity As bandMemberActivity
        bandMemberActivity = New bandMemberActivity(Me, seshCore.notification.notificationType.MUSICIAN_DROPPED_OUT)
        bandMemberActivity.Log()

        ' notify the organizers if a band member of a submitted or playing band drops out
        If band.playStatus = seshCore.band.playStatusType.SUBMITTED Or band.playStatus = seshCore.band.playStatusType.PLAYING Then
            Dim emailNotificationType As notification.notificationType
            If band.playStatus = seshCore.band.playStatusType.SUBMITTED Then
                emailNotificationType = seshCore.notification.notificationType.ORGANIZER_SUBMITTED_BAND_UNFORMED
            End If

            If band.playStatus = seshCore.band.playStatusType.PLAYING Then
                emailNotificationType = seshCore.notification.notificationType.ORGANIZER_PLAYING_BAND_UNFORMED
            End If
            ' get the event organizers
            toUsers = New System.Collections.ObjectModel.Collection(Of user)
            For Each organizer As organizer In band.seshEvent.organizers
                toUsers.Add(organizer.user)
            Next
            notification = New notification(emailNotificationType, band.leader.user, toUsers, band, band.seshEvent)

            ' log the activity
            Dim bandActivity As bandActivity
            bandActivity = New bandActivity(band, emailNotificationType)
            bandActivity.Log()

        End If
        

    End Sub

    'invite other band members to join the band
    Public Sub Invite(toEmails As System.Collections.ObjectModel.Collection(Of String))

        Dim usr As user
        Dim toUsers As System.Collections.ObjectModel.Collection(Of user)
        toUsers = New System.Collections.ObjectModel.Collection(Of user)
        For Each email As String In toEmails
            usr = New user
            usr.credentials = New credentials
            usr.credentials.email = email
            toUsers.Add(usr)
        Next

        ' send email notification of invite to recipient
        Dim notification As notification
        notification = New notification(seshCore.notification.notificationType.BANDMEMBER_INVITATION, user, toUsers, band, band.seshEvent)

    End Sub


    Public Sub PopulateBandMember()

    End Sub


    Public Sub cancelJoinRequest()

    End Sub

    Public Sub loadBandMemberfromBandmemberGUID(bandmemberGUID As String)

    End Sub

    Public Sub New()

    End Sub

    Public Sub New(band As band)
        _band = band
    End Sub

    Public Sub New(userGUID As String)
        MyBase.user.GUID = userGUID
        '_bandMemberGUID = bandMemberGUID
    End Sub

    



End Class

Public Class bandActivity
    Inherits baseItem
    Public Property activityType As notification.notificationType
    Public Property activityDate As String

    Public Property band As band

    ' log the activity to SQL
    Public Sub Log()

    End Sub

    Public Sub New(band As band, activityType As notification.notificationType)
        _band = band
        _activityType = activityType
    End Sub

    Public Sub New()

    End Sub

End Class

Public Class band
    Inherits baseItem


    Public Property name As String

    Public Property members As System.Collections.ObjectModel.Collection(Of bandMember)

    Public Property leader As bandMember

    Public Property creator As artist

    Public Property creatorSlot As slot

    Public Property availableSlots As System.Collections.ObjectModel.Collection(Of slot)

    Public Property songs As System.Collections.ObjectModel.Collection(Of song)

    Public Property setLength As Integer

    Public Property seshEvent As seshEvent

    Public Enum playStatusType As Integer
        NOTFORMED = 0
        FORMED = 1
        SUBMITTED = 2
        PLAYING = 3
        FORMED_KICKED = 4
    End Enum

    Public Property playStatus As playStatusType

    Public Property isBandFormed As Boolean
    
    Public Property isAcceptedBandUnformed As Boolean

    Public Property isAcceptedBandCanceled As Boolean

    Public Property activityStream As System.Collections.ObjectModel.Collection(Of bandActivity)

    Public Property latestActivity As bandActivity


    

    Public Sub getMembers()

    End Sub

    Public Sub getMembers(statuses As System.Collections.ObjectModel.Collection(Of bandMember.joinStatusType))

    End Sub

    Public Sub getSongs()

    End Sub



    Public Sub getAvailableSlots()

    End Sub

    Public Sub AddSong()

    End Sub

    Public Sub AddSlot(slot As slot)
        ' Add slot, but unform and unsubmit band if band is formed, submitted or playing

        ' run SQL

        ' do messaging

        Dim emailNotificationType As notification.notificationType
        Dim notification As notification

        If playStatus = playStatusType.SUBMITTED Then
            emailNotificationType = seshCore.notification.notificationType.SUBMITTED_BAND_SLOT_ADDED
        End If

        If playStatus = playStatusType.PLAYING Then
            emailNotificationType = seshCore.notification.notificationType.PLAYING_BAND_SLOT_ADDED
        End If



        ' run SQL to update status.


        ' send email notification to band member regarding the added slot
        ' get the band members
        Dim toUsers As System.Collections.ObjectModel.Collection(Of user)
        toUsers = New System.Collections.ObjectModel.Collection(Of user)
        For Each bandMember As bandMember In members
            toUsers.Add(bandMember.user)
        Next
        notification = New notification(emailNotificationType, leader.user, toUsers, Me, seshEvent)


        ' log the activity
        Dim bandMemberActivity As bandActivity
        bandMemberActivity = New bandActivity(Me, emailNotificationType)
        bandMemberActivity.Log()



        ' if band has submitted or is playing, then there status gets changed back to FORMED and organizers are notified
        If playStatus = seshCore.band.playStatusType.SUBMITTED Or playStatus = seshCore.band.playStatusType.PLAYING Then

            If playStatus = seshCore.band.playStatusType.SUBMITTED Then
                emailNotificationType = seshCore.notification.notificationType.ORGANIZER_SUBMITTED_BAND_UNFORMED
            End If

            If playStatus = seshCore.band.playStatusType.PLAYING Then
                emailNotificationType = seshCore.notification.notificationType.ORGANIZER_PLAYING_BAND_UNFORMED
            End If



            ' get the event organizers
            toUsers = New System.Collections.ObjectModel.Collection(Of user)
            For Each organizer As organizer In seshEvent.organizers
                toUsers.Add(organizer.user)
            Next
            notification = New notification(emailNotificationType, leader.user, toUsers, Me, seshEvent)

            ' log the activity
            Dim bandActivity As bandActivity
            bandActivity = New bandActivity(Me, emailNotificationType)
            bandActivity.Log()

        End If

    End Sub


    Public Sub Create()
        ' make sure there are enough band slots for the event to create a new band

        ' run SQL to create a band using the bandLeader profile


        ' log the activity
        Dim bandActivity As bandActivity
        bandActivity = New bandActivity(Me, notification.notificationType.BAND_CREATION_INITIATED)
        bandActivity.Log()


    End Sub

    Public Sub Update()

    End Sub

    Public Sub updatePlayingStatus()

    End Sub

    ' message some or all of the band members
    Public Sub messageBandMembers(fromUser As user, subject As String, body As String)
        Dim notification As notification
        Dim toUsers As System.Collections.ObjectModel.Collection(Of user)
        toUsers = New System.Collections.ObjectModel.Collection(Of user)
        For Each bandMember As bandMember In members
            toUsers.Add(bandMember.user)
        Next

        notification = New notification(seshCore.notification.notificationType.CONTACT_MESSAGE, fromUser, toUsers, Nothing, Nothing, subject, body)
    End Sub

    ' message the band leader
    Public Sub messageBandLeader(fromUser As user, subject As String, body As String)
        Dim notification As notification
        Dim toUsers As System.Collections.ObjectModel.Collection(Of user)
        toUsers = New System.Collections.ObjectModel.Collection(Of user)

        toUsers.Add(leader.user)
        notification = New notification(seshCore.notification.notificationType.CONTACT_MESSAGE, fromUser, toUsers, Nothing, Nothing, subject, body)
        'messageBandMembers(fromUser, subject, toUsers, body)
    End Sub

    Public Sub New(leader As bandMember)
        _leader = leader
    End Sub

    Public Sub New(creator As artist, slot As slot)
        _creator = creator
        _creatorSlot = slot
    End Sub

    Public Sub New(GUID As String)
        MyBase.GUID = GUID
    End Sub

    Public Sub New()

    End Sub
End Class

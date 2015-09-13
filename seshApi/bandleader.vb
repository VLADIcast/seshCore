Public Class bandleader
    Inherits responseObj

    Public Property band As seshCore.band
    Public Property bandLeader As seshCore.bandMember


    Public Sub initRequiredParams()
        ' build up the required params
        requiredParameterMap = New Dictionary(Of String, String)
        requiredParameterMap("user.bandleader.bandmember.approve") = "bandGUID,bandMemberGUID"
        requiredParameterMap("user.bandleader.bandmember.drop") = "bandGUID,bandMemberGUID"
        requiredParameterMap("user.bandleader.bandmembers.list") = "bandGUID"
        requiredParameterMap("user.bandleader.song.add") = "bandGUID,title,artist"
        requiredParameterMap("user.bandleader.song.update") = "bandGUID,songGUID,title,artist"
        requiredParameterMap("user.bandleader.song.remove") = "bandGUID,songGUID"
        requiredParameterMap("user.bandleader.media.add") = "bandGUID,mediatype,url"
        requiredParameterMap("user.bandleader.media.update") = "bandGUID,mediaItemGUID,mediatype,url"
        requiredParameterMap("user.bandleader.media.remove") = "bandGUID,mediaItemGUID"
        requiredParameterMap("user.bandleader.bandmembers.message") = "bandGUID,subject,message"


    End Sub


    Public Sub userBandleaderBandmemberDrop()
        Dim anotherBandMember As seshCore.bandMember
        anotherBandMember = New seshCore.bandMember
        anotherBandMember.loadBandMemberfromBandmemberGUID(param("bandMemberGUID"))
        bandLeader.joinStatus = seshCore.bandMember.joinStatusType.MEMBER_REJECTED
        bandLeader.updateBandMembershipStatus(anotherBandMember)

    End Sub
    Public Sub userBandleaderBandmemberApprove()
        Dim anotherBandMember As seshCore.bandMember
        anotherBandMember = New seshCore.bandMember
        anotherBandMember.loadBandMemberfromBandmemberGUID(param("bandMemberGUID"))
        bandLeader.joinStatus = seshCore.bandMember.joinStatusType.MEMBER_JOINED_ACCEPTED
        bandLeader.updateBandMembershipStatus(anotherBandMember)

    End Sub

    Public Sub userBandleaderBandmembersList()
        Dim statuses As System.Collections.ObjectModel.Collection(Of seshCore.bandMember.joinStatusType)
        statuses = New System.Collections.ObjectModel.Collection(Of seshCore.bandMember.joinStatusType)
        statuses.Add(seshCore.bandMember.joinStatusType.APPLIED_NOTJOINED)
        statuses.Add(seshCore.bandMember.joinStatusType.MEMBER_JOINED_ACCEPTED)
        statuses.Add(seshCore.bandMember.joinStatusType.MEMBER_DROPPEDOUT)
        band.getMembers(statuses)
    End Sub

    Public Sub userBandleaderSongAdd()
        Dim song As seshCore.song
        song = New seshCore.song(param("title"), param("artist"), param("description"))
        band.AddSong()
        'band.getSongs()
    End Sub

    Public Sub userBandleaderSongUpdate()
        Dim song As seshCore.song
        song = New seshCore.song(param("songGUID"))
        song.title = param("title")
        song.artist = param("artist")
        song.description = param("description")
        song.Update()
        'band.getSongs()
    End Sub

    Public Sub userBandleaderSongRemove()
        Dim song As seshCore.song
        song = New seshCore.song(param("songGUID"))
        song.Remove()
        'band.getSongs()
    End Sub

    Public Sub userBandleaderBandSubmit()
        If bandLeader.band.playStatus = seshCore.band.playStatusType.FORMED Then
            bandLeader.submitBandToEvent()
        Else
            errorCode = seshResponse.errorType.BAND_NOT_FORMED_CANNOT_SUBMIT
        End If


    End Sub

    Public Sub userBandleaderSlotAdd()

    End Sub

    Public Sub userBandleaderSlotRemove()

    End Sub


    Public Sub userBandleaderMediaAdd()
        Dim mediaItem As seshCore.mediaItem
        mediaItem = New seshCore.mediaItem(param("mediatype"), param("url"), param("description"))
        Dim song As seshCore.song
        song = New seshCore.song(param("songGUID"))
        song.AddMediaItem(mediaItem)

    End Sub

    Public Sub userBandleaderMediaUpdate()
        Dim mediaItem As seshCore.mediaItem
        mediaItem = New seshCore.mediaItem(param("mediaItemGUID"))
        mediaItem.mediaType = param("mediaType")
        mediaItem.URL = param("url")
        mediaItem.description = param("description")
        mediaItem.Update()


    End Sub

    Public Sub userBandleaderBandmembersMessage()
        Dim statuses As System.Collections.ObjectModel.Collection(Of seshCore.bandMember.joinStatusType)
        statuses = New System.Collections.ObjectModel.Collection(Of seshCore.bandMember.joinStatusType)

        statuses.Add(seshCore.bandMember.joinStatusType.MEMBER_JOINED_ACCEPTED)
        band.getMembers(statuses)

        band.messageBandMembers(band.leader.user, param("subject"), param("messageBody"))
    End Sub

    Public Sub userBandleaderMediaRemove()
        Dim mediaItem As seshCore.mediaItem
        mediaItem = New seshCore.mediaItem(param("mediaItemGUID"))
        mediaItem.Update()


    End Sub

    Public Sub New(methodName As String, userGUID As String, param As Dictionary(Of String, String))
        MyBase.New(methodName, param)


        _band = New seshCore.band(param("bandGUID"))

        _bandLeader = _band.leader

        'get the band leader GUID
        Dim bandleaderUserGUID As String = _bandLeader.user.GUID


        If bandleaderUserGUID <> userGUID Then
            errorCode = seshResponse.errorType.ACCESS_DENIED
        End If

        If errorCode = seshResponse.errorType.NO_ERROR Then

            ' check bandmember access
            If methodName.StartsWith("user.bandleader.bandmember.") Then
                Dim hasAccessToBandMember As Boolean = False
                For Each bm As seshCore.bandMember In band.members
                    If bm.GUID = param("bandMemberGUID") Then
                        hasAccessToBandMember = True
                    End If
                Next
                If hasAccessToBandMember = False Then
                    errorCode = seshResponse.errorType.ACCESS_TO_BANDMEMBER_DENIED
                End If

            End If

            ' check song access
            If methodName.StartsWith("user.bandleader.song.") And param("songGUID") <> "" Then
                Dim hasAccessToSong As Boolean = False
                For Each sng As seshCore.song In band.songs
                    If sng.GUID = param("songGUID") Then
                        hasAccessToSong = True
                    End If
                Next
                If hasAccessToSong = False Then
                    errorCode = seshResponse.errorType.ACCESS_TO_SONG_DENIED
                End If
            End If

            ' check media item access
            If methodName.StartsWith("user.bandleader.media.") And param("mediaItemGUID") Then
                Dim hasAccessToMedia As Boolean = False
                For Each sng As seshCore.song In band.songs
                    If Not sng.media Is Nothing Then
                        For Each mi As seshCore.mediaItem In sng.media
                            If mi.GUID = param("mediaItemGUID") Then
                                hasAccessToMedia = True
                            End If
                        Next
                    End If

                Next
                If hasAccessToMedia = False Then
                    errorCode = seshResponse.errorType.ACCESS_TO_MEDIA_DENIED
                End If
            End If


            If errorCode = seshResponse.errorType.NO_ERROR Then

                If methodName = "user.bandleader.bandmember.approve" Then
                    ' retrieve artist info from core artist object
                    userBandleaderBandmemberApprove()
                End If

                If methodName = "user.bandleader.bandmember.drop" Then
                    ' retrieve artist info from core artist object
                    userBandleaderBandmemberDrop()
                End If

                If methodName = "user.bandleader.bandmembers.list" Then
                    userBandleaderBandmembersList()
                End If

                If methodName = "user.bandleader.song.add" Then
                    userBandleaderSongAdd()
                End If

                If methodName = "user.bandleader.song.update" Then
                    userBandleaderSongUpdate()
                End If

                If methodName = "user.bandleader.song.remove" Then
                    userBandleaderSongRemove()
                End If

                If methodName = "user.bandleader.media.add" Then
                    userBandleaderMediaAdd()
                End If

                If methodName = "user.bandleader.media.update" Then
                    userBandleaderMediaUpdate()
                End If

                If methodName = "user.bandleader.media.remove" Then
                    userBandleaderMediaRemove()
                End If

                If methodName = "user.bandleader.bandmembers.message" Then
                    userBandleaderBandmembersMessage()
                End If

                If methodName = "user.bandleader.band.submit" Then
                    userBandleaderBandSubmit()
                End If
            End If


        End If

    End Sub
End Class

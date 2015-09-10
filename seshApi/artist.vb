Public Class artist
    Inherits responseObj



    Public Property artistResponse As seshCore.artist



    Public Property createdBandGUID As String

    Public Property createdBandMemberGUID As String



    Public Sub initRequiredParams()
        ' build up the required params
        requiredParameterMap = New Dictionary(Of String, String)
        requiredParameterMap("user.artist.band.create") = "creatorInstrument,songs"
        requiredParameterMap("user.artist.band.join") = "bandGUID,slotGUID"


    End Sub



    Public Sub userArtistInfoSave()
        ' get parameters from request
        artistResponse.about = param("about")
        artistResponse.equipment = New Collection
        For Each equipmentItem As String In Split(param("equipment"), "|")
            artistResponse.equipment.Add(equipmentItem)
        Next

        ' save the info
        artistResponse.Save()


    End Sub



    Public Sub userArtistBandCreate()
        Dim band As seshCore.band


        ' initiate band
        Dim slotForLeader As seshCore.slot
        slotForLeader = New seshCore.slot
        slotForLeader.instrument = param("creatorInstrument")

        band = New seshCore.band(New seshCore.artist(artistResponse.GUID), slotForLeader)
        band.name = param("name")

        ' get slots
        band.availableSlots = New System.Collections.ObjectModel.Collection(Of seshCore.slot)
        For Each instrument As String In Split(param("instruments"), "|")
            Dim slotToCreate As seshCore.slot
            slotToCreate = New seshCore.slot
            slotToCreate.instrument = instrument
            band.availableSlots.Add(slotToCreate)
        Next

        ' get songs
        band.songs = New System.Collections.ObjectModel.Collection(Of seshCore.song)
        For Each song As String In Split(param("songs"), "|")
            Dim songArr
            Dim songTitle As String = ""
            Dim artistName As String = ""
            songArr = Split(song, "^")
            For Each songParam As String In songArr
                If songParam.StartsWith("title=") Then
                    songTitle = Split(songParam, "=")(1)
                End If
                If songParam.StartsWith("artist=") Then
                    artistName = Split(songParam, "=")(1)
                End If
            Next

            If songTitle <> "" And artistName <> "" Then
                band.songs.Add(New seshCore.song(songTitle, artistName))
            End If

        Next

        ' create the band
        band.Create()

        ' get the created band GUID
        createdBandGUID = band.GUID
    End Sub

    Public Sub userArtistBandJoin()
        Dim bandJoining As seshCore.band


        ' initiate band
        bandJoining = New seshCore.band(param("bandGUID"))
        Dim slotJoining As seshCore.slot
        slotJoining = New seshCore.slot(param("slotGUID"))

        ' join band
        Dim bandmember As seshCore.bandMember
        bandmember = New seshCore.bandMember(bandJoining)
        bandmember.bandSlot = slotJoining
        bandmember.joinBand()

        ' get the created band member GUID
        createdBandMemberGUID = bandmember.bandMemberGUID


    End Sub


    Public Sub New(methodName As String, userGUID As String, param As Dictionary(Of String, String))
        MyBase.New(methodName, param)

        _artistResponse = New seshCore.artist(userGUID)
        'MyBase.New(userGUID)




        ' check for methods that require an artist page to be created
        If methodName.StartsWith("user.artist.band.") Then
            If artistResponse.artistID = 0 Then
                errorCode = seshResponse.errorType.ARTIST_DATA_MISSING
            End If
        End If

        ' ACCESS OK
        If errorCode = seshResponse.errorType.NO_ERROR Then
            ' check any required parameters
            initRequiredParams()
            checkRequiredParams()

            ' PARAMETERS OK
            If errorCode = seshResponse.errorType.NO_ERROR Then
                ' user.artist.info
                If methodName = "user.artist.info" Then
                    ' retrieve artist info from core artist object
                End If

                If methodName = "user.artist.info.save" Then
                    userArtistInfoSave()
                End If

                If methodName = "user.artist.band.create" Then
                    userArtistBandCreate()
                End If

                If methodName = "user.artist.band.join" Then
                    userArtistBandJoin()
                End If

                ' repopulate to return the updated artist info
                artistResponse.Populate()

            End If



        End If


    End Sub
    Public Sub New()

    End Sub

End Class

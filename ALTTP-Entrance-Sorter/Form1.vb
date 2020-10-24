Class frmES
    Private doResize As Boolean = True
    Private standardResize As Boolean = False
    Private iSize As Int16 = 0
    Private startZone As Int16 = 0
    Private startRoom As Int16 = 0
    Private loDoors As New List(Of Point)
    Private loZones As New List(Of Rectangle)
    Private loRooms As New List(Of Rectangle)
    Private loSettings As New List(Of Rectangle)
    Private firstReDim As Boolean = True
    Private arrZ2R(0) As Short
    Private arrR2Z(0) As Short
    Private arrSkips(0) As Byte
    Private lineMode As Int16 = 0

    Private Sub frmES_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        arrR2Z(0) = -1
        arrZ2R(0) = -1
        fillEntrances()
        drawEntrances()
    End Sub

    Private Sub frmES_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        doResize = True
        standardResize = True
        resizeMap()
    End Sub

    Private Sub drawEntrances()
        Dim nPoint As New Point
        Dim nRectD As New Rectangle
        Dim nRectF As New Rectangle
        Dim iPen = New Pen(Color.Red)
        Dim iPen2 = New Pen(Color.Black)
        Dim iBrush As New SolidBrush(Color.Red)
        Dim iGfx As Graphics = map.CreateGraphics()
        iSize = Math.Floor(map.Width / 128)
        loZones.Clear()
        getRooms()
        For i = 0 To loDoors.Count - 1
            nPoint = New Point(getLoc(loDoors(i), iSize / 2 * -1))
            nRectD = New Rectangle(nPoint, New Size(iSize - 1, iSize - 1))
            nRectF = New Rectangle(nPoint, New Size(iSize, iSize))
            If arrSkips(i) = 0 Then
                iGfx.FillRectangle(iBrush, nRectF)
                iGfx.DrawRectangle(iPen2, nRectD)
            Else
                iGfx.DrawRectangle(iPen, nRectD)
            End If
            loZones.Add(nRectF)
        Next
        iPen.Dispose()
        iGfx.Dispose()
        makeLines()
    End Sub

    Private Sub getRooms()
        Dim iWidth As Double = map.Width / 32
        Dim iheight As Double = map.Height / 20
        loRooms.Clear()
        loSettings.Clear()
        For i = 0 To 2
            loSettings.Add(New Rectangle(Math.Floor(iWidth * 3 * i), Math.Floor(0), Math.Floor(iWidth * 3 - 1), Math.Floor(iheight - 1)))
        Next
        For i = 3 To 28
            loRooms.Add(New Rectangle(Math.Floor(iWidth * i), Math.Floor(iheight), Math.Floor(iWidth - 1), Math.Floor(iheight - 1)))
        Next
        For i = 0 To 31
            loRooms.Add(New Rectangle(Math.Floor(iWidth * i), Math.Floor(iheight * 18), Math.Floor(iWidth - 1), Math.Floor(iheight - 1)))
        Next
        For i = 1 To 30
            loRooms.Add(New Rectangle(Math.Floor(iWidth * i), Math.Floor(iheight * 19), Math.Floor(iWidth - 1), Math.Floor(iheight - 1)))
        Next
        If firstReDim = True Then
            ReDim arrR2Z(loRooms.Count - 1)
            For i As Int16 = 0 To loRooms.Count - 1
                arrR2Z(i) = -1
            Next
            firstReDim = False
        End If
    End Sub

    Private Function getLoc(ByVal oPoint As Point, Optional ByVal change As Int16 = 0) As Point
        Return New Point((oPoint.X / 8192 * map.Width) + change, (oPoint.Y / 5120 * map.Height) + change)
    End Function

    Private Sub fillEntrances()
        loDoors.Add(New Point(760, 568))    '00 Forest Chest Game
        loDoors.Add(New Point(752, 1120))   '01 Forest Hideout Entrance
        loDoors.Add(New Point(1360, 640))   '02 Lumberjack Tree Entrance
        loDoors.Add(New Point(1376, 760))   '03 Lumberjack House
        loDoors.Add(New Point(1472, 1136))  '04 Death Mountain Exit Back
        loDoors.Add(New Point(1456, 1232))  '05 Death Mountain Entry Cave
        loDoors.Add(New Point(768, 1832))   '06 Kakariko Fortune Teller
        loDoors.Add(New Point(2296, 648))   '07 Tower of Hera
        loDoors.Add(New Point(2000, 928))   '08 Spectacle Rock Top
        loDoors.Add(New Point(1616, 1072))  '09 Death Mountain Exit Front
        loDoors.Add(New Point(1872, 1072))  '10 Spectacle Rock Left
        loDoors.Add(New Point(2000, 1104))  '11 Spectable Rock Right
        loDoors.Add(New Point(2192, 1168))  '12 Old Man Back Door
        loDoors.Add(New Point(1664, 1280))  '13 Death Mountain Entry Back
        loDoors.Add(New Point(1840, 1472))  '14 Old Man Home
        loDoors.Add(New Point(1600, 1712))  '15 North Bonk Rocks
        loDoors.Add(New Point(1888, 1608))  '16 Sanctuary Entrance
        loDoors.Add(New Point(2336, 1632))  '17 Graveyard Ledge Entrance
        loDoors.Add(New Point(2464, 1728))  '18 King's Tomb Entrance
        loDoors.Add(New Point(2736, 1632))  '19 Houlihan Entrance
        loDoors.Add(New Point(3520, 768))   '20 Paradox Cave Top
        loDoors.Add(New Point(3264, 880))   '21 Spiral Cave Top
        loDoors.Add(New Point(3456, 880))   '22 Mimic Cave Entrance
        loDoors.Add(New Point(3280, 1040))  '23 Spiral Cave Bottom
        loDoors.Add(New Point(3360, 976))   '24 EDM Connector Top
        loDoors.Add(New Point(3360, 1072))  '25 EDM Connector Bottom
        loDoors.Add(New Point(3456, 1104))  '26 Hookshot Fairy Cave
        loDoors.Add(New Point(3504, 1104))  '27 Paradox Cave Middle
        loDoors.Add(New Point(3688, 1064))  '28 Waterfall Fairy Entrance
        loDoors.Add(New Point(3536, 1392))  '29 Paradox Cave Bottom
        loDoors.Add(New Point(3280, 1880))  '30 Witch's Hut
        loDoors.Add(New Point(192, 2256))   '31 Kakariko Well Entrance
        loDoors.Add(New Point(528, 2232))   '32 Blind's House Entrance
        loDoors.Add(New Point(624, 2232))   '33 Elder Left Door
        loDoors.Add(New Point(688, 2232))   '34 Elder Right Door
        loDoors.Add(New Point(208, 2424))   '35 Left Snitch House
        loDoors.Add(New Point(848, 2488))   '36 Right Snitch House
        loDoors.Add(New Point(400, 2728))   '37 Chicken House Entrance
        loDoors.Add(New Point(640, 2712))   '38 Sick Kid Entrance
        loDoors.Add(New Point(832, 2696))   '39 Grass House
        loDoors.Add(New Point(112, 2952))   '40 Bomb Hut
        loDoors.Add(New Point(448, 2904))   '41 Kakariko Shop
        loDoors.Add(New Point(656, 2832))   '42 Tavern
        loDoors.Add(New Point(656, 2952))   '43 Front Tavern
        loDoors.Add(New Point(1248, 2696))  '44 Smith's House
        loDoors.Add(New Point(1296, 2784))  '45 Magic Bat Entrance
        loDoors.Add(New Point(1840, 2104))  '46 Castle Left Entrance
        loDoors.Add(New Point(2048, 2152))  '47 Agahnim's Tower Entrance
        loDoors.Add(New Point(2256, 2104))  '48 Castle Right Entrance
        loDoors.Add(New Point(2048, 2312))  '49 Castle Main Entrance
        loDoors.Add(New Point(2256, 2272))  '50 Castle Secret Entrance
        loDoors.Add(New Point(3320, 2368))  '51 Sahasrahla's Hut Entrance
        loDoors.Add(New Point(3928, 2104))  '52 Eastern Palace Entrance
        loDoors.Add(New Point(448, 3448))   '53 Quarreling Brothers Left
        loDoors.Add(New Point(576, 3448))   '54 Quarreling Brothers Right
        loDoors.Add(New Point(640, 3208))   '55 Library Entrance
        loDoors.Add(New Point(880, 3384))   '56 Kakariko Chest Game
        loDoors.Add(New Point(1936, 3184))  '57 Central Bonk Rocks
        loDoors.Add(New Point(2240, 3336))  '58 Link's House Entrance
        loDoors.Add(New Point(3376, 3152))  '59 Trees Fairy Cave
        loDoors.Add(New Point(4016, 3376))  '60 Long Fairy Cave
        loDoors.Add(New Point(144, 3768))   '61 Desert Left Entrance
        loDoors.Add(New Point(304, 3648))   '62 Desert Back Entrance
        loDoors.Add(New Point(304, 3776))   '63 Desert Front Entrance
        loDoors.Add(New Point(464, 3768))   '64 Desert Right Entrance
        loDoors.Add(New Point(720, 3696))   '65 Checkerboard Cave Entrance
        loDoors.Add(New Point(816, 3888))   '66 Aginah's Cave Entrance
        loDoors.Add(New Point(1088, 3888))  '67 South of Grove
        loDoors.Add(New Point(1136, 4160))  '68 Desert Fairy Cave
        loDoors.Add(New Point(1280, 4432))  '69 Fifty Rupee Cave
        loDoors.Add(New Point(1920, 4344))  '70 Dam Entrance
        loDoors.Add(New Point(2448, 3696))  '71 Hype Fairy Cave
        loDoors.Add(New Point(2656, 3800))  '72 Lake Fortune Teller
        loDoors.Add(New Point(2976, 3648))  '73 Lake Shop
        loDoors.Add(New Point(2672, 4352))  '74 Mini Moldrom Cave Entrance
        loDoors.Add(New Point(3248, 4000))  '75 Upgrade Fairy
        loDoors.Add(New Point(3664, 3664))  '76 Ice Rod Cave Entrance
        loDoors.Add(New Point(3744, 3664))  '77 Cold Bee Cave
        loDoors.Add(New Point(3696, 3728))  '78 Twenty Rupee Cave
        loDoors.Add(New Point(4256, 720))   '79 Skull Woods Back
        loDoors.Add(New Point(4336, 1040))  '80 Skull Woods Back South
        loDoors.Add(New Point(4688, 1104))  '81 Skull Woods Front West
        loDoors.Add(New Point(4848, 1120))  '82 Skull Woods Front East
        loDoors.Add(New Point(5472, 744))   '83 Dark Lumberjack
        loDoors.Add(New Point(5568, 1136))  '84 Bumper Cave Top
        loDoors.Add(New Point(5552, 1232))  '85 Bumper Cave Bottom
        loDoors.Add(New Point(5760, 1280))  '86 Dark Mountain Fairy
        loDoors.Add(New Point(6400, 584))   '87 Ganon's Tower Entrance
        loDoors.Add(New Point(6448, 1104))  '88 Spike Cave Entrance
        loDoors.Add(New Point(7376, 576))   '89 Hookshot Cave Top
        loDoors.Add(New Point(7504, 784))   '90 Hookshot Cave Entrance
        loDoors.Add(New Point(7616, 768))   '91 Super-Bunny Cave Top
        loDoors.Add(New Point(7360, 880))   '92 TR Bridge Left
        loDoors.Add(New Point(7552, 880))   '93 TR Bridge Right
        loDoors.Add(New Point(7456, 976))   '94 TR Safety Door
        loDoors.Add(New Point(7552, 1104))  '95 Super-Bunny Cave Bottom
        loDoors.Add(New Point(7600, 1104))  '96 Dark Death Mountain Shop
        loDoors.Add(New Point(7952, 856))   '97 Turtle Rock Entrance
        loDoors.Add(New Point(4864, 1832))  '98 Dark Village Fortune Teller
        loDoors.Add(New Point(5984, 1632))  '99 Dark Chapel
        loDoors.Add(New Point(7392, 1896))  '100 Dark Witch's Hut
        loDoors.Add(New Point(4304, 2424))  '101 Chest Game Entrance
        loDoors.Add(New Point(4608, 2504))  '102 Thieves Town Entrance
        loDoors.Add(New Point(4944, 2488))  '103 C-Shaped House Entrance
        loDoors.Add(New Point(4928, 2696))  '104 Hammer House
        loDoors.Add(New Point(4544, 2904))  '105 Brewery Entrance
        loDoors.Add(New Point(5456, 2392))  '106 Shield Shop
        loDoors.Add(New Point(5392, 2992))  '107 Hammer Pegs Entrance
        loDoors.Add(New Point(5872, 2504))  '108 Pyramid Hole Entrance
        loDoors.Add(New Point(6016, 2504))  '109 Pyramid Fairy Entrance
        loDoors.Add(New Point(8024, 2120))  '110 Palace of Darkness Entrance
        loDoors.Add(New Point(7578, 2568))  '111 Dark Sahasrahla
        loDoors.Add(New Point(4976, 3384))  '112 Archery Game
        loDoors.Add(New Point(6032, 3184))  '113 Dark Bonk Rocks
        loDoors.Add(New Point(6336, 3336))  '114 Bomb Shop
        loDoors.Add(New Point(7472, 3152))  '115 Dark Trees Fairy
        loDoors.Add(New Point(8112, 3376))  '116 East Storyteller Cave
        loDoors.Add(New Point(4256, 3792))  '117 Mire Shed Entrance
        loDoors.Add(New Point(4400, 3808))  '118 Misery Mire Entrance
        loDoors.Add(New Point(4544, 3792))  '119 Mire Fairy
        loDoors.Add(New Point(4912, 3888))  '120 Mire Hint Cave
        loDoors.Add(New Point(6016, 4344))  '121 Swamp Palace Entrance
        loDoors.Add(New Point(6544, 3696))  '122 Hype Cave Entrance
        loDoors.Add(New Point(6752, 3800))  '123 Dark Lake Shop
        loDoors.Add(New Point(7360, 4048))  '124 Swamp Palace Entrance
        loDoors.Add(New Point(7760, 3664))  '125 Dark Lake Hylia Fairy
        loDoors.Add(New Point(7840, 3664))  '126 Hamburger Helper Cave
        loDoors.Add(New Point(7792, 3728))  '127 Spike Hint Cave
        'Holes
        loDoors.Add(New Point(776, 1048))   '128 Forest Hideout Dropdown
        loDoors.Add(New Point(1232, 808))   '129 Lumberjack Tree Dropdown
        loDoors.Add(New Point(2128, 1712))  '130 Sanctuary Grave
        loDoors.Add(New Point(2632, 1784))  '131 Houlihan Hole
        loDoors.Add(New Point(96, 2256))    '132 Kakariko Well
        loDoors.Add(New Point(1328, 2816))  '133 Magic Bat Dropdown
        loDoors.Add(New Point(2440, 2216))  '134 Castle Secret Dropdown
        loDoors.Add(New Point(4592, 880))   '135 Skull Woods Back Dropdown
        loDoors.Add(New Point(4872, 1048))  '136 Skull Woods Front West Dropdown
        loDoors.Add(New Point(4736, 1232))  '137 Skull Woods Big Chest Dropdown
        loDoors.Add(New Point(4896, 1200))  '138 Skull Woods Front East Dropdown
        loDoors.Add(New Point(6136, 2184))  '139 Pyramid Hole
        ReDim arrZ2R(loDoors.Count - 1)
        ReDim arrSkips(loDoors.Count - 1)
        For i As Int16 = 0 To loDoors.Count - 1
            arrZ2R(i) = -1
            arrSkips(i) = 0
        Next

    End Sub

    Private Sub resizeMap()
        If doResize = False Then Return
        Dim ix As Int16 = Math.Floor(Me.ClientSize.Width / 8)
        Dim iy As Int16 = Math.Floor(Me.ClientSize.Height / 5)
        Dim iz As Int16 = iy
        If standardResize Then
            If ix > iy Then iz = ix
            Dim bX = Me.Width - Me.ClientSize.Width
            Dim bY = Me.Height - Me.ClientSize.Height
            Me.Width = iz * 8 + bX
            Me.Height = iz * 5 + bY
        Else
            If ix < iy Then iz = ix
        End If
        map.Width = iz * 8
        map.Height = iz * 5
        map.Left = (Me.ClientSize.Width - map.Width) / 2
        map.Top = (Me.ClientSize.Height - map.Height) / 2
        standardResize = False
        drawEntrances()
    End Sub

    Private Sub frmES_ClientSizeChanged(sender As Object, e As EventArgs) Handles Me.ClientSizeChanged
        resizeMap()
    End Sub

    Private Sub frmES_ResizeBegin(sender As Object, e As EventArgs) Handles Me.ResizeBegin
        doResize = False
    End Sub


    Private Sub map_Paint(sender As Object, e As PaintEventArgs) Handles map.Paint
        drawEntrances()
    End Sub

    Private Sub map_MouseClick(sender As Object, e As MouseEventArgs) Handles map.MouseClick
        For i As Int16 = 0 To loSettings.Count - 1
            If loSettings(i).Contains(e.Location) Then
                Select Case i
                    Case 0
                        writeVars()
                    Case 1
                        readVars()
                    Case 2
                        clearVars()
                End Select
            End If
        Next
        For i As Int16 = loZones.Count - 1 To 0 Step -1
            If loZones(i).Contains(e.Location) Then
                If e.Button = MouseButtons.Left Then
                    If lineMode = 2 Then
                        If i < loZones.Count - 12 Then
                            arrR2Z(startRoom) = i + 1
                            lineMode = 0
                            map.Invalidate()
                        End If
                    Else
                        startZone = i
                        lineMode = 1
                    End If
                    Exit For
                ElseIf e.Button = MouseButtons.Right Then
                    startZone = 0
                    startRoom = 0
                    lineMode = 0
                    arrZ2R(i) = -1
                    If arrSkips(i) = 0 Then
                        arrSkips(i) = 1
                    Else
                        arrSkips(i) = 0
                    End If
                    map.Invalidate()
                    Exit For
                End If
            End If
        Next
        For i As Int16 = 0 To loRooms.Count - 1
            If loRooms(i).Contains(e.Location) Then
                If e.Button = MouseButtons.Left Then
                    If lineMode = 1 Then
                        arrZ2R(startZone) = i + 1
                        lineMode = 0
                        map.Invalidate()
                    Else
                        If i < loRooms.Count - 12 Then
                            startRoom = i
                            lineMode = 2
                        End If
                    End If
                    Exit For
                ElseIf e.Button = MouseButtons.Right Then
                    startZone = 0
                    startRoom = 0
                    lineMode = 0
                    If arrR2Z(i) > 0 Then
                        arrR2Z(i) = 0
                        map.Invalidate()
                    End If
                    Exit For
                End If
            End If
        Next
    End Sub

    Private Sub makeLines()
        Dim iPenR = New Pen(Color.Red, Math.Floor(iSize / 4))
        Dim iPenG = New Pen(Color.LawnGreen, Math.Floor(iSize / 4))
        iPenR.EndCap = Drawing2D.LineCap.ArrowAnchor
        iPenG.EndCap = Drawing2D.LineCap.ArrowAnchor
        iPenR.CustomEndCap = New Drawing2D.AdjustableArrowCap(4, 4)
        iPenG.CustomEndCap = New Drawing2D.AdjustableArrowCap(3, 3)
        Dim iBrush As New SolidBrush(Color.Red)
        Dim iGfx As Graphics = map.CreateGraphics()
        Dim zSize As Int16 = Math.Floor(iSize / 2)
        Dim rSize As Int16 = Math.Floor(map.Width / 64)
        For i As Int16 = 0 To arrZ2R.Count - 1
            If arrZ2R(i) > 0 Then
                iGfx.DrawLine(iPenR, loZones(i).X + zSize, loZones(i).Y + zSize, loRooms(arrZ2R(i) - 1).X + rSize, loRooms(arrZ2R(i) - 1).Y + rSize)
            End If
        Next
        For i As Int16 = 0 To arrR2Z.Count - 1
            If arrR2Z(i) > 0 Then
                iGfx.DrawLine(iPenG, loRooms(i).X + rSize, loRooms(i).Y + rSize, loZones(arrR2Z(i) - 1).X + zSize, loZones(arrR2Z(i) - 1).Y + zSize)
            End If
        Next
        iPenR.Dispose()
        iPenG.Dispose()
        iGfx.Dispose()
    End Sub

    Private Sub writeVars()
        Using fOutput As IO.StreamWriter = New IO.StreamWriter("save.dat")
            For i As Int16 = 0 To arrR2Z.Count - 1
                If i < arrR2Z.Count - 1 Then
                    fOutput.Write(arrR2Z(i).ToString & ",")
                Else
                    fOutput.WriteLine(arrR2Z(i).ToString)
                End If
            Next
            For i As Int16 = 0 To arrZ2R.Count - 1
                If i < arrZ2R.Count - 1 Then
                    fOutput.Write(arrZ2R(i).ToString & ",")
                Else
                    fOutput.WriteLine(arrZ2R(i).ToString)
                End If
            Next
            For i As Int16 = 0 To arrSkips.Count - 1
                If i < arrSkips.Count - 1 Then
                    fOutput.Write(arrSkips(i).ToString & ",")
                Else
                    fOutput.WriteLine(arrSkips(i).ToString)
                End If
            Next
            fOutput.Close()
        End Using
    End Sub

    Private Sub readVars()
        On Error GoTo fileNotFound
        Using fInput As IO.StreamReader = New IO.StreamReader("save.dat")
            Dim line1 As String = fInput.ReadLine
            Dim lineR2Z() As String = Split(line1, ",")
            Dim line2 As String = fInput.ReadLine
            Dim lineZ2R() As String = Split(line2, ",")
            Dim line3 As String = fInput.ReadLine
            Dim lineSkips() As String = Split(line3, ",")
            For i As Int16 = 0 To arrR2Z.Count - 1
                arrR2Z(i) = Convert.ToInt16(lineR2Z(i))
            Next
            For i As Int16 = 0 To arrZ2R.Count - 1
                arrZ2R(i) = Convert.ToInt16(lineZ2R(i))
            Next
            For i As Int16 = 0 To arrSkips.Count - 1
                arrSkips(i) = Convert.ToByte(lineSkips(i))
            Next
            fInput.Close()
        End Using
        map.Invalidate()
fileNotFound:
    End Sub

    Private Sub clearVars()
        For i As Int16 = 0 To arrR2Z.Count - 1
            arrR2Z(i) = -1
        Next
        For i As Int16 = 0 To arrZ2R.Count - 1
            arrZ2R(i) = -1
        Next
        For i As Int16 = 0 To arrSkips.Count - 1
            arrSkips(i) = 0
        Next
        map.Invalidate()
    End Sub
End Class
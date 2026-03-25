Imports System
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Collections
Imports System.Drawing
Imports System.ComponentModel
Imports System.Data
Imports System.IO
Imports System.Drawing.Imaging
Imports System.Text



Public Enum TwainCommand
    [Not] = -1
    Null = 0
    TransferReady = 1
    CloseRequest = 2
    CloseOk = 3
    DeviceEvent = 4
End Enum

#Region " Enums "
<Flags()> Friend Enum TwDG As Short
    Control = &H1
    Image = &H2
    Audio = &H4
End Enum

Friend Enum TwDAT As Short
    Null = &H0
    Capability = &H1
    [Event] = &H2
    Identity = &H3
    Parent = &H4
    PendingXfers = &H5
    SetupMemXfer = &H6
    SetupFileXfer = &H7
    Status = &H8
    UserInterface = &H9
    XferGroup = &HA
    TwunkIdentity = &HB
    CustomDSData = &HC
    DeviceEvent = &HD
    FileSystem = &HE
    PassThru = &HF

    ImageInfo = &H101
    ImageLayout = &H102
    ImageMemXfer = &H103
    ImageNativeXfer = &H104
    ImageFileXfer = &H105
    CieColor = &H106
    GrayResponse = &H107
    RGBResponse = &H108
    JpegCompression = &H109
    Palette8 = &H10A
    ExtImageInfo = &H10B

    SetupFileXfer2 = &H301
End Enum

Friend Enum TwMSG As Short
    Null = &H0
    [Get] = &H1
    GetCurrent = &H2
    GetDefault = &H3
    GetFirst = &H4
    GetNext = &H5
    [Set] = &H6
    Reset = &H7
    QuerySupport = &H8

    XFerReady = &H101
    CloseDSReq = &H102
    CloseDSOK = &H103
    DeviceEvent = &H104

    CheckStatus = &H201

    OpenDSM = &H301
    CloseDSM = &H302

    OpenDS = &H401
    CloseDS = &H402
    UserSelect = &H403

    DisableDS = &H501
    EnableDS = &H502
    EnableDSUIOnly = &H503

    ProcessEvent = &H601

    EndXfer = &H701
    StopFeeder = &H702

    ChangeDirectory = &H801
    CreateDirectory = &H802
    Delete = &H803
    FormatMedia = &H804
    GetClose = &H805
    GetFirstFile = &H806
    GetInfo = &H807
    GetNextFile = &H808
    Rename = &H809
    Copy = &H80A
    AutoCaptureDir = &H80B

    PassThru = &H901
End Enum

Friend Enum TwRC As Short
    Success = &H0
    Failure = &H1
    CheckStatus = &H2
    Cancel = &H3
    DSEvent = &H4
    NotDSEvent = &H5
    XferDone = &H6
    EndOfList = &H7
    InfoNotSupported = &H8
    DataNotAvailable = &H9
End Enum

Friend Enum TwCC As Short
    Success = &H0
    Bummer = &H1
    LowMemory = &H2
    NoDS = &H3
    MaxConnections = &H4
    OperationError = &H5
    BadCap = &H6
    BadProtocol = &H9
    BadValue = &HA
    SeqError = &HB
    BadDest = &HC
    CapUnsupported = &HD
    CapBadOperation = &HE
    CapSeqError = &HF
    Denied = &H10
    FileExists = &H11
    FileNotFound = &H12
    NotEmpty = &H13
    PaperJam = &H14
    PaperDoubleFeed = &H15
    FileWriteError = &H16
    CheckDeviceOnline = &H17
End Enum

Friend Enum TwOn As Short
    Array = &H3
    [Enum] = &H4
    One = &H5
    Range = &H6
    DontCare = -1
End Enum

Friend Enum TwType As Short
    Int8 = &H0
    Int16 = &H1
    Int32 = &H2
    UInt8 = &H3
    UInt16 = &H4
    UInt32 = &H5
    Bool = &H6
    Fix32 = &H7
    Frame = &H8
    Str32 = &H9
    Str64 = &HA
    Str128 = &HB
    Str255 = &HC
    Str1024 = &HD
    Str512 = &HE
End Enum

Friend Enum TwCap As Short
    XferCount = &H1
    ICompression = &H100
    IPixelType = &H101
    IUnits = &H102
    IXferMech = &H103
End Enum
#End Region

Public Class Twain

    Private hwnd As IntPtr
    Private appid As TwIdentity
    Private srcds As TwIdentity
    Private evtmsg As TwEvent
    Private winmsg_m As WINMSG_S

    <DllImport("twain_32.dll", EntryPoint:="#1")> Private Shared Function DSMparent(<[In](), Out()> ByVal origin As TwIdentity, ByVal zeroptr As IntPtr, ByVal dg As TwDG, ByVal dat As TwDAT, ByVal msg As TwMSG, ByRef refptr As IntPtr) As TwRC
    End Function

    <DllImport("twain_32.dll", EntryPoint:="#1")> Private Shared Function DSMident(<[In](), Out()> ByVal origin As TwIdentity, ByVal zeroptr As IntPtr, ByVal dg As TwDG, ByVal dat As TwDAT, ByVal msg As TwMSG, <[In](), Out()> ByVal idds As TwIdentity) As TwRC
    End Function

    <DllImport("twain_32.dll", EntryPoint:="#1")> Private Shared Function DSMstatus(<[In](), Out()> ByVal origin As TwIdentity, ByVal zeroptr As IntPtr, ByVal dg As TwDG, ByVal dat As TwDAT, ByVal msg As TwMSG, <[In](), Out()> ByVal dsmstat As TwStatus) As TwRC
    End Function

    <DllImport("twain_32.dll", EntryPoint:="#1")> Private Shared Function DSuserif(<[In](), Out()> ByVal origin As TwIdentity, <[In](), Out()> ByVal dest As TwIdentity, ByVal dg As TwDG, ByVal dat As TwDAT, ByVal msg As TwMSG, ByVal guif As TwUserInterface) As TwRC
    End Function

    <DllImport("twain_32.dll", EntryPoint:="#1")> Private Shared Function DSevent(<[In](), Out()> ByVal origin As TwIdentity, <[In](), Out()> ByVal dest As TwIdentity, ByVal dg As TwDG, ByVal dat As TwDAT, ByVal msg As TwMSG, ByRef evt As TwEvent) As TwRC
    End Function

    <DllImport("twain_32.dll", EntryPoint:="#1")> Private Shared Function DSstatus(<[In](), Out()> ByVal origin As TwIdentity, <[In]()> ByVal dest As TwIdentity, ByVal dg As TwDG, ByVal dat As TwDAT, ByVal msg As TwMSG, <[In](), Out()> ByVal dsmstat As TwStatus) As TwRC
    End Function

    <DllImport("twain_32.dll", EntryPoint:="#1")> Private Shared Function DScap(<[In](), Out()> ByVal origin As TwIdentity, <[In]()> ByVal dest As TwIdentity, ByVal dg As TwDG, ByVal dat As TwDAT, ByVal msg As TwMSG, <[In](), Out()> ByVal capa As TwCapability) As TwRC
    End Function

    <DllImport("twain_32.dll", EntryPoint:="#1")> Private Shared Function DSiinf(<[In](), Out()> ByVal origin As TwIdentity, <[In]()> ByVal dest As TwIdentity, ByVal dg As TwDG, ByVal dat As TwDAT, ByVal msg As TwMSG, <[In](), Out()> ByVal imginf As TwImageInfo) As TwRC
    End Function

    <DllImport("twain_32.dll", EntryPoint:="#1")> Private Shared Function DSixfer(<[In](), Out()> ByVal origin As TwIdentity, <[In]()> ByVal dest As TwIdentity, ByVal dg As TwDG, ByVal dat As TwDAT, ByVal msg As TwMSG, ByRef hbitmap As IntPtr) As TwRC
    End Function

    <DllImport("twain_32.dll", EntryPoint:="#1")> Private Shared Function DSpxfer(<[In](), Out()> ByVal origin As TwIdentity, <[In]()> ByVal dest As TwIdentity, ByVal dg As TwDG, ByVal dat As TwDAT, ByVal msg As TwMSG, <[In](), Out()> ByVal pxfr As TwPendingXfers) As TwRC
    End Function

    <DllImport("kernel32.dll", ExactSpelling:=True)> Friend Shared Function GlobalAlloc(ByVal flags As Integer, ByVal size As Integer) As IntPtr
    End Function

    <DllImport("kernel32.dll", ExactSpelling:=True)> Friend Shared Function GlobalLock(ByVal handle As IntPtr) As IntPtr
    End Function

    <DllImport("kernel32.dll", ExactSpelling:=True)> Friend Shared Function GlobalUnlock(ByVal handle As IntPtr) As Boolean
    End Function

    <DllImport("kernel32.dll", ExactSpelling:=True)> Friend Shared Function GlobalFree(ByVal handle As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll", ExactSpelling:=True)> Private Shared Function GetMessagePos() As Integer
    End Function

    <DllImport("user32.dll", ExactSpelling:=True)> Private Shared Function GetMessageTime() As Integer
    End Function

    <DllImport("gdi32.dll", ExactSpelling:=True)> Private Shared Function GetDeviceCaps(ByVal hDC As IntPtr, ByVal nIndex As Integer) As Integer
    End Function

    <DllImport("gdi32.dll", CharSet:=CharSet.Auto)> Private Shared Function CreateDC(ByVal szdriver As String, ByVal szdevice As String, ByVal szoutput As String, ByVal devmode As IntPtr) As IntPtr
    End Function
    <DllImport("gdi32.dll", ExactSpelling:=True)> Private Shared Function DeleteDC(ByVal hdc As IntPtr) As Boolean
    End Function

    Private Const CountryUSA As Short = 1
    Private Const LanguageUSA As Short = 13

    Public Sub New()
        appid = New TwIdentity()
        appid.Id = IntPtr.Zero
        appid.Version.MajorNum = 1
        appid.Version.MinorNum = 1
        appid.Version.Language = LanguageUSA
        appid.Version.Country = CountryUSA
        appid.Version.Info = "Twain 1.0"
        appid.ProtocolMajor = TwProtocol.Major
        appid.ProtocolMinor = TwProtocol.Minor
        appid.SupportedGroups = CType(TwDG.Image Or TwDG.Control, Integer)
        appid.Manufacturer = "MKA-SOFT"
        appid.ProductFamily = "Freeware"
        appid.ProductName = "EasyTwain"

        srcds = New TwIdentity()
        srcds.Id = IntPtr.Zero

        evtmsg.EventPtr = Marshal.AllocHGlobal(Marshal.SizeOf(winmsg_m))
    End Sub

    Public Sub Dispose()
        Marshal.FreeHGlobal(evtmsg.EventPtr)
    End Sub

    Protected Overrides Sub Finalize()
        Marshal.FreeHGlobal(evtmsg.EventPtr)
    End Sub

    Public Sub Init(ByVal hwndp As IntPtr)
        Finish()
        Dim rc As TwRC = DSMparent(appid, IntPtr.Zero, TwDG.Control, TwDAT.Parent, TwMSG.OpenDSM, hwndp)
        If (rc = TwRC.Success) Then
            rc = DSMident(appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.GetDefault, srcds)
            If (rc = TwRC.Success) Then
                hwnd = hwndp
            Else
                rc = DSMparent(appid, IntPtr.Zero, TwDG.Control, TwDAT.Parent, TwMSG.CloseDSM, hwndp)
            End If
        End If
    End Sub

    Public Sub [Select]()
        Dim rc As TwRC
        '        CloseSrc()
        If Equals(appid.Id, IntPtr.Zero) = True Then
            Init(hwnd)
            If Equals(appid.Id, IntPtr.Zero) = True Then
                Return
            End If
        End If
        rc = DSMident(appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.UserSelect, srcds)
    End Sub

    Public Sub Acquire()
        Dim rc As TwRC
        '        CloseSrc()
        If Equals(appid.Id, IntPtr.Zero) = True Then
            Init(hwnd)
            If Equals(appid.Id, IntPtr.Zero) = True Then
                Return
            End If
        End If

        rc = DSMident(appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.OpenDS, srcds)
        If (rc <> TwRC.Success) Then
            Return
        End If

        Dim cap As TwCapability = New TwCapability(TwCap.XferCount, 1)
        rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap)
        If (rc <> TwRC.Success) Then
            CloseSrc()
            Return
        End If

        Dim guif As TwUserInterface = New TwUserInterface()
        guif.ShowUI = 1
        guif.ModalUI = 1
        guif.ParentHand = hwnd
        rc = DSuserif(appid, srcds, TwDG.Control, TwDAT.UserInterface, TwMSG.EnableDS, guif)
        If (rc <> TwRC.Success) Then
            CloseSrc()
            Return
        End If
    End Sub

    Public Function TransferPictures() As ArrayList
        Dim pics As ArrayList = New ArrayList()
        If Equals(srcds.Id, IntPtr.Zero) Then
            Return pics
        End If

        Dim rc As TwRC
        Dim hbitmap As IntPtr = IntPtr.Zero
        Dim pxfr As TwPendingXfers = New TwPendingXfers()

        Do
            pxfr.Count = 0
            hbitmap = IntPtr.Zero

            Dim iinf As TwImageInfo = New TwImageInfo()
            rc = DSiinf(appid, srcds, TwDG.Image, TwDAT.ImageInfo, TwMSG.Get, iinf)
            If (rc <> TwRC.Success) Then
                CloseSrc()
                Return pics
            End If

            rc = DSixfer(appid, srcds, TwDG.Image, TwDAT.ImageNativeXfer, TwMSG.Get, hbitmap)
            If (rc <> TwRC.XferDone) Then
                CloseSrc()
                Return pics
            End If

            rc = DSpxfer(appid, srcds, TwDG.Control, TwDAT.PendingXfers, TwMSG.EndXfer, pxfr)
            If (rc <> TwRC.Success) Then
                CloseSrc()
                Return pics
            End If

            pics.Add(hbitmap)
        Loop While (pxfr.Count <> 0)

        rc = DSpxfer(appid, srcds, TwDG.Control, TwDAT.PendingXfers, TwMSG.Reset, pxfr)
        Return pics
    End Function

    Public Function PassMessage(ByVal m As Message) As TwainCommand
        If Equals(srcds.Id, IntPtr.Zero) Then
            Return TwainCommand.Not
        End If

        Dim pos As Integer = GetMessagePos()

        winmsg_m.hwnd = m.HWnd
        winmsg_m.message = m.Msg
        winmsg_m.wParam = m.WParam
        winmsg_m.lParam = m.LParam
        winmsg_m.time = GetMessageTime()
        winmsg_m.x = pos 'CType(pos, Short)
        winmsg_m.y = Int(pos / 2 ^ 16) 'CType(Int(pos / 2 ^ 16), Short)

        Marshal.StructureToPtr(winmsg_m, evtmsg.EventPtr, False)
        evtmsg.Message = 0

        Dim rc As TwRC = DSevent(appid, srcds, TwDG.Control, TwDAT.Event, TwMSG.ProcessEvent, evtmsg)
        If (rc = TwRC.NotDSEvent) Then
            Return TwainCommand.Not
        End If

        If (evtmsg.Message = CType(TwMSG.XFerReady, Short)) Then
            Return TwainCommand.TransferReady
        End If

        If (evtmsg.Message = CType(TwMSG.CloseDSReq, Short)) Then
            Return TwainCommand.CloseRequest
        End If
        If (evtmsg.Message = CType(TwMSG.CloseDSOK, Short)) Then
            Return TwainCommand.CloseOk
        End If
        If (evtmsg.Message = CType(TwMSG.DeviceEvent, Short)) Then
            Return TwainCommand.DeviceEvent
        End If

        Return TwainCommand.Null
    End Function

    Public Sub CloseSrc()
        Dim rc As TwRC
        If Not Equals(srcds.Id, IntPtr.Zero) Then
            Dim guif As TwUserInterface = New TwUserInterface()
            rc = DSuserif(appid, srcds, TwDG.Control, TwDAT.UserInterface, TwMSG.DisableDS, guif)
            rc = DSMident(appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.CloseDS, srcds)
        End If
    End Sub

    Public Sub Finish()
        Dim rc As TwRC
        CloseSrc()
        If Not Equals(appid.Id, IntPtr.Zero) Then
            rc = DSMparent(appid, IntPtr.Zero, TwDG.Control, TwDAT.Parent, TwMSG.CloseDSM, hwnd)
        End If
        appid.Id = IntPtr.Zero
    End Sub

    Public Shared ReadOnly Property ScreenBitDepth() As Integer
        Get
            Dim screenDC As IntPtr = CreateDC("DISPLAY", Nothing, Nothing, IntPtr.Zero)
            Dim bitDepth As Integer = GetDeviceCaps(screenDC, 12)
            bitDepth *= GetDeviceCaps(screenDC, 14)
            DeleteDC(screenDC)
            Return bitDepth
        End Get
    End Property

    <StructLayout(LayoutKind.Sequential, Pack:=4)> Friend Structure WINMSG_S
        Public hwnd As IntPtr
        Public message As Integer
        Public wParam As IntPtr
        Public lParam As IntPtr
        Public time As Integer
        Public x As Integer
        Public y As Integer
    End Structure
End Class

Public Class TwProtocol
    Public Const Major As Short = 1
    Public Const Minor As Short = 9
End Class


<StructLayout(LayoutKind.Sequential, Pack:=2, CharSet:=CharSet.Ansi)> Friend Class TwIdentity
    Public Id As IntPtr
    Public Version As TwVersion
    Public ProtocolMajor As Short
    Public ProtocolMinor As Short
    Public SupportedGroups As Integer
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=34)> Public Manufacturer As String
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=34)> Public ProductFamily As String
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=34)> Public ProductName As String
End Class

<StructLayout(LayoutKind.Sequential, Pack:=2, CharSet:=CharSet.Ansi)> Friend Structure TwVersion
    Public MajorNum As Short
    Public MinorNum As Short
    Public Language As Short
    Public Country As Short
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=34)> Public Info As String
End Structure

<StructLayout(LayoutKind.Sequential, Pack:=2)> Friend Class TwUserInterface
    Public ShowUI As Short
    Public ModalUI As Short
    Public ParentHand As IntPtr
End Class

<StructLayout(LayoutKind.Sequential, Pack:=2)> Friend Class TwStatus
    Public ConditionCode As Short
    Public Reserved As Short
End Class

<StructLayout(LayoutKind.Sequential, Pack:=2)> Friend Structure TwEvent
    Public EventPtr As IntPtr
    Public Message As Short
End Structure

<StructLayout(LayoutKind.Sequential, Pack:=2)> Friend Class TwImageInfo
    Public XResolution As Integer
    Public YResolution As Integer
    Public ImageWidth As Integer
    Public ImageLength As Integer
    Public SamplesPerPixel As Short
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=8)> Public BitsPerSample() As Short
    Public BitsPerPixel As Short
    Public Planar As Short
    Public PixelType As Short
    Public Compression As Short
End Class

<StructLayout(LayoutKind.Sequential, Pack:=2)> Friend Class TwPendingXfers
    Public Count As Short
    Public EOJ As Integer
End Class

<StructLayout(LayoutKind.Sequential, Pack:=2)> Friend Structure TwFix32
    Public Whole As Short
    Public Frac As Short

    Public Function ToFloat() As Single
        Return CType(Whole + (CType(Frac, Single) / 65536.0F), Single)
    End Function

    Public Sub FromFloat(ByVal f As Single)
        Dim i As Integer = CType(((f * 65536.0F) + 0.5F), Integer)
        Whole = CType(i / 2 ^ 16, Short)
        Frac = CType(i & &HFFFF, Short)
    End Sub
End Structure

<StructLayout(LayoutKind.Sequential, Pack:=2)> Friend Class TwCapability
    Public Cap As Short
    Public ConType As Short
    Public Handle As IntPtr
    Public Sub TwCapability(ByVal capIn As TwCap)
        Cap = CType(capIn, Short)
        ConType = -1
    End Sub

    Public Sub New(ByVal capIn As TwCap, ByVal sval As Short)
        Cap = CType(capIn, Short)
        ConType = CType(TwOn.One, Short)
        Handle = Twain.GlobalAlloc(&H42, 6)
        Dim pv As IntPtr = Twain.GlobalLock(Handle)
        Marshal.WriteInt16(pv, 0, CType(TwType.Int16, Short))
        Marshal.WriteInt32(pv, 2, CType(sval, Integer))
        Twain.GlobalUnlock(Handle)
    End Sub

    Public Sub Dispose()
        If Not Equals(Handle, IntPtr.Zero) Then
            Twain.GlobalFree(Handle)
        End If
    End Sub

    Protected Overrides Sub Finalize()
        If Not Equals(Handle, IntPtr.Zero) Then
            Twain.GlobalFree(Handle)
        End If
    End Sub
End Class





Public Class TwainHandler
    Inherits System.Windows.Forms.Form
    Implements IMessageFilter

    Private msgfilter As Boolean
    Private tw As Twain
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Private picnumber As Integer = 0

    Public FilePath As String
    Public FileName As String

    <DllImport("gdi32.dll", ExactSpelling:=True)> Friend Shared Function SetDIBitsToDevice(ByVal hdc As IntPtr, ByVal xdst As Integer, ByVal ydst As Integer, ByVal width As Integer, ByVal height As Integer, ByVal xsrc As Integer, ByVal ysrc As Integer, ByVal start As Integer, ByVal lines As Integer, ByVal bitsptr As IntPtr, ByVal bmiptr As IntPtr, ByVal color As Integer) As Integer
    End Function
    <DllImport("kernel32.dll", ExactSpelling:=True)> Friend Shared Function GlobalLock(ByVal handle As IntPtr) As IntPtr
    End Function
    <DllImport("kernel32.dll", ExactSpelling:=True)> Friend Shared Function GlobalFree(ByVal handle As IntPtr) As IntPtr
    End Function
    <DllImport("kernel32.dll", CharSet:=CharSet.Auto)> Public Shared Sub OutputDebugString(ByVal outstr As String)
    End Sub



    Public Sub New()
        InitializeComponent()
        tw = New Twain()
        tw.Init(Me.Handle)
    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    Private components As System.ComponentModel.IContainer



    Public Function PreFilterMessage(ByRef m As Message) As Boolean Implements IMessageFilter.PreFilterMessage
        Dim cmd As TwainCommand = tw.PassMessage(m)
        If (cmd = TwainCommand.Not) Then
            Return False
        End If

        Select Case cmd
            Case TwainCommand.CloseRequest
                EndingScan()
                tw.CloseSrc()
            Case TwainCommand.CloseOk
                EndingScan()
                tw.CloseSrc()
            Case TwainCommand.DeviceEvent
            Case TwainCommand.TransferReady
                Dim pics As ArrayList = tw.TransferPictures()
                EndingScan()
                tw.CloseSrc()
                picnumber += 1
                Dim i As Integer
                For i = 0 To pics.Count - 1 Step 1
                    Dim img As IntPtr = CType(pics(i), IntPtr)
                    Dim dibhand As IntPtr
                    Dim bmpptr As IntPtr
                    Dim pixptr As IntPtr
                    dibhand = img
                    bmpptr = GlobalLock(dibhand)
                    pixptr = GetPixelInfo(bmpptr)
                    If Not Gdip.SaveDIBAs(FilePath & "\" & FileName, bmpptr, pixptr) Then
                        FileName = ""
                    End If
                    GlobalFree(dibhand)
                    dibhand = IntPtr.Zero
                Next
            Case TwainCommand.Null
                EndingScan()
                tw.CloseSrc()
        End Select

        Return True
    End Function

    Protected Function GetPixelInfo(ByVal bmpptr As IntPtr) As IntPtr
        Dim Bmi As BITMAPINFOHEADER
        '   Dim bmprect As Rectangle
        Bmi = New BITMAPINFOHEADER()
        Marshal.PtrToStructure(bmpptr, Bmi)

        If (Bmi.biSizeImage = 0) Then
            Bmi.biSizeImage = Int((((Bmi.biWidth * Bmi.biBitCount) + 31) & Hex(Not (31))) / 2 ^ 3) * Bmi.biHeight
        End If

        Dim p As Integer = Bmi.biClrUsed
        If ((p = 0) And (Bmi.biBitCount <= 8)) Then
            p = Int(1 * 2 ^ Bmi.biBitCount)
        End If
        p = (p * 4) + Bmi.biSize + CType(bmpptr.ToInt32, Integer)
        Return New IntPtr(p)
    End Function


    Private Sub EndingScan()
        If (msgfilter) Then
            Application.RemoveMessageFilter(Me)
            msgfilter = False
            Me.Enabled = True
            Me.Activate()
        End If
    End Sub



    Public Function ScanIt(ByVal TheFilePath As String) As String
        tw.Select()
        If (Not msgfilter) Then
            Me.Enabled = False
            msgfilter = True
            Application.AddMessageFilter(Me)
        End If
        FilePath = TheFilePath
        FileName = Format(Now, "yyyyMMddhhmmss") + ".jpg"
        tw.Acquire()

        Do While Me.Enabled = False
            Application.DoEvents()
        Loop
        '  tw.CloseSrc()

        Return FileName
    End Function


    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'TwainHandler
        '
        Me.ClientSize = New System.Drawing.Size(197, 106)
        Me.Name = "TwainHandler"
        Me.ResumeLayout(False)

    End Sub

    Private Sub TwainHandler_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class


Public Class Gdip
    <DllImport("gdiplus.dll", ExactSpelling:=True)> Friend Shared Function GdipCreateBitmapFromGdiDib(ByVal bminfo As IntPtr, ByVal pixdat As IntPtr, ByRef image As IntPtr) As Integer
    End Function
    <DllImport("gdiplus.dll", ExactSpelling:=True, CharSet:=CharSet.Unicode)> Friend Shared Function GdipSaveImageToFile(ByVal image As IntPtr, ByVal filename As String, <[In]()> ByRef clsid As Guid, ByVal encparams As IntPtr) As Integer
    End Function
    <DllImport("gdiplus.dll", ExactSpelling:=True)> Friend Shared Function GdipDisposeImage(ByVal image As IntPtr) As Integer
    End Function

    'private static ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
    Private Shared codecs() As ImageCodecInfo = ImageCodecInfo.GetImageEncoders()

    'private static bool GetCodecClsid( string filename, out Guid clsid )
    Private Shared Function GetCodecClsid(ByVal filename As String, ByRef clsid As Guid) As Boolean
        clsid = Guid.Empty
        Dim ext As String = Path.GetExtension(filename)
        'Checking string for null
        If IsNothing(ext) Then
            Return False
        End If
        ext = "*" + ext.ToUpper()
        Dim codec As ImageCodecInfo
        For Each codec In codecs
            If (codec.FilenameExtension.IndexOf(ext) >= 0) Then
                clsid = codec.Clsid
                Return True
            End If
        Next
        Return False
    End Function

    'public static bool SaveDIBAs( string picname, IntPtr bminfo, IntPtr pixdat )
    Public Shared Function SaveDIBAs(ByVal picname As String, ByVal bminfo As IntPtr, ByVal pixdat As IntPtr) As Boolean
        Dim sd As SaveFileDialog = New SaveFileDialog()

        sd.FileName = picname

        Dim clsid As Guid
        If Not GetCodecClsid(sd.FileName, clsid) Then
            MessageBox.Show("Unknown picture format for extension " + Path.GetExtension(sd.FileName), "Image Codec", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If

        Dim img As IntPtr = IntPtr.Zero
        Dim st As Integer = GdipCreateBitmapFromGdiDib(bminfo, pixdat, img)
        If (st <> 0) Or (Equals(img, IntPtr.Zero)) Then
            Return False
        End If

        st = GdipSaveImageToFile(img, sd.FileName, clsid, IntPtr.Zero)
        GdipDisposeImage(img)
        Return st = 0
    End Function

End Class



<StructLayout(LayoutKind.Sequential, Pack:=2)> Friend Class BITMAPINFOHEADER
    Public biSize As Integer
    Public biWidth As Integer
    Public biHeight As Integer
    Public biPlanes As Short
    Public biBitCount As Short
    Public biCompression As Integer
    Public biSizeImage As Integer
    Public biXPelsPerMeter As Integer
    Public biYPelsPerMeter As Integer
    Public biClrUsed As Integer
    Public biClrImportant As Integer
End Class

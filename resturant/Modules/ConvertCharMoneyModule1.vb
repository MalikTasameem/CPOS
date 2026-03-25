Module ConvertCharMoneyModule1
    Public Function HANY(ByVal HANY1 As Decimal, ByVal HANY2 As String) As String
        On Error Resume Next
        Dim VPS As Decimal = 0
        Dim V As Decimal = 0
        Dim WORDINTEGER As String = ""
        Dim LE As String = ""
        Dim P As String = ""
        Dim PS As String = ""
        HANY = ""
        Dim POUNDS As String = ""
        Dim WORDPS As String = ""
        Dim DOLLAR As String = ""
        Dim SENT As String = ""
        Dim SENTS As String = ""
        Dim TON As String = ""
        Dim KG As String = ""
        Dim KGS As String = ""
        Select Case HANY2
            Case "EGYPT"
                LE = " دينار "
                P = " قرشا "
                PS = " دراهم "
                POUNDS = " جنيهات "
                V = Int(Math.Abs(HANY1))
                VPS = Val(Right(Format(HANY1, "000000000000.00"), 2))
                WORDINTEGER = AmountWord(V)
                WORDPS = AmountWord(VPS)
                If WORDINTEGER <> "" And (VPS <= 2) Then HANY = WORDINTEGER & LE & " و " & WORDPS & P & "فقط لاغير "
                If WORDINTEGER <> "" And (VPS >= 3 And VPS <= 9) Then HANY = WORDINTEGER & LE & " و " & WORDPS & PS & "فقط لاغير "
                If WORDINTEGER <> "" And (VPS > 9) Then HANY = WORDINTEGER & LE & " و " & WORDPS & P & "فقط لاغير "
                If WORDINTEGER = "" And (VPS <= 2) Then HANY = WORDPS & P & "فقط لاغير "
                If WORDINTEGER = "" And (VPS >= 3 And VPS <= 9) Then HANY = WORDPS & PS & "فقط لاغير "
                If WORDINTEGER = "" And VPS > 9 Then HANY = WORDPS & P & "فقط لاغير "
                If WORDINTEGER = "" And VPS = 0 Then HANY = ""
                If WORDINTEGER <> "" And VPS = 0 Then HANY = WORDINTEGER & LE & "فقط لاغير "
            Case "USA"
                DOLLAR = " دولار "
                SENT = " سنتاً "
                SENTS = "سنتات"
                V = Int(System.Math.Abs(HANY1))
                VPS = Val(Right(Format(HANY1, "000000000000.00"), 2))
                WORDINTEGER = AmountWord(V)
                WORDPS = AmountWord(VPS)
                If WORDINTEGER <> "" And (VPS <= 2) Then HANY = WORDINTEGER & DOLLAR & " و " & WORDPS & SENT & "فقط لاغير "
                If WORDINTEGER <> "" And (VPS >= 3 And VPS <= 9) Then HANY = WORDINTEGER & DOLLAR & " و " & WORDPS & " " & SENTS & " " & "فقط لاغير "
                If WORDINTEGER <> "" And (VPS > 9) Then HANY = WORDINTEGER & DOLLAR & " و " & WORDPS & SENT & "فقط لاغير "
                If WORDINTEGER = "" And (VPS <= 2) Then HANY = WORDPS & SENT & "فقط لاغير "
                If WORDINTEGER = "" And (VPS >= 3 And VPS <= 9) Then HANY = WORDPS & " " & SENTS & " " & "فقط لاغير "
                If WORDINTEGER = "" And VPS > 9 Then HANY = WORDPS & SENT & "فقط لاغير "
                If WORDINTEGER = "" And VPS = 0 Then HANY = ""
                If WORDINTEGER <> "" And VPS = 0 Then HANY = WORDINTEGER & DOLLAR & "فقط لاغير "

            Case "EUR"
                DOLLAR = " يورو "
                SENT = " سنتاً "
                SENTS = "سنتات"
                V = Int(System.Math.Abs(HANY1))
                VPS = Val(Right(Format(HANY1, "000000000000.00"), 2))
                WORDINTEGER = AmountWord(V)
                WORDPS = AmountWord(VPS)
                If WORDINTEGER <> "" And (VPS <= 2) Then HANY = WORDINTEGER & DOLLAR & " و " & WORDPS & SENT & "فقط لاغير "
                If WORDINTEGER <> "" And (VPS >= 3 And VPS <= 9) Then HANY = WORDINTEGER & DOLLAR & " و " & WORDPS & " " & SENTS & " " & "فقط لاغير "
                If WORDINTEGER <> "" And (VPS > 9) Then HANY = WORDINTEGER & DOLLAR & " و " & WORDPS & SENT & "فقط لاغير "
                If WORDINTEGER = "" And (VPS <= 2) Then HANY = WORDPS & SENT & "فقط لاغير "
                If WORDINTEGER = "" And (VPS >= 3 And VPS <= 9) Then HANY = WORDPS & " " & SENTS & " " & "فقط لاغير "
                If WORDINTEGER = "" And VPS > 9 Then HANY = WORDPS & SENT & "فقط لاغير "
                If WORDINTEGER = "" And VPS = 0 Then HANY = ""
                If WORDINTEGER <> "" And VPS = 0 Then HANY = WORDINTEGER & DOLLAR & "فقط لاغير "

            Case "WEIGHT"
                TON = " طن "
                KG = " كيلو جرام "
                KGS = "كيلو جرامات"
                V = Int(Math.Abs(HANY1))
                VPS = Val(Right(Format(HANY1, "000000000000.000"), 3))
                WORDINTEGER = AmountWord(V)
                WORDPS = AmountWord(VPS)
                If WORDINTEGER <> "" And (VPS <= 2) Then HANY = WORDINTEGER & TON & " و " & WORDPS & KG & "فقط لاغير "
                If WORDINTEGER <> "" And (VPS >= 3 And VPS <= 9) Then HANY = WORDINTEGER & TON & " و " & WORDPS & KGS & "فقط لاغير "
                If WORDINTEGER <> "" And (VPS > 9) Then HANY = WORDINTEGER & TON & " و " & WORDPS & KG & "فقط لاغير "
                If WORDINTEGER = "" And (VPS <= 2) Then HANY = WORDPS & KG & "فقط لاغير "
                If WORDINTEGER = "" And (VPS >= 3 And VPS <= 9) Then HANY = WORDPS & KGS & "فقط لاغير "
                If WORDINTEGER = "" And VPS > 9 Then HANY = WORDPS & KG & "فقط لاغير "
                If WORDINTEGER = "" And VPS = 0 Then HANY = ""
                If WORDINTEGER <> "" And VPS = 0 Then HANY = WORDINTEGER & TON & "فقط لاغير "
        End Select
    End Function
    Private Function AmountWord(ByVal AMOUNT As Decimal) As String
        On Error Resume Next
        Dim n As Decimal = 0
        Dim C1 As Decimal = 0
        Dim C2 As Decimal = 0
        Dim C3 As Decimal = 0
        Dim C4 As Decimal = 0
        Dim C5 As Decimal = 0
        Dim C6 As Decimal = 0
        Dim S1 As String = ""
        Dim S2 As String = ""
        Dim S3 As String = ""
        Dim S4 As String = ""
        Dim S5 As String = ""
        Dim S6 As String = ""
        Dim C As String = ""
        n = Int(AMOUNT)
        C = Format(n, "000000000000")
        C1 = Val(Mid(C, 12, 1))
        Select Case C1
            Case Is = 1 : S1 = "واحد"
            Case Is = 2 : S1 = "اثنان"
            Case Is = 3 : S1 = "ثلاثة"
            Case Is = 4 : S1 = "اربعة"
            Case Is = 5 : S1 = "خمسة"
            Case Is = 6 : S1 = "ستة"
            Case Is = 7 : S1 = "سبعة"
            Case Is = 8 : S1 = "ثمانية"
            Case Is = 9 : S1 = "تسعة"
        End Select

        C2 = Val(Mid(C, 11, 1))
        Select Case C2
            Case Is = 1 : S2 = "عشر"
            Case Is = 2 : S2 = "عشرون"
            Case Is = 3 : S2 = "ثلاثون"
            Case Is = 4 : S2 = "اربعون"
            Case Is = 5 : S2 = "خمسون"
            Case Is = 6 : S2 = "ستون"
            Case Is = 7 : S2 = "سبعون"
            Case Is = 8 : S2 = "ثمانون"
            Case Is = 9 : S2 = "تسعون"
        End Select

        If S1 <> "" And C2 > 1 Then S2 = S1 + " و" + S2
        If S2 = "" Then S2 = S1
        If C1 = 0 And C2 = 1 Then S2 = S2 + "ة"
        If C1 = 1 And C2 = 1 Then S2 = "احدى عشر"
        If C1 = 2 And C2 = 1 Then S2 = "اثنى عشر"
        If C1 > 2 And C2 = 1 Then S2 = S1 + " " + S2
        C3 = Val(Mid(C, 10, 1))
        Select Case C3
            Case Is = 1 : S3 = "مائة"
            Case Is = 2 : S3 = "مئتان"
            Case Is > 2 : S3 = Left(AmountWord(C3), Len(AmountWord(C3)) - 1) + "مائة"
        End Select
        If S3 <> "" And S2 <> "" Then S3 = S3 + " و" + S2
        If S3 = "" Then S3 = S2

        C4 = Val(Mid(C, 7, 3))
        Select Case C4
            Case Is = 1 : S4 = "الف"
            Case Is = 2 : S4 = "الفان"
            Case 3 To 10 : S4 = AmountWord(C4) + " آلاف"
            Case Is > 10 : S4 = AmountWord(C4) + " الف"
        End Select
        If S4 <> "" And S3 <> "" Then S4 = S4 + " و" + S3
        If S4 = "" Then S4 = S3
        C5 = Val(Mid(C, 4, 3))
        Select Case C5
            Case Is = 1 : S5 = "مليون"
            Case Is = 2 : S5 = "مليونان"
            Case 3 To 10 : S5 = AmountWord(C5) + " ملايين"
            Case Is > 10 : S5 = AmountWord(C5) + " مليون"
        End Select
        If S5 <> "" And S4 <> "" Then S5 = S5 + " و" + S4
        If S5 = "" Then S5 = S4

        C6 = Val(Mid(C, 1, 3))

        Select Case C6
            Case Is = 1 : S6 = "مليار"
            Case Is = 2 : S6 = "ملياران"
            Case Is > 2 : S6 = AmountWord(C6) + " مليار"
        End Select
        If S6 <> "" And S5 <> "" Then S6 = S6 + " و" + S5
        If S6 = "" Then S6 = S5
        AmountWord = S6
    End Function
End Module

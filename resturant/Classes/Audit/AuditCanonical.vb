Option Strict On
Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Security.Cryptography
Imports System.Text
Imports System.Xml
Imports System.Xml.Linq

Friend NotInheritable Class AuditCanonical
    ' Version 1: ordered UTF-8 tokens; Int32 little-endian byte length; -1 = null, 0 = empty.
    Friend Shared Function Encode(tokens As IEnumerable(Of String)) As Byte()
        Using stream As New MemoryStream(), writer As New BinaryWriter(stream, New UTF8Encoding(False, True))
            For Each token In tokens
                If token Is Nothing Then
                    writer.Write(-1)
                Else
                    Dim bytes = New UTF8Encoding(False, True).GetBytes(token)
                    writer.Write(bytes.Length)
                    writer.Write(bytes)
                End If
                If stream.Length > AuditContract.MaxPayload Then Throw New ArgumentException("حمولة المراقبة تتجاوز 1 MiB.")
            Next
            Return stream.ToArray()
        End Using
    End Function

    Friend Shared Function Hash(bytes As Byte()) As Byte()
        Using digest = SHA256.Create()
            Return digest.ComputeHash(bytes)
        End Using
    End Function

    Friend Shared Function S(value As Object) As String
        If value Is Nothing Then Return Nothing
        If TypeOf value Is DateTime Then Return CDate(value).ToString("yyyy-MM-ddTHH:mm:ss.fff", CultureInfo.InvariantCulture)
        Return Convert.ToString(value, CultureInfo.InvariantCulture)
    End Function

    Friend Shared Function RequestBytes(request As AuditOperationRequest) As Byte()
        AuditContract.Request(request)
        Dim c = request.Context
        Dim tokens As New List(Of String) From {"CPOS-AUDIT/1", "Request", S(c.SourceId), S(request.RequestId), request.OperationCode,
            S(CByte(c.ActorKind)), S(c.ActorUserId), c.ActorServiceCode, S(c.SessionId), c.ScreenCode, request.EntityType, request.EntityId, S(request.Inputs.Count)}
        For Each input As AuditInput In request.Inputs.OrderBy(Function(x) x.Name, StringComparer.Ordinal)
            tokens.AddRange({input.Name, input.ValueType, S(CByte(input.Value.State)), input.Value.Text})
        Next
        Return Encode(tokens)
    End Function

    Friend Shared Function ChangesXml(item As AuditEventRequest) As String
        Dim root As New XElement("Changes", New XAttribute("version", "1"))
        For Each change In item.Changes.OrderBy(Function(x) x.ChangeNo)
            root.Add(New XElement("Change", New XAttribute("no", change.ChangeNo), New XAttribute("entityType", change.EntityType),
                New XAttribute("entityId", change.EntityId), New XAttribute("field", change.FieldCode), New XAttribute("type", change.ValueType),
                If(change.SourceSqlType Is Nothing, Nothing, New XAttribute("sqlType", change.SourceSqlType)), New XAttribute("origin", change.ValueOrigin),
                New XElement("Before", New XAttribute("state", CByte(change.Before.State)), change.Before.Text),
                New XElement("After", New XAttribute("state", CByte(change.After.State)), change.After.Text)))
        Next
        Return LimitedXml(root)
    End Function

    Friend Shared Function RelationsXml(item As AuditEventRequest) As String
        Dim root As New XElement("Entities", New XAttribute("version", "1"))
        For Each r In item.RelatedEntities.OrderBy(Function(x) x.Role, StringComparer.Ordinal).ThenBy(Function(x) x.EntityType, StringComparer.Ordinal).ThenBy(Function(x) x.EntityId, StringComparer.Ordinal)
            root.Add(New XElement("Entity", New XAttribute("role", r.Role), New XAttribute("type", r.EntityType), New XAttribute("id", r.EntityId)))
        Next
        Return LimitedXml(root)
    End Function

    Friend Shared Function LimitedXml(root As XElement) As String
        Dim xml = root.ToString(SaveOptions.DisableFormatting)
        If xml.Length > AuditContract.MaxXmlChars Then Throw New ArgumentException("XML المراقبة يتجاوز الحد المسموح.")
        Return xml
    End Function

    Friend Shared Function ParseXml(xml As String) As XElement
        If xml Is Nothing OrElse xml.Length > AuditContract.MaxXmlChars Then Throw New ArgumentException("XML غير صالح/كبير.")
        Dim settings As New XmlReaderSettings With {.DtdProcessing = DtdProcessing.Prohibit, .XmlResolver = Nothing, .MaxCharactersInDocument = AuditContract.MaxXmlChars}
        Using text As New StringReader(xml), reader As XmlReader = XmlReader.Create(text, settings)
            Return XElement.Load(reader)
        End Using
    End Function

    Friend Shared Function EventBytes(request As AuditOperationRequest, item As AuditEventRequest, notes As String, changes As String, relations As String) As Byte()
        Dim c = request.Context
        Return Encode({"CPOS-AUDIT/1", "Event", S(c.SourceId), S(request.RequestId), S(item.EventId), S(item.EventOrdinal), S(item.CorrelationId),
            item.EventCode, AuditContract.Domain(item.EventCode), c.ScreenCode, S(AuditContract.Kind(item.EventCode)), S(item.Outcome),
            S(CByte(c.ActorKind)), S(c.ActorUserId), c.ActorServiceCode, c.ActorName, S(c.SessionId), c.MachineName, c.ApplicationVersion,
            item.EntityType, item.EntityId, item.DocumentNumber, S(item.OccurredAtUtc), S(item.BusinessDate), item.ReasonCode,
            item.EventCode, "1", notes, relations, changes})
    End Function
End Class

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.SqlServer.TransactSql.ScriptDom;

// SQL 2014 AST, not a search through migration replacement strings. Inspect the FINAL module.
public sealed class AuditSqlSafetyVisitor : TSqlFragmentVisitor
{
    public readonly List<string> Findings = new List<string>();
    public readonly HashSet<string> DeclaredOperations = new HashSet<string>(StringComparer.Ordinal);
    private static string Text(TSqlFragment node) {
        if (node.ScriptTokenStream == null) return "";
        return String.Concat(node.ScriptTokenStream.Skip(node.FirstTokenIndex).Take(node.LastTokenIndex-node.FirstTokenIndex+1).Select(t => t.Text));
    }
    private static bool RawValue(string text) {
        return Regex.IsMatch(text, @"(?i)(\b(?:AfterValue|BeforeValue|QueueDateValue|InputValue|ValueText)\b|@value\b)");
    }
    private void Check(DataTypeReference type, ScalarExpression value, int line) {
        var sqlType = type as SqlDataTypeReference;
        if (sqlType == null) return;
        var name = sqlType.SqlDataTypeOption.ToString();
        if (new [] {"Char","VarChar","NChar","NVarChar","Text","NText","Binary","VarBinary"}.Contains(name)) return;
        if (RawValue(Text(value))) Findings.Add("line " + line + ": unsafe conversion of raw audit text to " + name + "; use TRY_CONVERT and explicit validation");
    }
    public override void ExplicitVisit(ConvertCall node) { Check(node.DataType, node.Parameter, node.StartLine); base.ExplicitVisit(node); }
    public override void ExplicitVisit(CastCall node) { Check(node.DataType, node.Parameter, node.StartLine); base.ExplicitVisit(node); }
    public override void ExplicitVisit(TryConvertCall node) {
        var type = node.DataType as SqlDataTypeReference;
        var parsed = node.Parameter as TryConvertCall;
        var parsedType = parsed == null ? null : parsed.DataType as SqlDataTypeReference;
        if (type != null && type.SqlDataTypeOption == SqlDataTypeOption.DateTime && RawValue(Text(node.Parameter))
            && (parsedType == null || parsedType.SqlDataTypeOption != SqlDataTypeOption.DateTime2))
            Findings.Add("line " + node.StartLine + ": direct datetime parsing rejects canonical seven-digit timestamps on SQL2014; use TRY_CONVERT(datetime,TRY_CONVERT(datetime2(7),value,126))");
        base.ExplicitVisit(node);
    }
    private static bool OperationVariable(ScalarExpression expression) {
        var variable = expression as VariableReference;
        return variable != null && new [] {"@OperationCode","@operation","@code","@EventCode"}.Contains(variable.Name, StringComparer.OrdinalIgnoreCase);
    }
    private void Add(ScalarExpression expression) {
        var literal = expression as StringLiteral;
        if (literal != null && Regex.IsMatch(literal.Value, @"^[A-Z][A-Za-z0-9]+\.[A-Z][A-Za-z0-9]+$")) DeclaredOperations.Add(literal.Value);
    }
    public override void ExplicitVisit(InPredicate node) {
        if (OperationVariable(node.Expression)) foreach (var expression in node.Values) Add(expression);
        base.ExplicitVisit(node);
    }
    public override void ExplicitVisit(BooleanComparisonExpression node) {
        if (OperationVariable(node.FirstExpression)) Add(node.SecondExpression);
        if (OperationVariable(node.SecondExpression)) Add(node.FirstExpression);
        base.ExplicitVisit(node);
    }
    public override void ExplicitVisit(StringLiteral node) {
        if (Regex.IsMatch(node.Value, @"(?is)\bTRY_CONVERT\s*\(\s*datetime\s*,\s*(?:@value\b|(?:\w+\.)?\[?(?:AfterValue|BeforeValue|QueueDateValue|InputValue|ValueText)\b)"))
            Findings.Add("line " + node.StartLine + ": direct datetime parsing in dynamic SQL rejects canonical seven-digit timestamps");
        // Dynamic validation SQL is a string in the outer AST. Scan its conversion expression too.
        if (RawValue(node.Value) && Regex.IsMatch(node.Value, @"(?i)\b(?:CONVERT|CAST)\s*\(")) {
            var unsafeDynamic = Regex.IsMatch(node.Value, @"(?is)\bCONVERT\s*\(\s*(?:date(?:time2?|offset)?|smalldatetime|int|bigint|smallint|tinyint|decimal|numeric|float|real|money|smallmoney|bit|uniqueidentifier)\b[^;]{0,180}?(?:@value\b|\b(?:AfterValue|BeforeValue|QueueDateValue|InputValue|ValueText)\b)")
                || Regex.IsMatch(node.Value, @"(?is)\bCAST\s*\([^;]{0,180}?(?:@value\b|\b(?:AfterValue|BeforeValue|QueueDateValue|InputValue|ValueText)\b)[^;]{0,180}?\bAS\s+(?:date|datetime|int|bigint|decimal|numeric|float|money|bit)\b");
            if (unsafeDynamic) Findings.Add("line " + node.StartLine + ": unsafe raw-value conversion in dynamic SQL");
        }
        base.ExplicitVisit(node);
    }
    public static AuditSqlSafetyVisitor Inspect(string sql) {
        IList<ParseError> errors;
        var fragment = new TSql120Parser(true).Parse(new StringReader(sql), out errors);
        var visitor = new AuditSqlSafetyVisitor();
        foreach (var error in errors) visitor.Findings.Add("SQL 2014 parse line " + error.Line + ": " + error.Message);
        if (errors.Count == 0) fragment.Accept(visitor);
        return visitor;
    }
}

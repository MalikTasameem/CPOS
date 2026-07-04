# CPOS.PosApi

طبقة API مستقلة لنقاط البيع والطاولات، تعمل بجانب مشروع WinForms الحالي بدون تعديل شاشاته.

## التشغيل

```powershell
dotnet run --project CPOS.PosApi\CPOS.PosApi.csproj --urls http://localhost:5185
```

ثم افتح:

```text
http://localhost:5185/swagger
```

## إعداد الاتصال

عدّل قيمة `ConnectionStrings:CPOS` في `appsettings.json` حسب سيرفر SQL Server وقاعدة بيانات النظام.

## المرحلة الحالية

- `POST /api/auth/login`
- `GET /api/tables`
- `POST /api/tables/{tableId}/open-bill`
- `GET /api/bills/{transactionId}`
- `POST /api/bills/{transactionId}/items`
- `PATCH /api/bills/items/{detailId}/qty`
- `DELETE /api/bills/items/{detailId}`
- `POST /api/bills/{transactionId}/send-order`

المرحلة التالية ستضيف جلب الأصناف والمجموعات للويب ثم حماية العمليات من التكرار.

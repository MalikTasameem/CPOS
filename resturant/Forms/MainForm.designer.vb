<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub



    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.TimeTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ValidWorker = New System.ComponentModel.BackgroundWorker()
        Me.ValidTimer = New System.Windows.Forms.Timer(Me.components)
        Me.Con_Timer = New System.Windows.Forms.Timer(Me.components)
        Me.Con_Worker = New System.ComponentModel.BackgroundWorker()
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.Sales_Btn = New System.Windows.Forms.ToolStripDropDownButton()
        Me.SB_LB = New System.Windows.Forms.ToolStripMenuItem()
        Me.Prd_LB = New System.Windows.Forms.ToolStripMenuItem()
        Me.عرضفواتيرزبونToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LinkLabel2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.CashLB = New System.Windows.Forms.ToolStripMenuItem()
        Me.SBRtn_LB = New System.Windows.Forms.ToolStripMenuItem()
        Me.طلبياتخارجيةToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.إعدادطلبيةToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.إدارةالطلبياتToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.لوائحالأسعـــارToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.فواتيرمبيعاتمتعارضهمعالإيصالاتToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.Pch_btn = New System.Windows.Forms.ToolStripDropDownButton()
        Me.Pch_LB = New System.Windows.Forms.ToolStripMenuItem()
        Me.Pch_Rtn_LB = New System.Windows.Forms.ToolStripMenuItem()
        Me.Deliver_LB = New System.Windows.Forms.ToolStripMenuItem()
        Me.Barcode_Btn = New System.Windows.Forms.ToolStripMenuItem()
        Me.عرضفواتيرموردToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.الطلبياتالقادمـــةToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ITEMS_btn = New System.Windows.Forms.ToolStripDropDownButton()
        Me.GM_LB = New System.Windows.Forms.ToolStripMenuItem()
        Me.IM_LB = New System.Windows.Forms.ToolStripMenuItem()
        Me.UNITS_LB = New System.Windows.Forms.ToolStripMenuItem()
        Me.NOTES_LB = New System.Windows.Forms.ToolStripMenuItem()
        Me.CHPRICE_LB = New System.Windows.Forms.ToolStripMenuItem()
        Me.تغيربقيمةمعينةToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.تغييربنسبةمئويةToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.شاشةتعديلالأسعارToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.تقريرتعديلالأسعارToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.إدارةالأصنافToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.عروضToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.قائمةصلاخيةالأصنافToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ST_Btn = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ST_Explore_LB = New System.Windows.Forms.ToolStripMenuItem()
        Me.Invoice_LB = New System.Windows.Forms.ToolStripMenuItem()
        Me.IMEX_LB = New System.Windows.Forms.ToolStripMenuItem()
        Me.STTRANC_LB = New System.Windows.Forms.ToolStripMenuItem()
        Me.Prepare_Invoice_LB = New System.Windows.Forms.ToolStripMenuItem()
        Me.أوامرمحزنToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.إذنصرفToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.إذنإستلامToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.IM_Frm_Btn = New System.Windows.Forms.ToolStripDropDownButton()
        Me.FRM_Auto_LB = New System.Windows.Forms.ToolStripMenuItem()
        Me.FRM_M_LB = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5_Frm = New System.Windows.Forms.ToolStripSeparator()
        Me.Exp_Btn = New System.Windows.Forms.ToolStripDropDownButton()
        Me.Exp_LB = New System.Windows.Forms.ToolStripMenuItem()
        Me.Exp_Static_LB = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.Agents_btn = New System.Windows.Forms.ToolStripDropDownButton()
        Me.بطاقةالعملاءToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.Balances_btn = New System.Windows.Forms.ToolStripDropDownButton()
        Me.شاشةالحساباتToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.الخزينــةToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.سحــبToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.إيداعToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.تحويلبينالحساباتToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.Reports_btn = New System.Windows.Forms.ToolStripDropDownButton()
        Me.General_Report_LB = New System.Windows.Forms.ToolStripMenuItem()
        Me.ALL_Report_LB = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.Sys_Setting_btn = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.DB_Name_Tool = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.U_Name_Tool = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel6 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ServeConnect_LB = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripDropDownButton1 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.لوحةمفاتيحToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.فتحدرجالنقوذToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.عرضأخرالملفاتالمرفوعةToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.رفعملــفToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripStatusLabel5 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TxTSelectM = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Bill_Num_LB = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel8 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Date_Tool = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel4 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel7 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Time_Tool = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel10 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel13 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Button23 = New System.Windows.Forms.ToolStripSplitButton()
        Me.ToolStripStatusLabel11 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel9 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.ClockLabel = New System.Windows.Forms.Label()
        Me.DateLabel = New System.Windows.Forms.Label()
        Me.Serv_Label = New System.Windows.Forms.Label()
        Me.CompPictureBox = New System.Windows.Forms.PictureBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TitleBar_Panel = New System.Windows.Forms.Panel()
        Me.Title_Label = New System.Windows.Forms.Label()
        Me.MinBtn = New System.Windows.Forms.Button()
        Me.MaxBtn = New System.Windows.Forms.Button()
        Me.CloseBtn = New System.Windows.Forms.Button()
        Me.Main_Workspace_Panel = New System.Windows.Forms.Panel()
        Me.Panel_Center = New System.Windows.Forms.Panel()
        Me.Panel_QuickButtons = New System.Windows.Forms.Panel()
        Me.Btn_QuickSales = New System.Windows.Forms.Button()
        Me.Btn_QuickPch = New System.Windows.Forms.Button()
        Me.Btn_QuickOffers = New System.Windows.Forms.Button()
        Me.Panel_DashBoard_Top = New System.Windows.Forms.Panel()
        Me.Panel_Dash1 = New System.Windows.Forms.Panel()
        Me.Lb_Value1 = New System.Windows.Forms.Label()
        Me.Lb_Title1 = New System.Windows.Forms.Label()
        Me.Panel_Dash2 = New System.Windows.Forms.Panel()
        Me.Lb_Value2 = New System.Windows.Forms.Label()
        Me.Lb_Title2 = New System.Windows.Forms.Label()
        Me.Panel_Shortcuts_Bottom = New System.Windows.Forms.Panel()
        Me.Table_Bill_Screen_btn = New System.Windows.Forms.Button()
        Me.ActiveLinkLa = New System.Windows.Forms.LinkLabel()
        Me.ExitFormButton = New System.Windows.Forms.Button()
        Me.Panel_Right = New System.Windows.Forms.Panel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ALERT_DGV = New System.Windows.Forms.DataGridView()
        Me.Panel_NotifControls = New System.Windows.Forms.Panel()
        Me.NotifFilter_Cmb = New System.Windows.Forms.ComboBox()
        Me.MuteAlerts_Btn = New System.Windows.Forms.Button()
        Me.Panel_Left = New System.Windows.Forms.Panel()
        Me.Setting_GroupBox = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.KashierPrinterComboBox = New System.Windows.Forms.ComboBox()
        Me.check_print_Btn1 = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.A4Printer_Cmb = New System.Windows.Forms.ComboBox()
        Me.check_print_Btn2 = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Barcode_DefPrinter_Cm = New System.Windows.Forms.ComboBox()
        Me.check_print_Btn3 = New System.Windows.Forms.Button()
        Me.Save_butt = New System.Windows.Forms.Button()
        Me.DataB = New System.Windows.Forms.BindingSource(Me.components)
        Me.ToolStrip.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.CompPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TitleBar_Panel.SuspendLayout()
        Me.Main_Workspace_Panel.SuspendLayout()
        Me.Panel_Center.SuspendLayout()
        Me.Panel_QuickButtons.SuspendLayout()
        Me.Panel_DashBoard_Top.SuspendLayout()
        Me.Panel_Dash1.SuspendLayout()
        Me.Panel_Dash2.SuspendLayout()
        Me.Panel_Shortcuts_Bottom.SuspendLayout()
        Me.Panel_Right.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.ALERT_DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel_NotifControls.SuspendLayout()
        Me.Panel_Left.SuspendLayout()
        Me.Setting_GroupBox.SuspendLayout()
        CType(Me.DataB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TimeTimer
        '
        '
        'ValidWorker
        '
        '
        'ValidTimer
        '
        '
        'Con_Timer
        '
        '
        'Con_Worker
        '
        '
        'ToolStrip
        '
        Me.ToolStrip.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ToolStrip.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Sales_Btn, Me.ToolStripSeparator1, Me.Pch_btn, Me.ToolStripSeparator2, Me.ITEMS_btn, Me.ToolStripSeparator3, Me.ST_Btn, Me.ToolStripSeparator4, Me.IM_Frm_Btn, Me.ToolStripSeparator5_Frm, Me.Exp_Btn, Me.ToolStripSeparator6, Me.Agents_btn, Me.ToolStripSeparator7, Me.Balances_btn, Me.ToolStripSeparator8, Me.Reports_btn, Me.ToolStripSeparator9, Me.Sys_Setting_btn, Me.ToolStripSeparator10})
        Me.ToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.ToolStrip.Location = New System.Drawing.Point(0, 40)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Padding = New System.Windows.Forms.Padding(5, 2, 5, 2)
        Me.ToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ToolStrip.Size = New System.Drawing.Size(1200, 30)
        Me.ToolStrip.TabIndex = 2
        Me.ToolStrip.Tag = "HEADER"
        '
        'Sales_Btn
        '
        Me.Sales_Btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.Sales_Btn.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SB_LB, Me.Prd_LB, Me.عرضفواتيرزبونToolStripMenuItem, Me.LinkLabel2, Me.CashLB, Me.SBRtn_LB, Me.طلبياتخارجيةToolStripMenuItem, Me.لوائحالأسعـــارToolStripMenuItem, Me.فواتيرمبيعاتمتعارضهمعالإيصالاتToolStripMenuItem})
        Me.Sales_Btn.Name = "Sales_Btn"
        Me.Sales_Btn.Size = New System.Drawing.Size(74, 23)
        Me.Sales_Btn.Text = "المبيعات"
        '
        'SB_LB
        '
        Me.SB_LB.Name = "SB_LB"
        Me.SB_LB.Size = New System.Drawing.Size(179, 24)
        Me.SB_LB.Text = "فاتورة المبيعات"
        '
        'Prd_LB
        '
        Me.Prd_LB.Name = "Prd_LB"
        Me.Prd_LB.Size = New System.Drawing.Size(179, 24)
        Me.Prd_LB.Text = "الورديات"
        '
        'عرضفواتيرزبونToolStripMenuItem
        '
        Me.عرضفواتيرزبونToolStripMenuItem.Name = "عرضفواتيرزبونToolStripMenuItem"
        Me.عرضفواتيرزبونToolStripMenuItem.Size = New System.Drawing.Size(179, 24)
        Me.عرضفواتيرزبونToolStripMenuItem.Text = "عرض فواتير زبون"
        '
        'LinkLabel2
        '
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(179, 24)
        Me.LinkLabel2.Text = "فاتورة عرض"
        '
        'CashLB
        '
        Me.CashLB.Name = "CashLB"
        Me.CashLB.Size = New System.Drawing.Size(179, 24)
        Me.CashLB.Text = "إيصال قبض"
        '
        'SBRtn_LB
        '
        Me.SBRtn_LB.Name = "SBRtn_LB"
        Me.SBRtn_LB.Size = New System.Drawing.Size(179, 24)
        Me.SBRtn_LB.Text = "إرجاع مبيعات"
        '
        'طلبياتخارجيةToolStripMenuItem
        '
        Me.طلبياتخارجيةToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.إعدادطلبيةToolStripMenuItem, Me.إدارةالطلبياتToolStripMenuItem})
        Me.طلبياتخارجيةToolStripMenuItem.Name = "طلبياتخارجيةToolStripMenuItem"
        Me.طلبياتخارجيةToolStripMenuItem.Size = New System.Drawing.Size(179, 24)
        Me.طلبياتخارجيةToolStripMenuItem.Text = "طلبيات خارجية"
        '
        'إعدادطلبيةToolStripMenuItem
        '
        Me.إعدادطلبيةToolStripMenuItem.Name = "إعدادطلبيةToolStripMenuItem"
        Me.إعدادطلبيةToolStripMenuItem.Size = New System.Drawing.Size(182, 24)
        Me.إعدادطلبيةToolStripMenuItem.Text = "إعداد رحلة"
        '
        'إدارةالطلبياتToolStripMenuItem
        '
        Me.إدارةالطلبياتToolStripMenuItem.Name = "إدارةالطلبياتToolStripMenuItem"
        Me.إدارةالطلبياتToolStripMenuItem.Size = New System.Drawing.Size(182, 24)
        Me.إدارةالطلبياتToolStripMenuItem.Text = "إدارة فواتير الرحلة"
        '
        'لوائحالأسعـــارToolStripMenuItem
        '
        Me.لوائحالأسعـــارToolStripMenuItem.Name = "لوائحالأسعـــارToolStripMenuItem"
        Me.لوائحالأسعـــارToolStripMenuItem.Size = New System.Drawing.Size(179, 24)
        Me.لوائحالأسعـــارToolStripMenuItem.Text = "لوائح الأسعـــار"
        '
        'فواتيرمبيعاتمتعارضهمعالإيصالاتToolStripMenuItem
        '
        Me.فواتيرمبيعاتمتعارضهمعالإيصالاتToolStripMenuItem.Name = "فواتيرمبيعاتمتعارضهمعالإيصالاتToolStripMenuItem"
        Me.فواتيرمبيعاتمتعارضهمعالإيصالاتToolStripMenuItem.Size = New System.Drawing.Size(179, 24)
        Me.فواتيرمبيعاتمتعارضهمعالإيصالاتToolStripMenuItem.Text = "فواتير متعارضه"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 26)
        '
        'Pch_btn
        '
        Me.Pch_btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.Pch_btn.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Pch_LB, Me.Pch_Rtn_LB, Me.Deliver_LB, Me.Barcode_Btn, Me.عرضفواتيرموردToolStripMenuItem, Me.الطلبياتالقادمـــةToolStripMenuItem})
        Me.Pch_btn.Name = "Pch_btn"
        Me.Pch_btn.Size = New System.Drawing.Size(85, 23)
        Me.Pch_btn.Text = "المشتريات"
        '
        'Pch_LB
        '
        Me.Pch_LB.Name = "Pch_LB"
        Me.Pch_LB.Size = New System.Drawing.Size(179, 24)
        Me.Pch_LB.Text = "فاتورة مشتريات"
        '
        'Pch_Rtn_LB
        '
        Me.Pch_Rtn_LB.Name = "Pch_Rtn_LB"
        Me.Pch_Rtn_LB.Size = New System.Drawing.Size(179, 24)
        Me.Pch_Rtn_LB.Text = "إرجاع مشتريات"
        '
        'Deliver_LB
        '
        Me.Deliver_LB.Name = "Deliver_LB"
        Me.Deliver_LB.Size = New System.Drawing.Size(179, 24)
        Me.Deliver_LB.Text = "إيصال صرف"
        '
        'Barcode_Btn
        '
        Me.Barcode_Btn.Name = "Barcode_Btn"
        Me.Barcode_Btn.Size = New System.Drawing.Size(179, 24)
        Me.Barcode_Btn.Text = "مصنع الباركود"
        '
        'عرضفواتيرموردToolStripMenuItem
        '
        Me.عرضفواتيرموردToolStripMenuItem.Name = "عرضفواتيرموردToolStripMenuItem"
        Me.عرضفواتيرموردToolStripMenuItem.Size = New System.Drawing.Size(179, 24)
        Me.عرضفواتيرموردToolStripMenuItem.Text = "عرض فواتير مورد"
        '
        'الطلبياتالقادمـــةToolStripMenuItem
        '
        Me.الطلبياتالقادمـــةToolStripMenuItem.Name = "الطلبياتالقادمـــةToolStripMenuItem"
        Me.الطلبياتالقادمـــةToolStripMenuItem.Size = New System.Drawing.Size(179, 24)
        Me.الطلبياتالقادمـــةToolStripMenuItem.Text = "الطلبيات القادمة"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 26)
        '
        'ITEMS_btn
        '
        Me.ITEMS_btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ITEMS_btn.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GM_LB, Me.IM_LB, Me.UNITS_LB, Me.NOTES_LB, Me.CHPRICE_LB, Me.إدارةالأصنافToolStripMenuItem, Me.عروضToolStripMenuItem, Me.قائمةصلاخيةالأصنافToolStripMenuItem})
        Me.ITEMS_btn.Name = "ITEMS_btn"
        Me.ITEMS_btn.Size = New System.Drawing.Size(74, 23)
        Me.ITEMS_btn.Text = "المنتجات"
        '
        'GM_LB
        '
        Me.GM_LB.Name = "GM_LB"
        Me.GM_LB.Size = New System.Drawing.Size(165, 24)
        Me.GM_LB.Text = "التصنيفات"
        '
        'IM_LB
        '
        Me.IM_LB.Name = "IM_LB"
        Me.IM_LB.Size = New System.Drawing.Size(165, 24)
        Me.IM_LB.Text = "الآصناف"
        '
        'UNITS_LB
        '
        Me.UNITS_LB.Name = "UNITS_LB"
        Me.UNITS_LB.Size = New System.Drawing.Size(165, 24)
        Me.UNITS_LB.Text = "الوحدات"
        '
        'NOTES_LB
        '
        Me.NOTES_LB.Name = "NOTES_LB"
        Me.NOTES_LB.Size = New System.Drawing.Size(165, 24)
        Me.NOTES_LB.Text = "الملاحظات"
        '
        'CHPRICE_LB
        '
        Me.CHPRICE_LB.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.تغيربقيمةمعينةToolStripMenuItem, Me.تغييربنسبةمئويةToolStripMenuItem, Me.شاشةتعديلالأسعارToolStripMenuItem, Me.تقريرتعديلالأسعارToolStripMenuItem})
        Me.CHPRICE_LB.Name = "CHPRICE_LB"
        Me.CHPRICE_LB.Size = New System.Drawing.Size(165, 24)
        Me.CHPRICE_LB.Text = "تغيير الأسعار"
        '
        'تغيربقيمةمعينةToolStripMenuItem
        '
        Me.تغيربقيمةمعينةToolStripMenuItem.Name = "تغيربقيمةمعينةToolStripMenuItem"
        Me.تغيربقيمةمعينةToolStripMenuItem.Size = New System.Drawing.Size(182, 24)
        Me.تغيربقيمةمعينةToolStripMenuItem.Text = "تغير بقيمة معينة"
        '
        'تغييربنسبةمئويةToolStripMenuItem
        '
        Me.تغييربنسبةمئويةToolStripMenuItem.Name = "تغييربنسبةمئويةToolStripMenuItem"
        Me.تغييربنسبةمئويةToolStripMenuItem.Size = New System.Drawing.Size(182, 24)
        Me.تغييربنسبةمئويةToolStripMenuItem.Text = "تغيير بنسبة مئوية"
        '
        'شاشةتعديلالأسعارToolStripMenuItem
        '
        Me.شاشةتعديلالأسعارToolStripMenuItem.Name = "شاشةتعديلالأسعارToolStripMenuItem"
        Me.شاشةتعديلالأسعارToolStripMenuItem.Size = New System.Drawing.Size(182, 24)
        Me.شاشةتعديلالأسعارToolStripMenuItem.Text = "شاشة التعديل"
        '
        'تقريرتعديلالأسعارToolStripMenuItem
        '
        Me.تقريرتعديلالأسعارToolStripMenuItem.Name = "تقريرتعديلالأسعارToolStripMenuItem"
        Me.تقريرتعديلالأسعارToolStripMenuItem.Size = New System.Drawing.Size(182, 24)
        Me.تقريرتعديلالأسعارToolStripMenuItem.Text = "تقرير التعديل"
        '
        'إدارةالأصنافToolStripMenuItem
        '
        Me.إدارةالأصنافToolStripMenuItem.Name = "إدارةالأصنافToolStripMenuItem"
        Me.إدارةالأصنافToolStripMenuItem.Size = New System.Drawing.Size(165, 24)
        Me.إدارةالأصنافToolStripMenuItem.Text = "إدارة الأصناف"
        '
        'عروضToolStripMenuItem
        '
        Me.عروضToolStripMenuItem.Name = "عروضToolStripMenuItem"
        Me.عروضToolStripMenuItem.Size = New System.Drawing.Size(165, 24)
        Me.عروضToolStripMenuItem.Text = "عروض"
        '
        'قائمةصلاخيةالأصنافToolStripMenuItem
        '
        Me.قائمةصلاخيةالأصنافToolStripMenuItem.Name = "قائمةصلاخيةالأصنافToolStripMenuItem"
        Me.قائمةصلاخيةالأصنافToolStripMenuItem.Size = New System.Drawing.Size(165, 24)
        Me.قائمةصلاخيةالأصنافToolStripMenuItem.Text = "قائمة الصلاحية"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 26)
        '
        'ST_Btn
        '
        Me.ST_Btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ST_Btn.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ST_Explore_LB, Me.Invoice_LB, Me.IMEX_LB, Me.STTRANC_LB, Me.Prepare_Invoice_LB, Me.أوامرمحزنToolStripMenuItem})
        Me.ST_Btn.Name = "ST_Btn"
        Me.ST_Btn.Size = New System.Drawing.Size(66, 23)
        Me.ST_Btn.Text = "المخازن"
        '
        'ST_Explore_LB
        '
        Me.ST_Explore_LB.Name = "ST_Explore_LB"
        Me.ST_Explore_LB.Size = New System.Drawing.Size(184, 24)
        Me.ST_Explore_LB.Text = "كشف المخزن"
        '
        'Invoice_LB
        '
        Me.Invoice_LB.Name = "Invoice_LB"
        Me.Invoice_LB.Size = New System.Drawing.Size(184, 24)
        Me.Invoice_LB.Text = "فاتورة جرد"
        '
        'IMEX_LB
        '
        Me.IMEX_LB.Name = "IMEX_LB"
        Me.IMEX_LB.Size = New System.Drawing.Size(184, 24)
        Me.IMEX_LB.Text = "فاتورة تالف"
        '
        'STTRANC_LB
        '
        Me.STTRANC_LB.Name = "STTRANC_LB"
        Me.STTRANC_LB.Size = New System.Drawing.Size(184, 24)
        Me.STTRANC_LB.Text = "تحويل بين المخازن"
        '
        'Prepare_Invoice_LB
        '
        Me.Prepare_Invoice_LB.Name = "Prepare_Invoice_LB"
        Me.Prepare_Invoice_LB.Size = New System.Drawing.Size(184, 24)
        Me.Prepare_Invoice_LB.Text = "قائمة جرد"
        '
        'أوامرمحزنToolStripMenuItem
        '
        Me.أوامرمحزنToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.إذنصرفToolStripMenuItem, Me.إذنإستلامToolStripMenuItem})
        Me.أوامرمحزنToolStripMenuItem.Name = "أوامرمحزنToolStripMenuItem"
        Me.أوامرمحزنToolStripMenuItem.Size = New System.Drawing.Size(184, 24)
        Me.أوامرمحزنToolStripMenuItem.Text = "أوامر مخزن"
        '
        'إذنصرفToolStripMenuItem
        '
        Me.إذنصرفToolStripMenuItem.Name = "إذنصرفToolStripMenuItem"
        Me.إذنصرفToolStripMenuItem.Size = New System.Drawing.Size(140, 24)
        Me.إذنصرفToolStripMenuItem.Text = "إذن صرف"
        '
        'إذنإستلامToolStripMenuItem
        '
        Me.إذنإستلامToolStripMenuItem.Name = "إذنإستلامToolStripMenuItem"
        Me.إذنإستلامToolStripMenuItem.Size = New System.Drawing.Size(140, 24)
        Me.إذنإستلامToolStripMenuItem.Text = "إذن إستلام"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 26)
        '
        'IM_Frm_Btn
        '
        Me.IM_Frm_Btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.IM_Frm_Btn.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FRM_Auto_LB, Me.FRM_M_LB})
        Me.IM_Frm_Btn.Name = "IM_Frm_Btn"
        Me.IM_Frm_Btn.Size = New System.Drawing.Size(68, 23)
        Me.IM_Frm_Btn.Text = "التصنيع"
        '
        'FRM_Auto_LB
        '
        Me.FRM_Auto_LB.Name = "FRM_Auto_LB"
        Me.FRM_Auto_LB.Size = New System.Drawing.Size(149, 24)
        Me.FRM_Auto_LB.Text = "تصنيع آلي"
        '
        'FRM_M_LB
        '
        Me.FRM_M_LB.Name = "FRM_M_LB"
        Me.FRM_M_LB.Size = New System.Drawing.Size(149, 24)
        Me.FRM_M_LB.Text = "تصنيع يدوي"
        '
        'ToolStripSeparator5_Frm
        '
        Me.ToolStripSeparator5_Frm.Name = "ToolStripSeparator5_Frm"
        Me.ToolStripSeparator5_Frm.Size = New System.Drawing.Size(6, 26)
        '
        'Exp_Btn
        '
        Me.Exp_Btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.Exp_Btn.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Exp_LB, Me.Exp_Static_LB})
        Me.Exp_Btn.Name = "Exp_Btn"
        Me.Exp_Btn.Size = New System.Drawing.Size(88, 23)
        Me.Exp_Btn.Text = "المصروفات"
        '
        'Exp_LB
        '
        Me.Exp_LB.Name = "Exp_LB"
        Me.Exp_LB.Size = New System.Drawing.Size(166, 24)
        Me.Exp_LB.Text = "مصروفات عامة"
        '
        'Exp_Static_LB
        '
        Me.Exp_Static_LB.Name = "Exp_Static_LB"
        Me.Exp_Static_LB.Size = New System.Drawing.Size(166, 24)
        Me.Exp_Static_LB.Text = "مصروفات ثابتة"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 26)
        '
        'Agents_btn
        '
        Me.Agents_btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.Agents_btn.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.بطاقةالعملاءToolStripMenuItem})
        Me.Agents_btn.Name = "Agents_btn"
        Me.Agents_btn.Size = New System.Drawing.Size(62, 23)
        Me.Agents_btn.Text = "العملاء"
        '
        'بطاقةالعملاءToolStripMenuItem
        '
        Me.بطاقةالعملاءToolStripMenuItem.Name = "بطاقةالعملاءToolStripMenuItem"
        Me.بطاقةالعملاءToolStripMenuItem.Size = New System.Drawing.Size(157, 24)
        Me.بطاقةالعملاءToolStripMenuItem.Text = "بطاقة العملاء"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 26)
        '
        'Balances_btn
        '
        Me.Balances_btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.Balances_btn.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.شاشةالحساباتToolStripMenuItem, Me.الخزينــةToolStripMenuItem, Me.تحويلبينالحساباتToolStripMenuItem})
        Me.Balances_btn.Name = "Balances_btn"
        Me.Balances_btn.Size = New System.Drawing.Size(77, 23)
        Me.Balances_btn.Text = "الحسابات"
        '
        'شاشةالحساباتToolStripMenuItem
        '
        Me.شاشةالحساباتToolStripMenuItem.Name = "شاشةالحساباتToolStripMenuItem"
        Me.شاشةالحساباتToolStripMenuItem.Size = New System.Drawing.Size(172, 24)
        Me.شاشةالحساباتToolStripMenuItem.Text = "شاشة الحسابات"
        '
        'الخزينــةToolStripMenuItem
        '
        Me.الخزينــةToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.سحــبToolStripMenuItem, Me.إيداعToolStripMenuItem, Me.ToolStripMenuItem3})
        Me.الخزينــةToolStripMenuItem.Name = "الخزينــةToolStripMenuItem"
        Me.الخزينــةToolStripMenuItem.Size = New System.Drawing.Size(172, 24)
        Me.الخزينــةToolStripMenuItem.Text = "الخزينة"
        '
        'سحــبToolStripMenuItem
        '
        Me.سحــبToolStripMenuItem.Name = "سحــبToolStripMenuItem"
        Me.سحــبToolStripMenuItem.Size = New System.Drawing.Size(113, 24)
        Me.سحــبToolStripMenuItem.Text = "سحب"
        '
        'إيداعToolStripMenuItem
        '
        Me.إيداعToolStripMenuItem.Name = "إيداعToolStripMenuItem"
        Me.إيداعToolStripMenuItem.Size = New System.Drawing.Size(113, 24)
        Me.إيداعToolStripMenuItem.Text = "إيداع"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(113, 24)
        Me.ToolStripMenuItem3.Text = "تحويل"
        '
        'تحويلبينالحساباتToolStripMenuItem
        '
        Me.تحويلبينالحساباتToolStripMenuItem.Name = "تحويلبينالحساباتToolStripMenuItem"
        Me.تحويلبينالحساباتToolStripMenuItem.Size = New System.Drawing.Size(172, 24)
        Me.تحويلبينالحساباتToolStripMenuItem.Text = "تحويل حسابات"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 26)
        '
        'Reports_btn
        '
        Me.Reports_btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.Reports_btn.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.General_Report_LB, Me.ALL_Report_LB})
        Me.Reports_btn.Name = "Reports_btn"
        Me.Reports_btn.Size = New System.Drawing.Size(65, 23)
        Me.Reports_btn.Text = "التقارير"
        '
        'General_Report_LB
        '
        Me.General_Report_LB.Name = "General_Report_LB"
        Me.General_Report_LB.Size = New System.Drawing.Size(164, 24)
        Me.General_Report_LB.Text = "التقرير العام"
        '
        'ALL_Report_LB
        '
        Me.ALL_Report_LB.Name = "ALL_Report_LB"
        Me.ALL_Report_LB.Size = New System.Drawing.Size(164, 24)
        Me.ALL_Report_LB.Text = "تقارير تفصيلية"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(6, 26)
        '
        'Sys_Setting_btn
        '
        Me.Sys_Setting_btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.Sys_Setting_btn.Name = "Sys_Setting_btn"
        Me.Sys_Setting_btn.Size = New System.Drawing.Size(91, 23)
        Me.Sys_Setting_btn.Text = "إدارة النظام"
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(6, 26)
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(6, 6)
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.StatusStrip1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.StatusStrip1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.DB_Name_Tool, Me.ToolStripStatusLabel2, Me.ToolStripStatusLabel3, Me.U_Name_Tool, Me.ToolStripStatusLabel6, Me.ServeConnect_LB, Me.ToolStripDropDownButton1, Me.ToolStripStatusLabel5})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 0)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StatusStrip1.Size = New System.Drawing.Size(1200, 35)
        Me.StatusStrip1.TabIndex = 0
        Me.StatusStrip1.Tag = "HEADER"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(77, 30)
        Me.ToolStripStatusLabel1.Text = "قاعدة البيانات:"
        '
        'DB_Name_Tool
        '
        Me.DB_Name_Tool.ForeColor = System.Drawing.Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.DB_Name_Tool.Name = "DB_Name_Tool"
        Me.DB_Name_Tool.Size = New System.Drawing.Size(17, 30)
        Me.DB_Name_Tool.Text = "--"
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(17, 30)
        Me.ToolStripStatusLabel2.Text = " | "
        '
        'ToolStripStatusLabel3
        '
        Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(92, 30)
        Me.ToolStripStatusLabel3.Text = "المستخدم الحالي:"
        '
        'U_Name_Tool
        '
        Me.U_Name_Tool.ForeColor = System.Drawing.Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.U_Name_Tool.Name = "U_Name_Tool"
        Me.U_Name_Tool.Size = New System.Drawing.Size(17, 30)
        Me.U_Name_Tool.Text = "--"
        '
        'ToolStripStatusLabel6
        '
        Me.ToolStripStatusLabel6.Name = "ToolStripStatusLabel6"
        Me.ToolStripStatusLabel6.Size = New System.Drawing.Size(17, 30)
        Me.ToolStripStatusLabel6.Text = " | "
        '
        'ServeConnect_LB
        '
        Me.ServeConnect_LB.ForeColor = System.Drawing.Color.DarkRed
        Me.ServeConnect_LB.Name = "ServeConnect_LB"
        Me.ServeConnect_LB.Size = New System.Drawing.Size(60, 30)
        Me.ServeConnect_LB.Text = "حالة الخادم"
        '
        'ToolStripDropDownButton1
        '
        Me.ToolStripDropDownButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.لوحةمفاتيحToolStripMenuItem, Me.فتحدرجالنقوذToolStripMenuItem, Me.عرضأخرالملفاتالمرفوعةToolStripMenuItem, Me.رفعملــفToolStripMenuItem})
        Me.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
        Me.ToolStripDropDownButton1.Size = New System.Drawing.Size(43, 33)
        Me.ToolStripDropDownButton1.Text = "أخرى"
        '
        'لوحةمفاتيحToolStripMenuItem
        '
        Me.لوحةمفاتيحToolStripMenuItem.Name = "لوحةمفاتيحToolStripMenuItem"
        Me.لوحةمفاتيحToolStripMenuItem.Size = New System.Drawing.Size(145, 22)
        Me.لوحةمفاتيحToolStripMenuItem.Text = "لوحة مفاتيح"
        '
        'فتحدرجالنقوذToolStripMenuItem
        '
        Me.فتحدرجالنقوذToolStripMenuItem.Name = "فتحدرجالنقوذToolStripMenuItem"
        Me.فتحدرجالنقوذToolStripMenuItem.Size = New System.Drawing.Size(145, 22)
        Me.فتحدرجالنقوذToolStripMenuItem.Text = "فتح درج النقود"
        '
        'عرضأخرالملفاتالمرفوعةToolStripMenuItem
        '
        Me.عرضأخرالملفاتالمرفوعةToolStripMenuItem.Name = "عرضأخرالملفاتالمرفوعةToolStripMenuItem"
        Me.عرضأخرالملفاتالمرفوعةToolStripMenuItem.Size = New System.Drawing.Size(145, 22)
        Me.عرضأخرالملفاتالمرفوعةToolStripMenuItem.Text = "أخر الملفات"
        '
        'رفعملــفToolStripMenuItem
        '
        Me.رفعملــفToolStripMenuItem.Name = "رفعملــفToolStripMenuItem"
        Me.رفعملــفToolStripMenuItem.Size = New System.Drawing.Size(145, 22)
        Me.رفعملــفToolStripMenuItem.Text = "رفع ملف"
        '
        'ToolStripStatusLabel5
        '
        Me.ToolStripStatusLabel5.Name = "ToolStripStatusLabel5"
        Me.ToolStripStatusLabel5.Size = New System.Drawing.Size(62, 30)
        Me.ToolStripStatusLabel5.Text = "حول النظام"
        '
        'TxTSelectM
        '
        Me.TxTSelectM.Name = "TxTSelectM"
        Me.TxTSelectM.Size = New System.Drawing.Size(23, 23)
        '
        'Bill_Num_LB
        '
        Me.Bill_Num_LB.Name = "Bill_Num_LB"
        Me.Bill_Num_LB.Size = New System.Drawing.Size(23, 23)
        '
        'ToolStripStatusLabel8
        '
        Me.ToolStripStatusLabel8.Name = "ToolStripStatusLabel8"
        Me.ToolStripStatusLabel8.Size = New System.Drawing.Size(23, 23)
        '
        'Date_Tool
        '
        Me.Date_Tool.Name = "Date_Tool"
        Me.Date_Tool.Size = New System.Drawing.Size(23, 23)
        '
        'ToolStripStatusLabel4
        '
        Me.ToolStripStatusLabel4.Name = "ToolStripStatusLabel4"
        Me.ToolStripStatusLabel4.Size = New System.Drawing.Size(23, 23)
        '
        'ToolStripStatusLabel7
        '
        Me.ToolStripStatusLabel7.Name = "ToolStripStatusLabel7"
        Me.ToolStripStatusLabel7.Size = New System.Drawing.Size(23, 23)
        '
        'Time_Tool
        '
        Me.Time_Tool.Name = "Time_Tool"
        Me.Time_Tool.Size = New System.Drawing.Size(23, 23)
        '
        'ToolStripStatusLabel10
        '
        Me.ToolStripStatusLabel10.Name = "ToolStripStatusLabel10"
        Me.ToolStripStatusLabel10.Size = New System.Drawing.Size(23, 23)
        '
        'ToolStripStatusLabel13
        '
        Me.ToolStripStatusLabel13.Name = "ToolStripStatusLabel13"
        Me.ToolStripStatusLabel13.Size = New System.Drawing.Size(23, 23)
        '
        'Button23
        '
        Me.Button23.Name = "Button23"
        Me.Button23.Size = New System.Drawing.Size(23, 23)
        '
        'ToolStripStatusLabel11
        '
        Me.ToolStripStatusLabel11.Name = "ToolStripStatusLabel11"
        Me.ToolStripStatusLabel11.Size = New System.Drawing.Size(23, 23)
        '
        'ToolStripStatusLabel9
        '
        Me.ToolStripStatusLabel9.Name = "ToolStripStatusLabel9"
        Me.ToolStripStatusLabel9.Size = New System.Drawing.Size(23, 23)
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.StatusStrip1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 665)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1200, 35)
        Me.Panel1.TabIndex = 4
        '
        'Panel2
        '
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(200, 100)
        Me.Panel2.TabIndex = 5
        Me.Panel2.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(156, Byte), Integer), CType(CType(18, Byte), Integer))
        Me.Panel3.Controls.Add(Me.ClockLabel)
        Me.Panel3.Controls.Add(Me.DateLabel)
        Me.Panel3.Controls.Add(Me.Serv_Label)
        Me.Panel3.Controls.Add(Me.CompPictureBox)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 70)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1200, 103)
        Me.Panel3.TabIndex = 1
        '
        'ClockLabel
        '
        Me.ClockLabel.AutoSize = True
        Me.ClockLabel.Font = New System.Drawing.Font("Segoe UI", 28.0!, System.Drawing.FontStyle.Bold)
        Me.ClockLabel.ForeColor = System.Drawing.Color.White
        Me.ClockLabel.Location = New System.Drawing.Point(20, 10)
        Me.ClockLabel.Name = "ClockLabel"
        Me.ClockLabel.Size = New System.Drawing.Size(174, 51)
        Me.ClockLabel.TabIndex = 0
        Me.ClockLabel.Text = "00:00:00"
        '
        'DateLabel
        '
        Me.DateLabel.AutoSize = True
        Me.DateLabel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.DateLabel.ForeColor = System.Drawing.Color.WhiteSmoke
        Me.DateLabel.Location = New System.Drawing.Point(25, 61)
        Me.DateLabel.Name = "DateLabel"
        Me.DateLabel.Size = New System.Drawing.Size(50, 21)
        Me.DateLabel.TabIndex = 1
        Me.DateLabel.Text = "التاريخ"
        '
        'Serv_Label
        '
        Me.Serv_Label.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(156, Byte), Integer), CType(CType(18, Byte), Integer))
        Me.Serv_Label.Dock = System.Windows.Forms.DockStyle.Right
        Me.Serv_Label.Font = New System.Drawing.Font("Segoe UI", 20.0!, System.Drawing.FontStyle.Bold)
        Me.Serv_Label.ForeColor = System.Drawing.Color.White
        Me.Serv_Label.Location = New System.Drawing.Point(865, 0)
        Me.Serv_Label.Name = "Serv_Label"
        Me.Serv_Label.Size = New System.Drawing.Size(335, 103)
        Me.Serv_Label.TabIndex = 2
        Me.Serv_Label.Tag = ""
        Me.Serv_Label.Text = "إسم الشركة"
        Me.Serv_Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'CompPictureBox
        '
        Me.CompPictureBox.Image = Global.resturant.My.Resources.Resources.CLASS_Splash
        Me.CompPictureBox.Location = New System.Drawing.Point(789, 10)
        Me.CompPictureBox.Name = "CompPictureBox"
        Me.CompPictureBox.Size = New System.Drawing.Size(230, 88)
        Me.CompPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.CompPictureBox.TabIndex = 3
        Me.CompPictureBox.TabStop = False
        '
        'Panel4
        '
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(200, 100)
        Me.Panel4.TabIndex = 6
        Me.Panel4.Visible = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(200, 100)
        Me.TableLayoutPanel1.TabIndex = 7
        Me.TableLayoutPanel1.Visible = False
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(200, 100)
        Me.TableLayoutPanel2.TabIndex = 8
        Me.TableLayoutPanel2.Visible = False
        '
        'TitleBar_Panel
        '
        Me.TitleBar_Panel.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.TitleBar_Panel.Controls.Add(Me.Title_Label)
        Me.TitleBar_Panel.Controls.Add(Me.MinBtn)
        Me.TitleBar_Panel.Controls.Add(Me.MaxBtn)
        Me.TitleBar_Panel.Controls.Add(Me.CloseBtn)
        Me.TitleBar_Panel.Cursor = System.Windows.Forms.Cursors.SizeAll
        Me.TitleBar_Panel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TitleBar_Panel.Location = New System.Drawing.Point(0, 0)
        Me.TitleBar_Panel.Name = "TitleBar_Panel"
        Me.TitleBar_Panel.Size = New System.Drawing.Size(1200, 40)
        Me.TitleBar_Panel.TabIndex = 3
        '
        'Title_Label
        '
        Me.Title_Label.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Title_Label.AutoSize = True
        Me.Title_Label.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Title_Label.ForeColor = System.Drawing.Color.White
        Me.Title_Label.Location = New System.Drawing.Point(914, 9)
        Me.Title_Label.Name = "Title_Label"
        Me.Title_Label.Size = New System.Drawing.Size(274, 21)
        Me.Title_Label.TabIndex = 0
        Me.Title_Label.Text = "نظام المبيعات وإدارة المخزون - الداشبورد"
        '
        'MinBtn
        '
        Me.MinBtn.Dock = System.Windows.Forms.DockStyle.Left
        Me.MinBtn.FlatAppearance.BorderSize = 0
        Me.MinBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.MinBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.MinBtn.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.MinBtn.ForeColor = System.Drawing.Color.White
        Me.MinBtn.Location = New System.Drawing.Point(90, 0)
        Me.MinBtn.Name = "MinBtn"
        Me.MinBtn.Size = New System.Drawing.Size(45, 40)
        Me.MinBtn.TabIndex = 1
        Me.MinBtn.Text = "—"
        '
        'MaxBtn
        '
        Me.MaxBtn.Dock = System.Windows.Forms.DockStyle.Left
        Me.MaxBtn.FlatAppearance.BorderSize = 0
        Me.MaxBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(94, Byte), Integer))
        Me.MaxBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.MaxBtn.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.MaxBtn.ForeColor = System.Drawing.Color.White
        Me.MaxBtn.Location = New System.Drawing.Point(45, 0)
        Me.MaxBtn.Name = "MaxBtn"
        Me.MaxBtn.Size = New System.Drawing.Size(45, 40)
        Me.MaxBtn.TabIndex = 2
        Me.MaxBtn.Text = "☐"
        '
        'CloseBtn
        '
        Me.CloseBtn.Dock = System.Windows.Forms.DockStyle.Left
        Me.CloseBtn.FlatAppearance.BorderSize = 0
        Me.CloseBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.CloseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CloseBtn.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.CloseBtn.ForeColor = System.Drawing.Color.White
        Me.CloseBtn.Location = New System.Drawing.Point(0, 0)
        Me.CloseBtn.Name = "CloseBtn"
        Me.CloseBtn.Size = New System.Drawing.Size(45, 40)
        Me.CloseBtn.TabIndex = 3
        Me.CloseBtn.Text = "X"
        '
        'Main_Workspace_Panel
        '
        Me.Main_Workspace_Panel.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.Main_Workspace_Panel.Controls.Add(Me.Panel_Center)
        Me.Main_Workspace_Panel.Controls.Add(Me.Panel_Right)
        Me.Main_Workspace_Panel.Controls.Add(Me.Panel_Left)
        Me.Main_Workspace_Panel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Main_Workspace_Panel.Location = New System.Drawing.Point(0, 173)
        Me.Main_Workspace_Panel.Name = "Main_Workspace_Panel"
        Me.Main_Workspace_Panel.Padding = New System.Windows.Forms.Padding(15)
        Me.Main_Workspace_Panel.Size = New System.Drawing.Size(1200, 492)
        Me.Main_Workspace_Panel.TabIndex = 0
        '
        'Panel_Center
        '
        Me.Panel_Center.BackColor = System.Drawing.Color.Transparent
        Me.Panel_Center.Controls.Add(Me.Panel_QuickButtons)
        Me.Panel_Center.Controls.Add(Me.Panel_DashBoard_Top)
        Me.Panel_Center.Controls.Add(Me.Panel_Shortcuts_Bottom)
        Me.Panel_Center.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Center.Location = New System.Drawing.Point(365, 15)
        Me.Panel_Center.Name = "Panel_Center"
        Me.Panel_Center.Size = New System.Drawing.Size(460, 462)
        Me.Panel_Center.TabIndex = 0
        '
        'Panel_QuickButtons
        '
        Me.Panel_QuickButtons.BackColor = System.Drawing.Color.White
        Me.Panel_QuickButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_QuickButtons.Controls.Add(Me.Btn_QuickSales)
        Me.Panel_QuickButtons.Controls.Add(Me.Btn_QuickPch)
        Me.Panel_QuickButtons.Controls.Add(Me.Btn_QuickOffers)
        Me.Panel_QuickButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_QuickButtons.Location = New System.Drawing.Point(0, 140)
        Me.Panel_QuickButtons.Name = "Panel_QuickButtons"
        Me.Panel_QuickButtons.Size = New System.Drawing.Size(460, 242)
        Me.Panel_QuickButtons.TabIndex = 0
        '
        'Btn_QuickSales
        '
        Me.Btn_QuickSales.BackColor = System.Drawing.Color.FromArgb(CType(CType(142, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Btn_QuickSales.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_QuickSales.FlatAppearance.BorderSize = 0
        Me.Btn_QuickSales.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_QuickSales.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_QuickSales.ForeColor = System.Drawing.Color.White
        Me.Btn_QuickSales.Location = New System.Drawing.Point(30, 30)
        Me.Btn_QuickSales.Name = "Btn_QuickSales"
        Me.Btn_QuickSales.Size = New System.Drawing.Size(130, 90)
        Me.Btn_QuickSales.TabIndex = 0
        Me.Btn_QuickSales.Text = "فاتورة مبيعات"
        Me.Btn_QuickSales.UseVisualStyleBackColor = False
        '
        'Btn_QuickPch
        '
        Me.Btn_QuickPch.BackColor = System.Drawing.Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.Btn_QuickPch.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_QuickPch.FlatAppearance.BorderSize = 0
        Me.Btn_QuickPch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_QuickPch.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_QuickPch.ForeColor = System.Drawing.Color.White
        Me.Btn_QuickPch.Location = New System.Drawing.Point(170, 30)
        Me.Btn_QuickPch.Name = "Btn_QuickPch"
        Me.Btn_QuickPch.Size = New System.Drawing.Size(130, 90)
        Me.Btn_QuickPch.TabIndex = 1
        Me.Btn_QuickPch.Text = "فاتورة مشتريات"
        Me.Btn_QuickPch.UseVisualStyleBackColor = False
        '
        'Btn_QuickOffers
        '
        Me.Btn_QuickOffers.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(34, Byte), Integer))
        Me.Btn_QuickOffers.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_QuickOffers.FlatAppearance.BorderSize = 0
        Me.Btn_QuickOffers.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_QuickOffers.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Btn_QuickOffers.ForeColor = System.Drawing.Color.White
        Me.Btn_QuickOffers.Location = New System.Drawing.Point(310, 30)
        Me.Btn_QuickOffers.Name = "Btn_QuickOffers"
        Me.Btn_QuickOffers.Size = New System.Drawing.Size(130, 90)
        Me.Btn_QuickOffers.TabIndex = 2
        Me.Btn_QuickOffers.Text = "عروض أسعار"
        Me.Btn_QuickOffers.UseVisualStyleBackColor = False
        '
        'Panel_DashBoard_Top
        '
        Me.Panel_DashBoard_Top.Controls.Add(Me.Panel_Dash1)
        Me.Panel_DashBoard_Top.Controls.Add(Me.Panel_Dash2)
        Me.Panel_DashBoard_Top.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel_DashBoard_Top.Location = New System.Drawing.Point(0, 0)
        Me.Panel_DashBoard_Top.Name = "Panel_DashBoard_Top"
        Me.Panel_DashBoard_Top.Padding = New System.Windows.Forms.Padding(20)
        Me.Panel_DashBoard_Top.Size = New System.Drawing.Size(460, 140)
        Me.Panel_DashBoard_Top.TabIndex = 1
        '
        'Panel_Dash1
        '
        Me.Panel_Dash1.BackColor = System.Drawing.Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.Panel_Dash1.Controls.Add(Me.Lb_Value1)
        Me.Panel_Dash1.Controls.Add(Me.Lb_Title1)
        Me.Panel_Dash1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel_Dash1.Location = New System.Drawing.Point(240, 20)
        Me.Panel_Dash1.Margin = New System.Windows.Forms.Padding(10)
        Me.Panel_Dash1.Name = "Panel_Dash1"
        Me.Panel_Dash1.Size = New System.Drawing.Size(200, 100)
        Me.Panel_Dash1.TabIndex = 0
        '
        'Lb_Value1
        '
        Me.Lb_Value1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Lb_Value1.Font = New System.Drawing.Font("Segoe UI", 20.0!, System.Drawing.FontStyle.Bold)
        Me.Lb_Value1.ForeColor = System.Drawing.Color.White
        Me.Lb_Value1.Location = New System.Drawing.Point(0, 35)
        Me.Lb_Value1.Name = "Lb_Value1"
        Me.Lb_Value1.Size = New System.Drawing.Size(200, 65)
        Me.Lb_Value1.TabIndex = 0
        Me.Lb_Value1.Text = "0.00"
        Me.Lb_Value1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Lb_Title1
        '
        Me.Lb_Title1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Lb_Title1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Lb_Title1.ForeColor = System.Drawing.Color.White
        Me.Lb_Title1.Location = New System.Drawing.Point(0, 0)
        Me.Lb_Title1.Name = "Lb_Title1"
        Me.Lb_Title1.Size = New System.Drawing.Size(200, 35)
        Me.Lb_Title1.TabIndex = 1
        Me.Lb_Title1.Text = "إجمالي المبيعات"
        Me.Lb_Title1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel_Dash2
        '
        Me.Panel_Dash2.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(96, Byte), Integer))
        Me.Panel_Dash2.Controls.Add(Me.Lb_Value2)
        Me.Panel_Dash2.Controls.Add(Me.Lb_Title2)
        Me.Panel_Dash2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel_Dash2.Location = New System.Drawing.Point(20, 20)
        Me.Panel_Dash2.Margin = New System.Windows.Forms.Padding(10)
        Me.Panel_Dash2.Name = "Panel_Dash2"
        Me.Panel_Dash2.Size = New System.Drawing.Size(200, 100)
        Me.Panel_Dash2.TabIndex = 1
        '
        'Lb_Value2
        '
        Me.Lb_Value2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Lb_Value2.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Bold)
        Me.Lb_Value2.ForeColor = System.Drawing.Color.White
        Me.Lb_Value2.Location = New System.Drawing.Point(0, 35)
        Me.Lb_Value2.Name = "Lb_Value2"
        Me.Lb_Value2.Size = New System.Drawing.Size(200, 65)
        Me.Lb_Value2.TabIndex = 0
        Me.Lb_Value2.Text = "0"
        Me.Lb_Value2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Lb_Title2
        '
        Me.Lb_Title2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Lb_Title2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Lb_Title2.ForeColor = System.Drawing.Color.White
        Me.Lb_Title2.Location = New System.Drawing.Point(0, 0)
        Me.Lb_Title2.Name = "Lb_Title2"
        Me.Lb_Title2.Size = New System.Drawing.Size(200, 35)
        Me.Lb_Title2.TabIndex = 1
        Me.Lb_Title2.Text = "عدد الفواتير"
        Me.Lb_Title2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel_Shortcuts_Bottom
        '
        Me.Panel_Shortcuts_Bottom.BackColor = System.Drawing.Color.Transparent
        Me.Panel_Shortcuts_Bottom.Controls.Add(Me.Table_Bill_Screen_btn)
        Me.Panel_Shortcuts_Bottom.Controls.Add(Me.ActiveLinkLa)
        Me.Panel_Shortcuts_Bottom.Controls.Add(Me.ExitFormButton)
        Me.Panel_Shortcuts_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel_Shortcuts_Bottom.Location = New System.Drawing.Point(0, 382)
        Me.Panel_Shortcuts_Bottom.Name = "Panel_Shortcuts_Bottom"
        Me.Panel_Shortcuts_Bottom.Size = New System.Drawing.Size(460, 80)
        Me.Panel_Shortcuts_Bottom.TabIndex = 2
        '
        'Table_Bill_Screen_btn
        '
        Me.Table_Bill_Screen_btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.Table_Bill_Screen_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Table_Bill_Screen_btn.FlatAppearance.BorderSize = 0
        Me.Table_Bill_Screen_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Table_Bill_Screen_btn.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Table_Bill_Screen_btn.ForeColor = System.Drawing.Color.Black
        Me.Table_Bill_Screen_btn.Location = New System.Drawing.Point(20, 20)
        Me.Table_Bill_Screen_btn.Name = "Table_Bill_Screen_btn"
        Me.Table_Bill_Screen_btn.Size = New System.Drawing.Size(120, 45)
        Me.Table_Bill_Screen_btn.TabIndex = 0
        Me.Table_Bill_Screen_btn.Text = "طلبيات داخلية"
        Me.Table_Bill_Screen_btn.UseVisualStyleBackColor = False
        '
        'ActiveLinkLa
        '
        Me.ActiveLinkLa.BackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.ActiveLinkLa.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.ActiveLinkLa.ForeColor = System.Drawing.Color.White
        Me.ActiveLinkLa.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.ActiveLinkLa.LinkColor = System.Drawing.Color.White
        Me.ActiveLinkLa.Location = New System.Drawing.Point(150, 20)
        Me.ActiveLinkLa.Name = "ActiveLinkLa"
        Me.ActiveLinkLa.Size = New System.Drawing.Size(120, 45)
        Me.ActiveLinkLa.TabIndex = 1
        Me.ActiveLinkLa.TabStop = True
        Me.ActiveLinkLa.Tag = "DELETE"
        Me.ActiveLinkLa.Text = "منتج غير مفعل"
        Me.ActiveLinkLa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ExitFormButton
        '
        Me.ExitFormButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.ExitFormButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ExitFormButton.FlatAppearance.BorderSize = 0
        Me.ExitFormButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ExitFormButton.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.ExitFormButton.ForeColor = System.Drawing.Color.White
        Me.ExitFormButton.Location = New System.Drawing.Point(280, 20)
        Me.ExitFormButton.Name = "ExitFormButton"
        Me.ExitFormButton.Size = New System.Drawing.Size(120, 45)
        Me.ExitFormButton.TabIndex = 2
        Me.ExitFormButton.Tag = "APP_CONTROL"
        Me.ExitFormButton.Text = "تسجيل الخروج"
        Me.ExitFormButton.UseVisualStyleBackColor = False
        '
        'Panel_Right
        '
        Me.Panel_Right.Controls.Add(Me.GroupBox2)
        Me.Panel_Right.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel_Right.Location = New System.Drawing.Point(825, 15)
        Me.Panel_Right.Name = "Panel_Right"
        Me.Panel_Right.Padding = New System.Windows.Forms.Padding(0, 0, 10, 0)
        Me.Panel_Right.Size = New System.Drawing.Size(360, 462)
        Me.Panel_Right.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.White
        Me.GroupBox2.Controls.Add(Me.ALERT_DGV)
        Me.GroupBox2.Controls.Add(Me.Panel_NotifControls)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.GroupBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(156, Byte), Integer), CType(CType(18, Byte), Integer))
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(350, 462)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "التحذيرات والتذكير"
        '
        'ALERT_DGV
        '
        Me.ALERT_DGV.AllowUserToAddRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.ALERT_DGV.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.ALERT_DGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.ALERT_DGV.BackgroundColor = System.Drawing.Color.White
        Me.ALERT_DGV.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ALERT_DGV.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.ALERT_DGV.ColumnHeadersVisible = False
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(156, Byte), Integer), CType(CType(18, Byte), Integer))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ALERT_DGV.DefaultCellStyle = DataGridViewCellStyle2
        Me.ALERT_DGV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ALERT_DGV.GridColor = System.Drawing.Color.Silver
        Me.ALERT_DGV.Location = New System.Drawing.Point(3, 70)
        Me.ALERT_DGV.Name = "ALERT_DGV"
        Me.ALERT_DGV.RowHeadersVisible = False
        Me.ALERT_DGV.RowTemplate.Height = 40
        Me.ALERT_DGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.ALERT_DGV.Size = New System.Drawing.Size(344, 389)
        Me.ALERT_DGV.TabIndex = 0
        '
        'Panel_NotifControls
        '
        Me.Panel_NotifControls.BackColor = System.Drawing.Color.Transparent
        Me.Panel_NotifControls.Controls.Add(Me.NotifFilter_Cmb)
        Me.Panel_NotifControls.Controls.Add(Me.MuteAlerts_Btn)
        Me.Panel_NotifControls.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel_NotifControls.Location = New System.Drawing.Point(3, 25)
        Me.Panel_NotifControls.Name = "Panel_NotifControls"
        Me.Panel_NotifControls.Size = New System.Drawing.Size(344, 45)
        Me.Panel_NotifControls.TabIndex = 1
        '
        'NotifFilter_Cmb
        '
        Me.NotifFilter_Cmb.Dock = System.Windows.Forms.DockStyle.Right
        Me.NotifFilter_Cmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.NotifFilter_Cmb.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.NotifFilter_Cmb.Items.AddRange(New Object() {"كل الإشعارات", "نواقص مخزن", "صلاحية أصناف", "ديون عملاء"})
        Me.NotifFilter_Cmb.Location = New System.Drawing.Point(86, 0)
        Me.NotifFilter_Cmb.Margin = New System.Windows.Forms.Padding(5)
        Me.NotifFilter_Cmb.Name = "NotifFilter_Cmb"
        Me.NotifFilter_Cmb.Size = New System.Drawing.Size(258, 25)
        Me.NotifFilter_Cmb.TabIndex = 0
        '
        'MuteAlerts_Btn
        '
        Me.MuteAlerts_Btn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.MuteAlerts_Btn.Dock = System.Windows.Forms.DockStyle.Left
        Me.MuteAlerts_Btn.FlatAppearance.BorderSize = 0
        Me.MuteAlerts_Btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.MuteAlerts_Btn.ForeColor = System.Drawing.Color.Black
        Me.MuteAlerts_Btn.Location = New System.Drawing.Point(0, 0)
        Me.MuteAlerts_Btn.Name = "MuteAlerts_Btn"
        Me.MuteAlerts_Btn.Size = New System.Drawing.Size(69, 45)
        Me.MuteAlerts_Btn.TabIndex = 1
        Me.MuteAlerts_Btn.Text = "🔕 كتم"
        Me.MuteAlerts_Btn.UseVisualStyleBackColor = False
        '
        'Panel_Left
        '
        Me.Panel_Left.Controls.Add(Me.Setting_GroupBox)
        Me.Panel_Left.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel_Left.Location = New System.Drawing.Point(15, 15)
        Me.Panel_Left.Name = "Panel_Left"
        Me.Panel_Left.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.Panel_Left.Size = New System.Drawing.Size(350, 462)
        Me.Panel_Left.TabIndex = 2
        '
        'Setting_GroupBox
        '
        Me.Setting_GroupBox.BackColor = System.Drawing.Color.White
        Me.Setting_GroupBox.Controls.Add(Me.Button1)
        Me.Setting_GroupBox.Controls.Add(Me.Label5)
        Me.Setting_GroupBox.Controls.Add(Me.KashierPrinterComboBox)
        Me.Setting_GroupBox.Controls.Add(Me.check_print_Btn1)
        Me.Setting_GroupBox.Controls.Add(Me.Label11)
        Me.Setting_GroupBox.Controls.Add(Me.A4Printer_Cmb)
        Me.Setting_GroupBox.Controls.Add(Me.check_print_Btn2)
        Me.Setting_GroupBox.Controls.Add(Me.Label14)
        Me.Setting_GroupBox.Controls.Add(Me.Barcode_DefPrinter_Cm)
        Me.Setting_GroupBox.Controls.Add(Me.check_print_Btn3)
        Me.Setting_GroupBox.Controls.Add(Me.Save_butt)
        Me.Setting_GroupBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Setting_GroupBox.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Setting_GroupBox.ForeColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(96, Byte), Integer))
        Me.Setting_GroupBox.Location = New System.Drawing.Point(10, 0)
        Me.Setting_GroupBox.Name = "Setting_GroupBox"
        Me.Setting_GroupBox.Size = New System.Drawing.Size(340, 462)
        Me.Setting_GroupBox.TabIndex = 0
        Me.Setting_GroupBox.TabStop = False
        Me.Setting_GroupBox.Text = "إعدادات الطابعات"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(96, Byte), Integer))
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(20, 321)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(305, 45)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = "حفظ الإعدادات"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(200, 50)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(119, 21)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "طابعة الإيصالات:"
        '
        'KashierPrinterComboBox
        '
        Me.KashierPrinterComboBox.Location = New System.Drawing.Point(7, 75)
        Me.KashierPrinterComboBox.Name = "KashierPrinterComboBox"
        Me.KashierPrinterComboBox.Size = New System.Drawing.Size(270, 29)
        Me.KashierPrinterComboBox.TabIndex = 1
        '
        'check_print_Btn1
        '
        Me.check_print_Btn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.check_print_Btn1.ForeColor = System.Drawing.Color.Black
        Me.check_print_Btn1.Location = New System.Drawing.Point(290, 75)
        Me.check_print_Btn1.Name = "check_print_Btn1"
        Me.check_print_Btn1.Size = New System.Drawing.Size(35, 28)
        Me.check_print_Btn1.TabIndex = 2
        Me.check_print_Btn1.Text = "✓"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(200, 120)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(104, 21)
        Me.Label11.TabIndex = 3
        Me.Label11.Text = "طابعة A4/A5:"
        '
        'A4Printer_Cmb
        '
        Me.A4Printer_Cmb.Location = New System.Drawing.Point(7, 145)
        Me.A4Printer_Cmb.Name = "A4Printer_Cmb"
        Me.A4Printer_Cmb.Size = New System.Drawing.Size(270, 29)
        Me.A4Printer_Cmb.TabIndex = 4
        '
        'check_print_Btn2
        '
        Me.check_print_Btn2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.check_print_Btn2.ForeColor = System.Drawing.Color.Black
        Me.check_print_Btn2.Location = New System.Drawing.Point(290, 145)
        Me.check_print_Btn2.Name = "check_print_Btn2"
        Me.check_print_Btn2.Size = New System.Drawing.Size(35, 28)
        Me.check_print_Btn2.TabIndex = 5
        Me.check_print_Btn2.Text = "✓"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(200, 190)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(105, 21)
        Me.Label14.TabIndex = 6
        Me.Label14.Text = "طابعة الباركود:"
        '
        'Barcode_DefPrinter_Cm
        '
        Me.Barcode_DefPrinter_Cm.Location = New System.Drawing.Point(7, 215)
        Me.Barcode_DefPrinter_Cm.Name = "Barcode_DefPrinter_Cm"
        Me.Barcode_DefPrinter_Cm.Size = New System.Drawing.Size(270, 29)
        Me.Barcode_DefPrinter_Cm.TabIndex = 7
        '
        'check_print_Btn3
        '
        Me.check_print_Btn3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.check_print_Btn3.ForeColor = System.Drawing.Color.Black
        Me.check_print_Btn3.Location = New System.Drawing.Point(290, 215)
        Me.check_print_Btn3.Name = "check_print_Btn3"
        Me.check_print_Btn3.Size = New System.Drawing.Size(35, 28)
        Me.check_print_Btn3.TabIndex = 8
        Me.check_print_Btn3.Text = "✓"
        '
        'Save_butt
        '
        Me.Save_butt.BackColor = System.Drawing.Color.FromArgb(CType(CType(39, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(96, Byte), Integer))
        Me.Save_butt.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Save_butt.FlatAppearance.BorderSize = 0
        Me.Save_butt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Save_butt.ForeColor = System.Drawing.Color.White
        Me.Save_butt.Location = New System.Drawing.Point(20, 270)
        Me.Save_butt.Name = "Save_butt"
        Me.Save_butt.Size = New System.Drawing.Size(305, 45)
        Me.Save_butt.TabIndex = 9
        Me.Save_butt.Text = "حفظ الإعدادات"
        Me.Save_butt.UseVisualStyleBackColor = False
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1200, 700)
        Me.Controls.Add(Me.Main_Workspace_Panel)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.ToolStrip)
        Me.Controls.Add(Me.TitleBar_Panel)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(1000, 600)
        Me.Name = "MainForm"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "الرئيسية - الداشبورد"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.CompPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TitleBar_Panel.ResumeLayout(False)
        Me.TitleBar_Panel.PerformLayout()
        Me.Main_Workspace_Panel.ResumeLayout(False)
        Me.Panel_Center.ResumeLayout(False)
        Me.Panel_QuickButtons.ResumeLayout(False)
        Me.Panel_DashBoard_Top.ResumeLayout(False)
        Me.Panel_Dash1.ResumeLayout(False)
        Me.Panel_Dash2.ResumeLayout(False)
        Me.Panel_Shortcuts_Bottom.ResumeLayout(False)
        Me.Panel_Right.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.ALERT_DGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel_NotifControls.ResumeLayout(False)
        Me.Panel_Left.ResumeLayout(False)
        Me.Setting_GroupBox.ResumeLayout(False)
        Me.Setting_GroupBox.PerformLayout()
        CType(Me.DataB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    ' تعريفات المتغيرات (لا يوجد نقص أو زيادة نهائياً)
    Friend WithEvents TimeTimer As System.Windows.Forms.Timer
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents DB_Name_Tool As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents U_Name_Tool As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel6 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TxTSelectM As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel8 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Date_Tool As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel4 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel7 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Time_Tool As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel10 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripDropDownButton1 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents لوحةمفاتيحToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents فتحدرجالنقوذToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents عرضأخرالملفاتالمرفوعةToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents رفعملــفToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripStatusLabel13 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ServeConnect_LB As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Button23 As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents ToolStripStatusLabel11 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Bill_Num_LB As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel9 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel5 As System.Windows.Forms.ToolStripStatusLabel

    Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator5_Frm As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator11 As System.Windows.Forms.ToolStripSeparator

    Friend WithEvents Sales_Btn As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents Pch_btn As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ITEMS_btn As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ST_Btn As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents IM_Frm_Btn As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents Exp_Btn As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents Agents_btn As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents Balances_btn As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents Reports_btn As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents Sys_Setting_btn As System.Windows.Forms.ToolStripDropDownButton

    Friend WithEvents SB_LB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Prd_LB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents عرضفواتيرزبونToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LinkLabel2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CashLB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SBRtn_LB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents طلبياتخارجيةToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents إعدادطلبيةToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents إدارةالطلبياتToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents لوائحالأسعـــارToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents فواتيرمبيعاتمتعارضهمعالإيصالاتToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Pch_LB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Pch_Rtn_LB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Deliver_LB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Barcode_Btn As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents عرضفواتيرموردToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents الطلبياتالقادمـــةToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GM_LB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IM_LB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UNITS_LB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NOTES_LB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CHPRICE_LB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents تغيربقيمةمعينةToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents تغييربنسبةمئويةToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents شاشةتعديلالأسعارToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents تقريرتعديلالأسعارToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents إدارةالأصنافToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents عروضToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents قائمةصلاخيةالأصنافToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ST_Explore_LB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Invoice_LB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IMEX_LB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents STTRANC_LB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Prepare_Invoice_LB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents أوامرمحزنToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents إذنصرفToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents إذنإستلامToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FRM_Auto_LB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FRM_M_LB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Exp_LB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Exp_Static_LB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents بطاقةالعملاءToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents شاشةالحساباتToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents الخزينــةToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents سحــبToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents إيداعToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents تحويلبينالحساباتToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents General_Report_LB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ALL_Report_LB As System.Windows.Forms.ToolStripMenuItem

    ' المتغيرات المضافة للمظهر الجديد والتنسيقات
    Friend WithEvents TitleBar_Panel As System.Windows.Forms.Panel
    Friend WithEvents CloseBtn As System.Windows.Forms.Button
    Friend WithEvents MaxBtn As System.Windows.Forms.Button
    Friend WithEvents MinBtn As System.Windows.Forms.Button
    Friend WithEvents Title_Label As System.Windows.Forms.Label

    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Main_Workspace_Panel As System.Windows.Forms.Panel
    Friend WithEvents Panel_Right As System.Windows.Forms.Panel
    Friend WithEvents Panel_Left As System.Windows.Forms.Panel
    Friend WithEvents Panel_Center As System.Windows.Forms.Panel
    Friend WithEvents Panel_DashBoard_Top As System.Windows.Forms.Panel
    Friend WithEvents Panel_Shortcuts_Bottom As System.Windows.Forms.Panel
    Friend WithEvents Panel_QuickButtons As System.Windows.Forms.Panel

    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents ALERT_DGV As System.Windows.Forms.DataGridView
    Friend WithEvents Panel_NotifControls As System.Windows.Forms.Panel
    Friend WithEvents NotifFilter_Cmb As System.Windows.Forms.ComboBox
    Friend WithEvents MuteAlerts_Btn As System.Windows.Forms.Button

    Friend WithEvents Setting_GroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents check_print_Btn1 As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents A4Printer_Cmb As System.Windows.Forms.ComboBox
    Friend WithEvents check_print_Btn2 As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Barcode_DefPrinter_Cm As System.Windows.Forms.ComboBox
    Friend WithEvents check_print_Btn3 As System.Windows.Forms.Button
    Friend WithEvents Save_butt As System.Windows.Forms.Button

    Friend WithEvents CompPictureBox As System.Windows.Forms.PictureBox
    Friend WithEvents Serv_Label As System.Windows.Forms.Label
    Friend WithEvents ClockLabel As System.Windows.Forms.Label
    Friend WithEvents DateLabel As System.Windows.Forms.Label

    Friend WithEvents Panel_Dash1 As System.Windows.Forms.Panel
    Friend WithEvents Lb_Title1 As System.Windows.Forms.Label
    Friend WithEvents Lb_Value1 As System.Windows.Forms.Label
    Friend WithEvents Panel_Dash2 As System.Windows.Forms.Panel
    Friend WithEvents Lb_Title2 As System.Windows.Forms.Label
    Friend WithEvents Lb_Value2 As System.Windows.Forms.Label

    Friend WithEvents Btn_QuickSales As System.Windows.Forms.Button
    Friend WithEvents Btn_QuickPch As System.Windows.Forms.Button
    Friend WithEvents Btn_QuickOffers As System.Windows.Forms.Button

    Friend WithEvents Table_Bill_Screen_btn As System.Windows.Forms.Button
    Friend WithEvents ActiveLinkLa As System.Windows.Forms.LinkLabel
    Friend WithEvents ExitFormButton As System.Windows.Forms.Button

    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Public WithEvents ValidWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents ValidTimer As System.Windows.Forms.Timer
    Friend WithEvents Con_Timer As System.Windows.Forms.Timer
    Public WithEvents Con_Worker As System.ComponentModel.BackgroundWorker
    Friend WithEvents DataB As System.Windows.Forms.BindingSource

    ' حاويات قديمة مخفية حتى لا ينكسر أي كود يطلبها
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Button1 As Button
    Friend WithEvents KashierPrinterComboBox As System.Windows.Forms.ComboBox
End Class
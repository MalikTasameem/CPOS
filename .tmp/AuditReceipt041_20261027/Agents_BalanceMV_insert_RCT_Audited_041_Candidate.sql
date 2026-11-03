-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Agents_BalanceMV_insert_RCT_Audited]
@T_ID int OUTPUT,
@Date DATETIME, 
@Receipt_Num int OUTPUT,
@AG_ID	int,
@Pr_ID int = NULL,
@Receipt_Tran_ID INT = NULL,
@ReferNum nvarchar(100) = NULL,
@Receipt_Title	nvarchar(MAX) = NULL,
@Pure	numeric(18, 3)= NULL,
@About	nvarchar(MAX) = NULL,
@BsType_ID	int,
@User_ID int,
@Tr_ID	int = NULL,
@Bank_Name nvarchar(100) = NULL,
@CheckNum nvarchar(100) = NULL,
@Pay_ID	int	=1,
@Discount numeric(18, 3)= NULL

AS
-- ReceiptInsert041: isolated ordinary receipt currency target; legacy custody procedure is unchanged.

declare @AGBalance_T_ID int , @Cr_Equal_Value FLOAT,@Debit	numeric(18, 3) = NULL,@Credit	numeric(18, 3) = NULL ,@T_ID_2 INT,@Tr_Type_ID	int 

BEGIN TRY
BEGIN TRANSACTION

--begin


if @Pr_ID = 0 BEGIN SET @Pr_ID = NULL  END 

--IF @Receipt_Num = 0 BEGIN 
EXEC @Receipt_Num = AG_BalanceMV_GetMaxRecepitNum

IF @BsType_ID = 3 BEGIN SELECT @Debit  = @PURE , @Tr_Type_ID = 2  END
ELSE IF @BsType_ID = 4 BEGIN SELECT @Credit = @PURE , @Tr_Type_ID = 1 END

   EXEC @T_ID = AA_GET_MAX_ID 'Agents_Balance_MV_RCT'

insert into Agents_Balance_MV_RCT (T_ID,Receipt_Num,Receipt_Tran_ID,AG_ID,Pr_ID,Date,Receipt_Title,About,BsType_ID,isPied,User_ID,Pure,Pay_ID,Tr_ID,Discount)
	                        values(@T_ID,@Receipt_Num,@Receipt_Tran_ID,@AG_ID,@Pr_ID,@Date,@Receipt_Title,@About,@BsType_ID,1,@User_ID,@Pure,@Pay_ID,@Tr_ID,@Discount)
	 SELECT @AGBalance_T_ID = @T_ID

---------------------------------------------------------------------
	--if  @BsType_ID = 3 BEGIN UPDATE Agents_Balance_MV SET isPied = 1 WHERE T_ID = @Receipt_Tran_ID END

	EXEC @T_ID_2 = [AA_GET_MAX_Treasury_Balance_MV]
	Insert Into Treasury_Balance_MV 
	(T_ID,AGBalance_T_ID,Tran_ID,date,Tr_ID,notice_move,Bank_Name,CheckNum,Pure,Credit,Debit,Tr_Type_ID,isTran_From,isTran_To,User_ID,isVoid)
	Values
	 (@T_ID_2,@AGBalance_T_ID,NULL,@Date,@Tr_ID,@Receipt_Title,@Bank_Name,@CheckNum,@Pure,@Debit,@Credit,@Tr_Type_ID,NULL,NULL,@User_ID,0)


IF @AG_ID > 1 AND ( SELECT Type_ID FROM Agents WHERE AG_ID = @AG_ID ) <> 2
BEGIN
SELECT @Cr_Equal_Value = Cr_Equal_Value FROM Agents_Balance_MV_RCT WHERE T_ID = @T_ID
IF @BsType_ID = 4 BEGIN Update Agents_Balance_MV_RCT SET Debit_2 = (@Debit / @Cr_Equal_Value) WHERE T_ID = @T_ID END
IF @BsType_ID = 3 BEGIN Update Agents_Balance_MV_RCT SET Credit_2 = (@Credit / @Cr_Equal_Value) WHERE T_ID = @T_ID END

END


--if @Receipt_Tran_ID <> 0 and (SELECT BsType_ID FROM Agents_Balance_MV WHERE T_ID = @Receipt_Tran_ID) = 1
--BEGIN
--EXEC [AG_Balance_Update_Pied] @Receipt_Tran_ID
--END


	
	COMMIT
END TRY

BEGIN CATCH
IF @@TRANCOUNT > 0
	print 'DB ERROR ! __ [Agents_BalanceMV_insert_RCT]'
ROLLBACK
END CATCH

--end













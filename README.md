### 程式結構

1. 設定JwtToken資訊，將登入者身分(Role)加入至Claim
2. 利用IAuthorizationFilter設定呼叫API前先驗證
3. 利用Attribute將驗證功能寫成Filter
4. 利用Enum特性 設定UserType優先級
5. 呼叫API時判斷使用者身分權限是否大於該API設定
( >= 呼叫成功， < 呼叫失敗)

### 使用方法

1. 登入時輸入當前使用者身分 (Admin,User,None)
2. 取得JwtToken後設定JwtToken
3. 呼叫GetJwtToken將JwtToken帶進去，若身分驗證失敗跳出401，成功則跳出JwtToken內容

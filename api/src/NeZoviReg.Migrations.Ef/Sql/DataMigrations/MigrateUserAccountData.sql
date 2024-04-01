INSERT INTO `nezovireg`.`useraccount`
(
    `GuidId`,
    `RegUserId`,
    `Username`,
    `Password`,
    `AccessTokenExpirationTime`,
    `RefreshToken`,
    `RefreshTokenExpirationTime`,
    `ForgotPasswordToken`,
    `ForgotPasswordTokenExpirationTime`,
    `CreatedBy`,
    `ModifiedBy`,
    `CreatedOn`,
    `ModifiedOn`)
SELECT
    UUID(),
    r.Id,
    r.Username,
    r.Password,
    r.AccessTokenExpirationTime,
    r.RefreshToken,
    r.RefreshTokenExpirationTime,
    r.ForgotPasswordToken,
    r.ForgotPasswordTokenExpirationTime,
    r.CreatedBy,
    r.ModifiedBy,
    r.CreatedOn,
    r.ModifiedOn
FROM nezovireg.reguser r
where r.Username != 'ratel' and r.Username != 'ratel2'
